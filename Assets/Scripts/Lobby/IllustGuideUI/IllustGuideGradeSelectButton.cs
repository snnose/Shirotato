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
        // ������ ���� ���� ���
        ButtonSoundManager.Instance.PlayOnPointerEnterSound1();
    }

    void OnClickGradeSelectButton()
    {
        // ��ư Ŭ�� ���� ���
        ButtonSoundManager.Instance.PlayOnClickButtonSound1();

        // ���� ��µ� ����Ʈ�� ��Ȱ��ȭ�Ѵ�
        illustGuideSelectManager.InActiveAllListUI();

        // �������� ���õ� ������ ��
        if (illustGuideSelectManager.currentSelect == "Item")
        {
            // ��� �̸��� ���� ��µǴ� ����Ʈ�� �޶�����
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

        // ���Ⱑ ���õ� ������ ��
        if (illustGuideSelectManager.currentSelect == "Weapon")
        {
            // ��� �̸��� ���� ��µǴ� ����Ʈ�� �޶�����
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
