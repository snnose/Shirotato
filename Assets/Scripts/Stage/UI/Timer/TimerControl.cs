using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerControl : MonoBehaviour
{
    private float remainTime = 3f;
    private bool isTicking = false;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isTicking)
            StartCoroutine(StartTimer());

        if (remainTime < 10f)
            this.GetComponent<TextMeshProUGUI>().color = Color.red;

        if (remainTime <= 0f)
        {
            GameRoot.Instance.SetIsRoundClear(true);
            remainTime = 0f;
            Time.timeScale = 0f;
        }
    }

    private void Initialize()
    {
        // remainTime = ���� ������ ���ѽð�
        this.GetComponent<TextMeshProUGUI>().text = remainTime.ToString();
        this.GetComponent<TextMeshProUGUI>().color = Color.black;
        isTicking = false;
    }

    IEnumerator StartTimer()
    {
        remainTime--;
        this.GetComponent<TextMeshProUGUI>().text = remainTime.ToString();
        isTicking = true;

        yield return new WaitForSeconds(1f);
        isTicking = false;
    }
}
