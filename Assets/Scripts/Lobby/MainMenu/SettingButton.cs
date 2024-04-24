using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SettingButton : MonoBehaviour, IPointerEnterHandler
{
    Button settingButton;

    private void Awake()
    {
        settingButton = this.GetComponent<Button>();
        settingButton.onClick.AddListener(OnClickSettingButton);
    }

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnClickSettingButton()
    {
        // 음성 출력
        ButtonSoundManager.Instance.PlayOnClickButtonSound1();

        // ConfigSettingUI 플로팅
        ConfigSettingUI.Instance.SetActive(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ButtonSoundManager.Instance.PlayOnPointerEnterSound1();
    }
}
