using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EffectVolumeControl : MonoBehaviour
{
    private Scrollbar effectScrollbar;
    private TextMeshProUGUI effectPercentage;

    private void Awake()
    {
        effectPercentage = this.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        effectScrollbar = this.transform.GetChild(2).GetComponent<Scrollbar>();
    }

    void Start()
    {
        effectScrollbar.onValueChanged.AddListener(OnValueChangedEffect);
    }

    void Update()
    {

    }

    public void OnValueChangedEffect(float value)
    {
        value = Mathf.Round(value * 100) / 100;

        ConfigManager.Instance.effectVolume = value;
        effectPercentage.text = (value * 100).ToString() + "%";
    }
}
