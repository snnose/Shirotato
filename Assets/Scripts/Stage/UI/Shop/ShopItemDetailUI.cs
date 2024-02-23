using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemDetailUI : MonoBehaviour
{
    private static ShopItemDetailUI instance;
    public static ShopItemDetailUI Instance
    {
        get
        {
            if (null == instance)
                return null;

            return instance;
        }
    }

    private GameObject detailUI;
    private GameObject itemImage;
    private GameObject itemName;
    private GameObject itemStatus;

    private Image image;
    private TextMeshProUGUI itemNameText;
    private TextMeshProUGUI itemStatusText;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        detailUI = this.gameObject;
        itemImage = this.gameObject.transform.GetChild(1).gameObject;
        itemName = this.gameObject.transform.GetChild(2).gameObject;
        itemStatus = this.gameObject.transform.GetChild(3).gameObject;

        image = itemImage.GetComponent<Image>();
        itemNameText = itemName.GetComponent<TextMeshProUGUI>();
        itemStatusText = itemStatus.GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        this.gameObject.SetActive(false);
    }

    void Update()
    {
        
    }
    
    public void SetItemStatusText(ItemInfo itemInfo)
    {
        // 아이템 이름 변경
        itemNameText.text = itemInfo.itemName;

        itemStatusText.text = "";

        // 공격 관련
        if (itemInfo.DMGPercent != 0)
            itemStatusText.text += "대미지 " + itemInfo.DMGPercent + "%\n";
        if (itemInfo.ATKSpeed != 0)
            itemStatusText.text += "공격속도 " + itemInfo.ATKSpeed + "%\n";
        if (itemInfo.FixedDMG != 0)
            itemStatusText.text += "고정 대미지 " + itemInfo.FixedDMG + '\n';
        if (itemInfo.Critical != 0)
            itemStatusText.text += "치명타 " + itemInfo.Critical + "%\n";
        if (itemInfo.Range != 0)
            itemStatusText.text += "범위 " + itemInfo.Range + "%\n";

        // 방어 관련
        if (itemInfo.HP != 0)
            itemStatusText.text += "최대 체력 " + itemInfo.HP + '\n';
        if (itemInfo.Recovery != 0)
            itemStatusText.text += "체력 회복 " + itemInfo.Recovery + '\n';
        if (itemInfo.Armor != 0)
            itemStatusText.text += "방어력 " + itemInfo.Armor + '\n';
        if (itemInfo.Evasion != 0)
            itemStatusText.text += "회피 " + itemInfo.Evasion + "%\n";
        // 유틸 관련
        if (itemInfo.MovementSpeed != 0)
            itemStatusText.text += "이동속도 " + itemInfo.MovementSpeed + "%\n";
        if (itemInfo.RootingRange != 0)
            itemStatusText.text += "획득 범위 " + itemInfo.RootingRange + "%\n";
        if (itemInfo.Luck != 0)
            itemStatusText.text += "행운 " + itemInfo.Luck + '\n';
    }

    public void SetUIPosition(Vector2 pos)
    {
        this.gameObject.transform.position = pos;
    }

    public GameObject GetDetailUI()
    {
        return this.gameObject;
    }
}
