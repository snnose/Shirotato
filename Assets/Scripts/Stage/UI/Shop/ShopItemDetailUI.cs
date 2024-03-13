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
        // 아이템 이름 변경
        itemNameText.text = itemInfo.itemName;

        itemStatusText.text = "";

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
            tmpText += "치명타 +" + itemInfo.Critical + "%\n";
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
            tmpText += "체력 회복 +" + itemInfo.Recovery + '\n';
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
            tmpText += "회피 +" + itemInfo.Evasion + "%\n";
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
            tmpText += "경험치 획득 " + itemInfo.ExpGain + '\n';
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
            tmpText += "치명타 " + itemInfo.Critical + "%\n";
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
            tmpText += "체력 회복 " + itemInfo.Recovery + '\n';
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
            tmpText += "회피 " + itemInfo.Evasion + "%\n";
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
            tmpText += "경험치 획득 " + itemInfo.ExpGain + '\n';
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
                if (lines[j][k] == '+' || lines[j][k] == '%')
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
                if (lines[j][k] == '-' || lines[j][k] == '%')
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
