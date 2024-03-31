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
            case "명사수":
                advantage.Add("치명타 확률 +10%");
                advantage.Add("범위 +10%");
                advantage.Add("치명타 확률 증감폭 1.3배");

                disadvantage.Add("수확 증감폭 0배");

                detailTextPro.text = PaintText(advantage, disadvantage);
                break;
            default:
                break;
        }

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
