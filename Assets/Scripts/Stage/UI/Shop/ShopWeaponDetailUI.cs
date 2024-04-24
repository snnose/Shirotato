using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopWeaponDetailUI : MonoBehaviour
{
    private static ShopWeaponDetailUI instance;
    public static ShopWeaponDetailUI Instance
    {
        get
        {
            if (null == instance)
                return null;

            return instance;
        }
    }

    private GameObject weaponImage;
    private GameObject weaponName;
    private GameObject weaponStatus;
    private GameObject combinationButton;
    private GameObject sellButton;
    private GameObject closeButton;

    private Image image;
    private TextMeshProUGUI weaponNameText;
    private TextMeshProUGUI weaponStatusText;
    private TextMeshProUGUI weaponSellButtonText;

    // true일 때 취소 버튼을 누르기 전 까지 사라지지 않음.
    private bool isLockOn = false;
    private int currentWeaponRoomNumber; // 현재 주목된 무기의 무기 리스트 내의 순서
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        weaponImage = this.gameObject.transform.GetChild(1).gameObject;
        weaponName = this.gameObject.transform.GetChild(2).gameObject;
        weaponStatus = this.gameObject.transform.GetChild(3).gameObject;
        combinationButton = this.gameObject.transform.GetChild(4).gameObject;
        sellButton = this.gameObject.transform.GetChild(5).gameObject;
        closeButton = this.gameObject.transform.GetChild(6).gameObject;

        image = weaponImage.GetComponent<Image>();
        weaponNameText = weaponName.GetComponent<TextMeshProUGUI>();
        weaponStatusText = weaponStatus.GetComponent<TextMeshProUGUI>();
        weaponSellButtonText = sellButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        this.gameObject.SetActive(false);
    }
    
    // 무기를 결합해 상위 등급의 무기로 만드는 함수
    private void CombineWeapon()
    {
        // 현재 무기 리스트를 탐색해 같은 등급이면서 같은 무기인 것을 찾는다.
        List<GameObject> weaponList = WeaponManager.Instance.GetCurrentWeaponList();
        List<WeaponInfo> weaponInfoList = WeaponManager.Instance.GetCurrentWeaponInfoList();

        GameObject currWeapon = weaponList[currentWeaponRoomNumber];
        WeaponInfo currWeaponInfo = weaponInfoList[currentWeaponRoomNumber];

        //Debug.Log("결합 전 등급 : " + currWeaponInfo.GetWeaponGrade());

        // 무기 등급이 3이면 (전설이면) return
        if (currWeaponInfo.GetWeaponGrade() == 3)
            return;

        int count = weaponList.Count;

        for (int i = 0; i < count; i++)
        {
            // 탐색한 무기가 현재 주목된 무기와 같으면 패스
            if (i == currentWeaponRoomNumber)
                continue;

            GameObject materialWeapon = weaponList[i];
            WeaponInfo materialWeaponInfo = weaponInfoList[i];

            // 무기와 등급이 같다면 결합
            if (currWeaponInfo.weaponName == materialWeaponInfo.weaponName &&
                currWeaponInfo.GetWeaponGrade() == materialWeaponInfo.GetWeaponGrade())
            {
                // 현재 무기의 등급을 한 단계 상승시킨 능력치를 적용시킨다
                int currGrade = currWeaponInfo.GetWeaponGrade();
                currWeaponInfo.SetWeaponStatus(currWeaponInfo.weaponName, currGrade + 1);

                // 재료 무기에 해당하는 칸을 비운다
                weaponList.RemoveAt(i);
                weaponInfoList.RemoveAt(i);

                // 재료로 소모된 무기 이후의 무기들의 번호를 차감한다 (ex 2번째 무기 소모되면 3번째 무기 -> 2번째 무기로 변경)
                AdjustWeaponNum(i);
  
                // ShopOwnItemListControl의 코루틴 호출로 UI를 갱신한다
                ShopOwnWeaponListControl shopOwnWeaponListControl = ShopUIControl.Instance.GetShopOwnWeaponListControl();               
                shopOwnWeaponListControl.renewOwnWeaponList = shopOwnWeaponListControl.RenewOwnWeaponList();

                // WeaponDetailUI 비활성화
                this.gameObject.SetActive(false);
                isLockOn = false;
                return;
            }
        }
    }

    private void SellWeapon()
    {
        // LegendItem19, LegendItem28 비활성화
        // 원래의 공격속도로 돌려놓는다
        WeaponManager.Instance.InActivateLegendItem19();
        WeaponManager.Instance.InActivateLegendItem28();

        List<GameObject> weaponList = WeaponManager.Instance.GetCurrentWeaponList();
        List<WeaponInfo> weaponInfoList = WeaponManager.Instance.GetCurrentWeaponInfoList();

        int weaponPrice = weaponInfoList[currentWeaponRoomNumber].price;
        int sellPrice = ActivateRareItem32(Mathf.FloorToInt(weaponPrice + GameRoot.Instance.GetCurrentRound() +
                                                            (weaponPrice * GameRoot.Instance.GetCurrentRound() / 10)));
        PlayerInfo.Instance.SetCurrentWaffle(PlayerInfo.Instance.GetCurrentWaffle() + sellPrice);

        // 판매한 무기의 칸을 비운다
        weaponList.RemoveAt(currentWeaponRoomNumber);
        weaponInfoList.RemoveAt(currentWeaponRoomNumber);

        // 판매한 무기 이후의 무기들의 번호를 차감한다
        AdjustWeaponNum(currentWeaponRoomNumber);

        // ShopOwnItemListControl의 코루틴 호출로 UI를 갱신한다
        ShopOwnWeaponListControl shopOwnWeaponListControl = ShopUIControl.Instance.GetShopOwnWeaponListControl();
        shopOwnWeaponListControl.renewOwnWeaponList = shopOwnWeaponListControl.RenewOwnWeaponList();

        // WeaponDetailUI 비활성화
        this.gameObject.SetActive(false);
        isLockOn = false;

        // LegendItem19, LegendItem28 활성화
        // 각기 다른 무기가 있을 때마다 공격속도 -3%
        WeaponManager.Instance.ActivateLegendItem19();
        WeaponManager.Instance.ActivateLegendItem28();
    }

    // 무기 결합 버튼
    public void OnClickWeaponCombinationButton()
    {
        ButtonSoundManager.Instance.PlayOnClickButtonSound1();
        CombineWeapon();
    }

    // 판매 버튼
    public void OnClickSellButton()
    {
        // 나중에 파는 소리 따로 추가해야될듯
        ButtonSoundManager.Instance.PlayOnClickButtonSound1();
        SellWeapon();
    }

    // UI 닫기 버튼
    public void OnClickCloseButton()
    {
        ButtonSoundManager.Instance.PlayOnClickButtonSound1();
        // UI 고정 해제
        SetIsLockOn(false);
        // UI 비활성화
        this.gameObject.SetActive(false);
    }

    // i번째 무기 이후의 무기 번호를 조정하는 함수
    private void AdjustWeaponNum(int start)
    {
        int count = WeaponManager.Instance.GetCurrentWeaponList().Count;
        List<WeaponInfo> tmp = WeaponManager.Instance.GetCurrentWeaponInfoList();
        if (start < count)
        {
            for (int i = start; i < count; i++)
            {
                tmp[i].SetWeaponNumber(tmp[i].GetWeaponNumber() - 1);
            }
        }
    }

    int ActivateRareItem32(int itemPrice)
    {
        if (ItemManager.Instance.GetOwnRareItemList()[32] > 0)
        {
            return Mathf.FloorToInt(itemPrice * 0.6f);
        }

        return Mathf.FloorToInt(itemPrice * 0.25f);
    }

    // UI 위치 설정
    public void SetUIPosition(Vector2 pos)
    {
        this.gameObject.transform.position = pos;
    }

    // 무기 이미지 설정
    public void SetWeaponImage(Image weaponImage)
    {
        this.image.sprite = weaponImage.sprite;
    }

    // 무기 이름 텍스트 설정
    public void SetWeaponNameText(WeaponInfo weaponInfo)
    {
        this.weaponNameText.text = weaponInfo.weaponName;
    }

    // 무기 스탯 설정
    public void SetWeaponStatusText(WeaponInfo weaponInfo)
    {
        int damage = Mathf.FloorToInt(
            (weaponInfo.damage + weaponInfo.damageCoeff * PlayerInfo.Instance.GetFixedDMG()) 
                                                * ((PlayerInfo.Instance.GetDMGPercent() + 100) / 100));
        // 대미지는 최소 1
        if (damage <= 0)
            damage = 1;

        int damageCoeff = Mathf.FloorToInt(weaponInfo.damageCoeff * 100);

        float coolDown = weaponInfo.coolDown - weaponInfo.coolDown * PlayerInfo.Instance.GetATKSpeed() / (100 + PlayerInfo.Instance.GetATKSpeed());
        float atkSpeed = Mathf.Round(1 / coolDown * 100) / 100;
        float range = Mathf.Floor(weaponInfo.range * ((PlayerInfo.Instance.GetRange() + 100) / 100) * 100) / 100;
        string specialNote = weaponInfo.GetSpecialNote();

        // 무기 특이사항 수정
        if (weaponInfo.weaponName == "Revolver")
        {
            specialNote = "6발 사격 후 " + Mathf.FloorToInt(coolDown * 5f * 100) / 100 + "초 간 재장전";
        }

        weaponStatusText.text = "대미지 : " + damage + " (+" + damageCoeff + "%)\n" +
                              "공격속도 : " + atkSpeed + "/s \n" +
                              "넉백 : " + weaponInfo.knockback + '\n' +
                              "범위 : " + range + '\n' +
                              specialNote;
    }

    public void SetWeaponSellButtonText(WeaponInfo weaponInfo)
    {
        // RareItem32를 보유하면 무기 가격의 60%, 아니라면 25%
        int weaponPrice = weaponInfo.price;
        int sellPrice = ActivateRareItem32(Mathf.FloorToInt(weaponPrice + GameRoot.Instance.GetCurrentRound() +
                                                            (weaponPrice * GameRoot.Instance.GetCurrentRound() / 10)));

        this.weaponSellButtonText.text = "판매 (+" + sellPrice + ")";
    }

    // 보유 무기 칸을 눌렀다면 UI가 고정되도록 함
    public void SetIsLockOn(bool ret)
    {
        this.isLockOn = ret;
    }

    // 현재 주목된 무기 칸의 번호를 설정한다.
    public void SetWeaponRoomNumber(int num)
    {
        this.currentWeaponRoomNumber = num;
    }

    // WeaponDetailUI 참조
    public GameObject GetWeaponDetailUI()
    {
        return this.gameObject;
    }

    public bool GetIsLockOn()
    {
        return this.isLockOn;
    }
}
