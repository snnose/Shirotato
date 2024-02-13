using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemListControl : MonoBehaviour
{
    public List<GameObject> itemList;

    public bool isRenewItemInfo = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        RenewItemInfo();
    }

    public void RenewItemInfo()
    {
        // 상점 아이템 리스트가 갱신 됐다면
        if (ItemManager.Instance.GetIsRenewItem() && !isRenewItemInfo)
        {
            // 각 슬롯에 아이템 정보를 기록한다.
            for (int i = 0; i < 4; i++)
            {
                // 아이템 이미지 변경
                itemList[i].transform.GetChild(1).GetComponent<Image>().sprite =
                    ItemManager.Instance.GetShopItemList()[i].Item1.GetComponent<SpriteRenderer>().sprite;
                // 아이템 이름 변경
                itemList[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text =
                    ItemManager.Instance.GetShopItemList()[i].Item1.name;

                // 무기, 아이템에 따라 스텟 설명이 다르다.
                // 무기일 경우
                if (ItemManager.Instance.GetShopItemList()[i].Item3)
                {
                    SetWeaponInfoText(i);
                }
                // 아이템일 경우
                else
                {
                    SetItemInfoText(i);
                }
            }

            isRenewItemInfo = true;
        }
    }

    void SetWeaponInfoText(int i)
    {
        TextMeshProUGUI weaponInfoText = itemList[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        WeaponInfo weaponInfo = ItemManager.Instance.GetShopItemList()[i].Item1.GetComponent<WeaponInfo>();
        weaponInfoText.text = "대미지 : " + weaponInfo.damage + '\n' +
                              "공격속도 : " + Mathf.Round(1 / weaponInfo.coolDown * 100) / 100 + "/s \n" +
                              "범위 : " + weaponInfo.range;

        TextMeshProUGUI weaponPrice = itemList[i].transform.GetChild(4).GetChild(2).GetComponent<TextMeshProUGUI>();
        weaponPrice.text = (weaponInfo.price + GameRoot.Instance.GetCurrentRound() +
                            Mathf.FloorToInt(weaponInfo.price * GameRoot.Instance.GetCurrentRound() / 10)).ToString();
    }

    void SetItemInfoText(int i)
    {
        TextMeshProUGUI itemInfoText = itemList[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        ItemInfo itemInfo = ItemManager.Instance.GetShopItemList()[i].Item1.GetComponent<ItemInfo>();
        itemInfoText.text = "";

        TextMeshProUGUI itemPrice = itemList[i].transform.GetChild(4).GetChild(2).GetComponent<TextMeshProUGUI>();
        itemPrice.text = (itemInfo.price + GameRoot.Instance.GetCurrentRound() +
                            Mathf.FloorToInt(itemInfo.price * GameRoot.Instance.GetCurrentRound() / 10)).ToString();

        // 공격 관련
        if (itemInfo.DMGPercent != 0)
            itemInfoText.text += "대미지 " + itemInfo.DMGPercent + "%\n";
        if (itemInfo.ATKSpeed != 0)
            itemInfoText.text += "공격속도 " + itemInfo.ATKSpeed + "%\n";
        if (itemInfo.FixedDMG != 0)
            itemInfoText.text += "고정 대미지 " + itemInfo.FixedDMG + '\n';
        if (itemInfo.Critical != 0)
            itemInfoText.text += "치명타 " + itemInfo.Critical + "%\n";
        if (itemInfo.Range != 0)
            itemInfoText.text += "범위 " + itemInfo.Range + "%\n";

        // 방어 관련
        if (itemInfo.HP != 0)
            itemInfoText.text += "최대 체력 " + itemInfo.HP + '\n';
        if (itemInfo.Recovery != 0)
            itemInfoText.text += "체력 회복 " + itemInfo.Recovery + '\n';
        if (itemInfo.Armor != 0)
            itemInfoText.text += "방어력 " + itemInfo.Armor + '\n';
        if (itemInfo.Evasion != 0)
            itemInfoText.text += "회피 " + itemInfo.Evasion + "%\n";
        // 유틸 관련
        if (itemInfo.MovementSpeed != 0)
            itemInfoText.text += "이동속도 " + itemInfo.MovementSpeed + "%\n";
        if (itemInfo.RootingRange != 0)
            itemInfoText.text += "획득 범위 " + itemInfo.RootingRange + "%\n";
        if (itemInfo.Luck != 0)
            itemInfoText.text += "행운 " + itemInfo.Luck + '\n';
    }

    public void SetItemListActive()
    {
        for(int i = 0; i < 4; i++)
        {
            itemList[i].SetActive(true);
        }
    }

    public void SetIsRenewInfo(bool ret)
    {
        this.isRenewItemInfo = ret;
    }
}
