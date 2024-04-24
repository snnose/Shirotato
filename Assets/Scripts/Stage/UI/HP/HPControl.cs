using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HPControl : MonoBehaviour
{
    public Image HPBar;
    public TextMeshProUGUI HPText;

    void Start()
    {
        
    }

    void Update()
    {
        SetHPText(RealtimeInfoManager.Instance.GetCurrentHP(), RealtimeInfoManager.Instance.GetHP());
        ChangeHPGageAmount(RealtimeInfoManager.Instance.GetCurrentHP() / RealtimeInfoManager.Instance.GetHP());
    }

    private void SetHPText(float currentHP, float MaxHP)
    {
        HPText.text = currentHP.ToString() + " / " + MaxHP.ToString();
    }

    // ���� ü�� ������ ���� ü�� ���� ����
    private void ChangeHPGageAmount(float amount)
    {
        HPBar.fillAmount = amount;

        if (HPBar.fillAmount <= 0f)
            HPBar.fillAmount = 0f;

        if (HPBar.fillAmount >= 1f)
            HPBar.fillAmount = 1f;
    }
}
