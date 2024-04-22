using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PreResultUIControl : MonoBehaviour
{
    // singleton
    private static PreResultUIControl instance;
    public static PreResultUIControl Instance
    {
        get
        {
            if (null == instance)
                return null;

            return instance;
        }
    }

    private HPControl hpControl;
    private ExpBarControl expBarControl;
    private TimerControl timerControl;

    private TextMeshProUGUI resultText;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        hpControl = this.transform.parent.GetChild(0).GetComponent<HPControl>();
        expBarControl = this.transform.parent.GetChild(1).GetComponent<ExpBarControl>();
        timerControl = this.transform.parent.GetChild(3).GetComponent<TimerControl>();
        resultText = this.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        this.transform.position = new Vector2(Screen.width, Screen.height);
        this.gameObject.SetActive(false);
    }

    // 두번째 인수가 true면 승리, false면 패배
    public void SetActive(bool ret, bool win)
    {
        if (ret)
        {
            // 라운드 중 보이는 모든 UI를 비활성화한다
            hpControl.gameObject.SetActive(false);
            expBarControl.gameObject.SetActive(false);
            RenewWaffleAmount.Instance.gameObject.SetActive(false);
            timerControl.gameObject.SetActive(false);

            if (win)
            {
                // 라운드 승리일 시 텍스트를 승리로 변경
                resultText.text = "승리";
            }
            else
            {
                // 라운드 패배일 시 텍스트를 패배로 변경
                resultText.text = "패배";
            }

            // 이후 PreResultUI 활성화
            this.gameObject.SetActive(ret);
            this.transform.position = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        }
        else
        {
            this.transform.position = new Vector2(Screen.width, Screen.height);
            this.gameObject.SetActive(ret);
        }
    }
}
