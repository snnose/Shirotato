using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IllustGuideItemButton : MonoBehaviour, IPointerEnterHandler
{
    Button itemButton;

    private void Awake()
    {
        itemButton = this.GetComponent<Button>();
        itemButton.onClick.AddListener(OnClickItemButton);
    }

    void Start()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 포인터 진입 음성 출력
        ButtonSoundManager.Instance.PlayOnPointerEnterSound2();
    }

    void OnClickItemButton()
    {
        // 버튼 클릭 음성 출력
        ButtonSoundManager.Instance.PlayOnClickButtonSound2();

        // 선택된 아이템의 등급, 이미지, 이름, 설명으로 교체
        IllustGuideSelectedDetail.Instance.selectedImage.transform.GetChild(0).GetComponent<Image>().color =
            this.transform.GetChild(0).GetComponent<Image>().color;

        IllustGuideSelectedDetail.Instance.selectedImage.transform.GetChild(1).GetComponent<Image>().sprite =
            this.transform.GetChild(1).GetComponent<Image>().sprite;

        IllustGuideSelectedDetail.Instance.selectedNameText.text = this.GetComponent<ItemInfo>().itemName;

        IllustGuideSelectedDetail.Instance.selectedDetailText.text = SetItemDetailText();

        string comment = GetItemComment(this.GetComponent<ItemInfo>().itemName);

        IllustGuideComment.Instance.SetCommentText(comment);
    }

    string GetItemComment(string itemName)
    {
        string comment = "";

        switch (itemName)
        {
            // 노말
            case "가방":
                comment = "24.04.20 / 신의상\n\n" +
                          "마마의 추천 가방";
                break;

            case "감기약":
                comment = "\n\n시로야~ 약 먹어라?";
                break;

            case "귀여운콧물":
                comment = "24.01.15\n\n" +
                          "궁금하면 1:41:30";
                break;

            case "금지판":
                comment = "\n\n??? : 미안 금지";
                break;

            case "꿀벌":
                comment = "쿠키요미 / 4:07:15\n\n" +
                          "왜 울고 있는 거야~";
                break;

            case "노른자":
                comment = "23.11.24 / 러버덕" +
                          "\n\n근본 러버덕";
                break;

            case "눈오리":
                comment = "\n\n정신없이 만들게 됩니다";
                break;

            case "렌즈":
                comment = "";
                break;

            case "마른꽃":
                comment = "\n\n드라이플라워";
                break;

            case "마우스":
                comment = "\n\n바XX 협찬";
                break;

            case "마이크":
                comment = "\n\n스트리머의 필수품";
                break;

            case "머리핀":
                comment = "\n\n포인트 중 하나";
                break;

            case "메모지":
                comment = "\n\n모니터에 붙여놓기 좋습니다";
                break;

            case "모래":
                comment = "\n\n모 랠 가";
                break;

            case "물":
                comment = "\n\n무르";
                break;

            case "바운스볼":
                comment = "24.04.25\n\n" +
                          "추억의 게임";
                break;

            case "반창고":
                comment = "\n\n쓸 일 없는 게 좋습니다";
                break;

            case "밥알":
                comment = "23.12.12\n\n" +
                          "밥알이 몇개고?";
                break;

            case "벼룩":
                comment = "24.04.19\n\n" +
                          "밍벼룩";
                break;

            case "보자기":
                comment = "\n\n너 마로된거야";
                break;

            case "블루베리":
                comment = "\n\n그릭요거트와 찰떡궁합";
                break;

            case "빨간모자":
                comment = "\n\n1개월";
                break;

            case "사인":
                comment = "\n\n정말 갖고 싶은 것";
                break;

            case "새싹":
                comment = "\n\n풋풋합니다";
                break;

            case "샴푸":
                comment = "\n\n";
                break;

            case "시계":
                comment = "23.10.04 ~\n\n" +
                          "밍무원";
                break;

            case "시험지":
                comment = "\n\n학력고사 2등";
                break;

            case "엿":
                comment = "\n\nF";
                break;

            case "오징어":
                comment = "23.09.20\n\n" +
                          "이마이쿠~~";
                break;

            case "용용이":
                comment = "\n\n시로의 인형 중 하나";
                break;

            case "유자차":
                comment = "\n\n온기가 필요했잖아";
                break;

            case "자석":
                comment = "\n\n누구의 것일까요?";
                break;

            case "작살":
                comment = "데이브 더 다이버\n\n" +
                          "40작쌀!";
                break;

            case "젓가락":
                comment = "23.12.12\n\n" +
                          "밥알 세기에 좋습니다";
                break;

            case "진주":
                comment = "23.11.13 / 스텔서버\n\n" +
                          "??? : 누구 죽일 사람 없나?";
                break;

            case "츄르":
                comment = "\n\n고양이에게 최고의 간식";
                break;

            case "츄리닝":
                comment = "24.01.03\n\n" +
                          "백수같다는 나쁜말은 ㄴㄴㄴ";
                break;

            case "침대":
                comment = "\n\n눕방의 필수요소";
                break;

            case "카트":
                comment = "24.04.07 / 카트게임\n\n" +
                          "물건만 실어야합니다";
                break;

            case "커피":
                comment = "\n\n연하게 마시는 편";
                break;

            case "탄약":
                comment = "\n\n탕약";
                break;

            case "텐텐":
                comment = "\n\n텐텐중독";
                break;

            case "티스푼":
                comment = "\n\n";
                break;

            case "포도당사탕":
                comment = "\n\n적당히 먹어야합니다";
                break;

            case "흠":
                comment = "23.11.10 / 스텔서버\n\n" +
                          "흠이 (~ 23.12.24)";
                break;

            // 레어
            case "개구리":
                comment = "24.01.01 / 스텔서버" +
                    "\n\n??? : 아.. 그런 컨셉?";
                break;

            case "곡괭이":
                comment = "스텔서버\n\n" +
                          "어마무시합니다";
                break;

            case "공갈":
                comment = "\n\n시로는 아가야..";
                break;

            case "낚싯대":
                comment = "스텔서버\n\n" +
                          "초초초행운겟";
                break;

            case "넥타이":
                comment = "의상\n\n" +
                          "교복 넥타이";
                break;

            case "닭하산":
                comment = "23.10.25 / 닭하산\n\n" +
                          "I believe I can fly~";
                break;

            case "물총":
                comment = "버X파X터\n\n" +
                          "버조쿠";
                break;

            case "미정이":
                comment = "23.10.12 / 우주모드\n\n" +
                          "미정아ㅏㅏㅏㅏ";
                break;

            case "밀짚모자":
                comment = "\n\n무기와라";
                break;

            case "바둑돌":
                comment = "23.08.15 / 스위치\n\n" +
                          "오목 10초 컷";
                break;

            case "불닭소스":
                comment = "\n\n맛있게 맵습니다";
                break;

            case "비니":
                comment = "23.12.04 / AGF후기\n\n" +
                          "귀엽습니다";
                break;

            case "삐에로모자":
                comment = "23.10.31 / 할로윈\n\n" +
                          "무?섭습니다";
                break;

            case "산타모자":
                comment = "23.12.25 / 크리스마스\n\n" +
                          "산타는 있습니다";
                break;

            case "선글라스":
                comment = "24.04.20 / 신의상\n\n" +
                          "눈나ㅏㅏㅏㅏ";
                break;

            case "썬캡":
                comment = "\n\n3개월";
                break;

            case "야포":
                comment = "24.01.06 / 문명\n\n" +
                          "시로의 애착병기";
                break;

            case "오토마톤":
                comment = "23.12.22 / 후열\n\n" +
                          "하찮게 귀엽습니다";
                break;

            case "와드":
                comment = "23.12.21 / 롤\n\n" +
                          "와드인 척!(움직이며)";
                break;

            case "요리사모자":
                comment = "\n\n오마카세 전문";
                break;

            case "윙슈즈":
                comment = "테X즈X너\n\n" +
                          "우다다의 비결일수도?";
                break;

            case "장미":
                comment = "23.07.02\n\n" +
                          "붉은 장밍";
                break;

            case "잼민이":
                comment = "23.11.24 / 러버덕\n\n" +
                          "멀리 떠났습니다";
                break;

            case "저금통":
                comment = "\n\n300억 저금통";
                break;

            case "청사과":
                comment = "\n\n시로가 좋아합니다";
                break;

            case "초록비니":
                comment = "\n\n6개월";
                break;

            case "초커":
                comment = "24.04.20 / 신의상\n\n" +
                          "은근하게 눈이 갑니다";
                break;

            case "치즈":
                comment = "\n\n밍 치 즈";
                break;

            case "토끼인형":
                comment = "24.04.20 / 신의상\n\n" +
                          "마마의 선물";
                break;

            case "하프소드":
                comment = "23.11.24 / 하프소드\n\n" +
                          "잔인한 게임";
                break;

            case "항아리":
                comment = "24.04.23 / 항아리\n\n" +
                          "깨부수고 싶습니다";
                break;

            case "해파리":
                comment = "\n\n신날 때 보입니다";
                break;

            case "후추":
                comment = "24.02.19 / 띵타\n\n" +
                          "건축은? 후튜브";
                break;

            case "흐":
                comment = "23.11.10 / 스텔서버\n\n" +
                          "흐";
                break;

            case "흠밍정음":
                comment = "\n\n어감이 좋습니다";
                break;

            // 에픽
            case "귀걸이":
                comment = "23.05.03 / 신의상\n\n" +
                          "오더메이드라 늦었습니다";
                break;

            case "금":
                comment = "발X란트\n\n" +
                          "그랜드 골드";
                break;

            case "깃발":
                comment = "\n\n확실한 플래그 회수";
                break;

            case "니모":
                comment = "스텔서버\n\n" +
                          "니모와";
                break;

            case "니얀다":
                comment = "23.11.16 / 스텔서버\n\n" +
                          "난다난다니얀다";
                break;

            case "다이아몬드":
                comment = "\n\n켰냐?";
                break;

            case "도리":
                comment = "스텔서버\n\n" +
                          "도리";
                break;

            case "루돌프":
                comment = "23.12.24 / 스텔서버\n\n" +
                          "리제의 선물(미개봉)";
                break;

            case "마즈피플":
                comment = "23.10.21\n\n" +
                          "일단은 머리를";
                break;

            case "말랑이":
                comment = "23.10.03 / 첫주령\n\n" +
                          "말랑이 거래해요~";
                break;

            case "민트초코":
                comment = "\n\n내 돈주고는 안먹는";
                break;

            case "밍":
                comment = "23.11.10 / 스텔서버\n\n" +
                          "밍이";
                break;

            case "바냐냐":
                comment = "24.03.06\n\n" +
                          "바냐냐~";
                break;

            case "바위":
                comment = "24.04.20 / 시시포스\n\n" +
                          "코를 긁지 않았다면..";
                break;

            case "밤양갱":
                comment = "24.03.16\n\n" +
                          "빨리 먹는 편입니다";
                break;

            case "백금":
                comment = "발X란트" +
                          "\n\n목표였던 것";
                break;

            case "밸런스볼":
                comment = "\n\n운동은 좋습니다";
                break;

            case "버튼":
                comment = "\n\n빠밤버튼(진짜있음)";
                break;

            case "붕붕이":
                comment = "24.04.14 / 서브노티카\n\n" +
                          "시모스";
                break;

            case "빅맥":
                comment = "23.12.29 ~\n\n" +
                          "무쌩긴 목소리";
                break;

            case "빵봉투":
                comment = "24.03.28 ~\n\n" +
                          "자존감이 충만해집니다";
                break;

            case "사랑니":
                comment = "24.04.15 ~\n\n" +
                          "말썽쟁이";
                break;

            case "상어":
                comment = "\n\n백상아리~ 청상아리~";
                break;

            case "온천":
                comment = "23.11.07 / 스텔서버\n\n" +
                          "잘 만들었습니다";
                break;

            case "의자":
                comment = "\n\n콘X사2";
                break;

            case "차르봄바":
                comment = "\n\n와 보소?";
                break;

            case "컵":
                comment = "23.03.01 / 컵헤드\n\n" +
                          "쉽네 ㅋ";
                break;

            case "케이크":
                comment = "23.12.25 / 크리스마스\n\n" +
                          "촛불에 소원을 담아";
                break;

            case "콩":
                comment = "";
                break;

            case "킁킁이":
                comment = "23.12.19 / 스텔서버\n\n" +
                          "킁킁";
                break;

            case "파란모자":
                comment = "\n\n9개월";
                break;

            case "포자꽃":
                comment = "24.02.16 / 띵타\n\n" +
                          "공매도의 위험성";
                break;

            case "포켓볼":
                comment = "24.04.21 / 포켓볼\n\n" +
                          "시밑마";
                break;

            case "퓨즈":
                comment = "\n\n공겜에서 맨날 없는";
                break;

            case "향수":
                comment = "\n\n한밤중에 카톡이 울려";
                break;

            // 전설
            case "Maro-15":
                comment = "24.02.22 / 시로생일\n\n" +
                          "마로의 선물";
                break;

            case "기타":
                comment = "\n\n노래방 치트키";
                break;

            case "나팔":
                comment = "\n\n빠밤~";
                break;

            case "날개옷":
                comment = "24.02.08 ~ / 의상\n\n" +
                          "굉장히 예쁜 옷";
                break;

            case "리본":
                comment = "23.11.05\n\n" +
                          "정실은?";
                break;

            case "마로":
                comment = "23.06.10 ~\n\n" +
                          "시로의 마로";
                break;

            case "마리오모자":
                comment = "23.10.02 / 뿌애앵\n\n" +
                          "너무 어려워";
                break;

            case "변기":
                comment = "23.06.10 / 데뷔\n\n" +
                          "강렬한 시작";
                break;

            case "부엉이":
                comment = "23.10.21 / Pale\n\n" +
                          "조금 질투납니다";
                break;

            case "빨간망토":
                comment = "23.09.21 / 의상\n\n" +
                          "굉장히 예쁜 옷 2";
                break;

            case "싹싹김치":
                comment = "23.12.04 ~\n\n" +
                          "싹싹김치~";
                break;

            case "안경":
                comment = "23.06.10 ~ / 의상\n\n" +
                          "갓경";
                break;

            case "왕관":
                comment = "23.09.17 / 100일\n\n" +
                          "서로에게 씌워준";
                break;

            case "우산":
                comment = "생일굿즈\n\n" +
                          "뛰어난 실용성";
                break;

            case "우주복헬멧":
                comment = "23.10.19 & 우주비행사\n\n" +
                          "고양이 사람 전용";
                break;

            case "우주선":
                comment = "\n\n시로의 자가용";
                break;

            case "자동차":
                comment = "23.06.08 / 퀘스쳔\n\n" +
                          "난데! 도시테";
                break;

            case "제복모자":
                comment = "23.06.10 ~ / 의상\n\n" +
                          "한쪽 귀에 걸쳐 씁니다";
                break;

            case "제복코트":
                comment = "23.06.10 ~ / 의상\n\n" +
                          "굉장히 멋진 옷";
                break;

            case "조선왕조실록":
                comment = "24.04.09 ~\n\n" +
                          "조선왕조씰룩쎌룩";
                break;

            case "중절모":
                comment = "\n\n벌써 1년";
                break;

            case "치파오":
                comment = "24.02.18 / 게로츄\n\n" +
                          "게게게로게게게로";
                break;

            case "카츄사":
                comment = "23.09.05 / 메이드\n\n" +
                          "타비의 메이드(였던)";
                break;

            case "포탈":
                comment = "24.03.24 / 포탈2\n\n" +
                          "클리어했지만 기록말살";
                break;

            case "화학통":
                comment = "24.03.13 / 밍도\n\n" +
                          "문도 그 자체";
                break;

            default:
                comment = "";
                break;
        }

        return comment;
    }

    string SetItemDetailText()
    {
        ItemInfo itemInfo = this.GetComponent<ItemInfo>();

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
            tmpText += "추가 대미지 +" + itemInfo.FixedDMG + '\n';
            plusCount++;
        }
        if (itemInfo.Critical > 0)
        {
            tmpText += "치명타 확률 +" + itemInfo.Critical + "%\n";
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
            tmpText += "생명력 흡수 +" + itemInfo.HPDrain + "%\n";
            plusCount++;
        }
        if (itemInfo.Armor > 0)
        {
            tmpText += "방어력 +" + itemInfo.Armor + '\n';
            plusCount++;
        }
        if (itemInfo.Evasion > 0)
        {
            tmpText += "회피 확률 +" + itemInfo.Evasion + "%\n";
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
            tmpText += "추가 대미지 " + itemInfo.FixedDMG + '\n';
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
            tmpText += "생명력 흡수 " + itemInfo.HPDrain + "%\n";
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

        return finalText;
    }
}
