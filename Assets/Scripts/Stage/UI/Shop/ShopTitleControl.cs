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
        // 상점 제목을 현재 상점 (n 라운드)로 변경
        this.gameObject.GetComponent<TextMeshProUGUI>().text =
            "상점 (" + round + " 라운드)";
    }
}
