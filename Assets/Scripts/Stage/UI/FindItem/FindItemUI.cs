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
        itemGrade = this.gameObject.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        itemImage = this.gameObject.transform.GetChild(1).GetChild(1).GetComponent<Image>();
        itemName = this.gameObject.transform.GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>();
        itemStatus = this.gameObject.transform.GetChild(1).GetChild(3).GetComponent<TextMeshProUGUI>();
        useButton = this.gameObject.transform.GetChild(1).GetChild(4).GetComponent<Button>();
        sellButton = this.gameObject.transform.GetChild(1).GetChild(5).GetComponent<Button>();
        sellButtonText = this.gameObject.transform.GetChild(1).GetChild(5).GetChild(1).GetComponent<TextMeshProUGUI>();

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
        // Ư�� ���ȿ� ����ؼ� ������ ������ �����۵� ó��
        // EpicItem29 ��Ȱ��ȭ
        PlayerInfo.Instance.InActivateEpicItem29();
        // LegendItem16 ��Ȱ��ȭ
        PlayerInfo.Instance.InActivateLegendItem16();
        // LegendItem17 ��Ȱ��ȭ
        PlayerInfo.Instance.InActivateLegendItem17();
        // LegendItem22 ��Ȱ��ȭ
        PlayerInfo.Instance.InActivateLegendItem22();
        // LegendItem23 ��Ȱ��ȭ
        PlayerInfo.Instance.InActivateLegendItem23();

        // �ش� �������� �ɷ�ġ ����
        ApplyStatus();

        // Ư�� ���ȿ� ����ؼ� ������ ������ �����۵� ó��
        // EpicItem29 Ȱ��ȭ
        PlayerInfo.Instance.ActivateEpicItem29();
        // LegendItem16 Ȱ��ȭ
        PlayerInfo.Instance.ActivateLegendItem16();
        // LegendItem17 Ȱ��ȭ
        PlayerInfo.Instance.ActivateLegendItem17();
        // LegendItem22 Ȱ��ȭ
        PlayerInfo.Instance.ActivateLegendItem22();
        // LegendItem23 Ȱ��ȭ
        PlayerInfo.Instance.ActivateLegendItem23();

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

        // Floating ����
        GameRoot.Instance.floatingFindItemUI = GameRoot.Instance.FloatingFindItemUI();

        // FindItemUI ��Ȱ��ȭ
        this.gameObject.SetActive(false);
        GameRoot.Instance.SetIsDuringFindItem(false);
    }

    private void OnClickSellButton()
    {
        // �Ǹ� ������ ������ ������ 25%
        int sellPrice = Mathf.FloorToInt(item.GetComponent<ItemInfo>().price * 0.25f);

        // ���� ���ÿ� �Ǹ��� ���ݸ�ŭ �߰�
        PlayerInfo.Instance.SetCurrentWaffle(PlayerInfo.Instance.GetCurrentWaffle() + sellPrice);
        StartCoroutine(RenewWaffleAmount.Instance.RenewCurrentWaffleAmount());

        // Floating ����
        GameRoot.Instance.floatingFindItemUI = GameRoot.Instance.FloatingFindItemUI();

        // FindItemUI ��Ȱ��ȭ
        this.gameObject.SetActive(false);
        GameRoot.Instance.SetIsDuringFindItem(false);
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
        // ����� ���
        PlayerInfo.Instance.SetHPDrain(PlayerInfo.Instance.GetHPDrain() + item.GetComponent<ItemInfo>().HPDrain);
        // ����
        PlayerInfo.Instance.SetArmor(PlayerInfo.Instance.GetArmor() + item.GetComponent<ItemInfo>().Armor);
        // ȸ��
        PlayerInfo.Instance.SetEvasion(PlayerInfo.Instance.GetEvasion() + item.GetComponent<ItemInfo>().Evasion);

        // �̵��ӵ� %
        PlayerInfo.Instance.SetMovementSpeedPercent(PlayerInfo.Instance.GetMovementSpeedPercent() + item.GetComponent<ItemInfo>().MovementSpeedPercent);
        // ȹ�� ����
        PlayerInfo.Instance.SetRootingRange(PlayerInfo.Instance.GetRootingRange() + item.GetComponent<ItemInfo>().RootingRange);
        // ���
        PlayerInfo.Instance.SetLuck(PlayerInfo.Instance.GetLuck() + item.GetComponent<ItemInfo>().Luck);
        // ��Ȯ
        PlayerInfo.Instance.SetHarvest(PlayerInfo.Instance.GetHarvest() + item.GetComponent<ItemInfo>().Harvest);
        // ����ġ ȹ�淮
        PlayerInfo.Instance.SetExpGain(PlayerInfo.Instance.GetExpGain() + item.GetComponent<ItemInfo>().ExpGain);
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
            tmpText += "ġ��Ÿ Ȯ��+" + itemInfo.Critical + "%\n";
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
            tmpText += "ȸ���� +" + itemInfo.Recovery + '\n';
            plusCount++;
        }
        if (itemInfo.HPDrain > 0)
        {
            tmpText += "����� ���% +" + itemInfo.HPDrain + '\n';
            plusCount++;
        }
        if (itemInfo.Armor > 0)
        {
            tmpText += "���� +" + itemInfo.Armor + '\n';
            plusCount++;
        }
        if (itemInfo.Evasion > 0)
        {
            tmpText += "ȸ�� Ȯ��+" + itemInfo.Evasion + "%\n";
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
        if (itemInfo.Harvest > 0)
        {
            tmpText += "��Ȯ +" + itemInfo.Harvest + '\n';
            plusCount++;
        }
        if (itemInfo.ExpGain > 0)
        {
            tmpText += "����ġ ȹ�� +" + itemInfo.ExpGain + "%\n";
            plusCount++;
        }

        // ������ Ư�� ȿ��
        if (itemInfo.positiveSpecial != "")
        {
            tmpText += itemInfo.positiveSpecial + "\n";
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
            tmpText += "ġ��Ÿ Ȯ��" + itemInfo.Critical + "%\n";
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
            tmpText += "ȸ���� " + itemInfo.Recovery + '\n';
            minusCount++;
        }
        if (itemInfo.HPDrain < 0)
        {
            tmpText += "����� ���% " + itemInfo.HPDrain + '\n';
            minusCount++;
        }
        if (itemInfo.Armor < 0)
        {
            tmpText += "���� " + itemInfo.Armor + '\n';
            minusCount++;
        }
        if (itemInfo.Evasion < 0)
        {
            tmpText += "ȸ�� Ȯ�� " + itemInfo.Evasion + "%\n";
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
        if (itemInfo.Harvest < 0)
        {
            tmpText += "��Ȯ " + itemInfo.Harvest + '\n';
            minusCount++;
        }
        if (itemInfo.ExpGain < 0)
        {
            tmpText += "����ġ ȹ�� " + itemInfo.ExpGain + "%\n";
            minusCount++;
        }

        // ������ Ư�� ȿ��
        if (itemInfo.negativeSpecial != "")
        {
            tmpText += itemInfo.negativeSpecial + "\n";
            minusCount++;
        }

        // �ؽ�Ʈ�� �� �������� ������
        string[] lines = tmpText.Split('\n');
        string finalText = ""; // ���� �ؽ�Ʈ

        // �ɷ�ġ�� ����ϸ� �ؽ�Ʈ�� �ʷϻ����� ����
        for (int j = 0; j < plusCount; j++)
        {
            string coloredLine = "";
            // +�� ���ڸ� ���� �����Ѵ�
            for (int k = 0; k < lines[j].Length; k++)
            {
                // #1FDE38 << ���� �ʷϻ�
                if (lines[j][k] == '+' || lines[j][k] == '%' || lines[j][k] == '.')
                    coloredLine += $"<color=#1FDE38>{lines[j][k]}</color>";
                else if (lines[j][k] > 47 && lines[j][k] < 58)
                    coloredLine += $"<color=#1FDE38>{lines[j][k]}</color>";
                else
                    coloredLine += lines[j][k];
            }

            // ���� �ؽ�Ʈ�� �߰�
            finalText += coloredLine;
            finalText += "\n";
        }

        // �ɷ�ġ�� �϶��ϸ� �ؽ�Ʈ�� ���������� ����
        for (int j = plusCount; j < plusCount + minusCount; j++)
        {
            string coloredLine = "";

            // -�� ���ڸ� ���� �����Ѵ�
            for (int k = 0; k < lines[j].Length; k++)
            {
                if (lines[j][k] == '-' || lines[j][k] == '%' || lines[j][k] == '.')
                    coloredLine += $"<color=#{ColorUtility.ToHtmlStringRGB(Color.red)}>{lines[j][k]}</color>";
                else if (lines[j][k] > 47 && lines[j][k] < 58)
                    coloredLine += $"<color=#{ColorUtility.ToHtmlStringRGB(Color.red)}>{lines[j][k]}</color>";
                else
                    coloredLine += lines[j][k];
            }

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

        // RareItem32�� �����ߴٸ� ������ 60%
        // �������� �ʾҴٸ� ������ 25%�� ��´�
        int sellPrice = ActivateRareItem32(itemInfo.price);

        sellButtonText.text = "�Ǹ� (+" + sellPrice.ToString() +")";
    }

    int ActivateRareItem32(int itemPrice)
    {
        if (ItemManager.Instance.GetOwnRareItemList()[32] > 0)
        {
            return Mathf.FloorToInt(itemPrice * 0.6f);
        }

        return Mathf.FloorToInt(itemPrice * 0.25f);
    }
}
