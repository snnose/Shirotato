using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    public string itemName = "";

    // 공격 관련
    public float DMGPercent = 0f;
    public float ATKSpeed = 0f;
    public float FixedDMG = 0f;
    public float Critical = 0f;
    public float Range = 0f;

    // 방어 관련
    public float HP = 0;
    public int Recovery = 0;
    public int Armor = 0;
    public int Evasion = 0;

    // 유틸 관련
    public float MovementSpeed = 0f;
    public float RootingRange = 0f;
    public float Luck = 0f;

    // 가격
    public int price = 0;

    public void SetItemInfo(GameObject item)
    {
        this.itemName = item.GetComponent<ItemInfo>().itemName;

        this.DMGPercent = item.GetComponent<ItemInfo>().DMGPercent;
        this.ATKSpeed = item.GetComponent<ItemInfo>().ATKSpeed;
        this.FixedDMG = item.GetComponent<ItemInfo>().FixedDMG;
        this.Critical = item.GetComponent<ItemInfo>().Critical;
        this.Range = item.GetComponent<ItemInfo>().Range;

        this.HP = item.GetComponent<ItemInfo>().HP;
        this.Recovery = item.GetComponent<ItemInfo>().Recovery;
        this.Armor = item.GetComponent<ItemInfo>().Armor;
        this.Evasion = item.GetComponent<ItemInfo>().Evasion;

        this.MovementSpeed = item.GetComponent<ItemInfo>().MovementSpeed;
        this.RootingRange = item.GetComponent<ItemInfo>().RootingRange;
        this.Luck = item.GetComponent<ItemInfo>().Luck;

        this.price = item.GetComponent<ItemInfo>().price;
    }
}
