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
        // 선택된 특성 정보를 전달한다.
        string individuality = this.transform.parent.GetChild(2).GetComponent<TextMeshProUGUI>().text;
        RoundSetting.Instance.SetIndividuality(individuality);
        // 특성 선택 창을 종료한다
        IndividualityUIControl.Instance.SetActive(false);
        // 무기 선택 창을 띄운다
        WeaponChooseUIControl.Instance.SetActive(true);
    }
}
