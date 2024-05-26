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
                advantage.Add("����� ������ 1.5��");

                disadvantage.Add("����� -40%");
                disadvantage.Add("���� -5");
                break;
            case "������":
                advantage.Add("���� ȹ�� �� 75% Ȯ���� ���� ������ ����� 25% ����");
                advantage.Add("��� +100");
                advantage.Add("��Ȯ +10");
                advantage.Add("��� ������ 1.25��");

                disadvantage.Add("���ݼӵ� -60%");
                disadvantage.Add("����ġ ȹ�� -50%");
                break;
            case "0222":
                advantage.Add("��� ���� +2");
                advantage.Add("��� ���� +2");
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
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/Pistol");
                weaponNamePro.text = "����";
                weaponDetailPro.text = "����� : " + 12 + " (+100%)\n" +
                                       "���ݼӵ� : " + 0.83 + "/s \n" +
                                       "�˹� : " + 15 + '\n' +
                                       "���� : " + 7;
                break;
            case "Revolver":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/Revolver");
                weaponNamePro.text = "������";
                weaponDetailPro.text = "����� : " + 15 + " (+100%)\n" +
                                       "���ݼӵ� : " + 2.32 + "/s \n" +
                                       "�˹� : " + 15 + '\n' +
                                       "���� : " + 7.7 + '\n' +
                                       "6�� ��� �� 2.15�� ���� ������";
                break;
            case "SMG":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/SMG");
                weaponNamePro.text = "����ӽŰ�";
                weaponDetailPro.text = "����� : " + 3 + " (+50%)\n" +
                                       "���ݼӵ� : " + 5.88 + "/s \n" +
                                       "�˹� : " + 0 + '\n' +
                                       "���� : " + 7 + '\n';
                break;
            case "Shotgun":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/Shotgun");
                weaponNamePro.text = "����";
                weaponDetailPro.text = "����� : " + 3 + " (+80%)\n" +
                                       "���ݼӵ� : " + 0.73 + "/s \n" +
                                       "�˹� : " + 8 + '\n' +
                                       "���� : " + 6.2 + '\n' +
                                       "�ѹ��� 4�� �߻�";
                break;
            case "Bow":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/Bow");
                weaponNamePro.text = "Ȱ";
                weaponDetailPro.text = "����� : " + 10 + " (+80%)\n" +
                                       "���ݼӵ� : " + 0.82 + "/s \n" +
                                       "�˹� : " + 5 + '\n' +
                                       "���� : " + 6 + '\n' +
                                       "ȭ���� 1ȸ ƨ��ϴ�";
                break;
            // Melee Weapon
            case "Nekohand":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/Nekohand");
                weaponNamePro.text = "����̼�";
                weaponDetailPro.text = "����� : " + 8 + " (+200%)\n" +
                                       "���ݼӵ� : " + 1.28 + "/s \n" +
                                       "�˹� : " + 15 + '\n' +
                                       "���� : " + 3.8 + '\n';
                break;
            case "Hammer":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/Hammer");
                weaponNamePro.text = "��ġ";
                weaponDetailPro.text = "����� : " + 15 + " (+250%)\n" +
                                       "���ݼӵ� : " + 0.57 + "/s \n" +
                                       "�˹� : " + 20 + '\n' +
                                       "���� : " + 4.2 + '\n';
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
                difficultyImage.sprite = Resources.Load<Sprite>("Sprites/Difficulty/Easy");
                difficultyNamePro.text = "����";
                difficultyDetailPro.text = "�� ü�� -20%\n" +
                                           "�� ����� -20%";
                break;
            case "Normal":
                difficultyImage.sprite = Resources.Load<Sprite>("Sprites/Difficulty/Normal");
                difficultyNamePro.text = "����";
                difficultyDetailPro.text = "ǥ�� ���̵�\n" +
                                           "���� ���� X";
                break;
            case "Hard":
                difficultyImage.sprite = Resources.Load<Sprite>("Sprites/Difficulty/Hard");
                difficultyNamePro.text = "�����";
                difficultyDetailPro.text = "���ο� �� �߰�\n" +
                                           "�� ����� +10%\n" + 
                                           "�� ü�� +10%";
                break;
            case "VeryHard":
                difficultyImage.sprite = Resources.Load<Sprite>("Sprites/Difficulty/VeryHard");
                difficultyNamePro.text = "�ſ�����";
                difficultyDetailPro.text = "���� Ȯ���� Ư�� �ɷ��� �߰��� �� ����(�̿�)\n" +
                                           "���ο� �� �߰�\n" +
                                           "�� ����� +25%\n" +
                                           "�� ü�� +25%";
                break;
            case "Hell":
                difficultyImage.sprite = Resources.Load<Sprite>("Sprites/Difficulty/Hell");
                difficultyNamePro.text = "����";
                difficultyDetailPro.text = "���� 2���� (�̿�)\n" +
                                           "���� Ȯ���� Ư�� �ɷ��� �߰��� �� ����(�̿�)\n" +
                                           "���ο� �� �߰�\n" +
                                           "�� ����� +40%\n" +
                                           "�� ü�� +40%";
                break;
            default:
                break;
        }
    }
}
