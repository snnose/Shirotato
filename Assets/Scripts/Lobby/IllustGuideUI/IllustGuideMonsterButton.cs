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
        // ������ ���� ���� ���
        ButtonSoundManager.Instance.PlayOnPointerEnterSound2();
    }

    void OnClickMonsterButton()
    {
        // ��ư Ŭ�� ���� ���
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
                name = "����";
                break;

            case "Sandwich":
                name = "������ġ";
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
                finalText = "ü�� : 10 + (5 x ���� ����)\n" +
                            "���� ��� �� : 3\n" +
                            "���� ����� : 100%\n" +
                            "���� ����� : 20%";
                break;

            case "Sandwich":
                finalText = "ü��: 3 + (2 x ���� ����)\n" +
                            "����� : 1 + (0.6 x ���� ����)\n" +
                            "���� ��� �� : 1\n" +
                            "���� ����� : 1%\n" +
                            "���� ����� : 1%";
                break;

            default:
                break;
        }

        return finalText;
    }
}
