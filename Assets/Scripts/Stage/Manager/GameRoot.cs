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
    private PlayerColideDetect playerCollideDetect;

    private float MaxHP;
    private float currentHP;

    public bool isPlayerImune = false;

    private IEnumerator calBeHitDamage; // 피격 시 대미지 계산 함수

    // 라운드 관련 필드
    private bool isGameOver = false;
    private bool isRoundClear = false;

    private int currentRound = 1;
    private float remainTime;

    // 상점 UI 관련 필드
    public GameObject shopUI;
    public IEnumerator floatingShopUI;

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
        // 라운드 클리어 시
        if (isRoundClear && floatingShopUI != null)
        {
            // 상점 UI를 띄운다
            StartCoroutine(floatingShopUI);
            floatingShopUI = null;
            // 인스턴스화된 무기들을 파괴한다
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
