using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopTitleControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SetTitleText(GameRoot.Instance.GetCurrentRound());
    }

    public void SetTitleText(int round)
    {
        // ���� ������ ���� ���� (n ����)�� ����
        this.gameObject.GetComponent<TextMeshProUGUI>().text =
            "���� (" + round + " ����)";
    }
}
