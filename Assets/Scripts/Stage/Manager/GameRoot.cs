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
        // �÷��̾ ���Ϳ� �浹�ߴٸ� (������ �ƴ� ��)
        if (playerCollideDetect.GetAttackedMonster() != null && !isPlayerImune)
        {
            StartCoroutine(Imune());
            StartCoroutine(calBeHitDamage);
        }

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

    // �ǰ� �� �޴� ����� ���
    IEnumerator CalBeHitDamage()
    {
        // �浹�� ���Ϳ� ������ �о�´�
        if (playerCollideDetect.GetAttackedMonster() != null)
        {
            GameObject monster = playerCollideDetect.GetAttackedMonster();
            MonsterInfo monsterInfo = monster.GetComponent<MonsterInfo>();

            // �޴� ������� ��� �� ���� ü���� �����Ѵ�
            int behitDamage = Mathf.CeilToInt(monsterInfo.ATK *
                                                            (1 - playerInfo.GetArmor() /
                                                                            (playerInfo.GetArmor() + 10)));
            currentHP = currentHP - behitDamage;
            // �÷��̾��� ü���� 0 ���Ϸ� �������� 
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
        // ���� ����� �ؽ�Ʈ ���
        GameObject damageText = Resources.Load<GameObject>("Prefabs/DamageText");
        TextMeshPro damagePro = damageText.GetComponent<TextMeshPro>();
        // �ؽ�Ʈ �� ���� ����
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
