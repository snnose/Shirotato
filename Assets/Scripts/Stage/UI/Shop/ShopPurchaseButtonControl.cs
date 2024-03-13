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

    // 구매 버튼을 누른 아이템 칸 번호
    private int currentNumber;
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

        GameObject item = FindItemNumber(btnPosX);

        // 아이템의 능력치를 플레이어의 능력치에 적용
        PurchaseItem(item);
    }

    // 버튼의 Pos X 값에 따라 몇번째 아이템을 구매하는 것인지 반환한다.
    private GameObject FindItemNumber(float posX)
    {
        currentNumber = -1;
        switch(posX)
        {
            // 첫번째 (0)
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

    // 아이템을 구매하고 능력치를 적용시킨다
    private void PurchaseItem(GameObject item)
    {
        int currentRound = GameRoot.Instance.GetCurrentRound();
        int currentWaffle = PlayerInfo.Instance.GetCurrentWaffle();

        // 무기를 구매한 경우
        if (ItemManager.Instance.GetShopWeaponInfoList()[currentNumber] != null)
        {
            WeaponInfo weaponInfo = ItemManager.Instance.GetShopWeaponInfoList()[currentNumber];

            int weaponPrice = weaponInfo.price;
            int price = weaponPrice + currentRound + Mathf.FloorToInt(weaponPrice * currentRound / 10);

            // 보유 와플이 가격보다 많고 보유 무기 수가 6개 미만이라면
            if (currentWaffle > price && WeaponManager.Instance.GetCurrentWeaponList().Count < 6)
            {
                // 무기 번호 부여
                weaponInfo.SetWeaponNumber(WeaponManager.Instance.GetCurrentWeaponList().Count);
             
                // 보유 와플 수 차감
                PlayerInfo.Instance.SetCurrentWaffle(currentWaffle - price);

                // 슬롯 비활성화
                currentClickButton.transform.parent.gameObject.SetActive(false);
                
                // 보유 무기 리스트에 추가
                WeaponManager.Instance.GetCurrentWeaponList().Add(item);
                WeaponManager.Instance.GetCurrentWeaponInfoList().Add(weaponInfo);
            }
            // 보유 와플이 가격보다 많고, 보유 무기 수가 6개라면
            else if (currentWaffle > price && WeaponManager.Instance.GetCurrentWeaponList().Count == 6)
            {
                WeaponInfo mainWeaponInfo = null;
                // 보유 무기 중 같은 등급의 같은 무기를 찾는다
                for (int i = 0; i < WeaponManager.Instance.GetCurrentWeaponList().Count; i++)
                {
                    mainWeaponInfo = WeaponManager.Instance.GetCurrentWeaponInfoList()[i];
                    // 찾았다면 탈출
                    if (mainWeaponInfo.weaponName == weaponInfo.weaponName
                        && mainWeaponInfo.GetWeaponGrade() == weaponInfo.GetWeaponGrade())
                        break;

                    // 못찾았다면 null 처리
                    if (i == 5)
                        mainWeaponInfo = null;
                }

                // 찾은 무기에 결합시킨다
                if (mainWeaponInfo != null)
                {
                    mainWeaponInfo.SetWeaponStatus(mainWeaponInfo.weaponName, mainWeaponInfo.GetWeaponGrade() + 1);
                    PlayerInfo.Instance.SetCurrentWaffle(currentWaffle - price);

                    // 슬롯 비활성화
                    currentClickButton.transform.parent.gameObject.SetActive(false);
                }
            }

            // 보유 무기 리스트 갱신
            ownWeaponListControl.renewOwnWeaponList = ownWeaponListControl.RenewOwnWeaponList();
        }
        // 아이템을 구매한 경우
        else
        {
            int itemPrice = item.GetComponent<ItemInfo>().price;

            int price = itemPrice + currentRound + Mathf.FloorToInt(itemPrice * currentRound / 10);

            // 보유 와플이 아이템 가격보다 높을 때 구매
            if (currentWaffle > price)
            {
                PlayerInfo.Instance.SetCurrentWaffle(currentWaffle - price);

                // 대미지%
                PlayerInfo.Instance.SetDMGPercent(PlayerInfo.Instance.GetDMGPercent() + item.GetComponent<ItemInfo>().DMGPercent);
                // 공격속도
                PlayerInfo.Instance.SetATKSpeed(PlayerInfo.Instance.GetATKSpeed() + item.GetComponent<ItemInfo>().ATKSpeed);
                // 고정 대미지
                PlayerInfo.Instance.SetFixedDMG(PlayerInfo.Instance.GetFixedDMG() + item.GetComponent<ItemInfo>().FixedDMG);
                // 치명타 확률
                PlayerInfo.Instance.SetCritical(PlayerInfo.Instance.GetCritical() + item.GetComponent<ItemInfo>().Critical);
                // 범위
                PlayerInfo.Instance.SetRange(PlayerInfo.Instance.GetRange() + item.GetComponent<ItemInfo>().Range);

                // 최대 체력
                PlayerInfo.Instance.SetHP(PlayerInfo.Instance.GetHP() + item.GetComponent<ItemInfo>().HP);
                // 회복력
                PlayerInfo.Instance.SetRecovery(PlayerInfo.Instance.GetRecovery() + item.GetComponent<ItemInfo>().Recovery);
                // 생명력 흡수
                PlayerInfo.Instance.SetHPDrain(PlayerInfo.Instance.GetHPDrain() + item.GetComponent<ItemInfo>().HPDrain);
                // 방어력
                PlayerInfo.Instance.SetArmor(PlayerInfo.Instance.GetArmor() + item.GetComponent<ItemInfo>().Armor);
                // 회피
                PlayerInfo.Instance.SetEvasion(PlayerInfo.Instance.GetEvasion() + item.GetComponent<ItemInfo>().Evasion);

                // 이동속도 %
                PlayerInfo.Instance.SetMovementSpeedPercent(PlayerInfo.Instance.GetMovementSpeedPercent() + item.GetComponent<ItemInfo>().MovementSpeedPercent);
                // 획득 범위
                PlayerInfo.Instance.SetRootingRange(PlayerInfo.Instance.GetRootingRange() + item.GetComponent<ItemInfo>().RootingRange);
                // 행운
                PlayerInfo.Instance.SetLuck(PlayerInfo.Instance.GetLuck() + item.GetComponent<ItemInfo>().Luck);
                // 수확
                PlayerInfo.Instance.SetHarvest(PlayerInfo.Instance.GetHarvest() + item.GetComponent<ItemInfo>().Harvest);
                // 경험치 획득량
                PlayerInfo.Instance.SetExpGain(PlayerInfo.Instance.GetExpGain() + item.GetComponent<ItemInfo>().ExpGain);

                // 현재 보유 아이템 리스트에 추가한다.
                ItemManager.Instance.GetCurrentItemList().Add(item);

                // 아이템 슬롯을 비활성화 한다.
                currentClickButton.transform.parent.gameObject.SetActive(false);
                // 상점 아이템 UI 갱신 (무기 정보 갱신)
                ShopUIControl.Instance.GetShopItemListControl().SetIsRenewInfo(false);
                // 보유 아이템 리스트 갱신
                ownItemListControl.renewOwnItemList = ownItemListControl.RenewOwnItemList(item);
            }
        }
    }
}
