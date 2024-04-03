using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinalListControl : MonoBehaviour
{
    // Ư��
    private Image individualityImage;
    private TextMeshProUGUI individualityNamePro;
    private TextMeshProUGUI individualityDetail;
    // ����
    private Image weaponImage;
    private TextMeshProUGUI weaponNamePro;
    private TextMeshProUGUI weaponDetailPro;
    // ���̵�
    private Image difficultyImage;
    private TextMeshProUGUI difficultyNamePro;
    private TextMeshProUGUI difficultyDetailPro;

    public Sprite pistol;

    private void Awake()
    {
        GameObject individualityInfoUI = this.transform.GetChild(0).gameObject;
        individualityImage = individualityInfoUI.transform.GetChild(1).GetComponent<Image>();
        individualityNamePro = individualityInfoUI.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        individualityDetail = individualityInfoUI.transform.GetChild(3).GetComponent<TextMeshProUGUI>();

        GameObject weaponInfoUI = this.transform.GetChild(1).gameObject;
        weaponImage = weaponInfoUI.transform.GetChild(1).GetChild(1).GetComponent<Image>();
        weaponNamePro = weaponInfoUI.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        weaponDetailPro = weaponInfoUI.transform.GetChild(3).GetComponent<TextMeshProUGUI>();

        GameObject difficultInfoUI = this.transform.GetChild(2).gameObject;
        difficultyImage = difficultInfoUI.transform.GetChild(1).GetComponent<Image>();
        difficultyNamePro = difficultInfoUI.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        difficultyDetailPro = difficultInfoUI.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        RenewDifficultyInfoUI("Easy");
    }

    void Update()
    {
        
    }

    public void RenewIndividualityInfoUI(string individualityName)
    {
        List<string> advantage = new();
        List<string> disadvantage = new();

        individualityNamePro.text = individualityName;

        switch (individualityName)
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
            default:
                break;
        }

        individualityDetail.text = PaintText(advantage, disadvantage);

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

    public void RenewWeaponInfoUI(string weaponName)
    {
        switch (weaponName)
        {
            case "Pistol":
                Texture2D pistolTexture = Resources.Load<Texture2D>("Sprites/Weapons/Pistol");
                Sprite pistol = Sprite.Create(pistolTexture, new Rect(0, 0, pistolTexture.width, pistolTexture.height), 
                                                             new Vector2(0.5f, 0.5f));
                weaponImage.sprite = pistol;
                weaponNamePro.text = "����";
                weaponDetailPro.text = "����� : " + 12 + '\n' +
                                       "���ݼӵ� : " + 0.83 + "/s \n" +
                                       "���� : " + 7;                
                break;
            case "Revolver":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/Revolver");
                weaponNamePro.text = "������";
                weaponDetailPro.text = "����� : " + 15 + '\n' +
                                       "���ݼӵ� : " + 2.32 + "/s \n" +
                                       "���� : " + 7.7 + '\n' +
                                       "6�� ��� �� 2.15�� ���� ������";
                break;
            default:
                break;
        }
    }

    public void RenewDifficultyInfoUI(string difficultyName)
    {
        switch (difficultyName)
        {
            case "Easy":
                // �̹��� ����

                difficultyNamePro.text = "����";
                difficultyDetailPro.text = "�� ü�� -20%\n" +
                                           "�� ����� -20%";
                break;
            case "Normal":
                break;
            default:
                break;
        }
    }
}
