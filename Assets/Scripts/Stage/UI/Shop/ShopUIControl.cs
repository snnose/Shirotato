using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUIControl : MonoBehaviour
{
    private static ShopUIControl instance = null;

    public static ShopUIControl Instance
    {
        get
        {
            if (null == instance)
                return null;

            return instance;
        }
    }

    private ShopRerollButton shopRerollButton;
    private ShopItemListControl shopItemListControl;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        shopRerollButton = this.gameObject.transform.GetChild(8).gameObject.GetComponent<ShopRerollButton>();
        shopItemListControl = this.gameObject.transform.GetChild(3).gameObject.GetComponent<ShopItemListControl>();
    }

    void Start()
    {
        this.transform.position = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        this.gameObject.SetActive(false);
    }

    public ShopRerollButton GetShopRerollButton()
    {
        return this.shopRerollButton;
    }

    public ShopItemListControl GetShopItemListControl()
    {
        return this.shopItemListControl;
    }
}
