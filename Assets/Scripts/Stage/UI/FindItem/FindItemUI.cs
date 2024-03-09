using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FindItemUI : MonoBehaviour
{
    // UI
    private Image itemGrade;
    private Image itemImage;
    private TextMeshProUGUI itemName;
    private TextMeshProUGUI itemStatus;
    private Button useButton;
    private Button sellButton;
    private TextMeshProUGUI sellButtonText;

    // ���õ� ������
    private GameObject item;
    private int rarity;

    public IEnumerator renewFindItem;

    private void Awake()
    {
        itemGrade = this.gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        itemImage = this.gameObject.transform.GetChild(0).GetChild(1).GetComponent<Image>();
        itemName = this.gameObject.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>();
        itemStatus = this.gameObject.transform.GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>();
        useButton = this.gameObject.transform.GetChild(0).GetChild(4).GetComponent<Button>();
        sellButton = this.gameObject.transform.GetChild(0).GetChild(5).GetComponent<Button>();
        sellButtonText = this.gameObject.transform.GetChild(0).GetChild(5).GetComponent<TextMeshProUGUI>();

        item = null;

        this.gameObject.transform.position = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
    }
    
    void Start()
    {
        // ��ư�� �̺�Ʈ ����
        useButton.onClick.AddListener(OnClickUseButton);
        sellButton.onClick.AddListener(OnClickSellButton);

        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // ���� ���� �� �ڽ��� ����ߴٸ� ���� ���� �� UI ���
        // UI�� �����Ѵ�
        if (renewFindItem != null)
        {
            StartCoroutine(renewFindItem);
        }
    }

    // ������ �߰� UI�� �����Ѵ�
    public IEnumerator RenewFindItem()
    {
        // ���� ���忡 ���� ������ Ȯ���� �����Ѵ�
        List<float> itemProbabilities = SetItemProbability();
        // Ȯ���� ���� ������ ����� �����Ѵ�
        rarity = ChooseItemRarity(itemProbabilities);
        // ����� �ش��ϴ� �����۵� �� �ϳ��� �����Ѵ�
        item = ChooseItem(rarity);

        // UI�� ���õ� ������ ������ �����Ѵ�
        SetUIThings(item);
        yield return null;
    }

    // ��� ��ư�� ���� ��� ���� �����ۿ� �߰�
    private void OnClickUseButton()
    {
        // �ش� �������� �ɷ�ġ ����
        ApplyStatus();

        // ��񵿾� ShopUI Ȱ��ȭ
        ShopUIControl shopUIControl = ShopUIControl.Instance;
        shopUIControl.gameObject.SetActive(true);
        // ��ġ�� �Ű� ȭ�鿡 ������ �ʰ� �Ѵ�
        ShopUIControl.Instance.transform.position = new Vector2(-2000f, 0f);
        // ���� ������ ����Ʈ�� �߰��Ѵ�
        shopUIControl.transform.GetChild(4).GetComponent<ShopOwnItemListControl>().renewOwnItemList =
            shopUIControl.transform.GetChild(4).GetComponent<ShopOwnItemListControl>().RenewOwnItemList(item);
        // ShopUI ��Ȱ��ȭ
        shopUIControl.gameObject.SetActive(false);
        // FindItemUI ��Ȱ��ȭ
        this.gameObject.SetActive(false);
        GameRoot.Instance.SetIsDuringFindItem(false);
    }

    private void OnClickSellButton()
    {

    }

    private void ApplyStatus()
    {
        // �����%
        PlayerInfo.Instance.SetDMGPercent(PlayerInfo.Instance.GetDMGPercent() + item.GetComponent<ItemInfo>().DMGPercent);
        // ���ݼӵ�
        PlayerInfo.Instance.SetATKSpeed(PlayerInfo.Instance.GetATKSpeed() + item.GetComponent<ItemInfo>().ATKSpeed);
        // ���� �����
        PlayerInfo.Instance.SetFixedDMG(PlayerInfo.Instance.GetFixedDMG() + item.GetComponent<ItemInfo>().FixedDMG);
        // ġ��Ÿ Ȯ��
        PlayerInfo.Instance.SetCritical(PlayerInfo.Instance.GetCritical() + item.GetComponent<ItemInfo>().Critical);
        // ����
        PlayerInfo.Instance.SetRange(PlayerInfo.Instance.GetRange() + item.GetComponent<ItemInfo>().Range);

        // �ִ� ü��
        PlayerInfo.Instance.SetHP(PlayerInfo.Instance.GetHP() + item.GetComponent<ItemInfo>().HP);
        // ȸ����
        PlayerInfo.Instance.SetRecovery(PlayerInfo.Instance.GetRecovery() + item.GetComponent<ItemInfo>().Recovery);
        // ����
        PlayerInfo.Instance.SetArmor(PlayerInfo.Instance.GetArmor() + item.GetComponent<ItemInfo>().Armor);
        // ȸ��
        PlayerInfo.Instance.SetEvasion(PlayerInfo.Instance.GetEvasion() + item.GetComponent<ItemInfo>().Evasion);

        // �̵��ӵ� %
        PlayerInfo.Instance.SetMovementSpeed(PlayerInfo.Instance.GetMovementSpeedPercent() + item.GetComponent<ItemInfo>().MovementSpeedPercent);
        // ȹ�� ����
        PlayerInfo.Instance.SetRootingRange(PlayerInfo.Instance.GetRootingRange() + item.GetComponent<ItemInfo>().RootingRange);
        // ���
        PlayerInfo.Instance.SetLuck(PlayerInfo.Instance.GetLuck() + item.GetComponent<ItemInfo>().Luck);
    }

    // ������ ��� ���� �Լ�
    private List<float> SetItemProbability()
    {
        List<float> tmp = new List<float>(new float[] { 100, 0, 0, 0 });

        int round = GameRoot.Instance.GetCurrentRound();
        // ���� ��� Ȯ�� (�ִ� 8%)
        tmp[3] = (round - 7) / 4 * ((1 + PlayerInfo.Instance.GetLuck()) / 100);
        if (tmp[3] <= 0)
            tmp[3] = 0;
        else if (tmp[3] >= 8)
            tmp[3] = 8;
        // ���� ��� Ȯ�� (�ִ� 25%)
        tmp[2] = (1f - (tmp[3] * 0.01f)) * 2 * (round - 3) * ((1 + PlayerInfo.Instance.GetLuck()) / 100) + tmp[3];
        if (tmp[2] <= 0)
            tmp[2] = 0;
        else if (tmp[2] >= (1f - (tmp[3] * 0.01f)) * 25 + tmp[3])
            tmp[2] = (1f - (tmp[3] * 0.01f)) * 25 + tmp[3];
        // ���� ��� Ȯ�� (�ִ� 60%)
        tmp[1] = (1f - (tmp[3] * 0.01f) - (tmp[2] * 0.01f)) * 6 * round * ((1 + PlayerInfo.Instance.GetLuck()) / 100) + tmp[2] + tmp[3];
        if (tmp[1] >= (1f - (tmp[3] * 0.01f) - (tmp[2] * 0.01f)) * 60 + tmp[2] + tmp[3])
            tmp[1] = (1f - (tmp[3] * 0.01f) - (tmp[2] * 0.01f)) * 60 + tmp[2] + tmp[3];

        return tmp;
    }

    // ������ ��� ���� �Լ�
    private int ChooseItemRarity(List<float> itemProbabilities)
    {
        // ������ ����� ������ ����� �����Ѵ�.
        float itemRandom = UnityEngine.Random.Range(0.0f, 100.0f);
        int rarity = -1;
        // ������ ��� ����Ʈ�� �ں��� Ž���Ѵ�.
        for (int j = 3; j >= 0; j--)
        {
            // ��� ����Ʈ�� ���ں��� ���ٸ� ��� ����
            if (itemRandom < itemProbabilities[j])
            {
                rarity = j;
                break;
            }
        }

        return rarity;
    }

    // ������ ���� �Լ�
    private GameObject ChooseItem(int rarity)
    {
        GameObject tmp = null;
        int random = 0;
        switch (rarity)
        {
            case 0:
                random = UnityEngine.Random.Range(0, ItemManager.Instance.normalItemList.Count);
                tmp = ItemManager.Instance.normalItemList[random];
                break;
            case 1:
                random = UnityEngine.Random.Range(0, ItemManager.Instance.rareItemList.Count);
                tmp = ItemManager.Instance.rareItemList[random];
                break;
            case 2:
                random = UnityEngine.Random.Range(0, ItemManager.Instance.epicItemList.Count);
                tmp = ItemManager.Instance.epicItemList[random];
                break;
            case 3:
                random = UnityEngine.Random.Range(0, ItemManager.Instance.legendItemList.Count);
                tmp = ItemManager.Instance.legendItemList[random];
                break;
            default:
                break;
        }

        return tmp;
    }

    private Color DecideGradeColor(int grade)
    {
        Color color = Color.white;
        switch (grade)
        {
            case 0:
                color = Color.white;
                break;
            // �Ķ���
            case 1:
                color = new Color(120, 166, 214);
                ColorUtility.TryParseHtmlString("#78A6D6", out color);
                break;
            // �����
            case 2:
                color = new Color(161, 120, 214);
                ColorUtility.TryParseHtmlString("#A178D6", out color);
                break;
            // ��Ȳ��
            case 3:
                color = new Color(233, 137, 76);
                ColorUtility.TryParseHtmlString("#E9894C", out color);
                break;
            default:
                color = Color.black;
                break;
        }

        return color;
    }

    void SetItemStatus(ItemInfo itemInfo)
    {
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
        itemStatus.text = finalText;
    }

    private void SetUIThings(GameObject item)
    {
        ItemInfo itemInfo = item.GetComponent<ItemInfo>();

        itemGrade.color = DecideGradeColor(rarity);
        itemImage.sprite = item.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite;
        itemName.text = itemInfo.itemName;
        SetItemStatus(itemInfo);
    }
}