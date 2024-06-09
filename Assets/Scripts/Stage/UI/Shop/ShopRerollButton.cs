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
        // ��� �������� �����ߴٸ�
        if (ItemManager.Instance.itemPurchaseCount == 4)
        {
            // ���� ����� 0�� �ȴ�
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
        // ��ư Ŭ�� �� ���� ���
        ButtonSoundManager.Instance.PlayOnClickButtonSound1();

        if (freeRerollCount > 0)
        {
            ActivateRareItem28();
            // ���� ������ ����Ʈ�� ����
            ClearShopItemList();
            return;
        }

        // ���� ������ ���� �䱸������ ������ ����
        if (PlayerInfo.Instance.GetCurrentWaffle() > rerollPrice)
        {
            // ���� ���� ����
            PlayerInfo.Instance.SetCurrentWaffle(PlayerInfo.Instance.GetCurrentWaffle() - rerollPrice);
            // ���� ������ ����Ʈ�� ����
            ClearShopItemList();

            // ���� ���� Ƚ�� ����
            rerollCount++;

            // ���� ��� ������
            rerollPrice = currentRound + rerollIncrease * rerollCount;

            SetTProtext(rerollPrice);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // ������ ���� �� ���� ���
        ButtonSoundManager.Instance.PlayOnPointerEnterSound1();
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

        // ������ ���� Ƚ�� �ʱ�ȭ
        ItemManager.Instance.itemPurchaseCount = 0;

        // ������ ����Ʈ UI�� Ȱ��ȭ�ϰ� ����
        shopItemListControl.SetItemListActive();
        // ���� Ʈ���� false�� ����
        //shopItemListControl.SetIsRenewInfo(false);
        ItemManager.Instance.SetIsRenewItem(false);
        shopItemListControl.SetIsRenewInfo(false);
    }

    void ActivateRareItem28()
    { 
        // ���� ���� Ƚ�� ����
        freeRerollCount--;

        // �� ���� ����� �˷��ִ� �ؽ�Ʈ�� ����
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
        priceText.text = "�ʱ�ȭ - " + price;
    }
}
