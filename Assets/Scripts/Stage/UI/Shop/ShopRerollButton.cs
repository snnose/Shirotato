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

        priceText.text = "초기화 - " + rerollPrice;
    }

    public void OnClickRerollButton()
    {
        // 보유 와플이 리롤 요구량보다 많으면 리롤
        if (PlayerInfo.Instance.GetCurrentWaffle() > rerollPrice)
        {
            // 보유 와플 차감
            PlayerInfo.Instance.SetCurrentWaffle(PlayerInfo.Instance.GetCurrentWaffle() - rerollPrice);
            // 현재 아이템 리스트를 비운다
            ItemManager.Instance.GetShopItemList().Clear();
            // 아이템 리스트 UI를 활성화하고 갱신
            shopItemListControl.SetItemListActive();
            // 갱신 트리거 false로 설정
            shopItemListControl.SetIsRenewInfo(false);
            ItemManager.Instance.SetIsRenewItem(false);

            rerollCount++;

            rerollPrice = currentRound + rerollIncrease * rerollCount;

            priceText = this.gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
            priceText.text = "초기화 - " + rerollPrice;
        }
    }
}
