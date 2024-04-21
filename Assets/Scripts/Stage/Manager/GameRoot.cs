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

    // �÷��̾� ���� �ʵ�
    private GameObject playerBox;
    private GameObject player;
    private PlayerInfo playerInfo;

    // ���� ���� ����
    private RoundSoundManager roundSoundManager;

    // ���� ���� �ʵ�
    private TimerControl timerControl;

    private bool isGameOver = false;
    private bool isRoundClear = false;

    private int currentRound = 1;
    private float remainTime;

    public IEnumerator stopRound = null;

    // ���� UI ���� �ʵ�
    public GameObject shopUI;
    public IEnumerator floatingShopUI = null;

    // ���׷��̵� UI ���� �ʵ�
    public GameObject upgradeUI;
    public IEnumerator floatingUpgradeUI = null;

    private int levelUpCount = 0;
    private bool isDuringUpgrade = false;

    // ������ ���� UI ���� �ʵ�
    public GameObject findItemUI;
    public IEnumerator floatingFindItemUI = null;
    private int boxCount = 0;
    private bool isDuringFindItem = false;

    // ���� ����
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
        // ����� ��
        //remainTime = 1f;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Esc Ű�� ������ ����
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUIControl.Instance.SetActive(true);
        }

        // ���� ���� ������ ���ٸ�
        if (isGameOver && gameOver != null)
        {
            StartCoroutine(gameOver);
            gameOver = null;
        }

        // ���尡 ������ ������ ȹ�� UI-> ���׷��̵� UI-> ���� UI������ �����Ѵ�
        if (isRoundClear
            && stopRound != null)
        {
            StartCoroutine(stopRound);
            stopRound = null;
            // ���� ���� ���� ���
            roundSoundManager.roundEndSound.Play();
        }

        // �ڽ��� ����ߴٸ�
        if (isRoundClear
            && boxCount > 0
            && !isDuringFindItem
            && floatingFindItemUI != null)
        {
            StartCoroutine(floatingFindItemUI);
            floatingFindItemUI = null;
        }

        // �������� �ߴٸ�
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

        // ���� Ŭ���� �� ���׷��̵� ���� ��
        if (isRoundClear 
            && boxCount == 0
            && levelUpCount == 0
            && !isDuringFindItem
            && !isDuringUpgrade
            && floatingShopUI != null)
        {
            // �ν��Ͻ�ȭ�� ������� �ı��Ѵ�
            WeaponManager.Instance.destroyWeapons = WeaponManager.Instance.DestroyWeapons();
            // ���� UI�� ����
            StartCoroutine(floatingShopUI);
            // ���� UI ��ġ ����
            ShopUIControl.Instance.transform.position = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
            // ���� ǰ�� �ʱ�ȭ
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
        // ��Ȯ �ɷ�ġ�� ����� ����ġ�� ���� ȹ��
        Harvest();
        // EpicItem35 ���� �� ȿ�� �ߵ�
        ActivateEpicItem35();
        // LegendItem21 ���� �� ȿ�� �ߵ�
        ActivateLegendItem21();

        floatingFindItemUI = null;
        floatingUpgradeUI = null;
        floatingShopUI = null;

        // ������ �÷��̾�� ���������� ��� ���� �д�
        yield return new WaitForSeconds(3.0f);
        // Ÿ�̸� ��Ȱ��ȭ
        timerControl.gameObject.SetActive(false);

        // UI ��� �ڷ�ƾ�� �Է�
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

        // ���� ȹ�� ó��
        int currWaffle = playerInfo.GetCurrentWaffle();
        currWaffle += Mathf.FloorToInt(playerInfo.GetHarvest());
        playerInfo.SetCurrentWaffle(currWaffle);
        RenewWaffleAmount.Instance.renewCurrentWaffleAmount = RenewWaffleAmount.Instance.RenewCurrentWaffleAmount();

        // ��Ȯ�� �� ���� ���� �� 5% �� �����Ѵ�
        // EpicItem24 ���� �� 8%p�� �߰��� �����Ѵ� (13% ����)
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

    // EpicItem35 ���� �� ���� ���Ḷ�� �����% +3%
    private void ActivateEpicItem35()
    {
        int itemCount = ItemManager.Instance.GetOwnEpicItemList()[35];

        if (itemCount > 0)
        {
            float dmgPercent = playerInfo.GetDMGPercent() + 3f * itemCount;
            playerInfo.SetDMGPercent(dmgPercent);
        }
    }

    // LegendItem15 ���� ��, ���� ���� �� ���� ���� ���׷��̵�. ���׷��̵� �� ���� ������ ���� +2
    private void ActivateLegendItem15()
    {
        if (ItemManager.Instance.GetOwnLegendItemList()[15] > 0)
        {
            // ���� ���Ⱑ �ϳ��� �ִٸ�
            if (WeaponManager.Instance.GetCurrentWeaponList().Count > 0)
            {
                // ���� ���� �� �ϳ��� ������ ����� ��½�Ų��.
                int random = Random.Range(0, WeaponManager.Instance.GetCurrentWeaponInfoList().Count);
                WeaponInfo selectedWeaponInfo = WeaponManager.Instance.GetCurrentWeaponInfoList()[random];

                // ���õ� ������ ����� �� �ܰ� ��½�Ų �ɷ�ġ�� �����Ų��
                int currGrade = selectedWeaponInfo.GetWeaponGrade();
                // ���õ� ���Ⱑ ���� ����̸� ���� +2
                if (currGrade == 3)
                    PlayerInfo.Instance.SetArmor(PlayerInfo.Instance.GetArmor() + 2);
                else
                    selectedWeaponInfo.SetWeaponStatus(selectedWeaponInfo.weaponName, currGrade + 1);

                // ShopOwnItemListControl�� �ڷ�ƾ ȣ��� UI�� �����Ѵ�
                ShopOwnWeaponListControl shopOwnWeaponListControl = ShopUIControl.Instance.GetShopOwnWeaponListControl();
                shopOwnWeaponListControl.renewOwnWeaponList = shopOwnWeaponListControl.RenewOwnWeaponList();
            }
            else
            {
                // ���� ���Ⱑ ���ٸ� ���� +2
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

    // ���� ���� ���� ����
    private IEnumerator GameOver()
    {
        // �÷��̾ �״´�
        Destroy(playerBox);
        // �ð� ����
        Time.timeScale = 0.0f;
        // �й� UI Ȱ��ȭ
        DefeatUIControl.Instance.SetActive(true);

        // 3�� �Ŀ� �й� UI�� ��Ȱ��ȭ �� ���� ��� UI Ȱ��ȭ
        yield return new WaitForSecondsRealtime(3.0f);
        DefeatUIControl.Instance.SetActive(false);
        GameResultUIControl.Instance.SetActive(true);
    }

    public IEnumerator FloatingShopUI()
    {
        yield return StartCoroutine(Sleep(1.0f));
        shopUI.SetActive(true);

        // LegendItem15 ���� �� �ش� ������ ȿ�� �ߵ�
        // ���� ���� �� ���� ���� ���׷��̵�. ���� ������ ���� +2
        ActivateLegendItem15();
        yield return null;
    }

    public IEnumerator FloatingUpgradeUI()
    {
        //yield return new WaitForSeconds(3.0f);

        // ���׷��̵� UI Ȱ��ȭ
        upgradeUI.SetActive(true);
        upgradeUI.transform.position = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        isDuringUpgrade = true;

        // ���� ���׷��̵� ������ 1 ���
        int currentUpgradeLevel = UpgradeManager.Instance.GetCurrentUpgradeLevel() + 1;
        UpgradeManager.Instance.SetCurrentUpgradeLevel(currentUpgradeLevel);

        // ���� ���׷��̵� ī��Ʈ ����
        levelUpCount--;
        yield return null;

        // ���׷��̵� ����Ʈ ����
        UpgradeManager.Instance.renewUpgradeList = UpgradeManager.Instance.RenewUpgradeList();
        yield return null;
    }

    public IEnumerator FloatingFindItemUI()
    {
        findItemUI.SetActive(true);
        isDuringFindItem = true;

        boxCount--;

        // �߰� ������ ����
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
