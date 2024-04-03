using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class DifficultyButtonControl : MonoBehaviour, IPointerEnterHandler
{
    Button button;
    string difficultyName;

    void Awake()
    {
        button = this.GetComponent<Button>();
        button.onClick.AddListener(OnClickDifficultyButton);
    }

    void Start()
    {
        
    }

    // ���콺 �����Ͱ� �����ϸ� ���̵� ���� ����
    public void OnPointerEnter(PointerEventData eventData)
    {
        difficultyName = this.name;

        DifficultyUIControl.Instance.GetFinalListControl().RenewDifficultyInfoUI(difficultyName);
    }

    // ��ư Ŭ�� �� ���̵� ��� �� ���� ����
    private void OnClickDifficultyButton()
    {
        // ���̵� ���
        switch (difficultyName)
        {
            case "Easy":
                RoundSetting.Instance.SetDifficulty(0);
                break;
            default:
                RoundSetting.Instance.SetDifficulty(0);
                break;
        }

        // RoundSetting Ȱ���ϵ��� DataManager�� Stage ���� �ѱ��

        // ���� ����
        SceneManager.LoadScene("Stage");
    }
}
