using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualityUIControl : MonoBehaviour
{
    // singleton
    private static IndividualityUIControl instance;
    public static IndividualityUIControl Instance
    {
        get
        {
            if (null == instance)
                return null;

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        SetActive(false);
    }

    void Start()
    {
        
    }

    void Update()
    {
        Cancel();
    }

    // 화면 플로팅 중 Esc 입력 시 UI 비활성화
    private void Cancel()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetActive(false);
        }
    }

    public void SetActive(bool ret)
    {
        // true일 시 UI 활성화 및 화면 중앙으로 이동
        if (ret)
        {
            this.transform.position = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        }
        // false일 시 UI 비활성화 및 화면 바깥으로 이동
        else
        {
            this.transform.position = new Vector2(-Screen.width, Screen.height);
        }

        this.gameObject.SetActive(ret);
    }
}
