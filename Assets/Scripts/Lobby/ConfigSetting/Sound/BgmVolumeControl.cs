using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BgmVolumeControl : MonoBehaviour
{
    private Scrollbar bgmScrollbar;
    private TextMeshProUGUI bgmPercentage;

    private void Awake()
    {
        bgmPercentage = this.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        bgmScrollbar = this.transform.GetChild(2).GetComponent<Scrollbar>();
    }

    void Start()
    {
        bgmScrollbar.onValueChanged.AddListener(OnValueChangedBGM);
    }

    void Update()
    {
        
    }

    public void OnValueChangedBGM(float value)
    {
        value = Mathf.Round(value * 100) / 100;

        ConfigManager.Instance.bgmVolume = value;
        ConfigManager.Instance.isBgmVolumeChanged = true;
        bgmPercentage.text = (value * 100).ToString() + "%";
    }
}
