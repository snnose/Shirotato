using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IndividualityDetailControl : MonoBehaviour
{
    private string individuality = "";

    TextMeshProUGUI detailTextPro;

    private void Awake()
    {
        detailTextPro = this.GetComponent<TextMeshProUGUI>();
        // UI에 출력되는 특성 이름
        individuality = this.transform.parent.GetChild(2).GetComponent<TextMeshProUGUI>().text;
    }

    void Start()
    {
        SetDetailText();
    }

    private void SetDetailText()
    {
        List<string> advantage = new();
        List<string> disadvantage = new();

        switch (individuality)
        {
            case "0222":
                advantage.Add("모든 스탯 +2");
                advantage.Add("모든 스탯 +2");
                break;
            case "명사수":
                advantage.Add("치명타 확률 +10%");
                advantage.Add("범위 +10%");
                advantage.Add("치명타 확률 증감폭 1.3배");

                disadvantage.Add("수확 증감폭 0배");
                break;
            case "우다다다":
                advantage.Add("공격속도 +100%");
                advantage.Add("이동속도 +15%");
                advantage.Add("대미지 증감폭 1.5배");

                disadvantage.Add("대미지 -40%");
                disadvantage.Add("방어력 -5");
                break;
            case "행운냥이":
                advantage.Add("와플 획득 시 75% 확률로 랜덤 적에게 행운의 25% 피해");
                advantage.Add("행운 +100");
                advantage.Add("수확 +5");
                advantage.Add("행운 증감폭 1.25배");

                disadvantage.Add("공격속도 -60%");
                disadvantage.Add("경험치 획득 -50%");
                break;
            case "불굴":
                advantage.Add("라운드 중 매초 대미지가 3% 상승합니다");
                advantage.Add("최대 체력 +15");
                advantage.Add("회복력 +10");
                advantage.Add("방어력 +5");

                disadvantage.Add("대미지 -100%");
                break;
            default:
                break;
        }

        detailTextPro.text = PaintText(advantage, disadvantage);

        advantage.Clear();
        disadvantage.Clear();
    }

    private string PaintText(List<string> advantage, List<string> disadvantage)
    {
        string finalText = ""; // 최종 텍스트

        // 이점은 특정 텍스트를 초록색으로 변경
        for (int i = 0; i < advantage.Count; i++)
        {
            string coloredLine = "";
            // '+', '%', '.'와 숫자만 색을 변경한다
            for (int j = 0; j < advantage[i].Length; j++)
            {
                // #1FDE38 << 진한 초록색
                if (advantage[i][j] == '+' || advantage[i][j] == '%' || advantage[i][j] == '.')
                    coloredLine += $"<color=#1FDE38>{advantage[i][j]}</color>";
                else if (advantage[i][j] > 47 && advantage[i][j] < 58)
                    coloredLine += $"<color=#1FDE38>{advantage[i][j]}</color>";
                else
                    coloredLine += advantage[i][j];
            }

            // 최종 텍스트에 추가
            finalText += coloredLine;
            finalText += "\n";
        }

        // 불이익은 특정 텍스트를 빨간색으로 변경
        for (int i = 0; i < disadvantage.Count; i++)
        {
            string coloredLine = "";
            // '-', '%', '.'와 숫자만 색을 변경한다
            for (int j = 0; j < disadvantage[i].Length; j++)
            {
                if (disadvantage[i][j] == '-' || disadvantage[i][j] == '%' || disadvantage[i][j] == '.')
                    coloredLine += $"<color=#{ColorUtility.ToHtmlStringRGB(Color.red)}>{disadvantage[i][j]}</color>";
                else if (disadvantage[i][j] > 47 && disadvantage[i][j] < 58)
                    coloredLine += $"<color=#{ColorUtility.ToHtmlStringRGB(Color.red)}>{disadvantage[i][j]}</color>";
                else
                    coloredLine += disadvantage[i][j];
            }

            // 최종 텍스트에 추가
            finalText += coloredLine;
            finalText += "\n";
        }

        return finalText;
    }
}
