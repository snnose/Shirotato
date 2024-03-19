using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundInit : MonoBehaviour
{
    private TimerControl timerControl;

    private static RoundInit instance;
    public static RoundInit Instance
    {
        get
        {
            if (null == instance)
                return null;

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        timerControl = GameObject.FindGameObjectWithTag("Timer").GetComponent<TimerControl>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator InitRound()
    {
        // GameRoot 초기화
        GameRoot.Instance.SetIsRoundClear(false);
        GameRoot.Instance.SetCurrentRound(GameRoot.Instance.GetCurrentRound() + 1);

        // 실시간 스탯 관리자 초기화
        // EpicItem30 보유 시 최대 체력의 50%로 라운드 시작
        RealtimeInfoManager.Instance.SetCurrentHP(ActivateEpicItem30(PlayerInfo.Instance.GetHP()));
        RealtimeInfoManager.Instance.SetHP(PlayerInfo.Instance.GetHP());
        StartCoroutine(RealtimeInfoManager.Instance.HPRecovery());
        StartCoroutine(RealtimeInfoManager.Instance.ActivateEpicItem21());
        StartCoroutine(RealtimeInfoManager.Instance.ActivateEpicItem36());

        // NormalItem45를 샀다면 HP 1로 시작
        if (ItemManager.Instance.GetOwnNormalItemList()[45] > 0)
        {
            RealtimeInfoManager.Instance.SetCurrentHP(1);
            ItemManager.Instance.GetOwnNormalItemList()[45] = 0;
        }

        // 라운드 제한 시간 조정
        float remainTime = 15f + GameRoot.Instance.GetCurrentRound() * 5f;
        if (remainTime > 60f)
            remainTime = 60f;
        timerControl.gameObject.SetActive(true);
        GameRoot.Instance.SetRemainTime(remainTime);
        timerControl.SetTimerText(remainTime.ToString());

        // SpawnManager 초기화
        SpawnManager.Instance.startSpawn = SpawnManager.Instance.StartSpawn(GameRoot.Instance.GetCurrentRound());

        // 플레이어의 상태 초기화
        GameObject playerBox = GameRoot.Instance.GetPlayerBox();
        PlayerInfo playerInfo = playerBox.transform.GetChild(0).GetComponent<PlayerInfo>();
        playerBox.transform.position = new Vector2(0f, 0f);
        playerInfo.SetMovementSpeed(playerInfo.GetMovementSpeed() * (1 + playerInfo.GetMovementSpeedPercent() / 100));
        PlayerControl.Instance.SetMovementSpeed(PlayerInfo.Instance.GetMovementSpeed());
        // 서거나 움직일 때 능력치 변동이 있는 아이템 코루틴 장전
        PlayerControl.Instance.activateEpicItem20 = PlayerControl.Instance.ActivateEpicItem20();
        PlayerControl.Instance.activateEpicItem22 = PlayerControl.Instance.ActivateEpicItem22();
        PlayerControl.Instance.activateEpicItem33 = PlayerControl.Instance.ActivateEpicItem33();

        // 업그레이드 초기화
        // 잠시 업그레이드 매니저 활성화
        UpgradeManager.Instance.gameObject.SetActive(true);
        // 업그레이드 리롤 비용을 초기화한다
        UpgradeManager.Instance.InitReroll();
        // 업그레이드 리롤 텍스트 변경
        UpgradeManager.Instance.SetTextToCurrentCost();
        // 다시 비활성화
        UpgradeManager.Instance.gameObject.SetActive(false);

        // 상점 초기화
        // 상점 품목 리스트를 모두 활성화한다
        ShopUIControl.Instance.GetShopItemListControl().SetItemListActive();
        // 상점 제목 변경
        ShopUIControl.Instance.GetShopTitleControl().SetTitleText(GameRoot.Instance.GetCurrentRound());
        // 아이템 리스트 UI 초기화
        ShopUIControl.Instance.GetShopItemListControl().SetIsRenewInfo(false);
        // 아이템 리스트 초기화
        ClearShopItemList();
        ItemManager.Instance.SetIsRenewItem(false);
        // 상점 리롤 비용 초기화
        ShopUIControl.Instance.GetShopRerollButton().Initialize(); 
        // 상점UI 비활성화
        GameRoot.Instance.shopUI.SetActive(false);

        // 아이템 효과 발동
        // RareItem30 보유 시 보유 재료의 20% 추가 획득
        ActivateRareItem30();

        // 무기 생성
        if (WeaponManager.Instance.GetCurrentWeaponList().Count != 0)
        {
            WeaponManager.Instance.equipWeapons = WeaponManager.Instance.EquipWeapons();
        }

        // 타임스케일 정상화
        Time.timeScale = 1f;

        yield return null;
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

    private void ActivateRareItem30()
    {
        if (ItemManager.Instance.GetOwnRareItemList()[30] > 0)
        {
            int currentWaffle = PlayerInfo.Instance.GetCurrentWaffle();
            PlayerInfo.Instance.SetCurrentWaffle(Mathf.FloorToInt(currentWaffle * 1.2f));
        }
    }

    private float ActivateEpicItem30(float currentHP)
    {
        float tmp = currentHP;

        if (ItemManager.Instance.GetOwnEpicItemList()[30] > 0)
        {
            tmp /= 2;
        }

        return tmp;
    }
}
