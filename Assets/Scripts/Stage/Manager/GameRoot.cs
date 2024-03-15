using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameRoot : MonoBehaviour
{
    // Singleton
    private static GameRoot instance = null;

    public static GameRoot Instance
    {
        get
        {
            if (null == instance)
                return null;

            return instance;
        }
    }

    // 플레이어 관련 필드
    private GameObject playerBox;
    private GameObject player;
    private PlayerInfo playerInfo;

    // 라운드 관련 필드
    private TimerControl timerControl;

    private bool isGameOver = false;
    private bool isRoundClear = false;

    private int currentRound = 1;
    private float remainTime;

    public IEnumerator stopRound = null;

    // 상점 UI 관련 필드
    public GameObject shopUI;
    public IEnumerator floatingShopUI = null;

    // 업그레이드 UI 관련 필드
    public GameObject upgradeUI;
    public IEnumerator floatingUpgradeUI = null;

    private int levelUpCount = 1;
    private bool isDuringUpgrade = false;

    // 아이템 습득 UI 관련 필드
    public GameObject findItemUI;
    public IEnumerator floatingFindItemUI = null;
    private int boxCount = 1;
    private bool isDuringFindItem = false;

    // 라운드 정보 관련 필드

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        playerBox = GameObject.FindGameObjectWithTag("GameController");
        player = GameObject.FindGameObjectWithTag("Player");
        playerInfo = player.GetComponent<PlayerInfo>();

        timerControl = GameObject.FindGameObjectWithTag("Timer").GetComponent<TimerControl>();

        stopRound = StopRound();

        remainTime = 3f;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // 라운드가 끝나면 아이템 획득 UI-> 업그레이드 UI-> 상점 UI순으로 진행한다
        if (isRoundClear
            && stopRound != null)
        {
            StartCoroutine(stopRound);
            stopRound = null;
        }

        // 박스를 드랍했다면
        if (isRoundClear
            && boxCount > 0
            && !isDuringFindItem
            && floatingFindItemUI != null)
        {
            StartCoroutine(floatingFindItemUI);
            floatingFindItemUI = null;
        }

        // 레벨업을 했다면
        if (isRoundClear 
            && boxCount == 0
            && levelUpCount > 0
            && !isDuringFindItem
            && !isDuringUpgrade
            && floatingUpgradeUI != null)
        {
            StartCoroutine(floatingUpgradeUI);
            floatingUpgradeUI = null;
        }

        // 라운드 클리어 및 업그레이드 종료 시
        if (isRoundClear 
            && boxCount == 0
            && levelUpCount == 0
            && !isDuringFindItem
            && !isDuringUpgrade
            && floatingShopUI != null)
        {
            // 인스턴스화된 무기들을 파괴한다
            WeaponManager.Instance.destroyWeapons = WeaponManager.Instance.DestroyWeapons();
            // 상점 UI를 띄운다
            StartCoroutine(floatingShopUI);
            // 상점 UI 위치 조정
            ShopUIControl.Instance.transform.position = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
            floatingShopUI = null;
        }
    }

    private IEnumerator Sleep(float time)
    {
        yield return new WaitForSeconds(time);
    }

    public IEnumerator StopRound()
    {
        // 수확 능력치에 비례해 경험치와 와플 획득
        // 경험치 획득 처리
        float currExp = ExpManager.Instance.GetCurrentExp();
        currExp += RealtimeInfoManager.Instance.GetHarvest();
        ExpManager.Instance.SetCurrentExp(currExp);
        ExpManager.Instance.levelUp = ExpManager.Instance.LevelUp();
        // 와플 획득 처리
        int currWaffle = playerInfo.GetCurrentWaffle();
        currWaffle += Mathf.FloorToInt(RealtimeInfoManager.Instance.GetHarvest());
        playerInfo.SetCurrentWaffle(currWaffle);
        // 수확은 매 라운드 종료 시 5% 씩 증가한다
        playerInfo.SetHarvest(playerInfo.GetHarvest() * 1.05f);

        // 와플이 플레이어에게 끌려지도록 잠시 텀을 둔다
        yield return StartCoroutine(Sleep(3.0f));

        // 타이머 비활성화
        timerControl.gameObject.SetActive(false);

        // UI 출력 코루틴을 입력
        floatingFindItemUI = FloatingFindItemUI();
        floatingUpgradeUI = FloatingUpgradeUI();
        floatingShopUI = FloatingShopUI();

        yield return null;
    }

    public IEnumerator FloatingShopUI()
    {
        yield return StartCoroutine(Sleep(1.0f));
        shopUI.SetActive(true);

        yield return null;
    }

    public IEnumerator FloatingUpgradeUI()
    {
        //yield return new WaitForSeconds(3.0f);

        // 업그레이드 UI 활성화
        upgradeUI.SetActive(true);
        upgradeUI.transform.position = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        isDuringUpgrade = true;

        // 현재 업그레이드 레벨을 1 상승
        int currentUpgradeLevel = UpgradeManager.Instance.GetCurrentUpgradeLevel();
        UpgradeManager.Instance.SetCurrentUpgradeLevel(currentUpgradeLevel++);

        // 남은 업그레이드 카운트 감소
        levelUpCount--;
        yield return null;

        // 업그레이드 리스트 갱신
        UpgradeManager.Instance.renewUpgradeList = UpgradeManager.Instance.RenewUpgradeList();
        yield return null;
    }

    public IEnumerator FloatingFindItemUI()
    {
        findItemUI.SetActive(true);
        isDuringFindItem = true;

        boxCount--;

        // 발견 아이템 갱신
        findItemUI.GetComponent<FindItemUI>().renewFindItem = findItemUI.GetComponent<FindItemUI>().RenewFindItem();
        yield return null;
    }

    public void SetIsRoundClear(bool isRoundClear)
    {
        this.isRoundClear = isRoundClear;
    }

    public void SetCurrentRound(int currentRound)
    {
        this.currentRound = currentRound;
    }

    public void SetRemainTime(float remainTime)
    {
        this.remainTime = remainTime;
    }

    public void SetLevelUpCount(int count)
    {
        this.levelUpCount = count;
    }

    public void SetIsDuringUpgrade(bool ret)
    {
        this.isDuringUpgrade = ret;
    }

    public void SetBoxCount(int count)
    {
        this.boxCount = count;
    }

    public void SetIsDuringFindItem(bool ret)
    {
        this.isDuringFindItem = ret;
    }

    public GameObject GetPlayerBox()
    {
        return this.playerBox;
    }

    public int GetCurrentRound()
    {
        return this.currentRound;
    }

    public bool GetIsRoundClear()
    {
        return this.isRoundClear;
    }

    public float GetRemainTime()
    {
        return this.remainTime;
    }
    public int GetLevelUpCount()
    {
        return this.levelUpCount;
    }

    public int GetBoxCount()
    {
        return this.boxCount;
    }
}
