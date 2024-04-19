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

    // 마우스 포인터가 진입하면 난이도 정보 갱신
    public void OnPointerEnter(PointerEventData eventData)
    {
        difficultyName = this.name;

        DifficultyUIControl.Instance.GetFinalListControl().RenewDifficultyInfoUI(difficultyName);
    }

    // 버튼 클릭 시 난이도 기록 및 게임 시작
    private void OnClickDifficultyButton()
    {
        // 난이도 기록
        switch (difficultyName)
        {
            case "Easy":
                RoundSetting.Instance.SetDifficulty(0);
                break;
            case "Normal":
                RoundSetting.Instance.SetDifficulty(1);
                break;
            case "Hard":
                RoundSetting.Instance.SetDifficulty(2);
                break;
            case "VeryHard":
                RoundSetting.Instance.SetDifficulty(3);
                break;
            case "Hell":
                RoundSetting.Instance.SetDifficulty(4);
                break;
            default:
                RoundSetting.Instance.SetDifficulty(0);
                break;
        }

        // RoundSetting 활용하도록 DataManager을 Stage 씬에 넘긴다

        // 게임 시작
        SceneManager.LoadScene("Stage");
    }
}
