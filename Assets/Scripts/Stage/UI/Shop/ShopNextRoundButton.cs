using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopNextRoundButton : MonoBehaviour
{
    private TimerControl timerControl;

    private TextMeshProUGUI buttonText;

    IEnumerator setNextRoundButtonText = null;

    private void Awake()
    {
        timerControl = this.gameObject.transform.parent.parent.GetChild(2).gameObject.GetComponent<TimerControl>();
        buttonText = this.gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {

    }

    private void Update()
    {
        if (GameRoot.Instance.GetIsRoundClear())
            setNextRoundButtonText = SetNextRoundButtonText();

        if (setNextRoundButtonText != null)
            StartCoroutine(setNextRoundButtonText);
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

        // ���� ���� ����
        ShopUIControl.Instance.GetShopTitleControl().SetTitleText(GameRoot.Instance.GetCurrentRound());

        // ������ ����Ʈ UI �ʱ�ȭ
        ShopUIControl.Instance.GetShopItemListControl().SetIsRenewInfo(false);

        // ������ ����Ʈ �ʱ�ȭ
        ClearShopItemList();
        ItemManager.Instance.SetIsRenewItem(false);
        
        ShopUIControl.Instance.GetShopRerollButton().Initialize(); // ���� ��� �ʱ�ȭ

        // ����UI ��Ȱ��ȭ
        GameRoot.Instance.shopUI.SetActive(false);

        // ���� ����
        WeaponManager.Instance.equipWeapons = WeaponManager.Instance.EquipWeapons();

        // Ÿ�ӽ����� ����ȭ
        Time.timeScale = 1f;
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
    }

    private IEnumerator SetNextRoundButtonText()
    {
        buttonText.text = "���� ���� (" + GameRoot.Instance.GetCurrentRound() + "����)";

        yield return null;
    }
}
