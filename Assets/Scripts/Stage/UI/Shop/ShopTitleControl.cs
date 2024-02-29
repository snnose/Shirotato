using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopTitleControl : MonoBehaviour
{
    private IEnumerator setTitleText = null;

    // Start is called before the first frame update
    void Update()
    {
        if (GameRoot.Instance.GetIsRoundClear())
            setTitleText = SetTitleText(GameRoot.Instance.GetCurrentRound());

        if (setTitleText != null)
            StartCoroutine(setTitleText);
    }

    public IEnumerator SetTitleText(int round)
    {
        // ���� ������ ���� ���� (n ����)�� ����
        this.gameObject.GetComponent<TextMeshProUGUI>().text =
            "���� (" + round + " ����)";

        yield return null;
    }
}
