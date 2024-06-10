using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUIControl : MonoBehaviour
{
    // singleton
    private static PauseUIControl instance;
    public static PauseUIControl Instance
    {
        get
        {
            if (null == instance)
                return null;

            return instance;
        }
    }

    private ShopOwnItemListControl shopOwnItemListControl;
    private ShopOwnWeaponListControl shopOwnWeaponListControl;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        shopOwnWeaponListControl = this.transform.GetChild(4).GetComponent<ShopOwnWeaponListControl>();
        shopOwnItemListControl = this.transform.GetChild(5).GetComponent<ShopOwnItemListControl>();
    }

    void Start()
    {
        SetActive(false);
    }

    void Update()
    {
        
    }

    public void SetActive(bool ret)
    {
        if (ret)
        {
            this.gameObject.SetActive(ret);
            this.transform.position = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
            // 보유 무기, 아이템 리스트 갱신
            StartCoroutine(shopOwnItemListControl.RenewOwnItemList());
            shopOwnWeaponListControl.renewOwnWeaponList = shopOwnWeaponListControl.RenewOwnWeaponList();

            Time.timeScale = 0.0f;
        }
        else
        {
            this.transform.position = new Vector2(0, -Screen.height - 100);
            this.gameObject.SetActive(ret);
            
            // 게임 오버 상태가 아닐 때만 시간이 가도록 한다
            if (!GameRoot.Instance.GetIsGameOver() || GameRoot.Instance.GetGameClear() != null)
                Time.timeScale = 1.0f;
        }
    }
}
