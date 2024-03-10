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
        GameRoot.Instance.SetCurrentHP(PlayerInfo.Instance.GetHP());
        GameRoot.Instance.SetMaxHP(PlayerInfo.Instance.GetHP());

        // �ش� ������ ������ ������ �����Ѵ� (���� ����, ���� �ð�)
        GameRoot.Instance.SetRemainTime(3f);
        timerControl.SetTimerText(3f.ToString());

        // SpawnManager �ʱ�ȭ
        SpawnManager.Instance.startSpawn = SpawnManager.Instance.StartSpawn(GameRoot.Instance.GetCurrentRound());

        // �÷��̾��� ���� �ʱ�ȭ
        GameObject playerBox = GameRoot.Instance.GetPlayerBox();
        PlayerInfo playerInfo = playerBox.transform.GetChild(0).GetComponent<PlayerInfo>();
        playerBox.transform.position = new Vector2(0f, 0f);
        playerInfo.SetMovementSpeed(playerInfo.GetMovementSpeed() * (100 + playerInfo.GetMovementSpeedPercent()) / 100);
        PlayerControl.Instance.SetMovementSpeed(PlayerInfo.Instance.GetMovementSpeed());

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

        // ���� ����
        WeaponManager.Instance.equipWeapons = WeaponManager.Instance.EquipWeapons();

        // �׽�Ʈ �� �ڵ��
        GameRoot.Instance.SetLevelUpCount(1);

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
}
