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
        // GameRoot �ʱ�ȭ
        GameRoot.Instance.SetIsRoundClear(false);
        GameRoot.Instance.SetCurrentRound(GameRoot.Instance.GetCurrentRound() + 1);
        // �ڷ�ƾ ������
        GameRoot.Instance.stopRound = GameRoot.Instance.StopRound();

        // ���� ��, ���� ȹ�� UI �ʱ�ȭ
        LevelUpUIControl.Instance.SetActive(false);
        GetBoxUIControl.Instance.SetActive(false);

        // �ǽð� ���� ������ �ʱ�ȭ
        // EpicItem30 ���� �� �ִ� ü���� 50%�� ���� ����
        RealtimeInfoManager.Instance.SetCurrentHP(ActivateEpicItem30(PlayerInfo.Instance.GetHP()));
        RealtimeInfoManager.Instance.SetHP(PlayerInfo.Instance.GetHP());
        RealtimeInfoManager.Instance.SetAllStatus(PlayerInfo.Instance);

        StartCoroutine(RealtimeInfoManager.Instance.ActivateEpicItem36());
        StartCoroutine(RealtimeInfoManager.Instance.ActivateLegendItem24());
        RealtimeInfoManager.Instance.startAllCoroutine = StartCoroutine(RealtimeInfoManager.Instance.StartAllCoroutine());
        RealtimeInfoManager.Instance.activateLegendItem25 = RealtimeInfoManager.Instance.ActivateLegendItem25();

        // NormalItem45�� ��ٸ� HP 1�� ����
        if (ItemManager.Instance.GetOwnNormalItemList()[45] > 0)
        {
            RealtimeInfoManager.Instance.SetCurrentHP(1);
            ItemManager.Instance.GetOwnNormalItemList()[45] = 0;
        }

        // ���� ���� �ð� ����
        float remainTime = 15f + GameRoot.Instance.GetCurrentRound() * 5f;
        // ������ ���尡 �ƴϸ� �ִ� 60��
        if (remainTime > 60f && GameRoot.Instance.GetCurrentRound() != 20)
            remainTime = 60f;
        // ������ ����� 90��
        if (GameRoot.Instance.GetCurrentRound() == 20)
            remainTime = 90f;
        
        // ������ �ӽ� ���� �ð� ����
        //remainTime = 1f;

        timerControl.gameObject.SetActive(true);
        GameRoot.Instance.SetRemainTime(remainTime);
        timerControl.SetTimerText(remainTime.ToString());

        // SpawnManager �ʱ�ȭ
        SpawnManager.Instance.startSpawn = SpawnManager.Instance.StartSpawn(GameRoot.Instance.GetCurrentRound());

        // �÷��̾��� ���� �ʱ�ȭ
        GameObject playerBox = GameRoot.Instance.GetPlayerBox();
        playerBox.transform.position = new Vector3(0f, 0f, -1f);

        // ���ų� ������ �� �ɷ�ġ ������ �ִ� ������ �ڷ�ƾ ����
        PlayerControl.Instance.activateEpicItem20 = PlayerControl.Instance.ActivateEpicItem20();
        PlayerControl.Instance.activateEpicItem22 = PlayerControl.Instance.ActivateEpicItem22();
        PlayerControl.Instance.activateEpicItem33 = PlayerControl.Instance.ActivateEpicItem33();

        // ���� ���� UI �ʱ�ȭ
        RenewWaffleAmount.Instance.renewCurrentWaffleAmount = RenewWaffleAmount.Instance.RenewCurrentWaffleAmount();

        // ���׷��̵� �ʱ�ȭ
        // ��� ���׷��̵� �Ŵ��� Ȱ��ȭ
        UpgradeManager.Instance.gameObject.SetActive(true);
        // ���׷��̵� ���� ����� �ʱ�ȭ�Ѵ�
        UpgradeManager.Instance.GetUpgradeRerollButton().InitReroll();
        // �ٽ� ��Ȱ��ȭ
        UpgradeManager.Instance.gameObject.SetActive(false);

        // ���� �ʱ�ȭ
        // ���� ǰ�� ����Ʈ�� ��� Ȱ��ȭ�Ѵ�
        ShopUIControl.Instance.GetShopItemListControl().SetItemListActive();
        // ���� ���� ����
        ShopUIControl.Instance.GetShopTitleControl().SetTitleText(GameRoot.Instance.GetCurrentRound());
        // ������ ����Ʈ UI �ʱ�ȭ
        ShopUIControl.Instance.GetShopItemListControl().SetIsRenewInfo(false);
        // ������ ����Ʈ �ʱ�ȭ
        ClearShopItemList();
        //ItemManager.Instance.SetIsRenewItem(false);
        // ���� ���� ��� �ʱ�ȭ
        ShopUIControl.Instance.GetShopRerollButton().Initialize(); 
        // ����UI ��Ȱ��ȭ
        GameRoot.Instance.shopUI.SetActive(false);

        // ������ ȿ�� �ߵ�
        // RareItem30 ���� �� ���� ����� 20% �߰� ȹ��
        ActivateRareItem30();

        // ���� ����
        if (WeaponManager.Instance.GetCurrentWeaponList().Count != 0)
        {
            WeaponManager.Instance.equipWeapons = WeaponManager.Instance.EquipWeapons();
        }

        // Ÿ�ӽ����� ����ȭ
        Time.timeScale = 1f;

        yield return null;
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

        // ������ ���� Ƚ�� �ʱ�ȭ
        ItemManager.Instance.itemPurchaseCount = 0;

        ItemManager.Instance.SetShopItemList(tmp);
        ItemManager.Instance.SetShopWeaponInfoList(tmpInfo);
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
