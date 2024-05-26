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

    // 다음 라운드로 이동하는 버튼
    public void OnClickNextRoundButton()
    {
        // 버튼 클릭 시 사운드 출력
        ButtonSoundManager.Instance.PlayOnClickButtonSound1();
        Debug.Log("다음 라운드 버튼 클릭");
        StartCoroutine(RoundInit.Instance.InitRound());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 포인터 진입 시 사운드 출력
        ButtonSoundManager.Instance.PlayOnPointerEnterSound1();
    }

    private IEnumerator SetNextRoundButtonText()
    {
        buttonText.text = "다음 라운드 (" + (GameRoot.Instance.GetCurrentRound() + 1) + "라운드)";

        yield return null;
    }
}
