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
        individualityImage.sprite = Resources.Load<Sprite>("Sprites/Individuality/" + individualityName);

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
            case "0222":
                advantage.Add("모든 스탯 +2");
                advantage.Add("모든 스탯 +2");
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
            case "권총":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/권총");
                weaponNamePro.text = "권총";
                weaponDetailPro.text = "대미지 : " + 12 + " (+100%)\n" +
                                       "공격속도 : " + 0.83 + "/s \n" +
                                       "넉백 : " + 15 + '\n' +
                                       "범위 : " + 7;
                break;
            case "리볼버":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/리볼버");
                weaponNamePro.text = "리볼버";
                weaponDetailPro.text = "대미지 : " + 15 + " (+100%)\n" +
                                       "공격속도 : " + 2.32 + "/s \n" +
                                       "넉백 : " + 15 + '\n' +
                                       "범위 : " + 7.7 + '\n' +
                                       "6발 사격 후 2.15초 동안 재장전";
                break;
            case "서브머신건":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/서브머신건");
                weaponNamePro.text = "서브머신건";
                weaponDetailPro.text = "대미지 : " + 3 + " (+50%)\n" +
                                       "공격속도 : " + 5.88 + "/s \n" +
                                       "넉백 : " + 0 + '\n' +
                                       "범위 : " + 7 + '\n';
                break;
            case "산탄총":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/산탄총");
                weaponNamePro.text = "샷건";
                weaponDetailPro.text = "대미지 : " + 3 + " (+80%)\n" +
                                       "공격속도 : " + 0.73 + "/s \n" +
                                       "넉백 : " + 8 + '\n' +
                                       "범위 : " + 6.2 + '\n' +
                                       "한번에 4발 발사";
                break;

            case "활":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/활");
                weaponNamePro.text = "활";
                weaponDetailPro.text = "대미지 : " + 10 + " (+80%)\n" +
                                       "공격속도 : " + 0.82 + "/s \n" +
                                       "넉백 : " + 5 + '\n' +
                                       "범위 : " + 6 + '\n' +
                                       "화살이 1회 튕깁니다";
                break;
            case "수리검":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/수리검");
                weaponNamePro.text = "수리검";
                weaponDetailPro.text = "대미지 : " + 7 + " (+58%)\n" +
                                       "공격속도 : " + 1.14 + "/s \n" +
                                       "넉백 : " + 0 + '\n' +
                                       "범위 : " + 7 + '\n' +
                                       "치명타 발생 시 1회 튕깁니다";
                break;
            // Melee Weapon
            case "고양이손":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/고양이손");
                weaponNamePro.text = "고양이손";
                weaponDetailPro.text = "대미지 : " + 8 + " (+200%)\n" +
                                       "공격속도 : " + 1.28 + "/s \n" +
                                       "넉백 : " + 15 + '\n' +
                                       "범위 : " + 3.8 + '\n';
                break;
            case "망치":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/망치");
                weaponNamePro.text = "망치";
                weaponDetailPro.text = "대미지 : " + 15 + " (+250%)\n" +
                                       "공격속도 : " + 0.57 + "/s \n" +
                                       "넉백 : " + 20 + '\n' +
                                       "범위 : " + 4.2 + '\n';
                break;
            case "솔":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/솔");
                weaponNamePro.text = "솔";
                weaponDetailPro.text = "대미지 : " + 1 + " (+100%)\n" +
                                       "공격속도 : " + 0.99 + "/s \n" +
                                       "넉백 : " + 30 + '\n' +
                                       "범위 : " + 3 + '\n' +
                                       "적 공격 시 3% 확률로 와플 드랍";
                break;
            case "방망이":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/방망이");
                weaponNamePro.text = "방망이";
                weaponDetailPro.text = "대미지 : " + 10 + " (+100%)\n" +
                                       "공격속도 : " + 0.70 + "/s \n" +
                                       "넉백 : " + 10 + '\n' +
                                       "범위 : " + 3.5 + '\n' +
                                       "레벨의 75%만큼 추가 대미지";
                break;
            case "사탕칼":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/사탕칼");
                weaponNamePro.text = "사탕칼";
                weaponDetailPro.text = "대미지 : " + 8 + " (+200%)\n" +
                                       "공격속도 : " + 0.8 + "/s \n" +
                                       "넉백 : " + 0 + '\n' +
                                       "범위 : " + 3.5 + '\n' +
                                       "일정 횟수 공격하면 부러집니다";
                break;
            case "화도일문자":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/화도일문자");
                weaponNamePro.text = "화도일문자";
                weaponDetailPro.text = "대미지 : " + 20 + " (+200%)\n" +
                                       "공격속도 : " + 0.69 + "/s \n" +
                                       "넉백 : " + 0 + '\n' +
                                       "범위 : " + 4 + '\n' +
                                       "휘두르기와 찌르기를 반복합니다";
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
                difficultyNamePro.text = "쉬움";
                difficultyDetailPro.text = "적 체력 -20%\n" +
                                           "적 대미지 -20%";
                break;
            case "Normal":
                difficultyImage.sprite = Resources.Load<Sprite>("Sprites/Difficulty/Normal");
                difficultyNamePro.text = "보통";
                difficultyDetailPro.text = "표준 난이도\n" +
                                           "변경 사항 X";
                break;
            case "Hard":
                difficultyImage.sprite = Resources.Load<Sprite>("Sprites/Difficulty/Hard");
                difficultyNamePro.text = "어려움";
                difficultyDetailPro.text = "새로운 적 추가\n" +
                                           "적 대미지 +10%\n" + 
                                           "적 체력 +10%";
                break;
            case "VeryHard":
                difficultyImage.sprite = Resources.Load<Sprite>("Sprites/Difficulty/VeryHard");
                difficultyNamePro.text = "매우어려움";
                difficultyDetailPro.text = "낮은 확률로 강화된 적 등장\n" +
                                           "새로운 적 추가\n" +
                                           "적 대미지 +25%\n" +
                                           "적 체력 +25%";
                break;
            case "Hell":
                difficultyImage.sprite = Resources.Load<Sprite>("Sprites/Difficulty/Hell");
                difficultyNamePro.text = "지옥";
                difficultyDetailPro.text = "낮은 확률로 강화된 적 등장\n" +
                                           "새로운 적 추가\n" +
                                           "적 대미지 +40%\n" +
                                           "적 체력 +40%";
                break;
            default:
                break;
        }
    }
}
