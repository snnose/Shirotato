using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyUIControl : MonoBehaviour
{
    // singleton
    private static DifficultyUIControl instance;
    public static DifficultyUIControl Instance
    {
        get
        {
            if (null == instance)
                return null;

            return instance;
        }
    }

    private FinalListControl finalListControl;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        finalListControl = this.transform.GetChild(2).GetComponent<FinalListControl>();
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
    // 특성 선택 창을 띄운다 (마치 난이도 선택에서 무기 선택 창으로 이동하는 것 처럼)
    private void Cancel()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetActive(false);
            WeaponChooseUIControl.Instance.SetActive(true);
            // RoundSetting 무기 초기화
            RoundSetting.Instance.SetStartWeapon("");
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
            this.transform.position = new Vector2(0f, Screen.height);
        }

        this.gameObject.SetActive(ret);

        if (ret)
        {
            // 최종 선택 리스트 정보 갱신
            finalListControl.RenewIndividualityInfoUI(RoundSetting.Instance.GetIndividuality());
            finalListControl.RenewWeaponInfoUI(RoundSetting.Instance.GetStartWeapon());
        }
    }

    public FinalListControl GetFinalListControl()
    {
        return this.finalListControl;
    }
}
