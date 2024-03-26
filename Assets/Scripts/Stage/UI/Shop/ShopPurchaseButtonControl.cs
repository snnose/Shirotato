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
        // 구매한 아이템 칸이 잠겨있었다면 잠금 해제
        ItemManager.Instance.GetIsLockItemList()[currentNumber] = false;
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
            // LegendItem19 비활성화
            // 원래의 공격속도로 돌려놓는다
            WeaponManager.Instance.InActivateLegendItem19();
            // LegendItem28 비활성화
            // 원래의 공격속도로 돌려놓는다
            WeaponManager.Instance.InActivateLegendItem28();
            WeaponInfo weaponInfo = ItemManager.Instance.GetShopWeaponInfoList()[currentNumber];

            int weaponPrice = weaponInfo.price;
            // (원가 + 현재 라운드 + (원가 * 현재 라운드 / 10)) * 아이템 할인 배율
            int price = 
                Mathf.FloorToInt(weaponPrice + currentRound + 
                                (weaponPrice * currentRound / 10) *
                                (1f - 0.05f * ItemManager.Instance.GetOwnNormalItemList()[39]));

            // 보유 와플이 가격보다 많고 보유 무기 수가 6개 미만이라면
            if (currentWaffle >= price && WeaponManager.Instance.GetCurrentWeaponList().Count < 6)
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
            else if (currentWaffle >= price && WeaponManager.Instance.GetCurrentWeaponList().Count == 6)
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

            // LegendItem19 활성화
            // 각기 다른 무기가 있을 때마다 공격속도 -3%
            WeaponManager.Instance.ActivateLegendItem19();
            // LegendItem28 활성화
            // 각기 다른 무기가 있을 때마다 공격속도 +3%
            WeaponManager.Instance.ActivateLegendItem28();

            // 보유 무기 리스트 갱신
            ownWeaponListControl.renewOwnWeaponList = ownWeaponListControl.RenewOwnWeaponList();
        }
        // 아이템을 구매한 경우
        else
        {
            int itemPrice = item.GetComponent<ItemInfo>().price;
            // (원가 + 현재 라운드 + (원가 * 현재 라운드 / 10)) * 아이템 할인 배율
            int price = Mathf.FloorToInt(itemPrice + currentRound + 
                                        (itemPrice * currentRound / 10) *
                                        (1f - 0.05f * ItemManager.Instance.GetOwnNormalItemList()[39]));

            // 보유 와플이 아이템 가격보다 높을 때 구매
            if (currentWaffle >= price)
            {
                // 특정 스탯에 비례해서 스탯이 오르는 아이템들 처리
                // EpicItem29 비활성화
                PlayerInfo.Instance.InActivateEpicItem29();
                // LegendItem16 비활성화
                PlayerInfo.Instance.InActivateLegendItem16();
                // LegendItem17 비활성화
                PlayerInfo.Instance.InActivateLegendItem17();
                // LegendItem22 비활성화
                PlayerInfo.Instance.InActivateLegendItem22();
                // LegendItem23 비활성화
                PlayerInfo.Instance.InActivateLegendItem23();

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

                // 구매한 아이템을 기억한다.
                switch (item.GetComponent<ItemInfo>().rarity)
                {
                    case 0:
                        ItemManager.Instance.GetOwnNormalItemList()[item.GetComponent<ItemInfo>().itemNumber] += 1;
                        break;
                    case 1:
                        ItemManager.Instance.GetOwnRareItemList()[item.GetComponent<ItemInfo>().itemNumber] += 1;
                        break;
                    case 2:
                        // 특정 아이템 구매 시 작동
                        // EpicItem26 구매 시 구매 시점의 최대 체력 이상으로 체력이 상승하지 않음
                        if (item.GetComponent<ItemInfo>().itemNumber == 26)
                            PlayerInfo.Instance.ActivateEpicItem26(PlayerInfo.Instance.GetHP());
                        // EpicItem31 구매 시 구매 시점의 이동 속도% 이상으로 이동 속도%가 상승하지 않음
                        if (item.GetComponent<ItemInfo>().itemNumber == 31)
                            PlayerInfo.Instance.ActivateEpicItem31(PlayerInfo.Instance.GetMovementSpeedPercent());
                        ItemManager.Instance.GetOwnEpicItemList()[item.GetComponent<ItemInfo>().itemNumber] += 1;
                        break;
                    case 3:
                        // 특정 아이템 구매시 작동
                        // LegendItem19 구매 시 해당 아이템 활성화 (각기 다른 무기 보유할 때마다 공격속도 -3%)
                        if (item.GetComponent<ItemInfo>().itemNumber == 19)
                            WeaponManager.Instance.ActivateLegendItem19();
                        // LegendItem28 구매 시 해당 아이템 활성화 (각기 다른 무기 보유할 때마다 공격속도 +6%)
                        if (item.GetComponent<ItemInfo>().itemNumber == 28)
                            WeaponManager.Instance.ActivateLegendItem28();
                        ItemManager.Instance.GetOwnLegendItemList()[item.GetComponent<ItemInfo>().itemNumber] += 1;
                        break;
                    default:
                        break;
                }

                // 아이템 처리
                // EpicItem25 효과 발동
                PlayerInfo.Instance.ActivateEpicItem25();

                // 특정 스탯에 비례해서 스탯이 오르는 아이템들 처리
                // EpicItem29 활성화
                PlayerInfo.Instance.ActivateEpicItem29();
                // LegendItem16 활성화
                PlayerInfo.Instance.ActivateLegendItem16();
                // LegendItem17 활성화
                PlayerInfo.Instance.ActivateLegendItem17();
                // LegendItem22 활성화
                PlayerInfo.Instance.ActivateLegendItem22();
                // LegendItem23 활성화
                PlayerInfo.Instance.ActivateLegendItem23();

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
