using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundControl : MonoBehaviour
{
    TextMeshProUGUI roundText;

    private void Awake()
    {
        roundText = this.GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        //roundText.text = "���� " + GameRoot.Instance.GetCurrentRound();
    }

    public void SetRoundText(int currentRound)
    {
        roundText.text = "���� " + currentRound;
    }
}
