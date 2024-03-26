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

    // 선택된 아이템
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
        // 버튼에 이벤트 부착
        useButton.onClick.AddListener(OnClickUseButton);
        sellButton.onClick.AddListener(OnClickSellButton);

        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // 라운드 진행 중 박스를 드랍했다면 라운드 종료 후 UI 출력
        // UI를 갱신한다
        if (renewFindItem != null)
        {
            StartCoroutine(renewFindItem);
        }
    }

    // 아이템 발견 UI를 갱신한다
    public IEnumerator RenewFindItem()
    {
        // 현재 라운드에 따른 아이템 확률을 설정한다
        List<float> itemProbabilities = SetItemProbability();
        // 확률에 따른 아이템 레어도를 결정한다
        rarity = ChooseItemRarity(itemProbabilities);
        // 레어도에 해당하는 아이템들 중 하나를 선택한다
        item = ChooseItem(rarity);

        // UI를 선택된 아이템 정보로 갱신한다
        SetUIThings(item);
        yield return null;
    }

    // 사용 버튼을 누를 경우 보유 아이템에 추가
    private void OnClickUseButton()
    {
        // 특정 스탯에 비례해서 스탯이 오르는 아이템들 처리
        // EpicItem29 비활성화
        PlayerInfo.Instance.InActivateEpicItem29();
        // LegendItem16 비활성화
        PlayerInfo.Instance.InActivateLegendItem16();
        // LegendItem17 비활성화
        PlayerInfo.Instance.InActivateLegendItem17();
        // LegendItem22 비활성화
        PlayerInfo.Instance.InActivateLegendItem22();
        // LegendItem23 비활성화
        PlayerInfo.Instance.InActivateLegendItem23();

        // 해당 아이템의 능력치 적용
        ApplyStatus();

        // 특정 스탯에 비례해서 스탯이 오르는 아이템들 처리
        // EpicItem29 활성화
        PlayerInfo.Instance.ActivateEpicItem29();
        // LegendItem16 활성화
        PlayerInfo.Instance.ActivateLegendItem16();
        // LegendItem17 활성화
        PlayerInfo.Instance.ActivateLegendItem17();
        // LegendItem22 활성화
        PlayerInfo.Instance.ActivateLegendItem22();
        // LegendItem23 활성화
        PlayerInfo.Instance.ActivateLegendItem23();

        // 잠깐동안 ShopUI 활성화
        ShopUIControl shopUIControl = ShopUIControl.Instance;
        shopUIControl.gameObject.SetActive(true);

        // 위치를 옮겨 화면에 보이지 않게 한다
        ShopUIControl.Instance.transform.position = new Vector2(-2000f, 0f);

        // 보유 아이템 리스트에 추가한다
        shopUIControl.transform.GetChild(4).GetComponent<ShopOwnItemListControl>().renewOwnItemList =
            shopUIControl.transform.GetChild(4).GetComponent<ShopOwnItemListControl>().RenewOwnItemList(item);

        // ShopUI 비활성화
        shopUIControl.gameObject.SetActive(false);

        // Floating 장전
        GameRoot.Instance.floatingFindItemUI = GameRoot.Instance.FloatingFindItemUI();

        // FindItemUI 비활성화
        this.gameObject.SetActive(false);
        GameRoot.Instance.SetIsDuringFindItem(false);
    }

    private void OnClickSellButton()
    {
        // 판매 가격은 아이템 가격의 25%
        int sellPrice = Mathf.FloorToInt(item.GetComponent<ItemInfo>().price * 0.25f);

        // 보유 와플에 판매한 가격만큼 추가
        PlayerInfo.Instance.SetCurrentWaffle(PlayerInfo.Instance.GetCurrentWaffle() + sellPrice);
        StartCoroutine(RenewWaffleAmount.Instance.RenewCurrentWaffleAmount());

        // Floating 장전
        GameRoot.Instance.floatingFindItemUI = GameRoot.Instance.FloatingFindItemUI();

        // FindItemUI 비활성화
        this.gameObject.SetActive(false);
        GameRoot.Instance.SetIsDuringFindItem(false);
    }

    private void ApplyStatus()
    {
        // 대미지%
        PlayerInfo.Instance.SetDMGPercent(PlayerInfo.Instance.GetDMGPercent() + item.GetComponent<ItemInfo>().DMGPercent);
        // 공격속도
        PlayerInfo.Instance.SetATKSpeed(PlayerInfo.Instance.GetATKSpeed() + item.GetComponent<ItemInfo>().ATKSpeed);
        // 고정 대미지
        PlayerInfo.Instance.SetFixedDMG(PlayerInfo.Instance.GetFixedDMG() + item.GetComponent<ItemInfo>().FixedDMG);
        // 치명타 확률
        PlayerInfo.Instance.SetCritical(PlayerInfo.Instance.GetCritical() + item.GetComponent<ItemInfo>().Critical);
        // 범위
        PlayerInfo.Instance.SetRange(PlayerInfo.Instance.GetRange() + item.GetComponent<ItemInfo>().Range);

        // 최대 체력
        PlayerInfo.Instance.SetHP(PlayerInfo.Instance.GetHP() + item.GetComponent<ItemInfo>().HP);
        // 회복력
        PlayerInfo.Instance.SetRecovery(PlayerInfo.Instance.GetRecovery() + item.GetComponent<ItemInfo>().Recovery);
        // 생명력 흡수
        PlayerInfo.Instance.SetHPDrain(PlayerInfo.Instance.GetHPDrain() + item.GetComponent<ItemInfo>().HPDrain);
        // 방어력
        PlayerInfo.Instance.SetArmor(PlayerInfo.Instance.GetArmor() + item.GetComponent<ItemInfo>().Armor);
        // 회피
        PlayerInfo.Instance.SetEvasion(PlayerInfo.Instance.GetEvasion() + item.GetComponent<ItemInfo>().Evasion);

        // 이동속도 %
        PlayerInfo.Instance.SetMovementSpeedPercent(PlayerInfo.Instance.GetMovementSpeedPercent() + item.GetComponent<ItemInfo>().MovementSpeedPercent);
        // 획득 범위
        PlayerInfo.Instance.SetRootingRange(PlayerInfo.Instance.GetRootingRange() + item.GetComponent<ItemInfo>().RootingRange);
        // 행운
        PlayerInfo.Instance.SetLuck(PlayerInfo.Instance.GetLuck() + item.GetComponent<ItemInfo>().Luck);
        // 수확
        PlayerInfo.Instance.SetHarvest(PlayerInfo.Instance.GetHarvest() + item.GetComponent<ItemInfo>().Harvest);
        // 경험치 획득량
        PlayerInfo.Instance.SetExpGain(PlayerInfo.Instance.GetExpGain() + item.GetComponent<ItemInfo>().ExpGain);
    }

    // 아이템 등급 설정 함수
    private List<float> SetItemProbability()
    {
        List<float> tmp = new List<float>(new float[] { 100, 0, 0, 0 });

        int round = GameRoot.Instance.GetCurrentRound();
        // 전설 등급 확률 (최대 8%)
        tmp[3] = (round - 7) / 4 * ((1 + PlayerInfo.Instance.GetLuck()) / 100);
        if (tmp[3] <= 0)
            tmp[3] = 0;
        else if (tmp[3] >= 8)
            tmp[3] = 8;
        // 에픽 등급 확률 (최대 25%)
        tmp[2] = (1f - (tmp[3] * 0.01f)) * 2 * (round - 3) * ((1 + PlayerInfo.Instance.GetLuck()) / 100) + tmp[3];
        if (tmp[2] <= 0)
            tmp[2] = 0;
        else if (tmp[2] >= (1f - (tmp[3] * 0.01f)) * 25 + tmp[3])
            tmp[2] = (1f - (tmp[3] * 0.01f)) * 25 + tmp[3];
        // 레어 등급 확률 (최대 60%)
        tmp[1] = (1f - (tmp[3] * 0.01f) - (tmp[2] * 0.01f)) * 6 * round * ((1 + PlayerInfo.Instance.GetLuck()) / 100) + tmp[2] + tmp[3];
        if (tmp[1] >= (1f - (tmp[3] * 0.01f) - (tmp[2] * 0.01f)) * 60 + tmp[2] + tmp[3])
            tmp[1] = (1f - (tmp[3] * 0.01f) - (tmp[2] * 0.01f)) * 60 + tmp[2] + tmp[3];

        return tmp;
    }

    // 아이템 등급 결정 함수
    private int ChooseItemRarity(List<float> itemProbabilities)
    {
        // 난수를 사용해 아이템 레어도를 결정한다.
        float itemRandom = UnityEngine.Random.Range(0.0f, 100.0f);
        int rarity = -1;
        // 아이템 레어도 리스트의 뒤부터 탐색한다.
        for (int j = 3; j >= 0; j--)
        {
            // 레어도 리스트의 숫자보다 낮다면 레어도 결정
            if (itemRandom < itemProbabilities[j])
            {
                rarity = j;
                break;
            }
        }

        return rarity;
    }

    // 아이템 결정 함수
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
            // 파랑색
            case 1:
                color = new Color(120, 166, 214);
                ColorUtility.TryParseHtmlString("#78A6D6", out color);
                break;
            // 보라색
            case 2:
                color = new Color(161, 120, 214);
                ColorUtility.TryParseHtmlString("#A178D6", out color);
                break;
            // 주황색
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
        // 아이템 스텟 정보를 모두 담는다
        string tmpText = "";
        int plusCount = 0;
        int minusCount = 0;

        // 공격 관련
        if (itemInfo.DMGPercent > 0)
        {
            tmpText += "대미지 +" + itemInfo.DMGPercent + "%\n";
            plusCount++;
        }
        if (itemInfo.ATKSpeed > 0)
        {
            tmpText += "공격속도 +" + itemInfo.ATKSpeed + "%\n";
            plusCount++;
        }
        if (itemInfo.FixedDMG > 0)
        {
            tmpText += "고정 대미지 +" + itemInfo.FixedDMG + '\n';
            plusCount++;
        }
        if (itemInfo.Critical > 0)
        {
            tmpText += "치명타 확률+" + itemInfo.Critical + "%\n";
            plusCount++;
        }
        if (itemInfo.Range > 0)
        {
            tmpText += "범위 +" + itemInfo.Range + "%\n";
            plusCount++;
        }

        // 방어 관련
        if (itemInfo.HP > 0)
        {
            tmpText += "최대 체력 +" + itemInfo.HP + '\n';
            plusCount++;
        }
        if (itemInfo.Recovery > 0)
        {
            tmpText += "회복력 +" + itemInfo.Recovery + '\n';
            plusCount++;
        }
        if (itemInfo.HPDrain > 0)
        {
            tmpText += "생명력 흡수% +" + itemInfo.HPDrain + '\n';
            plusCount++;
        }
        if (itemInfo.Armor > 0)
        {
            tmpText += "방어력 +" + itemInfo.Armor + '\n';
            plusCount++;
        }
        if (itemInfo.Evasion > 0)
        {
            tmpText += "회피 확률+" + itemInfo.Evasion + "%\n";
            plusCount++;
        }

        // 유틸 관련
        if (itemInfo.MovementSpeedPercent > 0)
        {
            tmpText += "이동속도 +" + itemInfo.MovementSpeedPercent + "%\n";
            plusCount++;
        }
        if (itemInfo.RootingRange > 0)
        {
            tmpText += "획득 범위 +" + itemInfo.RootingRange + "%\n";
            plusCount++;
        }
        if (itemInfo.Luck > 0)
        {
            tmpText += "행운 +" + itemInfo.Luck + '\n';
            plusCount++;
        }
        if (itemInfo.Harvest > 0)
        {
            tmpText += "수확 +" + itemInfo.Harvest + '\n';
            plusCount++;
        }
        if (itemInfo.ExpGain > 0)
        {
            tmpText += "경험치 획득 +" + itemInfo.ExpGain + "%\n";
            plusCount++;
        }

        // 긍정적 특수 효과
        if (itemInfo.positiveSpecial != "")
        {
            tmpText += itemInfo.positiveSpecial + "\n";
            plusCount++;
        }

        // 공격 관련
        if (itemInfo.DMGPercent < 0)
        {
            tmpText += "대미지 " + itemInfo.DMGPercent + "%\n";
            minusCount++;
        }
        if (itemInfo.ATKSpeed < 0)
        {
            tmpText += "공격속도 " + itemInfo.ATKSpeed + "%\n";
            minusCount++;
        }
        if (itemInfo.FixedDMG < 0)
        {
            tmpText += "고정 대미지 " + itemInfo.FixedDMG + '\n';
            minusCount++;
        }
        if (itemInfo.Critical < 0)
        {
            tmpText += "치명타 확률" + itemInfo.Critical + "%\n";
            minusCount++;
        }
        if (itemInfo.Range < 0)
        {
            tmpText += "범위 " + itemInfo.Range + "%\n";
            minusCount++;
        }

        // 방어 관련
        if (itemInfo.HP < 0)
        {
            tmpText += "최대 체력 " + itemInfo.HP + '\n';
            minusCount++;
        }
        if (itemInfo.Recovery < 0)
        {
            tmpText += "회복력 " + itemInfo.Recovery + '\n';
            minusCount++;
        }
        if (itemInfo.HPDrain < 0)
        {
            tmpText += "생명력 흡수% " + itemInfo.HPDrain + '\n';
            minusCount++;
        }
        if (itemInfo.Armor < 0)
        {
            tmpText += "방어력 " + itemInfo.Armor + '\n';
            minusCount++;
        }
        if (itemInfo.Evasion < 0)
        {
            tmpText += "회피 확률 " + itemInfo.Evasion + "%\n";
            minusCount++;
        }

        // 유틸 관련
        if (itemInfo.MovementSpeedPercent < 0)
        {
            tmpText += "이동속도 " + itemInfo.MovementSpeedPercent + "%\n";
            minusCount++;
        }
        if (itemInfo.RootingRange < 0)
        {
            tmpText += "획득 범위 " + itemInfo.RootingRange + "%\n";
            minusCount++;
        }
        if (itemInfo.Luck < 0)
        {
            tmpText += "행운 " + itemInfo.Luck + '\n';
            minusCount++;
        }
        if (itemInfo.Harvest < 0)
        {
            tmpText += "수확 " + itemInfo.Harvest + '\n';
            minusCount++;
        }
        if (itemInfo.ExpGain < 0)
        {
            tmpText += "경험치 획득 " + itemInfo.ExpGain + "%\n";
            minusCount++;
        }

        // 부정적 특수 효과
        if (itemInfo.negativeSpecial != "")
        {
            tmpText += itemInfo.negativeSpecial + "\n";
            minusCount++;
        }

        // 텍스트를 각 라인으로 나눈다
        string[] lines = tmpText.Split('\n');
        string finalText = ""; // 최종 텍스트

        // 능력치가 상승하면 텍스트를 초록색으로 변경
        for (int j = 0; j < plusCount; j++)
        {
            string coloredLine = "";
            // +와 숫자만 색을 변경한다
            for (int k = 0; k < lines[j].Length; k++)
            {
                // #1FDE38 << 진한 초록색
                if (lines[j][k] == '+' || lines[j][k] == '%' || lines[j][k] == '.')
                    coloredLine += $"<color=#1FDE38>{lines[j][k]}</color>";
                else if (lines[j][k] > 47 && lines[j][k] < 58)
                    coloredLine += $"<color=#1FDE38>{lines[j][k]}</color>";
                else
                    coloredLine += lines[j][k];
            }

            // 최종 텍스트에 추가
            finalText += coloredLine;
            finalText += "\n";
        }

        // 능력치가 하락하면 텍스트를 빨간색으로 변경
        for (int j = plusCount; j < plusCount + minusCount; j++)
        {
            string coloredLine = "";

            // -와 숫자만 색을 변경한다
            for (int k = 0; k < lines[j].Length; k++)
            {
                if (lines[j][k] == '-' || lines[j][k] == '%' || lines[j][k] == '.')
                    coloredLine += $"<color=#{ColorUtility.ToHtmlStringRGB(Color.red)}>{lines[j][k]}</color>";
                else if (lines[j][k] > 47 && lines[j][k] < 58)
                    coloredLine += $"<color=#{ColorUtility.ToHtmlStringRGB(Color.red)}>{lines[j][k]}</color>";
                else
                    coloredLine += lines[j][k];
            }

            // 최종 텍스트에 추가
            finalText += coloredLine;
            finalText += "\n";
        }

        // TextMeshProUGUI에 할당
        itemStatus.text = finalText;
    }

    private void SetUIThings(GameObject item)
    {
        ItemInfo itemInfo = item.GetComponent<ItemInfo>();

        itemGrade.color = DecideGradeColor(rarity);
        itemImage.sprite = item.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite;
        itemName.text = itemInfo.itemName;
        SetItemStatus(itemInfo);

        // RareItem32를 보유했다면 원가의 60%
        // 보유하지 않았다면 원가의 25%를 얻는다
        int sellPrice = ActivateRareItem32(itemInfo.price);

        sellButtonText.text = "판매 (+" + sellPrice.ToString() +")";
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
