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

    private int levelUpCount = 1;
    private bool isDuringUpgrade = false;

    // ������ ���� UI ���� �ʵ�
    public GameObject findItemUI;
    public IEnumerator floatingFindItemUI = null;
    private int boxCount = 1;
    private bool isDuringFindItem = false;

    // ���� ���� ���� �ʵ�

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
        // ���尡 ������ ������ ȹ�� UI-> ���׷��̵� UI-> ���� UI������ �����Ѵ�
        if (isRoundClear
            && stopRound != null)
        {
            StartCoroutine(stopRound);
            stopRound = null;
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
        // ����ġ ȹ�� ó��
        float currExp = ExpManager.Instance.GetCurrentExp();
        currExp += RealtimeInfoManager.Instance.GetHarvest();
        ExpManager.Instance.SetCurrentExp(currExp);
        ExpManager.Instance.levelUp = ExpManager.Instance.LevelUp();
        // ���� ȹ�� ó��
        int currWaffle = playerInfo.GetCurrentWaffle();
        currWaffle += Mathf.FloorToInt(RealtimeInfoManager.Instance.GetHarvest());
        playerInfo.SetCurrentWaffle(currWaffle);
        // ��Ȯ�� �� ���� ���� �� 5% �� �����Ѵ�
        playerInfo.SetHarvest(playerInfo.GetHarvest() * 1.05f);

        // ������ �÷��̾�� ���������� ��� ���� �д�
        yield return StartCoroutine(Sleep(3.0f));

        // Ÿ�̸� ��Ȱ��ȭ
        timerControl.gameObject.SetActive(false);

        // UI ��� �ڷ�ƾ�� �Է�
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

        // ���׷��̵� UI Ȱ��ȭ
        upgradeUI.SetActive(true);
        upgradeUI.transform.position = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        isDuringUpgrade = true;

        // ���� ���׷��̵� ������ 1 ���
        int currentUpgradeLevel = UpgradeManager.Instance.GetCurrentUpgradeLevel();
        UpgradeManager.Instance.SetCurrentUpgradeLevel(currentUpgradeLevel++);

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
