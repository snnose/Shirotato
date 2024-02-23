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
        // ������ �̸� ����
        itemNameText.text = itemInfo.itemName;

        itemStatusText.text = "";

        // ���� ����
        if (itemInfo.DMGPercent != 0)
            itemStatusText.text += "����� " + itemInfo.DMGPercent + "%\n";
        if (itemInfo.ATKSpeed != 0)
            itemStatusText.text += "���ݼӵ� " + itemInfo.ATKSpeed + "%\n";
        if (itemInfo.FixedDMG != 0)
            itemStatusText.text += "���� ����� " + itemInfo.FixedDMG + '\n';
        if (itemInfo.Critical != 0)
            itemStatusText.text += "ġ��Ÿ " + itemInfo.Critical + "%\n";
        if (itemInfo.Range != 0)
            itemStatusText.text += "���� " + itemInfo.Range + "%\n";

        // ��� ����
        if (itemInfo.HP != 0)
            itemStatusText.text += "�ִ� ü�� " + itemInfo.HP + '\n';
        if (itemInfo.Recovery != 0)
            itemStatusText.text += "ü�� ȸ�� " + itemInfo.Recovery + '\n';
        if (itemInfo.Armor != 0)
            itemStatusText.text += "���� " + itemInfo.Armor + '\n';
        if (itemInfo.Evasion != 0)
            itemStatusText.text += "ȸ�� " + itemInfo.Evasion + "%\n";
        // ��ƿ ����
        if (itemInfo.MovementSpeed != 0)
            itemStatusText.text += "�̵��ӵ� " + itemInfo.MovementSpeed + "%\n";
        if (itemInfo.RootingRange != 0)
            itemStatusText.text += "ȹ�� ���� " + itemInfo.RootingRange + "%\n";
        if (itemInfo.Luck != 0)
            itemStatusText.text += "��� " + itemInfo.Luck + '\n';
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
