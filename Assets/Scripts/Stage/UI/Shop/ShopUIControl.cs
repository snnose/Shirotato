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

    private ShopTitleControl shopTitleControl;
    private ShopRerollButton shopRerollButton;
    private ShopItemListControl shopItemListControl;
    private ShopOwnWeaponListControl shopOwnWeaponListControl;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        shopTitleControl = this.gameObject.GetComponentInChildren<ShopTitleControl>();
        shopItemListControl = this.gameObject.transform.GetChild(3).gameObject.GetComponent<ShopItemListControl>();
        shopOwnWeaponListControl = this.gameObject.transform.GetChild(5).gameObject.GetComponent<ShopOwnWeaponListControl>();
        shopRerollButton = this.gameObject.transform.GetChild(8).gameObject.GetComponent<ShopRerollButton>();
    }

    void Start()
    {
        this.transform.position = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        this.gameObject.SetActive(false);
    }

    public ShopTitleControl GetShopTitleControl()
    {
        return this.shopTitleControl;
    }

    public ShopRerollButton GetShopRerollButton()
    {
        return this.shopRerollButton;
    }

    public ShopItemListControl GetShopItemListControl()
    {
        return this.shopItemListControl;
    }

    public ShopOwnWeaponListControl GetShopOwnWeaponListControl()
    {
        return this.shopOwnWeaponListControl;
    }
}
