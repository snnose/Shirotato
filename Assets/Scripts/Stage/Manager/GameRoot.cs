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
    private PlayerColideDetect playerCollideDetect;

    private float MaxHP;
    private float currentHP;

    public bool isPlayerImune = false;

    private IEnumerator calBeHitDamage; // �ǰ� �� ����� ��� �Լ�

    // ���� ���� �ʵ�
    private bool isGameOver = false;
    private bool isRoundClear = false;

    private int currentRound = 1;
    private float remainTime;

    // ���� UI ���� �ʵ�
    public GameObject shopUI;
    public IEnumerator floatingShopUI;

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
        playerCollideDetect = player.GetComponent<PlayerColideDetect>();

        MaxHP = playerInfo.GetHP();
        currentHP = playerInfo.GetHP();

        floatingShopUI = FloatingShopUI();
    }
    // Start is called before the first frame update
    void Start()
    {
        remainTime = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        // ���� Ŭ���� ��
        if (isRoundClear && floatingShopUI != null)
        {
            // ���� UI�� ����
            StartCoroutine(floatingShopUI);
            floatingShopUI = null;
            // �ν��Ͻ�ȭ�� ������� �ı��Ѵ�
            WeaponManager.Instance.destroyWeapons = WeaponManager.Instance.DestroyWeapons();
        }
    }

    public IEnumerator FloatingShopUI()
    {
        yield return new WaitForSecondsRealtime(3.0f);
        shopUI.SetActive(true);
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
}
