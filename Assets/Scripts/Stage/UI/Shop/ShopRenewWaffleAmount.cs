using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopRenewWaffleAmount : MonoBehaviour
{
    private TextMeshProUGUI waffleAmount;

    // Start is called before the first frame update
    void Start()
    {
        waffleAmount = this.gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        waffleAmount.text = PlayerInfo.Instance.GetCurrentWaffle().ToString();
    }
}
