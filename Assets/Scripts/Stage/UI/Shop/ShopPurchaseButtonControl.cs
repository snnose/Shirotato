using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class ShopPurchaseButtonControl : MonoBehaviour, IPointerEnterHandler
{
    private ShopOwnItemListControl ownItemListControl;
    private ShopOwnWeaponListControl ownWeaponListControl;
    private Button purchaseButton;

    // ���� ��ư�� ���� ������ ĭ ��ȣ
    private int currentNumber;
    private void Awake()
    {
        purchaseButton = this.gameObject.GetComponent<Button>();
        purchaseButton.onClick.AddListener(OnClickItemPurchaseButton);
        // Button -> item -> List -> ShopUI �� ���� �ö� �� OwnItemList�� ã��
        ownItemListControl = this.gameObject.transform.
            parent.parent.parent.GetChild(4).gameObject.GetComponent<ShopOwnItemListControl>();
        // Button -> item -> List -> ShopUI�� ���� �ö� �� OwnWeaponList ã��
        ownWeaponListControl = this.gameObject.transform.
            parent.parent.parent.GetChild(5).gameObject.GetComponent<ShopOwnWeaponListControl>();
    }

    public void OnClickItemPurchaseButton()
    {
        // ��ư Ŭ�� �� ���� ���
        ButtonSoundManager.Instance.PlayOnClickButtonSound1();

        string buttonName = this.transform.parent.name;

        GameObject item = FindItemNumber(buttonName);

        // ������ ������ ĭ�� ����־��ٸ� ��� ����
        ItemManager.Instance.GetIsLockItemList()[currentNumber] = false;
        // �ڹ��� �̹��� ��Ȱ��ȭ
        this.transform.parent.GetChild(6).gameObject.SetActive(false);

        // �������� �ɷ�ġ�� �÷��̾��� �ɷ�ġ�� ����
        PurchaseItem(item);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // ���콺 �����Ͱ� ������ ���� ���
        ButtonSoundManager.Instance.PlayOnPointerEnterSound1();
    }

    // ��ư�� Pos X ���� ���� ���° �������� �����ϴ� ������ ��ȯ�Ѵ�.
    private GameObject FindItemNumber(string buttonName)
    {
        currentNumber = -1;
        switch(buttonName)
        {
            // ù��° (0)
            case "ItemZero":
                currentNumber = 0;
                break;
            case "ItemOne":
                currentNumber = 1;
                break;
            case "ItemTwo":
                currentNumber = 2;
                break;
            case "ItemThree":
                currentNumber = 3;
                break;
            default:
                currentNumber = -1;
                break;
        }

        GameObject tmp = ItemManager.Instance.GetShopItemList()[currentNumber];
        return tmp;
    }

    // �������� �����ϰ� �ɷ�ġ�� �����Ų��
    private void PurchaseItem(GameObject item)
    {
        int currentRound = GameRoot.Instance.GetCurrentRound();
        int currentWaffle = PlayerInfo.Instance.GetCurrentWaffle();

        // �������� ������ ���
        if (item.TryGetComponent<ItemInfo>(out ItemInfo itemInfo))
        {
            IndividualityManager individualityManager = IndividualityManager.Instance;

            int itemPrice = item.GetComponent<ItemInfo>().price;
            // (���� + ���� ���� + (���� * ���� ���� / 10)) * ������ ���� ����
            int price = Mathf.FloorToInt(itemPrice + currentRound +
                                        (itemPrice * currentRound / 10) *
                                        (1f - 0.05f * ItemManager.Instance.GetOwnNormalItemList()[39]));

            // ���� ������ ������ ���ݺ��� ���� �� ����
            if (currentWaffle >= price)
            {
                // Ư�� ���ȿ� ����ؼ� ������ ������ �����۵� ó��
                // EpicItem25 ��Ȱ��ȭ
                PlayerInfo.Instance.InActivateEpicItem25();
                // EpicItem29 ��Ȱ��ȭ
                PlayerInfo.Instance.InActivateEpicItem29();
                // LegendItem16 ��Ȱ��ȭ
                PlayerInfo.Instance.InActivateLegendItem16();
                // LegendItem17 ��Ȱ��ȭ
                PlayerInfo.Instance.InActivateLegendItem17();
                // LegendItem22 ��Ȱ��ȭ
                PlayerInfo.Instance.InActivateLegendItem22();
                // LegendItem23 ��Ȱ��ȭ
                PlayerInfo.Instance.InActivateLegendItem23();

                PlayerInfo.Instance.SetCurrentWaffle(currentWaffle - price);

                // �����%
                PlayerInfo.Instance.SetDMGPercent(PlayerInfo.Instance.GetDMGPercent() +
                    item.GetComponent<ItemInfo>().DMGPercent * individualityManager.GetDMGPercentCoeff());
                // ���ݼӵ�
                PlayerInfo.Instance.SetATKSpeed(PlayerInfo.Instance.GetATKSpeed() +
                    item.GetComponent<ItemInfo>().ATKSpeed * individualityManager.GetATKSpeedCoeff());
                // ���� �����
                PlayerInfo.Instance.SetFixedDMG(PlayerInfo.Instance.GetFixedDMG() +
                    item.GetComponent<ItemInfo>().FixedDMG * individualityManager.GetFixedDMGCoeff());
                // ġ��Ÿ Ȯ��
                PlayerInfo.Instance.SetCritical(PlayerInfo.Instance.GetCritical() +
                    item.GetComponent<ItemInfo>().Critical * individualityManager.GetCriticalCoeff());
                // ����
                PlayerInfo.Instance.SetRange(PlayerInfo.Instance.GetRange() +
                    item.GetComponent<ItemInfo>().Range * individualityManager.GetRangeCoeff());

                // �ִ� ü��
                PlayerInfo.Instance.SetHP(PlayerInfo.Instance.GetHP() +
                    item.GetComponent<ItemInfo>().HP * individualityManager.GetHPCoeff());
                // ȸ����
                PlayerInfo.Instance.SetRecovery(PlayerInfo.Instance.GetRecovery() +
                    Mathf.FloorToInt(item.GetComponent<ItemInfo>().Recovery * individualityManager.GetRecoveryCoeff()));
                // ����� ���
                PlayerInfo.Instance.SetHPDrain(PlayerInfo.Instance.GetHPDrain() +
                    item.GetComponent<ItemInfo>().HPDrain * individualityManager.GetHPDrainCoeff());
                // ����
                PlayerInfo.Instance.SetArmor(PlayerInfo.Instance.GetArmor() +
                    Mathf.FloorToInt(item.GetComponent<ItemInfo>().Armor * individualityManager.GetArmorCoeff()));
                // ȸ��
                PlayerInfo.Instance.SetEvasion(PlayerInfo.Instance.GetEvasion() +
                    Mathf.FloorToInt(item.GetComponent<ItemInfo>().Evasion * individualityManager.GetEvasionCoeff()));

                // �̵��ӵ� %
                PlayerInfo.Instance.SetMovementSpeedPercent(PlayerInfo.Instance.GetMovementSpeedPercent() +
                    item.GetComponent<ItemInfo>().MovementSpeedPercent * individualityManager.GetMovementSpeedPercentCoeff());
                // ȹ�� ����
                PlayerInfo.Instance.SetRootingRange(PlayerInfo.Instance.GetRootingRange() + item.GetComponent<ItemInfo>().RootingRange);
                // ���
                PlayerInfo.Instance.SetLuck(PlayerInfo.Instance.GetLuck() +
                    item.GetComponent<ItemInfo>().Luck * individualityManager.GetLuckCoeff());
                // ��Ȯ
                PlayerInfo.Instance.SetHarvest(PlayerInfo.Instance.GetHarvest() +
                    item.GetComponent<ItemInfo>().Harvest * individualityManager.GetHarvestCoeff());
                // ����ġ ȹ�淮
                PlayerInfo.Instance.SetExpGain(PlayerInfo.Instance.GetExpGain() + item.GetComponent<ItemInfo>().ExpGain);

                // �ǽð� ���� ������ �ʱ�ȭ
                RealtimeInfoManager.Instance.SetAllStatus(PlayerInfo.Instance);

                // ������ �������� ����Ѵ�.
                switch (item.GetComponent<ItemInfo>().rarity)
                {
                    case 0:
                        ++ItemManager.Instance.GetOwnNormalItemList()[item.GetComponent<ItemInfo>().itemNumber];
                        Debug.Log("Normal " + item.GetComponent<ItemInfo>().itemNumber + "��, "
                                    + ItemManager.Instance.GetOwnNormalItemList()[item.GetComponent<ItemInfo>().itemNumber]);
                        break;
                    case 1:
                        ++ItemManager.Instance.GetOwnRareItemList()[item.GetComponent<ItemInfo>().itemNumber];
                        Debug.Log("Rare " + item.GetComponent<ItemInfo>().itemNumber + "��, "
                                    + ItemManager.Instance.GetOwnRareItemList()[item.GetComponent<ItemInfo>().itemNumber]);
                        // Ư�� ������ ���� �� �۵�
                        // RareItem28 ���� �� ���� ������ ���� �ʱ�ȭ ����� 0������ ����
                        if (item.GetComponent<ItemInfo>().itemNumber == 28)
                        {
                            ShopRerollButton shopRerollButton = ShopUIControl.Instance.GetShopRerollButton();
                            // ���� ���� Ƚ�� +1
                            shopRerollButton.SetFreeRerollCount(ItemManager.Instance.GetOwnRareItemList()[item.GetComponent<ItemInfo>().itemNumber]);
                            shopRerollButton.SetTProtext(0);
                        }
                        break;
                    case 2:
                        ++ItemManager.Instance.GetOwnEpicItemList()[item.GetComponent<ItemInfo>().itemNumber];
                        Debug.Log("Epic " + item.GetComponent<ItemInfo>().itemNumber + "��, "
                                    + ItemManager.Instance.GetOwnEpicItemList()[item.GetComponent<ItemInfo>().itemNumber]);

                        // Ư�� ������ ���� �� �۵�
                        // EpicItem26 ���� �� ���� ������ �ִ� ü�� �̻����� ü���� ������� ����
                        if (item.GetComponent<ItemInfo>().itemNumber == 26)
                            PlayerInfo.Instance.ActivateEpicItem26(PlayerInfo.Instance.GetHP());
                        // EpicItem31 ���� �� ���� ������ �̵� �ӵ�% �̻����� �̵� �ӵ�%�� ������� ����
                        if (item.GetComponent<ItemInfo>().itemNumber == 31)
                            PlayerInfo.Instance.ActivateEpicItem31(PlayerInfo.Instance.GetMovementSpeedPercent());
                        break;
                    case 3:
                        ++ItemManager.Instance.GetOwnLegendItemList()[item.GetComponent<ItemInfo>().itemNumber];
                        Debug.Log("Legend " + item.GetComponent<ItemInfo>().itemNumber + "��, "
                                    + ItemManager.Instance.GetOwnLegendItemList()[item.GetComponent<ItemInfo>().itemNumber]);

                        // Ư�� ������ ���Ž� �۵�
                        // LegendItem19 ���� �� �ش� ������ Ȱ��ȭ (���� �ٸ� ���� ������ ������ ���ݼӵ� -3%)
                        if (item.GetComponent<ItemInfo>().itemNumber == 19)
                            WeaponManager.Instance.ActivateLegendItem19();
                        // LegendItem28 ���� �� �ش� ������ Ȱ��ȭ (���� �ٸ� ���� ������ ������ ���ݼӵ� +6%)
                        if (item.GetComponent<ItemInfo>().itemNumber == 28)
                            WeaponManager.Instance.ActivateLegendItem28();
                        break;
                    default:
                        break;
                }

                // ������ ó��
                // EpicItem25 ȿ�� �ߵ�
                PlayerInfo.Instance.ActivateEpicItem25();

                // Ư�� ���ȿ� ����ؼ� ������ ������ �����۵� ó��
                // EpicItem29 Ȱ��ȭ
                PlayerInfo.Instance.ActivateEpicItem29();
                // LegendItem16 Ȱ��ȭ
                PlayerInfo.Instance.ActivateLegendItem16();
                // LegendItem17 Ȱ��ȭ
                PlayerInfo.Instance.ActivateLegendItem17();
                // LegendItem22 Ȱ��ȭ
                PlayerInfo.Instance.ActivateLegendItem22();
                // LegendItem23 Ȱ��ȭ
                PlayerInfo.Instance.ActivateLegendItem23();

                // ������ ������ ��Ȱ��ȭ �Ѵ�.
                this.transform.parent.gameObject.SetActive(false);
                // ���� ������ UI ���� (���� ���� ����)
                ShopUIControl.Instance.GetShopItemListControl().SetIsRenewInfo(false);
                // ���� ������ ����Ʈ ����
                ownItemListControl.renewOwnItemList = ownItemListControl.RenewOwnItemList(item);
                // ������ ���� Ƚ�� +1
                ItemManager.Instance.itemPurchaseCount += 1;
            }
        }
        // ���⸦ ������ ���
        else
        {
            // LegendItem19 ��Ȱ��ȭ
            // ������ ���ݼӵ��� �������´�
            WeaponManager.Instance.InActivateLegendItem19();
            // LegendItem28 ��Ȱ��ȭ
            // ������ ���ݼӵ��� �������´�
            WeaponManager.Instance.InActivateLegendItem28();
            WeaponInfo weaponInfo = ItemManager.Instance.GetShopWeaponInfoList()[currentNumber];

            int weaponPrice = weaponInfo.price;
            // (���� + ���� ���� + (���� * ���� ���� / 10)) * ������ ���� ����
            int price =
                Mathf.FloorToInt(weaponPrice + currentRound +
                                (weaponPrice * currentRound / 10) *
                                (1f - 0.05f * ItemManager.Instance.GetOwnNormalItemList()[39]));

            // ���� ������ ���ݺ��� ���� ���� ���� ���� 6�� �̸��̶��
            if (currentWaffle >= price && WeaponManager.Instance.GetCurrentWeaponList().Count < 6)
            {
                // ���� ��ȣ �ο�
                weaponInfo.SetWeaponNumber(WeaponManager.Instance.GetCurrentWeaponList().Count);

                // ���� ���� �� ����
                PlayerInfo.Instance.SetCurrentWaffle(currentWaffle - price);

                // ���� ��Ȱ��ȭ
                this.transform.parent.gameObject.SetActive(false);

                // ���� ���� ����Ʈ�� �߰�
                WeaponManager.Instance.GetCurrentWeaponList().Add(item);
                WeaponManager.Instance.GetCurrentWeaponInfoList().Add(weaponInfo);

                // ������ ���� Ƚ�� +1
                ItemManager.Instance.itemPurchaseCount += 1;
            }
            // ���� ������ ���ݺ��� ����, ���� ���� ���� 6�����
            else if (currentWaffle >= price && WeaponManager.Instance.GetCurrentWeaponList().Count == 6)
            {
                // ���� ���� ����� �����ߴٸ� ������ �ȵǹǷ� ��ȯ
                if (weaponInfo.GetWeaponGrade() >= 3)
                    return;
                WeaponInfo mainWeaponInfo = null;

                // ���� ���� �� ���� ����� ���� ���⸦ ã�´�
                for (int i = 0; i < WeaponManager.Instance.GetCurrentWeaponList().Count; i++)
                {
                    mainWeaponInfo = WeaponManager.Instance.GetCurrentWeaponInfoList()[i];
                    // ã�Ҵٸ� Ż��
                    if (mainWeaponInfo.weaponName == weaponInfo.weaponName
                        && mainWeaponInfo.GetWeaponGrade() == weaponInfo.GetWeaponGrade())
                        break;

                    // ��ã�Ҵٸ� null ó��
                    if (i == 5)
                        mainWeaponInfo = null;
                }

                // ã�� ���⿡ ���ս�Ų��
                if (mainWeaponInfo != null)
                {
                    mainWeaponInfo.SetWeaponStatus(mainWeaponInfo.weaponName, mainWeaponInfo.GetWeaponGrade() + 1);
                    PlayerInfo.Instance.SetCurrentWaffle(currentWaffle - price);

                    // ���� ��Ȱ��ȭ
                    this.transform.parent.gameObject.SetActive(false);

                    // ������ ���� Ƚ�� +1
                    ItemManager.Instance.itemPurchaseCount += 1;
                }
            }

            // LegendItem19 Ȱ��ȭ
            // ���� �ٸ� ���Ⱑ ���� ������ ���ݼӵ� -3%
            WeaponManager.Instance.ActivateLegendItem19();
            // LegendItem28 Ȱ��ȭ
            // ���� �ٸ� ���Ⱑ ���� ������ ���ݼӵ� +3%
            WeaponManager.Instance.ActivateLegendItem28();

            // ���� ���� ����Ʈ ����
            ownWeaponListControl.renewOwnWeaponList = ownWeaponListControl.RenewOwnWeaponList();
        }
    }
}
