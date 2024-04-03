using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinalListControl : MonoBehaviour
{
    // 특성
    private Image individualityImage;
    private TextMeshProUGUI individualityNamePro;
    private TextMeshProUGUI individualityDetail;
    // 무기
    private Image weaponImage;
    private TextMeshProUGUI weaponNamePro;
    private TextMeshProUGUI weaponDetailPro;
    // 난이도
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
            case "명사수":
                advantage.Add("치명타 확률 +10%");
                advantage.Add("범위 +10%");
                advantage.Add("치명타 확률 증감폭 1.3배");

                disadvantage.Add("수확 증감폭 0배");
                break;
            case "우다다다":
                advantage.Add("공격속도 +100%");
                advantage.Add("이동속도 +10%");
                advantage.Add("대미지 증감폭 1.4배");

                disadvantage.Add("대미지 -45%");
                disadvantage.Add("방어력 -5");
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

    public void RenewWeaponInfoUI(string weaponName)
    {
        switch (weaponName)
        {
            case "Pistol":
                Texture2D pistolTexture = Resources.Load<Texture2D>("Sprites/Weapons/Pistol");
                Sprite pistol = Sprite.Create(pistolTexture, new Rect(0, 0, pistolTexture.width, pistolTexture.height), 
                                                             new Vector2(0.5f, 0.5f));
                weaponImage.sprite = pistol;
                weaponNamePro.text = "권총";
                weaponDetailPro.text = "대미지 : " + 12 + '\n' +
                                       "공격속도 : " + 0.83 + "/s \n" +
                                       "범위 : " + 7;                
                break;
            case "Revolver":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/Revolver");
                weaponNamePro.text = "리볼버";
                weaponDetailPro.text = "대미지 : " + 15 + '\n' +
                                       "공격속도 : " + 2.32 + "/s \n" +
                                       "범위 : " + 7.7 + '\n' +
                                       "6발 사격 후 2.15초 동안 재장전";
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
                // 이미지 설정

                difficultyNamePro.text = "쉬움";
                difficultyDetailPro.text = "적 체력 -20%\n" +
                                           "적 대미지 -20%";
                break;
            case "Normal":
                break;
            default:
                break;
        }
    }
}
