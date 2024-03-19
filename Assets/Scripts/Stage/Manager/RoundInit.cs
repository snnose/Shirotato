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

        // �ǽð� ���� ������ �ʱ�ȭ
        // EpicItem30 ���� �� �ִ� ü���� 50%�� ���� ����
        RealtimeInfoManager.Instance.SetCurrentHP(ActivateEpicItem30(PlayerInfo.Instance.GetHP()));
        RealtimeInfoManager.Instance.SetHP(PlayerInfo.Instance.GetHP());
        StartCoroutine(RealtimeInfoManager.Instance.HPRecovery());
        StartCoroutine(RealtimeInfoManager.Instance.ActivateEpicItem21());
        StartCoroutine(RealtimeInfoManager.Instance.ActivateEpicItem36());

        // NormalItem45�� ��ٸ� HP 1�� ����
        if (ItemManager.Instance.GetOwnNormalItemList()[45] > 0)
        {
            RealtimeInfoManager.Instance.SetCurrentHP(1);
            ItemManager.Instance.GetOwnNormalItemList()[45] = 0;
        }

        // ���� ���� �ð� ����
        float remainTime = 15f + GameRoot.Instance.GetCurrentRound() * 5f;
        if (remainTime > 60f)
            remainTime = 60f;
        timerControl.gameObject.SetActive(true);
        GameRoot.Instance.SetRemainTime(remainTime);
        timerControl.SetTimerText(remainTime.ToString());

        // SpawnManager �ʱ�ȭ
        SpawnManager.Instance.startSpawn = SpawnManager.Instance.StartSpawn(GameRoot.Instance.GetCurrentRound());

        // �÷��̾��� ���� �ʱ�ȭ
        GameObject playerBox = GameRoot.Instance.GetPlayerBox();
        PlayerInfo playerInfo = playerBox.transform.GetChild(0).GetComponent<PlayerInfo>();
        playerBox.transform.position = new Vector2(0f, 0f);
        playerInfo.SetMovementSpeed(playerInfo.GetMovementSpeed() * (1 + playerInfo.GetMovementSpeedPercent() / 100));
        PlayerControl.Instance.SetMovementSpeed(PlayerInfo.Instance.GetMovementSpeed());
        // ���ų� ������ �� �ɷ�ġ ������ �ִ� ������ �ڷ�ƾ ����
        PlayerControl.Instance.activateEpicItem20 = PlayerControl.Instance.ActivateEpicItem20();
        PlayerControl.Instance.activateEpicItem22 = PlayerControl.Instance.ActivateEpicItem22();
        PlayerControl.Instance.activateEpicItem33 = PlayerControl.Instance.ActivateEpicItem33();

        // ���׷��̵� �ʱ�ȭ
        // ��� ���׷��̵� �Ŵ��� Ȱ��ȭ
        UpgradeManager.Instance.gameObject.SetActive(true);
        // ���׷��̵� ���� ����� �ʱ�ȭ�Ѵ�
        UpgradeManager.Instance.InitReroll();
        // ���׷��̵� ���� �ؽ�Ʈ ����
        UpgradeManager.Instance.SetTextToCurrentCost();
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
        ItemManager.Instance.SetIsRenewItem(false);
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
