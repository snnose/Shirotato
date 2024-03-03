using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpBarControl : MonoBehaviour
{
    private Image ExpBar;
    private TextMeshProUGUI ExpText;

    private void Awake()
    {
        ExpBar = this.gameObject.transform.GetChild(1).GetComponent<Image>();
        ExpText = this.gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        SetExpText(ExpManager.Instance.GetCurrentLevel());
        ChangeExpGageAmount(ExpManager.Instance.GetCurrentExp() / ExpManager.Instance.GetDemandExp());
    }

    private void SetExpText(float currentLevel)
    {
        ExpText.text = "Lv." + currentLevel.ToString();
    }

    private void ChangeExpGageAmount(float amount)
    {
        ExpBar.fillAmount = amount;

        if (ExpBar.fillAmount <= 0f)
            ExpBar.fillAmount = 0f;

        if (ExpBar.fillAmount >= 1f)
            ExpBar.fillAmount = 1f;
    }
}
