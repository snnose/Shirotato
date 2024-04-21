using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class IndividualityButton : MonoBehaviour, IPointerEnterHandler
{
    Button choiceButton;

    void Start()
    {
        choiceButton = this.GetComponent<Button>();
        choiceButton.onClick.AddListener(OnClickChoiceButton);
    }

    void Update()
    {
        
    }

    private void OnClickChoiceButton()
    {
        // ��ư Ŭ�� ���� ���
        ButtonSoundManager.Instance.PlayOnClickButtonSound1();

        // ���õ� Ư�� ������ �����Ѵ�.
        string individuality = this.transform.parent.GetChild(2).GetComponent<TextMeshProUGUI>().text;
        RoundSetting.Instance.SetIndividuality(individuality);
        // Ư�� ���� â�� �����Ѵ�
        IndividualityUIControl.Instance.SetActive(false);
        // ���� ���� â�� ����
        WeaponChooseUIControl.Instance.SetActive(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ButtonSoundManager.Instance.PlayOnPointerEnterSound1();
    }
}
