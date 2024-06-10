using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSettingControl : MonoBehaviour
{
    private Toggle mapToggle;

    private void Awake()
    {
        mapToggle = this.transform.GetChild(1).GetComponent<Toggle>();
        mapToggle.onValueChanged.AddListener(OnValueChangedMapToggle);
    }

    void Start()
    {

    }

    void OnValueChangedMapToggle(bool ret)
    {
        ButtonSoundManager.Instance.PlayOnClickButtonSound1();

        // true¿œ ∂ß π„
        if (ret)
        {
            RoundSetting.Instance.SetMapMode(1);
        }
        else
        {
            RoundSetting.Instance.SetMapMode(0);
        }
    }
}
