using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RenewWaffleAmount : MonoBehaviour
{
    private static RenewWaffleAmount instance;
    public static RenewWaffleAmount Instance
    {
        get
        {
            if (null == instance)
                return null;

            return instance;
        }
    }

    private GameObject currentWaffleUI;
    private GameObject storedWaffleUI;

    private TextMeshProUGUI currentWaffleAmount;
    private TextMeshProUGUI storedWaffleAmount;

    public IEnumerator renewCurrentWaffleAmount;
    public IEnumerator renewStoredWaffleAmount;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        this.currentWaffleUI = this.gameObject.transform.GetChild(0).gameObject;
        this.storedWaffleUI = this.gameObject.transform.GetChild(1).gameObject;

        this.currentWaffleAmount = currentWaffleUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        this.storedWaffleAmount = storedWaffleUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        renewCurrentWaffleAmount = RenewCurrentWaffleAmount();
        renewStoredWaffleAmount = RenewStoredWaffleAmount();
    }

    // Update is called once per frame
    void Update()
    {
        if (renewCurrentWaffleAmount != null)
        {
            StartCoroutine(renewCurrentWaffleAmount);
        }

        if (renewStoredWaffleAmount != null)
        {
            StartCoroutine(renewStoredWaffleAmount);
        }
    }

    public IEnumerator RenewCurrentWaffleAmount()
    {
        currentWaffleAmount.text = PlayerInfo.Instance.GetCurrentWaffle().ToString();
        yield return null;
    }

    public IEnumerator RenewStoredWaffleAmount()
    {
        storedWaffleAmount.text = PlayerInfo.Instance.GetStoredWaffle().ToString();
        yield return null;
    }

    private void RenewAmount()
    {
        string amount = PlayerInfo.Instance.GetCurrentWaffle().ToString();
        //waffleAmount.text = amount;
    }

    public GameObject GetStoredWaffleUI()
    {
        return this.storedWaffleUI;
    }
}
