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

    private float MaxHP;
    private float currentHP;

    // 라운드 관련 필드
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

    private int levelUpCount = 0;
    private bool isDuringUpgrade = false;

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

        MaxHP = playerInfo.GetHP();
        currentHP = playerInfo.GetHP();

        stopRound = StopRound();

        //floatingShopUI = FloatingShopUI();

        remainTime = 20f;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isRoundClear
            && stopRound != null)
        {
            StartCoroutine(stopRound);
            stopRound = null;
        }
        // 라운드 클리어 및 레벨업을 했다면
        if (isRoundClear 
            && levelUpCount > 0
            && !isDuringUpgrade
            && floatingUpgradeUI != null)
        {
            StartCoroutine(floatingUpgradeUI);
            floatingUpgradeUI = null;
        }

        // 라운드 클리어 및 업그레이드 종료 시
        if (isRoundClear 
            && levelUpCount == 0
            && !isDuringUpgrade
            && floatingShopUI != null)
        {
            // 인스턴스화된 무기들을 파괴한다
            WeaponManager.Instance.destroyWeapons = WeaponManager.Instance.DestroyWeapons();
            // 상점 UI를 띄운다
            StartCoroutine(floatingShopUI);
            floatingShopUI = null;
        }
    }

    private IEnumerator Sleep(float time)
    {
        yield return new WaitForSeconds(time);
    }

    public IEnumerator StopRound()
    {
        // 와플이 플레이어에게 끌려지도록 잠시 텀을 둔다
        yield return StartCoroutine(Sleep(3.0f));
        // UI 출력 코루틴을 입력
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

    public void SetCurrentHP(float currentHP)
    {
        this.currentHP = currentHP;
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

    public float GetCurrentHP()
    {
        return this.currentHP;
    }

    public float GetMaxHP()
    {
        return this.MaxHP;
    }

    public int GetLevelUpCount()
    {
        return this.levelUpCount;
    }
}
