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
        // List -> ShopUI 로 거쳐 올라간 후 OwnItemList를 찾음
        ownItemListControl = this.gameObject.transform.
            parent.GetChild(4).gameObject.GetComponent<ShopOwnItemListControl>();
        // List -> ShopUI로 거쳐 올라간 후 OwnWeaponList 찾음
        ownWeaponListControl = this.gameObject.transform.
            parent.GetChild(5).gameObject.GetComponent<ShopOwnWeaponListControl>();
    }
    public void OnClickItemPurchaseButton()
    {
        currentClickButton = EventSystem.current.currentSelectedGameObject;
        
        float btnPosX = currentClickButton.transform.position.x - 0.5f;

        Tuple<GameObject, int, bool> item = FindItemNumber(btnPosX);

        // 아이템의 능력치를 플레이어의 능력치에 적용
        PurchaseItem(item);
    }

    // 버튼의 Pos X 값에 따라 몇번째 아이템을 구매하는 것인지 반환한다.
    private Tuple<GameObject, int, bool> FindItemNumber(float posX)
    {
        int num = -1;
        switch(posX)
        {
            // 첫번째 (0)
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

    // 아이템을 구매하고 능력치를 적용시킨다
    private void PurchaseItem(Tuple<GameObject, int, bool> item)
    {
        int currentRound = GameRoot.Instance.GetCurrentRound();
        int currentWaffle = PlayerInfo.Instance.GetCurrentWaffle();

        // 무기를 구매한 경우
        if (item.Item3)
        {
            int weaponPrice = item.Item1.GetComponent<WeaponInfo>().price;
            int price = weaponPrice + currentRound + Mathf.FloorToInt(weaponPrice * currentRound / 10);

            if (currentWaffle > price && WeaponManager.Instance.GetCurrentWeaponList().Count < 6)
            {
                PlayerInfo.Instance.SetCurrentWaffle(currentWaffle - price);
                // 무기 슬롯이 꽉 찬 경우도 구현해야함.

                // 슬롯 비활성화
                currentClickButton.transform.parent.gameObject.SetActive(false);
                // 보유 무기 리스트에 추가
                WeaponManager.Instance.GetCurrentWeaponList().Add(item.Item1);
                // 보유 무기 리스트 갱신
                ownWeaponListControl.renewOwnWeaponList = ownWeaponListControl.RenewOwnWeaponList();
            }
        }
        // 아이템을 구매한 경우
        else
        {
            int itemPrice = item.Item1.GetComponent<ItemInfo>().price;

            int price = itemPrice + currentRound + Mathf.FloorToInt(itemPrice * currentRound / 10);

            // 보유 와플이 아이템 가격보다 높을 때 구매
            if (currentWaffle > price)
            {
                PlayerInfo.Instance.SetCurrentWaffle(currentWaffle - price);

                // 대미지%
                PlayerInfo.Instance.SetDMGPercent(PlayerInfo.Instance.GetDMGPercent() + item.Item1.GetComponent<ItemInfo>().DMGPercent);
                // 공격속도
                PlayerInfo.Instance.SetATKSpeed(PlayerInfo.Instance.GetATKSpeed() + item.Item1.GetComponent<ItemInfo>().ATKSpeed);
                // 고정 대미지
                PlayerInfo.Instance.SetFixedDMG(PlayerInfo.Instance.GetFixedDMG() + item.Item1.GetComponent<ItemInfo>().FixedDMG);
                // 치명타 확률
                PlayerInfo.Instance.SetCritical(PlayerInfo.Instance.GetCritical() + item.Item1.GetComponent<ItemInfo>().Critical);
                // 범위
                PlayerInfo.Instance.SetRange(PlayerInfo.Instance.GetRange() + item.Item1.GetComponent<ItemInfo>().Range);

                // 최대 체력
                PlayerInfo.Instance.SetHP(PlayerInfo.Instance.GetHP() + item.Item1.GetComponent<ItemInfo>().HP);
                // 회복력
                PlayerInfo.Instance.SetRecovery(PlayerInfo.Instance.GetRecovery() + item.Item1.GetComponent<ItemInfo>().Recovery);
                // 방어력
                PlayerInfo.Instance.SetArmor(PlayerInfo.Instance.GetArmor() + item.Item1.GetComponent<ItemInfo>().Armor);
                // 회피
                PlayerInfo.Instance.SetEvasion(PlayerInfo.Instance.GetEvasion() + item.Item1.GetComponent<ItemInfo>().Evasion);

                // 이동속도
                PlayerInfo.Instance.SetMovementSpeed(PlayerInfo.Instance.GetMovementSpeed() + item.Item1.GetComponent<ItemInfo>().MovementSpeed);
                // 획득 범위
                PlayerInfo.Instance.SetRootingRange(PlayerInfo.Instance.GetRootingRange() + item.Item1.GetComponent<ItemInfo>().RootingRange);
                // 행운
                PlayerInfo.Instance.SetLuck(PlayerInfo.Instance.GetLuck() + item.Item1.GetComponent<ItemInfo>().Luck);

                // 현재 보유 아이템 리스트에 추가한다.
                ItemManager.Instance.GetCurrentItemList().Add(item.Item1);

                // 아이템 슬롯을 비활성화 한다.
                currentClickButton.transform.parent.gameObject.SetActive(false);
                // 보유 아이템 리스트 갱신
                ownItemListControl.renewOwnItemList = ownItemListControl.RenewOwnItemList(item.Item1);
            }
        }
    }
}
