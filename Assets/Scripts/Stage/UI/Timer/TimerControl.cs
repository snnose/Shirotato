using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerControl : MonoBehaviour
{
    private float remainTime;
    private bool isTicking = false;

    private void Awake()
    {
        Initialize();
    }

    // Start is called before the first frame update
    void Start()
    {
        // 첫 게임 시작 시 타이머 텍스트 설정
        remainTime = GameRoot.Instance.GetRemainTime();
        this.GetComponent<TextMeshProUGUI>().text = remainTime.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // 매 초마다 타이머를 갱신한다
        if (!isTicking
            && !GameRoot.Instance.GetIsRoundClear())
            StartCoroutine(StartTimer());
    }

    private void Initialize()
    {
        // remainTime = 현재 라운드의 제한시간
        this.GetComponent<TextMeshProUGUI>().text = remainTime.ToString();
        this.GetComponent<TextMeshProUGUI>().color = Color.black;
        isTicking = false;
    }

    public void SetTimerText(string text)
    {
        this.GetComponent<TextMeshProUGUI>().text = text.ToString();
    }

    IEnumerator StartTimer()
    {
        remainTime = GameRoot.Instance.GetRemainTime();
        isTicking = true;

        if (remainTime < 10f)
            this.GetComponent<TextMeshProUGUI>().color = Color.red;
        else
            this.GetComponent<TextMeshProUGUI>().color = Color.black;

        yield return new WaitForSeconds(1f);
        remainTime--;
        GameRoot.Instance.SetRemainTime(remainTime);

        if (remainTime < 10f)
            this.GetComponent<TextMeshProUGUI>().color = Color.red;
        else
            this.GetComponent<TextMeshProUGUI>().color = Color.black;

        if (remainTime <= 0f)
        {
            GameRoot.Instance.SetIsRoundClear(true);
            remainTime = 0f;
        }

        this.GetComponent<TextMeshProUGUI>().text = remainTime.ToString();
        isTicking = false;
    }
}
