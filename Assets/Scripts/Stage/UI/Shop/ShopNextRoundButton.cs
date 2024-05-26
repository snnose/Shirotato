using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ShopNextRoundButton : MonoBehaviour, IPointerEnterHandler
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
        // ��ư Ŭ�� �� ���� ���
        ButtonSoundManager.Instance.PlayOnClickButtonSound1();
        Debug.Log("���� ���� ��ư Ŭ��");
        StartCoroutine(RoundInit.Instance.InitRound());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // ������ ���� �� ���� ���
        ButtonSoundManager.Instance.PlayOnPointerEnterSound1();
    }

    private IEnumerator SetNextRoundButtonText()
    {
        buttonText.text = "���� ���� (" + (GameRoot.Instance.GetCurrentRound() + 1) + "����)";

        yield return null;
    }
}
