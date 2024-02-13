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

        Tuple<GameObject, int, bool> item = FindItemNumber(btnPosX);

        // �������� �ɷ�ġ�� �÷��̾��� �ɷ�ġ�� ����
        PurchaseItem(item);
    }

    // ��ư�� Pos X ���� ���� ���° �������� �����ϴ� ������ ��ȯ�Ѵ�.
    private Tuple<GameObject, int, bool> FindItemNumber(float posX)
    {
        int num = -1;
        switch(posX)
        {
            // ù��° (0)
            case 135:
                num = 0;
                break;
            case 380:
                num = 1;
                break;
            case 625:
                num = 2;
                break;
            case 870:
                num = 3;
                break;
            default:
                num = -1;
                break;
        }

        Tuple<GameObject, int, bool> tmp = new(ItemManager.Instance.GetShopItemList()[num].Item1,
                                               num,
                                               ItemManager.Instance.GetShopItemList()[num].Item3);
        return tmp;
    }

    // �������� �����ϰ� �ɷ�ġ�� �����Ų��
    private void PurchaseItem(Tuple<GameObject, int, bool> item)
    {
        int currentRound = GameRoot.Instance.GetCurrentRound();
        int currentWaffle = PlayerInfo.Instance.GetCurrentWaffle();

        // ���⸦ ������ ���
        if (item.Item3)
        {
            int weaponPrice = item.Item1.GetComponent<WeaponInfo>().price;
            int price = weaponPrice + currentRound + Mathf.FloorToInt(weaponPrice * currentRound / 10);

            if (currentWaffle > price && WeaponManager.Instance.GetCurrentWeaponList().Count < 6)
            {
                PlayerInfo.Instance.SetCurrentWaffle(currentWaffle - price);
                // ���� ������ �� �� ��쵵 �����ؾ���.

                // ���� ��Ȱ��ȭ
                currentClickButton.transform.parent.gameObject.SetActive(false);
                // ���� ���� ����Ʈ�� �߰�
                WeaponManager.Instance.GetCurrentWeaponList().Add(item.Item1);
                // ���� ���� ����Ʈ ����
                ownWeaponListControl.renewOwnWeaponList = ownWeaponListControl.RenewOwnWeaponList();
            }
        }
        // �������� ������ ���
        else
        {
            int itemPrice = item.Item1.GetComponent<ItemInfo>().price;

            int price = itemPrice + currentRound + Mathf.FloorToInt(itemPrice * currentRound / 10);

            // ���� ������ ������ ���ݺ��� ���� �� ����
            if (currentWaffle > price)
            {
                PlayerInfo.Instance.SetCurrentWaffle(currentWaffle - price);

                // �����%
                PlayerInfo.Instance.SetDMGPercent(PlayerInfo.Instance.GetDMGPercent() + item.Item1.GetComponent<ItemInfo>().DMGPercent);
                // ���ݼӵ�
                PlayerInfo.Instance.SetATKSpeed(PlayerInfo.Instance.GetATKSpeed() + item.Item1.GetComponent<ItemInfo>().ATKSpeed);
                // ���� �����
                PlayerInfo.Instance.SetFixedDMG(PlayerInfo.Instance.GetFixedDMG() + item.Item1.GetComponent<ItemInfo>().FixedDMG);
                // ġ��Ÿ Ȯ��
                PlayerInfo.Instance.SetCritical(PlayerInfo.Instance.GetCritical() + item.Item1.GetComponent<ItemInfo>().Critical);
                // ����
                PlayerInfo.Instance.SetRange(PlayerInfo.Instance.GetRange() + item.Item1.GetComponent<ItemInfo>().Range);

                // �ִ� ü��
                PlayerInfo.Instance.SetHP(PlayerInfo.Instance.GetHP() + item.Item1.GetComponent<ItemInfo>().HP);
                // ȸ����
                PlayerInfo.Instance.SetRecovery(PlayerInfo.Instance.GetRecovery() + item.Item1.GetComponent<ItemInfo>().Recovery);
                // ����
                PlayerInfo.Instance.SetArmor(PlayerInfo.Instance.GetArmor() + item.Item1.GetComponent<ItemInfo>().Armor);
                // ȸ��
                PlayerInfo.Instance.SetEvasion(PlayerInfo.Instance.GetEvasion() + item.Item1.GetComponent<ItemInfo>().Evasion);

                // �̵��ӵ�
                PlayerInfo.Instance.SetMovementSpeed(PlayerInfo.Instance.GetMovementSpeed() + item.Item1.GetComponent<ItemInfo>().MovementSpeed);
                // ȹ�� ����
                PlayerInfo.Instance.SetRootingRange(PlayerInfo.Instance.GetRootingRange() + item.Item1.GetComponent<ItemInfo>().RootingRange);
                // ���
                PlayerInfo.Instance.SetLuck(PlayerInfo.Instance.GetLuck() + item.Item1.GetComponent<ItemInfo>().Luck);

                // ���� ���� ������ ����Ʈ�� �߰��Ѵ�.
                ItemManager.Instance.GetCurrentItemList().Add(item.Item1);

                // ������ ������ ��Ȱ��ȭ �Ѵ�.
                currentClickButton.transform.parent.gameObject.SetActive(false);
                // ���� ������ ����Ʈ ����
                ownItemListControl.renewOwnItemList = ownItemListControl.RenewOwnItemList(item.Item1);
            }
        }
    }
}
