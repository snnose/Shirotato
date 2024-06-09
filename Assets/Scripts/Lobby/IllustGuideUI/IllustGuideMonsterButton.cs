using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IllustGuideMonsterButton : MonoBehaviour, IPointerEnterHandler
{
    Button monsterButton;

    private void Awake()
    {
        monsterButton = this.GetComponent<Button>();
        monsterButton.onClick.AddListener(OnClickMonsterButton);
    }

    void Start()
    {
        //rarity = DecideRarity(this.transform.parent.parent.parent.parent.name);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 포인터 진입 음성 출력
        ButtonSoundManager.Instance.PlayOnPointerEnterSound2();
    }

    void OnClickMonsterButton()
    {
        // 버튼 클릭 음성 출력
        ButtonSoundManager.Instance.PlayOnClickButtonSound2();

        IllustGuideSelectedDetail.Instance.selectedImage.transform.GetChild(1).GetComponent<Image>().sprite =
            this.transform.GetChild(1).GetComponent<Image>().sprite;

        IllustGuideSelectedDetail.Instance.selectedImage.transform.GetChild(1).GetComponent<Image>().color =
            this.transform.GetChild(1).GetComponent<Image>().color;

        //IllustGuideSelectedDetail.Instance.selectedNameText.text = SetSelectedNameText();

        IllustGuideSelectedDetail.Instance.selectedDetailText.text =
        SetMonsterDetailText(this.transform.GetChild(1).GetComponent<Image>().sprite.name);
    }

    string SetSelectedNameText(string monsterName)
    {
        string name = "";

        switch (monsterName)
        {
            case "Potaotes":
                name = "감자";
                break;

            case "Sandwich":
                name = "샌드위치";
                break;

            default:
                break;
        }

        return name;
    }

    string SetMonsterDetailText(string monsterName)
    {
        string finalText = "";

        switch (monsterName)
        {
            case "Potatoes":
                finalText = "체력 : 10 + (5 x 현재 라운드)\n" +
                            "와플 드랍 수 : 3\n" +
                            "우유 드랍률 : 100%\n" +
                            "상자 드랍률 : 20%";
                break;

            case "Sandwich":
                finalText = "체력: 3 + (2 x 현재 라운드)\n" +
                            "대미지 : 1 + (0.6 x 현재 라운드)\n" +
                            "와플 드랍 수 : 1\n" +
                            "우유 드랍률 : 1%\n" +
                            "상자 드랍률 : 1%";
                break;

            default:
                break;
        }

        return finalText;
    }
}
