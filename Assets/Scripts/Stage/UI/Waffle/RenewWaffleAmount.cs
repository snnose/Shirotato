using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RenewWaffleAmount : MonoBehaviour
{
    public TextMeshProUGUI waffleAmount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RenewAmount();
    }

    private void RenewAmount()
    {
        string amount = PlayerInfo.Instance.GetCurrentWaffle().ToString();
        waffleAmount.text = amount;
    }
}
