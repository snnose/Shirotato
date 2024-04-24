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

    // true�� �� ��� ��ư�� ������ �� ���� ������� ����.
    private bool isLockOn = false;
    private int currentWeaponRoomNumber; // ���� �ָ�� ������ ���� ����Ʈ ���� ����
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
    
    // ���⸦ ������ ���� ����� ����� ����� �Լ�
    private void CombineWeapon()
    {
        // ���� ���� ����Ʈ�� Ž���� ���� ����̸鼭 ���� ������ ���� ã�´�.
        List<GameObject> weaponList = WeaponManager.Instance.GetCurrentWeaponList();
        List<WeaponInfo> weaponInfoList = WeaponManager.Instance.GetCurrentWeaponInfoList();

        GameObject currWeapon = weaponList[currentWeaponRoomNumber];
        WeaponInfo currWeaponInfo = weaponInfoList[currentWeaponRoomNumber];

        //Debug.Log("���� �� ��� : " + currWeaponInfo.GetWeaponGrade());

        // ���� ����� 3�̸� (�����̸�) return
        if (currWeaponInfo.GetWeaponGrade() == 3)
            return;

        int count = weaponList.Count;

        for (int i = 0; i < count; i++)
        {
            // Ž���� ���Ⱑ ���� �ָ�� ����� ������ �н�
            if (i == currentWeaponRoomNumber)
                continue;

            GameObject materialWeapon = weaponList[i];
            WeaponInfo materialWeaponInfo = weaponInfoList[i];

            // ����� ����� ���ٸ� ����
            if (currWeaponInfo.weaponName == materialWeaponInfo.weaponName &&
                currWeaponInfo.GetWeaponGrade() == materialWeaponInfo.GetWeaponGrade())
            {
                // ���� ������ ����� �� �ܰ� ��½�Ų �ɷ�ġ�� �����Ų��
                int currGrade = currWeaponInfo.GetWeaponGrade();
                currWeaponInfo.SetWeaponStatus(currWeaponInfo.weaponName, currGrade + 1);

                // ��� ���⿡ �ش��ϴ� ĭ�� ����
                weaponList.RemoveAt(i);
                weaponInfoList.RemoveAt(i);

                // ���� �Ҹ�� ���� ������ ������� ��ȣ�� �����Ѵ� (ex 2��° ���� �Ҹ�Ǹ� 3��° ���� -> 2��° ����� ����)
                AdjustWeaponNum(i);
  
                // ShopOwnItemListControl�� �ڷ�ƾ ȣ��� UI�� �����Ѵ�
                ShopOwnWeaponListControl shopOwnWeaponListControl = ShopUIControl.Instance.GetShopOwnWeaponListControl();               
                shopOwnWeaponListControl.renewOwnWeaponList = shopOwnWeaponListControl.RenewOwnWeaponList();

                // WeaponDetailUI ��Ȱ��ȭ
                this.gameObject.SetActive(false);
                isLockOn = false;
                return;
            }
        }
    }

    private void SellWeapon()
    {
        // LegendItem19, LegendItem28 ��Ȱ��ȭ
        // ������ ���ݼӵ��� �������´�
        WeaponManager.Instance.InActivateLegendItem19();
        WeaponManager.Instance.InActivateLegendItem28();

        List<GameObject> weaponList = WeaponManager.Instance.GetCurrentWeaponList();
        List<WeaponInfo> weaponInfoList = WeaponManager.Instance.GetCurrentWeaponInfoList();

        int weaponPrice = weaponInfoList[currentWeaponRoomNumber].price;
        int sellPrice = ActivateRareItem32(Mathf.FloorToInt(weaponPrice + GameRoot.Instance.GetCurrentRound() +
                                                            (weaponPrice * GameRoot.Instance.GetCurrentRound() / 10)));
        PlayerInfo.Instance.SetCurrentWaffle(PlayerInfo.Instance.GetCurrentWaffle() + sellPrice);

        // �Ǹ��� ������ ĭ�� ����
        weaponList.RemoveAt(currentWeaponRoomNumber);
        weaponInfoList.RemoveAt(currentWeaponRoomNumber);

        // �Ǹ��� ���� ������ ������� ��ȣ�� �����Ѵ�
        AdjustWeaponNum(currentWeaponRoomNumber);

        // ShopOwnItemListControl�� �ڷ�ƾ ȣ��� UI�� �����Ѵ�
        ShopOwnWeaponListControl shopOwnWeaponListControl = ShopUIControl.Instance.GetShopOwnWeaponListControl();
        shopOwnWeaponListControl.renewOwnWeaponList = shopOwnWeaponListControl.RenewOwnWeaponList();

        // WeaponDetailUI ��Ȱ��ȭ
        this.gameObject.SetActive(false);
        isLockOn = false;

        // LegendItem19, LegendItem28 Ȱ��ȭ
        // ���� �ٸ� ���Ⱑ ���� ������ ���ݼӵ� -3%
        WeaponManager.Instance.ActivateLegendItem19();
        WeaponManager.Instance.ActivateLegendItem28();
    }

    // ���� ���� ��ư
    public void OnClickWeaponCombinationButton()
    {
        ButtonSoundManager.Instance.PlayOnClickButtonSound1();
        CombineWeapon();
    }

    // �Ǹ� ��ư
    public void OnClickSellButton()
    {
        // ���߿� �Ĵ� �Ҹ� ���� �߰��ؾߵɵ�
        ButtonSoundManager.Instance.PlayOnClickButtonSound1();
        SellWeapon();
    }

    // UI �ݱ� ��ư
    public void OnClickCloseButton()
    {
        ButtonSoundManager.Instance.PlayOnClickButtonSound1();
        // UI ���� ����
        SetIsLockOn(false);
        // UI ��Ȱ��ȭ
        this.gameObject.SetActive(false);
    }

    // i��° ���� ������ ���� ��ȣ�� �����ϴ� �Լ�
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

    // UI ��ġ ����
    public void SetUIPosition(Vector2 pos)
    {
        this.gameObject.transform.position = pos;
    }

    // ���� �̹��� ����
    public void SetWeaponImage(Image weaponImage)
    {
        this.image.sprite = weaponImage.sprite;
    }

    // ���� �̸� �ؽ�Ʈ ����
    public void SetWeaponNameText(WeaponInfo weaponInfo)
    {
        this.weaponNameText.text = weaponInfo.weaponName;
    }

    // ���� ���� ����
    public void SetWeaponStatusText(WeaponInfo weaponInfo)
    {
        int damage = Mathf.FloorToInt(
            (weaponInfo.damage + weaponInfo.damageCoeff * PlayerInfo.Instance.GetFixedDMG()) 
                                                * ((PlayerInfo.Instance.GetDMGPercent() + 100) / 100));
        // ������� �ּ� 1
        if (damage <= 0)
            damage = 1;

        int damageCoeff = Mathf.FloorToInt(weaponInfo.damageCoeff * 100);

        float coolDown = weaponInfo.coolDown - weaponInfo.coolDown * PlayerInfo.Instance.GetATKSpeed() / (100 + PlayerInfo.Instance.GetATKSpeed());
        float atkSpeed = Mathf.Round(1 / coolDown * 100) / 100;
        float range = Mathf.Floor(weaponInfo.range * ((PlayerInfo.Instance.GetRange() + 100) / 100) * 100) / 100;
        string specialNote = weaponInfo.GetSpecialNote();

        // ���� Ư�̻��� ����
        if (weaponInfo.weaponName == "Revolver")
        {
            specialNote = "6�� ��� �� " + Mathf.FloorToInt(coolDown * 5f * 100) / 100 + "�� �� ������";
        }

        weaponStatusText.text = "����� : " + damage + " (+" + damageCoeff + "%)\n" +
                              "���ݼӵ� : " + atkSpeed + "/s \n" +
                              "�˹� : " + weaponInfo.knockback + '\n' +
                              "���� : " + range + '\n' +
                              specialNote;
    }

    public void SetWeaponSellButtonText(WeaponInfo weaponInfo)
    {
        // RareItem32�� �����ϸ� ���� ������ 60%, �ƴ϶�� 25%
        int weaponPrice = weaponInfo.price;
        int sellPrice = ActivateRareItem32(Mathf.FloorToInt(weaponPrice + GameRoot.Instance.GetCurrentRound() +
                                                            (weaponPrice * GameRoot.Instance.GetCurrentRound() / 10)));

        this.weaponSellButtonText.text = "�Ǹ� (+" + sellPrice + ")";
    }

    // ���� ���� ĭ�� �����ٸ� UI�� �����ǵ��� ��
    public void SetIsLockOn(bool ret)
    {
        this.isLockOn = ret;
    }

    // ���� �ָ�� ���� ĭ�� ��ȣ�� �����Ѵ�.
    public void SetWeaponRoomNumber(int num)
    {
        this.currentWeaponRoomNumber = num;
    }

    // WeaponDetailUI ����
    public GameObject GetWeaponDetailUI()
    {
        return this.gameObject;
    }

    public bool GetIsLockOn()
    {
        return this.isLockOn;
    }
}
