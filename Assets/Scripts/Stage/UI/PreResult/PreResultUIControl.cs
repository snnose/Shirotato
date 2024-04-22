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

    // �ι�° �μ��� true�� �¸�, false�� �й�
    public void SetActive(bool ret, bool win)
    {
        if (ret)
        {
            // ���� �� ���̴� ��� UI�� ��Ȱ��ȭ�Ѵ�
            hpControl.gameObject.SetActive(false);
            expBarControl.gameObject.SetActive(false);
            RenewWaffleAmount.Instance.gameObject.SetActive(false);
            timerControl.gameObject.SetActive(false);

            if (win)
            {
                // ���� �¸��� �� �ؽ�Ʈ�� �¸��� ����
                resultText.text = "�¸�";
            }
            else
            {
                // ���� �й��� �� �ؽ�Ʈ�� �й�� ����
                resultText.text = "�й�";
            }

            // ���� PreResultUI Ȱ��ȭ
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
