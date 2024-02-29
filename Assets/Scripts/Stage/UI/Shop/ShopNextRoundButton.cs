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

    // 다음 라운드로 이동하는 버튼
    public void OnClickNextRoundButton()
    {
        // GameRoot 초기화
        GameRoot.Instance.SetIsRoundClear(false);
        GameRoot.Instance.SetCurrentRound(GameRoot.Instance.GetCurrentRound() + 1);
        GameRoot.Instance.floatingShopUI = GameRoot.Instance.FloatingShopUI();

        // 해당 라운드의 정보를 가져와 적용한다 (몬스터 정보, 제한 시간)
        timerControl.SetRemainTime(3f);
        timerControl.SetTimerText(3f.ToString());

        // 플레이어의 상태 초기화
        GameObject playerBox = GameRoot.Instance.GetPlayerBox();
        playerBox.transform.position = new Vector2(0f, 0f);

        // 상점 초기화
        ShopUIControl.Instance.GetShopItemListControl().SetItemListActive();

        // 상점 제목 변경
        ShopUIControl.Instance.GetShopTitleControl().SetTitleText(GameRoot.Instance.GetCurrentRound());

        // 아이템 리스트 UI 초기화
        ShopUIControl.Instance.GetShopItemListControl().SetIsRenewInfo(false);

        // 아이템 리스트 초기화
        ClearShopItemList();
        ItemManager.Instance.SetIsRenewItem(false);
        
        ShopUIControl.Instance.GetShopRerollButton().Initialize(); // 리롤 비용 초기화

        // 상점UI 비활성화
        GameRoot.Instance.shopUI.SetActive(false);

        // 무기 생성
        WeaponManager.Instance.equipWeapons = WeaponManager.Instance.EquipWeapons();

        // 타임스케일 정상화
        Time.timeScale = 1f;
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
    }

    private IEnumerator SetNextRoundButtonText()
    {
        buttonText.text = "다음 라운드 (" + GameRoot.Instance.GetCurrentRound() + "라운드)";

        yield return null;
    }
}
