using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class ShopPurchaseButtonControl : MonoBehaviour
{
    private ShopOwnItemListControl ownItemListControl;
    private ShopOwnWeaponListControl ownWeaponListControl;
    private GameObject currentClickButton;

    // ���� ��ư�� ���� ������ ĭ ��ȣ
    private int currentNumber;
    private void Awake()
    {
        // List -> ShopUI �� ���� �ö� �� OwnItemList�� ã��
        ownItemListControl = this.gameObject.transform.
            parent.GetChild(4).gameObject.GetComponent<ShopOwnItemListControl>();
        // List -> ShopUI�� ���� �ö� �� OwnWeaponList ã��
        ownWeaponListControl = this.gameObject.transform.
            parent.GetChild(5).gameObject.GetComponent<ShopOwnWeaponListControl>();
    }
    public void OnClickItemPurchaseButton()
    {
        currentClickButton = EventSystem.current.currentSelectedGameObject;
        
        float btnPosX = currentClickButton.transform.position.x - 0.5f;

        GameObject item = FindItemNumber(btnPosX);

        // �������� �ɷ�ġ�� �÷��̾��� �ɷ�ġ�� ����
        PurchaseItem(item);
    }

    // ��ư�� Pos X ���� ���� ���° �������� �����ϴ� ������ ��ȯ�Ѵ�.
    private GameObject FindItemNumber(float posX)
    {
        currentNumber = -1;
        switch(posX)
        {
            // ù��° (0)
            case 135:
                currentNumber = 0;
                break;
            case 380:
                currentNumber = 1;
                break;
            case 625:
                currentNumber = 2;
                break;
            case 870:
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

        // ���⸦ ������ ���
        if (ItemManager.Instance.GetShopWeaponInfoList()[currentNumber] != null)
        {
            WeaponInfo weaponInfo = ItemManager.Instance.GetShopWeaponInfoList()[currentNumber];

            int weaponPrice = weaponInfo.price;
            int price = weaponPrice + currentRound + Mathf.FloorToInt(weaponPrice * currentRound / 10);

            // ���� ������ ���ݺ��� ���� ���� ���� ���� 6�� �̸��̶��
            if (currentWaffle > price && WeaponManager.Instance.GetCurrentWeaponList().Count < 6)
            {
                // ���� ��ȣ �ο�
                weaponInfo.SetWeaponNumber(WeaponManager.Instance.GetCurrentWeaponList().Count);
             
                // ���� ���� �� ����
                PlayerInfo.Instance.SetCurrentWaffle(currentWaffle - price);

                // ���� ��Ȱ��ȭ
                currentClickButton.transform.parent.gameObject.SetActive(false);
                
                // ���� ���� ����Ʈ�� �߰�
                WeaponManager.Instance.GetCurrentWeaponList().Add(item);
                WeaponManager.Instance.GetCurrentWeaponInfoList().Add(weaponInfo);
            }
            // ���� ������ ���ݺ��� ����, ���� ���� ���� 6�����
            else if (currentWaffle > price && WeaponManager.Instance.GetCurrentWeaponList().Count == 6)
            {
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
                    currentClickButton.transform.parent.gameObject.SetActive(false);
                }
            }

            // ���� ���� ����Ʈ ����
            ownWeaponListControl.renewOwnWeaponList = ownWeaponListControl.RenewOwnWeaponList();
        }
        // �������� ������ ���
        else
        {
            int itemPrice = item.GetComponent<ItemInfo>().price;

            int price = itemPrice + currentRound + Mathf.FloorToInt(itemPrice * currentRound / 10);

            // ���� ������ ������ ���ݺ��� ���� �� ����
            if (currentWaffle > price)
            {
                PlayerInfo.Instance.SetCurrentWaffle(currentWaffle - price);

                // �����%
                PlayerInfo.Instance.SetDMGPercent(PlayerInfo.Instance.GetDMGPercent() + item.GetComponent<ItemInfo>().DMGPercent);
                // ���ݼӵ�
                PlayerInfo.Instance.SetATKSpeed(PlayerInfo.Instance.GetATKSpeed() + item.GetComponent<ItemInfo>().ATKSpeed);
                // ���� �����
                PlayerInfo.Instance.SetFixedDMG(PlayerInfo.Instance.GetFixedDMG() + item.GetComponent<ItemInfo>().FixedDMG);
                // ġ��Ÿ Ȯ��
                PlayerInfo.Instance.SetCritical(PlayerInfo.Instance.GetCritical() + item.GetComponent<ItemInfo>().Critical);
                // ����
                PlayerInfo.Instance.SetRange(PlayerInfo.Instance.GetRange() + item.GetComponent<ItemInfo>().Range);

                // �ִ� ü��
                PlayerInfo.Instance.SetHP(PlayerInfo.Instance.GetHP() + item.GetComponent<ItemInfo>().HP);
                // ȸ����
                PlayerInfo.Instance.SetRecovery(PlayerInfo.Instance.GetRecovery() + item.GetComponent<ItemInfo>().Recovery);
                // ����� ���
                PlayerInfo.Instance.SetHPDrain(PlayerInfo.Instance.GetHPDrain() + item.GetComponent<ItemInfo>().HPDrain);
                // ����
                PlayerInfo.Instance.SetArmor(PlayerInfo.Instance.GetArmor() + item.GetComponent<ItemInfo>().Armor);
                // ȸ��
                PlayerInfo.Instance.SetEvasion(PlayerInfo.Instance.GetEvasion() + item.GetComponent<ItemInfo>().Evasion);

                // �̵��ӵ� %
                PlayerInfo.Instance.SetMovementSpeedPercent(PlayerInfo.Instance.GetMovementSpeedPercent() + item.GetComponent<ItemInfo>().MovementSpeedPercent);
                // ȹ�� ����
                PlayerInfo.Instance.SetRootingRange(PlayerInfo.Instance.GetRootingRange() + item.GetComponent<ItemInfo>().RootingRange);
                // ���
                PlayerInfo.Instance.SetLuck(PlayerInfo.Instance.GetLuck() + item.GetComponent<ItemInfo>().Luck);
                // ��Ȯ
                PlayerInfo.Instance.SetHarvest(PlayerInfo.Instance.GetHarvest() + item.GetComponent<ItemInfo>().Harvest);
                // ����ġ ȹ�淮
                PlayerInfo.Instance.SetExpGain(PlayerInfo.Instance.GetExpGain() + item.GetComponent<ItemInfo>().ExpGain);

                // ���� ���� ������ ����Ʈ�� �߰��Ѵ�.
                ItemManager.Instance.GetCurrentItemList().Add(item);

                // ������ ������ ��Ȱ��ȭ �Ѵ�.
                currentClickButton.transform.parent.gameObject.SetActive(false);
                // ���� ������ UI ���� (���� ���� ����)
                ShopUIControl.Instance.GetShopItemListControl().SetIsRenewInfo(false);
                // ���� ������ ����Ʈ ����
                ownItemListControl.renewOwnItemList = ownItemListControl.RenewOwnItemList(item);
            }
        }
    }
}
