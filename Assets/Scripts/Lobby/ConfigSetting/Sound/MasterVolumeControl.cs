using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MasterVolumeControl : MonoBehaviour
{
    private Scrollbar masterScrollbar;
    private TextMeshProUGUI masterPercentage;

    private void Awake()
    {
        masterPercentage = this.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        masterScrollbar = this.transform.GetChild(2).GetComponent<Scrollbar>();
    }

    void Start()
    {
        masterScrollbar.onValueChanged.AddListener(OnValueChangedMaster);
    }

    void Update()
    {

    }

    public void OnValueChangedMaster(float value)
    {
        value = Mathf.Round(value * 100) / 100;

        ConfigManager.Instance.masterVolume = value;
        ConfigManager.Instance.isMasterVolumeChanged = true;
        masterPercentage.text = (value * 100).ToString() + "%";
    }
}
