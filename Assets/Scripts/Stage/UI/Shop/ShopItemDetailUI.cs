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

    public void SetItemImage(Image image)
    {
        this.image = image;
    }
    
    public void SetItemStatusText(ItemInfo itemInfo)
    {
        // ������ �̸� ����
        itemNameText.text = itemInfo.itemName;

        itemStatusText.text = "";

        // ������ ���� ������ ��� ��´�
        string tmpText = "";
        int plusCount = 0;
        int minusCount = 0;

        // ���� ����
        if (itemInfo.DMGPercent > 0)
        {
            tmpText += "����� +" + itemInfo.DMGPercent + "%\n";
            plusCount++;
        }
        if (itemInfo.ATKSpeed > 0)
        {
            tmpText += "���ݼӵ� +" + itemInfo.ATKSpeed + "%\n";
            plusCount++;
        }
        if (itemInfo.FixedDMG > 0)
        {
            tmpText += "���� ����� +" + itemInfo.FixedDMG + '\n';
            plusCount++;
        }
        if (itemInfo.Critical > 0)
        {
            tmpText += "ġ��Ÿ +" + itemInfo.Critical + "%\n";
            plusCount++;
        }
        if (itemInfo.Range > 0)
        {
            tmpText += "���� +" + itemInfo.Range + "%\n";
            plusCount++;
        }

        // ��� ����
        if (itemInfo.HP > 0)
        {
            tmpText += "�ִ� ü�� +" + itemInfo.HP + '\n';
            plusCount++;
        }
        if (itemInfo.Recovery > 0)
        {
            tmpText += "ü�� ȸ�� +" + itemInfo.Recovery + '\n';
            plusCount++;
        }
        if (itemInfo.Armor > 0)
        {
            tmpText += "���� +" + itemInfo.Armor + '\n';
            plusCount++;
        }
        if (itemInfo.Evasion > 0)
        {
            tmpText += "ȸ�� +" + itemInfo.Evasion + "%\n";
            plusCount++;
        }

        // ��ƿ ����
        if (itemInfo.MovementSpeedPercent > 0)
        {
            tmpText += "�̵��ӵ� +" + itemInfo.MovementSpeedPercent + "%\n";
            plusCount++;
        }
        if (itemInfo.RootingRange > 0)
        {
            tmpText += "ȹ�� ���� +" + itemInfo.RootingRange + "%\n";
            plusCount++;
        }
        if (itemInfo.Luck > 0)
        {
            tmpText += "��� +" + itemInfo.Luck + '\n';
            plusCount++;
        }

        // ���� ����
        if (itemInfo.DMGPercent < 0)
        {
            tmpText += "����� " + itemInfo.DMGPercent + "%\n";
            minusCount++;
        }
        if (itemInfo.ATKSpeed < 0)
        {
            tmpText += "���ݼӵ� " + itemInfo.ATKSpeed + "%\n";
            minusCount++;
        }
        if (itemInfo.FixedDMG < 0)
        {
            tmpText += "���� ����� " + itemInfo.FixedDMG + '\n';
            minusCount++;
        }
        if (itemInfo.Critical < 0)
        {
            tmpText += "ġ��Ÿ " + itemInfo.Critical + "%\n";
            minusCount++;
        }
        if (itemInfo.Range < 0)
        {
            tmpText += "���� " + itemInfo.Range + "%\n";
            minusCount++;
        }

        // ��� ����
        if (itemInfo.HP < 0)
        {
            tmpText += "�ִ� ü�� " + itemInfo.HP + '\n';
            minusCount++;
        }
        if (itemInfo.Recovery < 0)
        {
            tmpText += "ü�� ȸ�� " + itemInfo.Recovery + '\n';
            minusCount++;
        }
        if (itemInfo.Armor < 0)
        {
            tmpText += "���� " + itemInfo.Armor + '\n';
            minusCount++;
        }
        if (itemInfo.Evasion < 0)
        {
            tmpText += "ȸ�� " + itemInfo.Evasion + "%\n";
            minusCount++;
        }

        // ��ƿ ����
        if (itemInfo.MovementSpeedPercent < 0)
        {
            tmpText += "�̵��ӵ� " + itemInfo.MovementSpeedPercent + "%\n";
            minusCount++;
        }
        if (itemInfo.RootingRange < 0)
        {
            tmpText += "ȹ�� ���� " + itemInfo.RootingRange + "%\n";
            minusCount++;
        }
        if (itemInfo.Luck < 0)
        {
            tmpText += "��� " + itemInfo.Luck + '\n';
            minusCount++;
        }

        // �ؽ�Ʈ�� �� �������� ������
        string[] lines = tmpText.Split('\n');
        string finalText = ""; // ���� �ؽ�Ʈ

        // �ɷ�ġ�� ����ϸ� �ؽ�Ʈ�� �ʷϻ����� ����
        for (int j = 0; j < plusCount; j++)
        {
            string coloredLine = $"<color=#{ColorUtility.ToHtmlStringRGB(Color.green)}>{lines[j]}</color>";

            // ���� �ؽ�Ʈ�� �߰�
            finalText += coloredLine;
            finalText += "\n";
        }

        // �ɷ�ġ�� �϶��ϸ� �ؽ�Ʈ�� ���������� ����
        for (int j = plusCount; j < plusCount + minusCount; j++)
        {
            string coloredLine = $"<color=#{ColorUtility.ToHtmlStringRGB(Color.red)}>{lines[j]}</color>";

            // ���� �ؽ�Ʈ�� �߰�
            finalText += coloredLine;
            finalText += "\n";
        }

        // TextMeshProUGUI�� �Ҵ�
        itemStatusText.text = finalText;
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
