using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatUIControl : MonoBehaviour
{
    // singleton
    private static DefeatUIControl instance;
    public static DefeatUIControl Instance
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

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        hpControl = this.transform.parent.GetChild(0).GetComponent<HPControl>();
        expBarControl = this.transform.parent.GetChild(1).GetComponent<ExpBarControl>();
        timerControl = this.transform.parent.GetChild(3).GetComponent<TimerControl>();

        SetActive(false);
    }

    public void SetActive(bool ret)
    {
        if (ret)
        {
            // 패배 시 라운드 중 보이는 모든 UI를 비활성화한다.
            hpControl.gameObject.SetActive(false);
            expBarControl.gameObject.SetActive(false);
            RenewWaffleAmount.Instance.gameObject.SetActive(false);
            timerControl.gameObject.SetActive(false);

            // 이후 패배 UI 활성화
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
