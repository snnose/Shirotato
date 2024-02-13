using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopNextRoundButton : MonoBehaviour
{
    private TimerControl timerControl;

    private void Awake()
    {
        timerControl = this.gameObject.transform.parent.parent.GetChild(2).gameObject.GetComponent<TimerControl>();
    }

    void Start()
    {

    }

    // ���� ����� �̵��ϴ� ��ư
    public void OnClickNextRoundButton()
    {
        // GameRoot �ʱ�ȭ
        GameRoot.Instance.SetIsRoundClear(false);
        GameRoot.Instance.SetCurrentRound(GameRoot.Instance.GetCurrentRound() + 1);
        GameRoot.Instance.floatingShopUI = GameRoot.Instance.FloatingShopUI();

        // �ش� ������ ������ ������ �����Ѵ� (���� ����, ���� �ð�)
        timerControl.SetRemainTime(3f);
        timerControl.SetTimerText(3f.ToString());

        // �÷��̾��� ���� �ʱ�ȭ
        GameObject playerBox = GameRoot.Instance.GetPlayerBox();
        playerBox.transform.position = new Vector2(0f, 0f);

        // ���� �ʱ�ȭ
        ShopUIControl.Instance.GetShopItemListControl().SetItemListActive();

        // ������ ����Ʈ UI �ʱ�ȭ
        ShopUIControl.Instance.GetShopItemListControl().SetIsRenewInfo(false);
        
        // ������ ����Ʈ �ʱ�ȭ
        ItemManager.Instance.GetShopItemList().Clear();
        ItemManager.Instance.SetIsRenewItem(false);
        
        ShopUIControl.Instance.GetShopRerollButton().Initialize(); // ���� ��� �ʱ�ȭ

        // ����UI ��Ȱ��ȭ
        GameRoot.Instance.shopUI.SetActive(false);

        // ���� ����
        WeaponManager.Instance.equipWeapons = WeaponManager.Instance.EquipWeapons();

        // Ÿ�ӽ����� ����ȭ
        Time.timeScale = 1f;
    }
}
