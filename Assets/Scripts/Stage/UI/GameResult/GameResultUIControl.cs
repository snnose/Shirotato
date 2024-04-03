using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameResultUIControl : MonoBehaviour
{
    // singleton
    private static GameResultUIControl instance;
    public static GameResultUIControl Instance
    {
        get
        {
            if (null == instance)
                return null;

            return instance;
        }
    }

    private TextMeshProUGUI title;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        title = this.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        SetActive(false);
    }

    void Start()
    {
        
    }

    public void SetActive(bool ret)
    {
        if (ret)
        {
            this.gameObject.SetActive(ret);
            this.transform.position = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);

            // 게임 승리, 패배에 따라 출력되는 텍스트가 변경된다
            SetTitleText(GameRoot.Instance.GetIsGameOver());
        }
        else
        {
            this.transform.position = new Vector2(Screen.width, 0);
            this.gameObject.SetActive(ret);
        }
    }

    private void SetTitleText(bool isGameOver)
    {
        string difficultyName = "";

        switch (RoundSetting.Instance.GetDifficulty())
        {
            case 0:
                difficultyName = "쉬움";
                break;
            case 1:
                difficultyName = "보통";
                break;
            case 2:
                difficultyName = "어려움";
                break;
            case 3:
                break;
            case 4:
                break;
            default:
                break;
        }

        // 게임 패배 시
        if (isGameOver)
            title.text = "라운드 패배 (" + difficultyName + ")";
        // 게임 승리 시
        else
            title.text = "라운드 승리 (" + difficultyName + ")";
    }
}
