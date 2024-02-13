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

        MaxHP = currentHP = playerInfo.GetHP();

        calBeHitDamage = CalBeHitDamage();
        floatingShopUI = FloatingShopUI();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 플레이어가 몬스터와 충돌했다면 (무적이 아닐 시)
        if (playerCollideDetect.GetAttackedMonster() != null && !isPlayerImune)
        {
            StartCoroutine(Imune());
            StartCoroutine(calBeHitDamage);
        }

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

    // 피격 시 받는 대미지 계산
    IEnumerator CalBeHitDamage()
    {
        // 충돌한 몬스터와 정보를 읽어온다
        if (playerCollideDetect.GetAttackedMonster() != null)
        {
            GameObject monster = playerCollideDetect.GetAttackedMonster();
            MonsterInfo monsterInfo = monster.GetComponent<MonsterInfo>();

            // 받는 대미지를 계산 후 현재 체력을 차감한다
            int behitDamage = Mathf.CeilToInt(monsterInfo.ATK *
                                                            (1 - playerInfo.GetArmor() /
                                                                            (playerInfo.GetArmor() + 10)));
            currentHP = currentHP - behitDamage;
            // 플레이어의 체력이 0 이하로 떨어지면 
            if (currentHP <= 0)
            {
                currentHP = 0;
                player.GetComponent<PlayerControl>().currState = PlayerControl.state.DEAD;
            }

            PrintText(behitDamage);
        }

        yield return null;
    }

    IEnumerator Imune()
    {
        isPlayerImune = true;
        yield return new WaitForSeconds(0.5f);
        isPlayerImune = false;
        calBeHitDamage = CalBeHitDamage();
    }

    public IEnumerator FloatingShopUI()
    {
        yield return new WaitForSecondsRealtime(3.0f);
        shopUI.SetActive(true);
    }

    void PrintText(int damage)
    {
        // 받은 대미지 텍스트 출력
        GameObject damageText = Resources.Load<GameObject>("Prefabs/DamageText");
        TextMeshPro damagePro = damageText.GetComponent<TextMeshPro>();
        // 텍스트 및 색상 결정
        damagePro.text = (-damage).ToString();
        damagePro.color = Color.red;
        GameObject copy = Instantiate(damageText);
        Vector3 randomPos = new Vector3(Random.Range(-0.4f, 0.4f), Random.Range(0.4f, 0.6f), 0f);
        copy.transform.position = player.transform.position + randomPos;
    }

    public void SetIsRoundClear(bool isRoundClear)
    {
        this.isRoundClear = isRoundClear;
    }

    public void SetCurrentRound(int currentRound)
    {
        this.currentRound = currentRound;
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

    public float GetCurrentHP()
    {
        return this.currentHP;
    }

    public float GetMaxHP()
    {
        return this.MaxHP;
    }
}
