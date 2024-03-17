using System;
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

        if (ItemManager.Instance.GetOwnRareItemList()[28] > 0)
        {
            freeRerollCount = ItemManager.Instance.GetOwnRareItemList()[28];
            priceText.text = "�ʱ�ȭ - 0";
        }
    }

    public void OnClickRerollButton()
    {
        if (freeRerollCount > 0)
        {
            ActivateRareItem28();
            return;
        }

        // ���� ������ ���� �䱸������ ������ ����
        if (PlayerInfo.Instance.GetCurrentWaffle() > rerollPrice)
        {
            // ���� ���� ����
            PlayerInfo.Instance.SetCurrentWaffle(PlayerInfo.Instance.GetCurrentWaffle() - rerollPrice);
            // ���� ������ ����Ʈ�� ����
            ClearShopItemList();
            // ������ ����Ʈ UI�� Ȱ��ȭ�ϰ� ����
            shopItemListControl.SetItemListActive();
            // ���� Ʈ���� false�� ����
            shopItemListControl.SetIsRenewInfo(false);
            ItemManager.Instance.SetIsRenewItem(false);

            // ���� ���� Ƚ�� ����
            rerollCount++;

            // ���� ��� ������
            rerollPrice = currentRound + rerollIncrease * rerollCount;

            priceText.text = "�ʱ�ȭ - " + rerollPrice;
        }
    }
    
    private void ClearShopItemList()
    {
        List<GameObject> tmp = ItemManager.Instance.GetShopItemList();
        List<WeaponInfo> tmpInfo = ItemManager.Instance.GetShopWeaponInfoList();

        for (int i = 0; i < 4; i++)
        {
            // ����ִ� �׸��� �ƴ϶��
            if (!ItemManager.Instance.GetIsLockItemList()[i])
            {
                // �ش� �׸��� ����.
                tmp[i] = null;
                tmpInfo[i] = null;
            }
        }

        ItemManager.Instance.SetShopItemList(tmp);
        ItemManager.Instance.SetShopWeaponInfoList(tmpInfo);
    }

    void ActivateRareItem28()
    {
        // ���� ������ ����Ʈ�� ����
        ClearShopItemList();
        // ������ ����Ʈ UI�� Ȱ��ȭ�ϰ� ����
        shopItemListControl.SetItemListActive();
        // ���� Ʈ���� false�� ����
        shopItemListControl.SetIsRenewInfo(false);
        ItemManager.Instance.SetIsRenewItem(false);

        // ���� ���� Ƚ�� ����
        freeRerollCount--;

        // �� ���� ����� �˷��ִ� �ؽ�Ʈ�� ����
        if (freeRerollCount > 0)
        {
            priceText.text = "�ʱ�ȭ - 0";
        }
        else
        {
            priceText.text = "�ʱ�ȭ - " + rerollPrice;
        }
    }
}
