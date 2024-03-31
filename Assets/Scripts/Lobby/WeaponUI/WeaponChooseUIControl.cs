using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChooseUIControl : MonoBehaviour
{
    // singleton
    private static WeaponChooseUIControl instance;
    public static WeaponChooseUIControl Instance
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

    // 화면 플로팅 중 Esc 입력 시 해당 UI 비활성화
    // 특성 선택 창을 띄운다 (마치 무기 선택에서 특성 선택 창으로 이동하는 것 처럼)
    private void Cancel()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetActive(false);
            IndividualityUIControl.Instance.SetActive(true);
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
            this.transform.position = new Vector2(-Screen.width, 0f);
        }

        this.gameObject.SetActive(ret);
    }
}
