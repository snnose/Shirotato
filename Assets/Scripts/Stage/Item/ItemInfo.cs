using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    public string itemName = "";
    public int itemNumber = 0;
    public int rarity = 0;

    // 특수 능력 설명(텍스트)
    // 긍정적 특수 능력
    public string positiveSpecial;
    // 부정적 특수 능력
    public string negativeSpecial;

    // 공격 관련
    public float DMGPercent = 0f;
    public float ATKSpeed = 0f;
    public float FixedDMG = 0f;
    public float Critical = 0f;
    public float Range = 0f;

    // 방어 관련
    public float HP = 0f;
    public int Recovery = 0;
    public float HPDrain = 0f;
    public int Armor = 0;
    public int Evasion = 0;

    // 유틸 관련
    public float MovementSpeedPercent = 0f;
    public float RootingRange = 0f;
    public float Luck = 0f;
    public float Harvest = 0f;
    public float ExpGain = 0f;

    // 가격
    public int price = 0;

    public void SetItemInfo(GameObject item)
    {
        this.itemName = item.GetComponent<ItemInfo>().itemName;
        this.itemNumber = item.GetComponent<ItemInfo>().itemNumber;
        this.rarity = item.GetComponent<ItemInfo>().rarity;

        this.positiveSpecial = item.GetComponent<ItemInfo>().positiveSpecial;
        this.negativeSpecial = item.GetComponent<ItemInfo>().negativeSpecial;

        this.DMGPercent = item.GetComponent<ItemInfo>().DMGPercent;
        this.ATKSpeed = item.GetComponent<ItemInfo>().ATKSpeed;
        this.FixedDMG = item.GetComponent<ItemInfo>().FixedDMG;
        this.Critical = item.GetComponent<ItemInfo>().Critical;
        this.Range = item.GetComponent<ItemInfo>().Range;

        this.HP = item.GetComponent<ItemInfo>().HP;
        this.Recovery = item.GetComponent<ItemInfo>().Recovery;
        this.HPDrain = item.GetComponent<ItemInfo>().HPDrain;
        this.Armor = item.GetComponent<ItemInfo>().Armor;
        this.Evasion = item.GetComponent<ItemInfo>().Evasion;

        this.MovementSpeedPercent = item.GetComponent<ItemInfo>().MovementSpeedPercent;
        this.RootingRange = item.GetComponent<ItemInfo>().RootingRange;
        this.Luck = item.GetComponent<ItemInfo>().Luck;
        this.Harvest = item.GetComponent<ItemInfo>().Harvest;
        this.ExpGain = item.GetComponent<ItemInfo>().ExpGain;

        this.price = item.GetComponent<ItemInfo>().price;
    }
}
