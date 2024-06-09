using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IllustGuideWeaponButton : MonoBehaviour, IPointerEnterHandler
{
    Button weaponButton;
    public int rarity = 0;

    private void Awake()
    {
        weaponButton = this.GetComponent<Button>();
        weaponButton.onClick.AddListener(OnClickWeaponButton);
    }

    void Start()
    {
        rarity = DecideRarity(this.transform.parent.parent.parent.parent.name);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 포인터 진입 음성 출력
        ButtonSoundManager.Instance.PlayOnPointerEnterSound2();
    }

    void OnClickWeaponButton()
    {
        // 버튼 클릭 음성 출력
        ButtonSoundManager.Instance.PlayOnClickButtonSound2();

        // 선택된 아이템의 등급, 이미지, 이름, 설명으로 교체
        IllustGuideSelectedDetail.Instance.selectedImage.transform.GetChild(0).GetComponent<Image>().color =
            this.transform.GetChild(0).GetComponent<Image>().color;

        IllustGuideSelectedDetail.Instance.selectedImage.transform.GetChild(1).GetComponent<Image>().sprite =
            this.transform.GetChild(1).GetComponent<Image>().sprite;

        IllustGuideSelectedDetail.Instance.selectedNameText.text = this.transform.GetChild(1).GetComponent<Image>().sprite.name;

        IllustGuideSelectedDetail.Instance.selectedDetailText.text = 
        SetWeaponDetailText(this.transform.GetChild(1).GetComponent<Image>().sprite.name, rarity);

        string comment = GetWeaponComment(this.transform.GetChild(1).GetComponent<Image>().sprite.name);

        IllustGuideComment.Instance.SetCommentText(comment);
    }

    // 무기 이름과 레어도에 따라 출력되는 세부 사항 결정
    string SetWeaponDetailText(string weaponName, int rarity)
    {
        string finalText = "";

        switch (weaponName)
        {
            // 권총
            case "권총":
                switch (rarity)
                {
                    case 0:
                        finalText += "대미지 : 12 (+100%)\n" +
                                     "공격속도 : 0.83/s\n" +
                                     "넉백 : 15\n" +
                                     "범위 : 7";
                        
                        //this.price = 10;
                        break;
                    case 1:
                        finalText += "대미지 : 20 (+100%)\n" +
                                     "공격속도 : 0.89/s\n" +
                                     "넉백 : 15\n" +
                                     "범위 : 7";
                        
                        //this.price = 22;
                        break;
                    case 2:
                        finalText += "대미지 : 30 (+100%)\n" +
                                     "공격속도 : 0.97/s\n" +
                                     "넉백 : 15\n" +
                                     "범위 : 7";

                        //this.price = 45;
                        break;
                    case 3:
                        finalText += "대미지 : 50 (+100%)\n" +
                                     "공격속도 : 1.14/s\n" +
                                     "넉백 : 15\n" +
                                     "범위 : 7";

                        //this.price = 91;
                        break;
                    default:
                        break;
                }

                break;
            // 리볼버
            case "리볼버":
                switch (rarity)
                {
                    case 0:
                        finalText += "대미지 : 15 (+100%)\n" +
                                     "공격속도 : 2.32/s\n" +
                                     "넉백 : 15\n" +
                                     "범위 : 7.7\n" +
                                     "6발 사격 후 2.15초 간 재장전";
                        
                        //this.price = 20;
                        break;
                    case 1:
                        finalText += "대미지 : 20 (+130%)\n" +
                                     "공격속도 : 2.38/s\n" +
                                     "넉백 : 15\n" +
                                     "범위 : 7.7\n" +
                                     "6발 사격 후 2.1초 간 재장전";
                        
                        //this.price = 34;
                        break;
                    case 2:
                        finalText += "대미지 : 25 (+165%)\n" +
                                     "공격속도 : 2.5/s\n" +
                                     "넉백 : 15\n" +
                                     "범위 : 7.7\n" +
                                     "6발 사격 후 2초 간 재장전";
                        
                        //this.price = 70;
                        break;
                    case 3:
                        finalText += "대미지 : 40 (+200%)\n" +
                                     "공격속도 : 2.63/s\n" +
                                     "넉백 : 15\n" +
                                     "범위 : 7.7\n" +
                                     "6발 사격 후 1.9초 간 재장전";
                        
                        //this.price = 130;
                        break;
                    default:
                        break;
                }

                break;

            case "서브머신건":
                switch (rarity)
                {
                    case 0:
                        finalText += "대미지 : 3 (+50%)\n" +
                                     "공격속도 : 5.88/s\n" +
                                     "넉백 : 0\n" +
                                     "범위 : 7\n";
                        
                        //this.price = 20;
                        break;
                    case 1:
                        finalText += "대미지 : 4 (+60%)\n" +
                                     "공격속도 : 5.88/s\n" +
                                     "넉백 : 0\n" +
                                     "범위 : 7\n";
                        
                        //this.price = 39;
                        break;
                    case 2:
                        finalText += "대미지 : 5 (+70%)\n" +
                                     "공격속도 : 5.88/s\n" +
                                     "넉백 : 0\n" +
                                     "범위 : 7\n";
                        
                        //this.price = 74;
                        break;
                    case 3:
                        finalText += "대미지 : 8 (+80%)\n" +
                                     "공격속도 : 6.66/s\n" +
                                     "넉백 : 0\n" +
                                     "범위 : 7\n";
                        
                        //this.price = 149;
                        break;
                }
                break;

            case "산탄총":
                switch (rarity)
                {
                    case 0:
                        finalText += "대미지 : 3 (+80%)\n" +
                                     "공격속도 : 0.72/s\n" +
                                     "넉백 : 8\n" +
                                     "범위 : 6.2\n" +
                                     "한번에 4발 발사";
                        
                        //this.price = 20;
                        break;
                    case 1:
                        finalText += "대미지 : 6 (+85%)\n" +
                                     "공격속도 : 0.78/s\n" +
                                     "넉백 : 8\n" +
                                     "범위 : 6.2\n" +
                                     "한번에 4발 발사";
                        
                        //this.price = 39;
                        break;
                    case 2:
                        finalText += "대미지 : 9 (+90%)\n" +
                                     "공격속도 : 0.83/s\n" +
                                     "넉백 : 8\n" +
                                     "범위 : 6.2\n" +
                                     "한번에 4발 발사";
                        
                        //this.price = 74;
                        break;
                    case 3:
                        finalText += "대미지 : 9 (+100%)\n" +
                                     "공격속도 : 0.83/s\n" +
                                     "넉백 : 8\n" +
                                     "범위 : 6.2\n" +
                                     "한번에 6발 발사";
                        
                        //this.price = 149;
                        break;
                }
                break;

            case "활":
                switch (rarity)
                {
                    case 0:
                        finalText += "대미지 : 10 (+80%)\n" +
                                     "공격속도 : 0.81/s\n" +
                                     "넉백 : 5\n" +
                                     "범위 : 6\n" +
                                     "화살이 1회 튕깁니다";
                        
                        //this.price = 15;
                        break;
                    case 1:
                        finalText += "대미지 : 13 (+80%)\n" +
                                     "공격속도 : 0.85/s\n" +
                                     "넉백 : 5\n" +
                                     "범위 : 6\n" +
                                     "화살이 2회 튕깁니다";
                        
                        //this.price = 31;
                        break;
                    case 2:
                        finalText += "대미지 : 16 (+80%)\n" +
                                     "공격속도 : 0.88/s\n" +
                                     "넉백 : 5\n" +
                                     "범위 : 6\n" +
                                     "화살이 3회 튕깁니다";
                        
                        //this.price = 61;
                        break;
                    case 3:
                        finalText += "대미지 : 20 (+80%)\n" +
                                     "공격속도 : 0.90/s\n" +
                                     "넉백 : 5\n" +
                                     "범위 : 6\n" +
                                     "화살이 4회 튕깁니다";
                        
                        //this.price = 122;
                        break;
                }
                break;
            case "수리검":
                switch (rarity)
                {
                    case 0:
                        finalText += "대미지 : 7 (+59%)\n" +
                                     "공격속도 : 1.14/s\n" +
                                     "넉백 : 0\n" +
                                     "범위 : 7\n" +
                                     "치명타 발생 시 1회 튕깁니다";

                        //this.price = 12;
                        break;
                    case 1:
                        finalText += "대미지 : 9 (+81%)\n" +
                                     "공격속도 : 1.20/s\n" +
                                     "넉백 : 0\n" +
                                     "범위 : 7\n" +
                                     "치명타 발생 시 2회 튕깁니다";

                        //this.price = 26;
                        break;
                    case 2:
                        finalText += "대미지 : 12 (+100%)\n" +
                                     "공격속도 : 1.25/s\n" +
                                     "넉백 : 0\n" +
                                     "범위 : 7\n" +
                                     "치명타 발생 시 3회 튕깁니다";

                        //this.price = 52;
                        break;
                    case 3:
                        finalText += "대미지 : 18 (+122%)\n" +
                                     "공격속도 : 1.42/s\n" +
                                     "넉백 : 0\n" +
                                     "범위 : 7\n" +
                                     "치명타 발생 시 4회 튕깁니다";

                        //this.price = 105;
                        break;
                }
                break;

            // 원거리 특수
            case "매그넘":
                switch (rarity)
                {
                    case 2:
                        finalText += "대미지 : 116 (+450%)\n" +
                                     "공격속도 : 0.66/s\n" +
                                     "넉백 : 10\n" +
                                     "범위 : 10\n" +
                                     "무조건 관통";
                        break;
                    case 3:
                        finalText += "대미지 : 162 (+670%)\n" +
                                     "공격속도 : 0.66/s\n" +
                                     "넉백 : 10\n" +
                                     "범위 : 10\n" +
                                     "무조건 관통";
                        break;
                    default:
                        break;
                }
                break;

            // Melee Weapon
            case "고양이손":
                switch (rarity)
                {

                    case 0:
                        finalText += "대미지 : 8 (+200%)\n" +
                                     "공격속도 : 1.28/s\n" +
                                     "넉백 : 15\n" +
                                     "범위 : 3.8\n";
                        
                        //this.price = 10;
                        break;
                    case 1:
                        finalText += "대미지 : 16 (+200%)\n" +
                                     "공격속도 : 1.37/s\n" +
                                     "넉백 : 15\n" +
                                     "범위 : 3.8\n";
                        
                        //this.price = 22;
                        break;
                    case 2:
                        finalText += "대미지 : 32 (+200%)\n" +
                                     "공격속도 : 1.45/s\n" +
                                     "넉백 : 15\n" +
                                     "범위 : 3.8\n";
                        
                        //this.price = 45;
                        break;
                    case 3:
                        finalText += "대미지 : 64 (+200%)\n" +
                                     "공격속도 : 1.69/s\n" +
                                     "넉백 : 15\n" +
                                     "범위 : 3.8\n";
                        
                        //this.price = 91;
                        break;
                    default:
                        break;
                }
                break;

            case "망치":
                switch (rarity)
                {
                    case 0:
                        finalText += "대미지 : 15 (+250%)\n" +
                                     "공격속도 : 0.57/s\n" +
                                     "넉백 : 20\n" +
                                     "범위 : 4.2\n";
                        
                        //this.price = 25;
                        break;
                    case 1:
                        finalText += "대미지 : 30 (+300%)\n" +
                                     "공격속도 : 0.60/s\n" +
                                     "넉백 : 25\n" +
                                     "범위 : 4.2\n";
                        
                        //this.price = 51;
                        break;
                    case 2:
                        finalText += "대미지 : 60 (+350%)\n" +
                                     "공격속도 : 0.62/s\n" +
                                     "넉백 : 35\n" +
                                     "범위 : 4.2\n";
                        
                        //this.price = 95;
                        break;
                    case 3:
                        finalText += "대미지 : 100 (+400%)\n" +
                                     "공격속도 : 0.66/s\n" +
                                     "넉백 : 40\n" +
                                     "범위 : 4.2\n";
                        
                        //this.price = 190;
                        break;
                    default:
                        break;
                }
                break;

            case "솔":
                switch (rarity)
                {
                    case 0:
                        finalText += "대미지 : 1 (+100%)\n" +
                                     "공격속도 : 0.99/s\n" +
                                     "넉백 : 30\n" +
                                     "범위 : 3\n" +
                                     "적 공격 시 1% 확률로 와플 드랍";

                        //this.price = 10;
                        break;
                    case 1:
                        finalText += "대미지 : 1 (+100%)\n" +
                                     "공격속도 : 1.07/s\n" +
                                     "넉백 : 30\n" +
                                     "범위 : 3\n" +
                                     "적 공격 시 2% 확률로 와플 드랍";

                        //this.price = 22;
                        break;
                    case 2:
                        finalText += "대미지 : 1 (+100%)\n" +
                                     "공격속도 : 1.16/s\n" +
                                     "넉백 : 30\n" +
                                     "범위 : 3\n" +
                                     "적 공격 시 3% 확률로 와플 드랍";

                        //this.price = 45;
                        break;
                    case 3:
                        finalText += "대미지 : 1 (+100%)\n" +
                                     "공격속도 : 1.40/s\n" +
                                     "넉백 : 30\n" +
                                     "범위 : 3\n" +
                                     "적 공격 시 5% 확률로 와플 드랍";

                        //this.price = 91;
                        break;
                    default:
                        break;
                }
                break;

            case "방망이":
                switch (rarity)
                {
                    case 0:
                        finalText += "대미지 : 10 (+100%)\n" +
                                     "공격속도 : 0.70/s\n" +
                                     "넉백 : 10\n" +
                                     "범위 : 3.5\n" +
                                     "레벨의 75%만큼 추가 대미지";

                        //this.price = 17;
                        break;
                    case 1:
                        finalText += "대미지 : 15 (+130%)\n" +
                                     "공격속도 : 0.8/s\n" +
                                     "넉백 : 10\n" +
                                     "범위 : 3.5\n" +
                                     "레벨의 85%만큼 추가 대미지";

                        //this.price = 34;
                        break;
                    case 2:
                        finalText += "대미지 : 20 (+170%)\n" +
                                     "공격속도 : 0.91/s\n" +
                                     "넉백 : 10\n" +
                                     "범위 : 3.5\n" +
                                     "레벨의 100%만큼 추가 대미지";

                        //this.price = 66;
                        break;
                    case 3:
                        finalText += "대미지 : 30 (+200%)\n" +
                                     "공격속도 : 1.08/s\n" +
                                     "넉백 : 10\n" +
                                     "범위 : 3.5\n" +
                                     "레벨의 125%만큼 추가 대미지";

                        //this.price = 130;
                        break;
                    default:
                        break;
                }
                break;

            case "사탕칼":
                switch (rarity)
                {
                    case 0:
                        finalText += "대미지 : 8 (+200%)\n" +
                                     "공격속도 : 0.80/s\n" +
                                     "넉백 : 0\n" +
                                     "범위 : 3.5\n" +
                                     "일정 횟수 공격하면 부러집니다";

                        //this.price = 10;
                        break;
                    case 1:
                        finalText += "대미지 : 16 (+225%)\n" +
                                     "공격속도 : 0.85/s\n" +
                                     "넉백 : 0\n" +
                                     "범위 : 3.5\n" +
                                     "일정 횟수 공격하면 부러집니다";

                        //this.price = 22;
                        break;
                    case 2:
                        finalText += "대미지 : 24 (+250%)\n" +
                                     "공격속도 : 0.90/s\n" +
                                     "넉백 : 0\n" +
                                     "범위 : 3.5\n" +
                                     "일정 횟수 공격하면 부러집니다";

                        //this.price = 45;
                        break;
                    case 3:
                        finalText += "대미지 : 40 (+300%)\n" +
                                     "공격속도 : 0.97/s\n" +
                                     "넉백 : 0\n" +
                                     "범위 : 3.5\n" +
                                     "일정 횟수 공격하면 부러집니다";

                        //this.price = 91;
                        break;
                    default:
                        break;
                }
                break;

            case "화도일문자":
                switch (rarity)
                {
                    case 0:
                        finalText += "대미지 : 20 (+200%)\n" +
                                     "공격속도 : 0.69/s\n" +
                                     "넉백 : 5\n" +
                                     "범위 : 4\n" +
                                     "휘두르기와 찌르기를 반복합니다";

                        //this.price = 25;
                        break;
                    case 1:
                        finalText += "대미지 : 25 (+200%)\n" +
                                     "공격속도 : 0.78/s\n" +
                                     "넉백 : 5\n" +
                                     "범위 : 4\n" +
                                     "휘두르기와 찌르기를 반복합니다";

                        //this.price = 51;
                        break;
                    case 2:
                        finalText += "대미지 : 40 (+200%)\n" +
                                     "공격속도 : 0.88/s\n" +
                                     "넉백 : 5\n" +
                                     "범위 : 4\n" +
                                     "휘두르기와 찌르기를 반복합니다";

                        //this.price = 95;
                        break;
                    case 3:
                        finalText += "대미지 : 60 (+200%)\n" +
                                     "공격속도 : 1.02/s\n" +
                                     "넉백 : 5\n" +
                                     "범위 : 4\n" +
                                     "휘두르기와 찌르기를 반복합니다";

                        //this.price = 190;
                        break;
                    default:
                        break;
                }
                break;

            // 근거리 특수
            case "철쇄아":
                switch (rarity)
                {
                    case 2:
                        finalText += "대미지 : 30 (+240%)\n" +
                                     "공격속도 : 0.69/s\n" +
                                     "넉백 : 0\n" +
                                     "범위 : 4\n" +
                                     "바람의 상처!";
                        break;
                    case 3:
                        finalText += "대미지 : 55 (+270%)\n" +
                                     "공격속도 : 0.76/s\n" +
                                     "넉백 : 0\n" +
                                     "범위 : 4\n" +
                                     "바람의 상처!";
                        break;
                    default:
                        break;
                }
                break;

            default:
                break;
        }

        return finalText;
    }

    // rank에 따라 랭크 색깔을 반환하는 함수 (흰, 파, 보, 주)
    private int DecideRarity(string listName)
    {
        // 기본 흰색
        int rarity = 0;

        switch (listName)
        {
            case "RareWeaponList":
                rarity = 1;
                break;
            case "EpicWeaponList":
                rarity = 2;
                break;
            case "LegendWeaponList":
                rarity = 3;
                break;
            default:
                rarity = 0;
                break;
        }

        return rarity;
    }

    string GetWeaponComment(string WeaponName)
    {
        string comment = "";

        switch (WeaponName)
        {
            case "고양이손":
                comment = "\n\n푹신푹신 냥냥펀치";
                break;

            case "권총":
                comment = "";
                break;

            case "리볼버":
                comment = "";
                break;

            case "망치":
                comment = "\n\n안마용으로 좋습니다";
                break;

            case "방망이":
                comment = "\n\n좌측담장~";
                break;

            case "사탕칼":
                comment = "\n\n";
                break;

            case "산탄총":
                comment = "\n\n사탕총";
                break;

            case "서브머신건":
                comment = "";
                break;

            case "솔":
                comment = "띵타\n\n" +
                          "??? : ㅈ댐 시로옴";
                break;

            case "수리검":
                comment = "";
                break;

            case "화도일문자":
                comment = "\n\n'삼천세계'";
                break;

            case "활":
                comment = "\n\n시로의 최종변기";
                break;

            case "매그넘":
                comment = "\n\n매우 강력한";
                break;

            case "철쇄아":
                comment = "\n\n무시무시합니다";
                break;

            default:
                break;
        }

        return comment;
    }
}
