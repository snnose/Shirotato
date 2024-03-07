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

    private float MaxHP;
    private float currentHP;

    // ���� ���� �ʵ�
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
        // ���� Ŭ���� �� �������� �ߴٸ�
        if (isRoundClear 
            && levelUpCount > 0
            && !isDuringUpgrade
            && floatingUpgradeUI != null)
        {
            StartCoroutine(floatingUpgradeUI);
            floatingUpgradeUI = null;
        }

        // ���� Ŭ���� �� ���׷��̵� ���� ��
        if (isRoundClear 
            && levelUpCount == 0
            && !isDuringUpgrade
            && floatingShopUI != null)
        {
            // �ν��Ͻ�ȭ�� ������� �ı��Ѵ�
            WeaponManager.Instance.destroyWeapons = WeaponManager.Instance.DestroyWeapons();
            // ���� UI�� ����
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
        // ������ �÷��̾�� ���������� ��� ���� �д�
        yield return StartCoroutine(Sleep(3.0f));
        // UI ��� �ڷ�ƾ�� �Է�
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
