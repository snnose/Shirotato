using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IllustGuideGradeSelectButton : MonoBehaviour, IPointerEnterHandler
{
    private Button gradeSelectButton;
    private IllustGuideSelectManager illustGuideSelectManager;

    private void Awake()
    {
        gradeSelectButton = this.GetComponent<Button>();
        gradeSelectButton.onClick.AddListener(OnClickGradeSelectButton);

        illustGuideSelectManager = this.transform.parent.parent.GetComponent<IllustGuideSelectManager>();
    }

    void Start()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 포인터 진입 음성 출력
        ButtonSoundManager.Instance.PlayOnPointerEnterSound1();
    }

    void OnClickGradeSelectButton()
    {
        // 버튼 클릭 음성 출력
        ButtonSoundManager.Instance.PlayOnClickButtonSound1();

        // 현재 출력된 리스트를 비활성화한다
        illustGuideSelectManager.InActiveAllListUI();

        // 아이템이 선택된 상태일 때
        if (illustGuideSelectManager.currentSelect == "Item")
        {
            // 등급 이름에 따라 출력되는 리스트가 달라진다
            switch (this.name)
            {
                case "Normal":
                    illustGuideSelectManager.normalItemListUI.SetActive(true);
                    illustGuideSelectManager.normalItemListUI.transform.position =
                        new Vector3(Screen.width * 0.5f - 70, Screen.height * 0.5f - 50);
                    break;
                case "Rare":
                    illustGuideSelectManager.rareItemListUI.SetActive(true);
                    illustGuideSelectManager.rareItemListUI.transform.position =
                        new Vector3(Screen.width * 0.5f - 70, Screen.height * 0.5f - 50);
                    break;
                case "Epic":
                    illustGuideSelectManager.epicItemListUI.SetActive(true);
                    illustGuideSelectManager.epicItemListUI.transform.position =
                        new Vector3(Screen.width * 0.5f - 70, Screen.height * 0.5f - 50);
                    break;
                case "Legend":
                    illustGuideSelectManager.legendItemListUI.SetActive(true);
                    illustGuideSelectManager.legendItemListUI.transform.position =
                        new Vector3(Screen.width * 0.5f - 70, Screen.height * 0.5f - 50);
                    break;
                default:
                    break;
            }
        }

        // 무기가 선택된 상태일 때
        if (illustGuideSelectManager.currentSelect == "Weapon")
        {
            // 등급 이름에 따라 출력되는 리스트가 달라진다
            switch (this.name)
            {
                case "Normal":
                    illustGuideSelectManager.normalWeaponListUI.SetActive(true);
                    illustGuideSelectManager.normalWeaponListUI.transform.position =
                        new Vector3(Screen.width * 0.5f - 70, Screen.height * 0.5f - 50);
                    break;
                case "Rare":
                    illustGuideSelectManager.rareWeaponListUI.SetActive(true);
                    illustGuideSelectManager.rareWeaponListUI.transform.position =
                        new Vector3(Screen.width * 0.5f - 70, Screen.height * 0.5f - 50);
                    break;
                case "Epic":
                    illustGuideSelectManager.epicWeaponListUI.SetActive(true);
                    illustGuideSelectManager.epicWeaponListUI.transform.position =
                        new Vector3(Screen.width * 0.5f - 70, Screen.height * 0.5f - 50);
                    break;
                case "Legend":
                    illustGuideSelectManager.legendWeaponListUI.SetActive(true);
                    illustGuideSelectManager.legendWeaponListUI.transform.position =
                        new Vector3(Screen.width * 0.5f - 70, Screen.height * 0.5f - 50);
                    break;
                default:
                    break;
            }
        }

        this.transform.parent.gameObject.SetActive(false);
    }
}
