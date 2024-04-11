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
        // UI�� ��µǴ� Ư�� �̸�
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
            case "����":
                advantage.Add("ġ��Ÿ Ȯ�� +10%");
                advantage.Add("���� +10%");
                advantage.Add("ġ��Ÿ Ȯ�� ������ 1.3��");

                disadvantage.Add("��Ȯ ������ 0��");
                break;
            case "��ٴٴ�":
                advantage.Add("���ݼӵ� +100%");
                advantage.Add("�̵��ӵ� +10%");
                advantage.Add("����� ������ 1.4��");

                disadvantage.Add("����� -45%");
                disadvantage.Add("���� -5");
                break;
            case "������":
                advantage.Add("��� +100");
                advantage.Add("��Ȯ +10");
                advantage.Add("��� ������ 1.25��");

                disadvantage.Add("����� -50%");
                disadvantage.Add("����ġ ȹ�� -50%");
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
        string finalText = ""; // ���� �ؽ�Ʈ

        // ������ Ư�� �ؽ�Ʈ�� �ʷϻ����� ����
        for (int i = 0; i < advantage.Count; i++)
        {
            string coloredLine = "";
            // '+', '%', '.'�� ���ڸ� ���� �����Ѵ�
            for (int j = 0; j < advantage[i].Length; j++)
            {
                // #1FDE38 << ���� �ʷϻ�
                if (advantage[i][j] == '+' || advantage[i][j] == '%' || advantage[i][j] == '.')
                    coloredLine += $"<color=#1FDE38>{advantage[i][j]}</color>";
                else if (advantage[i][j] > 47 && advantage[i][j] < 58)
                    coloredLine += $"<color=#1FDE38>{advantage[i][j]}</color>";
                else
                    coloredLine += advantage[i][j];
            }

            // ���� �ؽ�Ʈ�� �߰�
            finalText += coloredLine;
            finalText += "\n";
        }

        // �������� Ư�� �ؽ�Ʈ�� ���������� ����
        for (int i = 0; i < disadvantage.Count; i++)
        {
            string coloredLine = "";
            // '-', '%', '.'�� ���ڸ� ���� �����Ѵ�
            for (int j = 0; j < disadvantage[i].Length; j++)
            {
                if (disadvantage[i][j] == '-' || disadvantage[i][j] == '%' || disadvantage[i][j] == '.')
                    coloredLine += $"<color=#{ColorUtility.ToHtmlStringRGB(Color.red)}>{disadvantage[i][j]}</color>";
                else if (disadvantage[i][j] > 47 && disadvantage[i][j] < 58)
                    coloredLine += $"<color=#{ColorUtility.ToHtmlStringRGB(Color.red)}>{disadvantage[i][j]}</color>";
                else
                    coloredLine += disadvantage[i][j];
            }

            // ���� �ؽ�Ʈ�� �߰�
            finalText += coloredLine;
            finalText += "\n";
        }

        return finalText;
    }
}
