using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopRerollButton : MonoBehaviour
{
    int rerollCount = 1;
    int currentRound;
    int rerollPrice; int rerollIncrease;

    TextMeshProUGUI priceText;
    ShopItemListControl shopItemListControl;

    private void Awake()
    {
        priceText = this.gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        shopItemListControl = this.gameObject.transform.parent.GetChild(3).GetComponent<ShopItemListControl>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize()
    {
        currentRound = GameRoot.Instance.GetCurrentRound();
        rerollCount = 1;
        rerollIncrease = Mathf.FloorToInt(currentRound / 2);
        if (rerollIncrease < 1)
            rerollIncrease = 1;
        rerollPrice = currentRound + rerollIncrease * rerollCount;

        priceText.text = "�ʱ�ȭ - " + rerollPrice;
    }

    public void OnClickRerollButton()
    {
        // ���� ������ ���� �䱸������ ������ ����
        if (PlayerInfo.Instance.GetCurrentWaffle() > rerollPrice)
        {
            // ���� ���� ����
            PlayerInfo.Instance.SetCurrentWaffle(PlayerInfo.Instance.GetCurrentWaffle() - rerollPrice);
            // ���� ������ ����Ʈ�� ����
            ItemManager.Instance.GetShopItemList().Clear();
            // ������ ����Ʈ UI�� Ȱ��ȭ�ϰ� ����
            shopItemListControl.SetItemListActive();
            // ���� Ʈ���� false�� ����
            shopItemListControl.SetIsRenewInfo(false);
            ItemManager.Instance.SetIsRenewItem(false);

            rerollCount++;

            rerollPrice = currentRound + rerollIncrease * rerollCount;

            priceText = this.gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
            priceText.text = "�ʱ�ȭ - " + rerollPrice;
        }
    }
}
