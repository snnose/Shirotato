using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopNextRoundButton : MonoBehaviour
{
    private TextMeshProUGUI buttonText;

    IEnumerator setNextRoundButtonText = null;

    private void Awake()
    {
        buttonText = this.gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {

    }

    private void Update()
    {
        if (GameRoot.Instance.GetIsRoundClear())
            setNextRoundButtonText = SetNextRoundButtonText();

        if (setNextRoundButtonText != null)
            StartCoroutine(setNextRoundButtonText);
    }

    // ���� ����� �̵��ϴ� ��ư
    public void OnClickNextRoundButton()
    {
        StartCoroutine(RoundInit.Instance.InitRound());
    }

    private IEnumerator SetNextRoundButtonText()
    {
        buttonText.text = "���� ���� (" + (GameRoot.Instance.GetCurrentRound() + 1) + "����)";

        yield return null;
    }
}
