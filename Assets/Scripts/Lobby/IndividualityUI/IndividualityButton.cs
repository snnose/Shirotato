using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IndividualityButton : MonoBehaviour
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
        // ���õ� Ư�� ������ �����Ѵ�.
        string individuality = this.transform.parent.GetChild(2).GetComponent<TextMeshProUGUI>().text;
        RoundSetting.Instance.SetIndividuality(individuality);
        // Ư�� ���� â�� �����Ѵ�
        IndividualityUIControl.Instance.SetActive(false);
        // ���� ���� â�� ����
        WeaponChooseUIControl.Instance.SetActive(true);
    }
}
