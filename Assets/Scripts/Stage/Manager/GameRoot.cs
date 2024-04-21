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

    // 라운드 관련 사운드
    private RoundSoundManager roundSoundManager;

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

    private int levelUpCount = 0;
    private bool isDuringUpgrade = false;

    // 아이템 습득 UI 관련 필드
    public GameObject findItemUI;
    public IEnumerator floatingFindItemUI = null;
    private int boxCount = 0;
    private bool isDuringFindItem = false;

    // 게임 오버
    private IEnumerator gameOver = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        playerBox = GameObject.FindGameObjectWithTag("GameController");
        player = GameObject.FindGameObjectWithTag("Player");
        playerInfo = PlayerInfo.Instance;
        timerControl = GameObject.FindGameObjectWithTag("Timer").GetComponent<TimerControl>();
        roundSoundManager = GameObject.FindGameObjectWithTag("AudioManager").transform.GetChild(1).GetComponent<RoundSoundManager>();

        gameOver = GameOver();

        stopRound = StopRound();

        remainTime = 20f;
        // 디버깅 용
        //remainTime = 1f;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Esc 키를 누르면 퍼즈
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUIControl.Instance.SetActive(true);
        }

        // 게임 오버 판정이 났다면
        if (isGameOver && gameOver != null)
        {
            StartCoroutine(gameOver);
            gameOver = null;
        }

        // 라운드가 끝나면 아이템 획득 UI-> 업그레이드 UI-> 상점 UI순으로 진행한다
        if (isRoundClear
            && stopRound != null)
        {
            StartCoroutine(stopRound);
            stopRound = null;
            // 라운드 종료 사운드 출력
            roundSoundManager.roundEndSound.Play();
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
            // 상점 품목 초기화
            ItemManager.Instance.SetIsRenewItem(false);
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
        Harvest();
        // EpicItem35 보유 시 효과 발동
        ActivateEpicItem35();
        // LegendItem21 보유 시 효과 발동
        ActivateLegendItem21();

        floatingFindItemUI = null;
        floatingUpgradeUI = null;
        floatingShopUI = null;

        // 와플이 플레이어에게 끌려지도록 잠시 텀을 둔다
        yield return new WaitForSeconds(3.0f);
        // 타이머 비활성화
        timerControl.gameObject.SetActive(false);

        // UI 출력 코루틴을 입력
        floatingFindItemUI = FloatingFindItemUI();
        floatingUpgradeUI = FloatingUpgradeUI();
        floatingShopUI = FloatingShopUI();

        yield return null;
    }

    private void Harvest()
    {
        float currExp = ExpManager.Instance.GetCurrentExp();
        currExp += playerInfo.GetHarvest();
        ExpManager.Instance.SetCurrentExp(currExp);
        ExpManager.Instance.levelUp = ExpManager.Instance.LevelUp();

        // 와플 획득 처리
        int currWaffle = playerInfo.GetCurrentWaffle();
        currWaffle += Mathf.FloorToInt(playerInfo.GetHarvest());
        playerInfo.SetCurrentWaffle(currWaffle);
        RenewWaffleAmount.Instance.renewCurrentWaffleAmount = RenewWaffleAmount.Instance.RenewCurrentWaffleAmount();

        // 수확은 매 라운드 종료 시 5% 씩 증가한다
        // EpicItem24 보유 시 8%p가 추가로 증가한다 (13% 증가)
        float harvestIncrease = 1.05f + ActivateEpicItem24();
        playerInfo.SetHarvest(Mathf.CeilToInt(playerInfo.GetHarvest() * harvestIncrease));
    }

    private float ActivateEpicItem24()
    {
        float tmp = 0f;

        if (ItemManager.Instance.GetOwnEpicItemList()[24] > 0)
            tmp = 0.08f;

        return tmp;
    }

    // EpicItem35 보유 시 라운드 종료마다 대미지% +3%
    private void ActivateEpicItem35()
    {
        int itemCount = ItemManager.Instance.GetOwnEpicItemList()[35];

        if (itemCount > 0)
        {
            float dmgPercent = playerInfo.GetDMGPercent() + 3f * itemCount;
            playerInfo.SetDMGPercent(dmgPercent);
        }
    }

    // LegendItem15 보유 시, 상점 입장 때 랜덤 무기 업그레이드. 업그레이드 할 무기 없으면 방어력 +2
    private void ActivateLegendItem15()
    {
        if (ItemManager.Instance.GetOwnLegendItemList()[15] > 0)
        {
            // 보유 무기가 하나라도 있다면
            if (WeaponManager.Instance.GetCurrentWeaponList().Count > 0)
            {
                // 보유 무기 중 하나를 선택해 등급을 상승시킨다.
                int random = Random.Range(0, WeaponManager.Instance.GetCurrentWeaponInfoList().Count);
                WeaponInfo selectedWeaponInfo = WeaponManager.Instance.GetCurrentWeaponInfoList()[random];

                // 선택된 무기의 등급을 한 단계 상승시킨 능력치를 적용시킨다
                int currGrade = selectedWeaponInfo.GetWeaponGrade();
                // 선택된 무기가 전설 등급이면 방어력 +2
                if (currGrade == 3)
                    PlayerInfo.Instance.SetArmor(PlayerInfo.Instance.GetArmor() + 2);
                else
                    selectedWeaponInfo.SetWeaponStatus(selectedWeaponInfo.weaponName, currGrade + 1);

                // ShopOwnItemListControl의 코루틴 호출로 UI를 갱신한다
                ShopOwnWeaponListControl shopOwnWeaponListControl = ShopUIControl.Instance.GetShopOwnWeaponListControl();
                shopOwnWeaponListControl.renewOwnWeaponList = shopOwnWeaponListControl.RenewOwnWeaponList();
            }
            else
            {
                // 보유 무기가 없다면 방어력 +2
                PlayerInfo.Instance.SetArmor(PlayerInfo.Instance.GetArmor() + 2);
            }
        }
    }

    private void ActivateLegendItem21()
    {
        int itemCount = ItemManager.Instance.GetOwnLegendItemList()[21];

        if (itemCount > 0)
        {
            float HP = PlayerInfo.Instance.GetHP() + 3f * itemCount;
            int recovery = PlayerInfo.Instance.GetRecovery() + 1 * itemCount;
            float HPDrain = PlayerInfo.Instance.GetHPDrain() + 1f * itemCount;

            PlayerInfo.Instance.SetHP(HP);
            PlayerInfo.Instance.SetRecovery(recovery);
            PlayerInfo.Instance.SetHPDrain(HPDrain);
        }
    }

    // 게임 오버 절차 실행
    private IEnumerator GameOver()
    {
        // 플레이어가 죽는다
        Destroy(playerBox);
        // 시간 정지
        Time.timeScale = 0.0f;
        // 패배 UI 활성화
        DefeatUIControl.Instance.SetActive(true);

        // 3초 후에 패배 UI를 비활성화 후 게임 결과 UI 활성화
        yield return new WaitForSecondsRealtime(3.0f);
        DefeatUIControl.Instance.SetActive(false);
        GameResultUIControl.Instance.SetActive(true);
    }

    public IEnumerator FloatingShopUI()
    {
        yield return StartCoroutine(Sleep(1.0f));
        shopUI.SetActive(true);

        // LegendItem15 보유 시 해당 아이템 효과 발동
        // 상점 입장 때 랜덤 무기 업그레이드. 무기 없으면 방어력 +2
        ActivateLegendItem15();
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
        int currentUpgradeLevel = UpgradeManager.Instance.GetCurrentUpgradeLevel() + 1;
        UpgradeManager.Instance.SetCurrentUpgradeLevel(currentUpgradeLevel);

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

    public void SetIsGameOver(bool isGameOver)
    {
        this.isGameOver = isGameOver;
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

    public bool GetIsGameOver()
    {
        return this.isGameOver;
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
