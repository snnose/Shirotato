using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HPControl : MonoBehaviour
{
    public Image HPBar;
    public TextMeshProUGUI HPText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetHPText(GameRoot.Instance.GetCurrentHP(), GameRoot.Instance.GetMaxHP());
        ChangeHPGageAmount(GameRoot.Instance.GetCurrentHP() / GameRoot.Instance.GetMaxHP());
    }

    private void SetHPText(float currentHP, float MaxHP)
    {
        HPText.text = currentHP.ToString() + " / " + MaxHP.ToString();
    }

    private void ChangeHPGageAmount(float amount)
    {
        HPBar.fillAmount = amount;

        if (HPBar.fillAmount <= 0f)
            HPBar.fillAmount = 0f;

        if (HPBar.fillAmount >= 1f)
            HPBar.fillAmount = 1f;
    }
}
