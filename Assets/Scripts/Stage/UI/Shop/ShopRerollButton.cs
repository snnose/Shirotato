using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ShopRerollButton : MonoBehaviour, IPointerEnterHandler
{
    int rerollCount = 1;
    int currentRound;
    int rerollPrice; int rerollIncrease;
    int freeRerollCount = 0;

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
        // 모든 아이템을 구매했다면
        if (ItemManager.Instance.itemPurchaseCount == 4)
        {
            // 리롤 비용이 0이 된다
            {
                rerollPrice = 0;
                SetTProtext(0);
            }
        }
    }

    public void Initialize()
    {
        currentRound = GameRoot.Instance.GetCurrentRound();
        rerollCount = 1;
        rerollIncrease = Mathf.FloorToInt(currentRound / 2);
        if (rerollIncrease < 1)
            rerollIncrease = 1;
        rerollPrice = currentRound + rerollIncrease * rerollCount;

        SetTProtext(rerollPrice);

        if (ItemManager.Instance.GetOwnRareItemList()[28] > 0)
        {
            SetFreeRerollCount(ItemManager.Instance.GetOwnRareItemList()[28]);
            SetTProtext(0);
        }
    }

    public void OnClickRerollButton()
    {
        // 버튼 클릭 시 사운드 출력
        ButtonSoundManager.Instance.PlayOnClickButtonSound1();

        if (freeRerollCount > 0)
        {
            ActivateRareItem28();
            // 현재 아이템 리스트를 비운다
            ClearShopItemList();
            return;
        }

        // 보유 와플이 리롤 요구량보다 많으면 리롤
        if (PlayerInfo.Instance.GetCurrentWaffle() > rerollPrice)
        {
            // 보유 와플 차감
            PlayerInfo.Instance.SetCurrentWaffle(PlayerInfo.Instance.GetCurrentWaffle() - rerollPrice);
            // 현재 아이템 리스트를 비운다
            ClearShopItemList();

            // 리롤 누적 횟수 증가
            rerollCount++;

            // 리롤 비용 재조정
            rerollPrice = currentRound + rerollIncrease * rerollCount;

            SetTProtext(rerollPrice);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 포인터 진입 시 사운드 출력
        ButtonSoundManager.Instance.PlayOnPointerEnterSound1();
    }
    
    private void ClearShopItemList()
    {
        List<GameObject> tmp = ItemManager.Instance.GetShopItemList();
        List<WeaponInfo> tmpInfo = ItemManager.Instance.GetShopWeaponInfoList();

        for (int i = 0; i < 4; i++)
        {
            // 잠겨있는 항목이 아니라면
            if (!ItemManager.Instance.GetIsLockItemList()[i])
            {
                // 해당 항목을 비운다.
                tmp[i] = null;
                tmpInfo[i] = null;
            }
        }

        ItemManager.Instance.SetShopItemList(tmp);
        ItemManager.Instance.SetShopWeaponInfoList(tmpInfo);

        // 아이템 구매 횟수 초기화
        ItemManager.Instance.itemPurchaseCount = 0;

        // 아이템 리스트 UI를 활성화하고 갱신
        shopItemListControl.SetItemListActive();
        // 갱신 트리거 false로 설정
        //shopItemListControl.SetIsRenewInfo(false);
        ItemManager.Instance.SetIsRenewItem(false);
        shopItemListControl.SetIsRenewInfo(false);
    }

    void ActivateRareItem28()
    { 
        // 무료 리롤 횟수 감소
        freeRerollCount--;

        // 그 다음 비용을 알려주는 텍스트를 설정
        if (freeRerollCount > 0)
        {
            SetTProtext(0);
        }
        else
        {
            SetTProtext(rerollPrice);
        }
    }

    public void SetFreeRerollCount(int count)
    {
        this.freeRerollCount = count;
    }

    public void SetTProtext(int price)
    {
        priceText.text = "초기화 - " + price;
    }
}
