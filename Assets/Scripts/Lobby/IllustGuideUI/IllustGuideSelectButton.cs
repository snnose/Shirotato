using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IllustGuideSelectButton : MonoBehaviour, IPointerEnterHandler
{
    private Button selectButton;
    private IllustGuideSelectManager illustGuideSelectManager;

    private GameObject gradeSelection;

    private void Awake()
    {
        selectButton = this.GetComponent<Button>();
        selectButton.onClick.AddListener(OnClickSelectButton);

        illustGuideSelectManager = this.transform.parent.GetComponent<IllustGuideSelectManager>();

        gradeSelection = this.transform.parent.GetChild(3).gameObject;
        gradeSelection.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 포인터 진입 음성 출력
        ButtonSoundManager.Instance.PlayOnPointerEnterSound1();
    }

    void Start()
    {
        
    }

    void OnClickSelectButton()
    {
        // 버튼 클릭 음성 출력
        ButtonSoundManager.Instance.PlayOnClickButtonSound1();
        string select = this.name;

        /*
        // 등급 선택 UI가 활성화라면
        if (gradeSelection.activeSelf)
        {
            // 다시 비활성화한다
            gradeSelection.SetActive(false);
            return;
        }
        */
        switch (select)
        {
            case "Item":
                illustGuideSelectManager.currentSelect = "Item";
                gradeSelection.SetActive(true);
                gradeSelection.transform.position = this.transform.position + new Vector3(165, 0);
                break;
            case "Weapon":
                illustGuideSelectManager.currentSelect = "Weapon";
                gradeSelection.SetActive(true);
                gradeSelection.transform.position = this.transform.position + new Vector3(165, 0);
                break;
            case "Monster":
                illustGuideSelectManager.currentSelect = "Monster";
                this.transform.parent.GetComponent<IllustGuideSelectManager>().monsterListUI.SetActive(true);
                this.transform.parent.GetComponent<IllustGuideSelectManager>().monsterListUI.transform.position =
                    new Vector3(Screen.width * 0.5f - 70, Screen.height * 0.5f - 50);
                break;
            default:
                break;
        }
    }
}
