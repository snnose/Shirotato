using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerColideDetect : MonoBehaviour
{
    private GameObject attackedMonster = null;

    private Collider2D playerCollider;
    private PlayerInfo playerInfo;

    public bool isPlayerImune = false;

    private IEnumerator calBeHitDamage; // 피격 시 대미지 계산 함수

    // 오디오 소스
    public AudioSource beHittedAudioSource;

    private void Awake()
    {
        playerCollider = this.GetComponent<CircleCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        calBeHitDamage = CalBeHitDamage();
        playerInfo = PlayerInfo.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        // 근처에 몬스터가 있으면 충돌 무시 상태가 된다
        //DetectNearbyMonster();

        // 플레이어가 몬스터와 충돌했다면 (무적이 아닐 시)
        if (GetAttackedMonster() != null && !isPlayerImune)
        {
            // 무적 상태가 되고
            StartCoroutine(Imune());
            // 대미지를 받는다
            StartCoroutine(calBeHitDamage);
        }
    }

    IEnumerator CalBeHitDamage()
    {
        // 충돌한 몬스터와 정보를 읽어온다
        if (GetAttackedMonster() != null)
        {
            bool isEvade = false;
            GameObject collision = GetAttackedMonster();

            // monster가 감자라면 예외처리
            if (collision.name == "Potatoes")
            {
                StopCoroutine(calBeHitDamage);
                calBeHitDamage = CalBeHitDamage();
            }

            // 회피 시도를 한다
            float random = Random.Range(0, 100f);
            // 회피 상한은 60
            int evadeNum = RealtimeInfoManager.Instance.GetEvasion();
            if (evadeNum >= 60)
                evadeNum = 60;

            // 난수 값이 회피 수치보다 낮게 나왔다면 회피 성공
            if (random < evadeNum && collision.TryGetComponent<MonsterInfo>(out MonsterInfo monsterInfo))
            {
                // RareItem33 보유 시 아이템 효과 발동
                // 회피 성공 시 반격 대미지를 입힌다
                ActivateRareItem33(collision.GetComponent<MonsterControl>());
                // EpicItem16 보유 시 아이템 효과 발동
                // 회피 성공 시 50% 확률로 체력 5를 회복한다
                ActivateEpicItem16();
                isEvade = true;
                PrintText(0, Color.white);
            }

            // 충돌한 오브젝트가 몬스터이고, 회피 실패 시 대미지를 받는다.
            if (collision.TryGetComponent<MonsterInfo>(out monsterInfo)
                && !isEvade)
            {
                // 받는 대미지를 계산 후 현재 체력을 차감한다
                // 받는 대미지 감소량
                float damageReduction = 100 - 100 * playerInfo.GetArmor() / (Mathf.Abs(playerInfo.GetArmor()) + 10);
                int behitDamage = Mathf.FloorToInt(monsterInfo.damage * damageReduction / 100);

                if (behitDamage <= 0)
                    behitDamage = 1;

                float currentHP = RealtimeInfoManager.Instance.GetCurrentHP();
                currentHP -= behitDamage;
                RealtimeInfoManager.Instance.SetCurrentHP(currentHP);

                // 플레이어의 체력이 0 이하로 떨어지면 
                if (currentHP <= 0)
                {
                    //플레이어 사망으로 게임 오버
                    GameRoot.Instance.SetIsGameOver(true);

                    currentHP = 0;
                    // Player를 죽음 상태로 변경
                    PlayerControl.Instance.currState = PlayerControl.state.DEAD;
                }

                // 아이템 효과 적용
                ActivateEpicItem34();

                PrintText(-behitDamage, Color.red);
            }

            // 충돌한 오브젝트가 투사체인 경우
            if (collision.TryGetComponent<ProjectileControl>(out ProjectileControl projectileControl)
                && !isEvade)
            {
                // 받는 대미지를 계산 후 현재 체력을 차감한다
                int behitDamage = Mathf.FloorToInt(projectileControl.GetProjectileDamage() *
                                                                (1 - playerInfo.GetArmor() /
                                                                                (Mathf.Abs(playerInfo.GetArmor()) + 10)));

                float currentHP = RealtimeInfoManager.Instance.GetCurrentHP();
                currentHP -= behitDamage;
                RealtimeInfoManager.Instance.SetCurrentHP(currentHP);

                // 플레이어의 체력이 0 이하로 떨어지면 
                if (currentHP <= 0)
                {
                    //플레이어 사망으로 게임 오버
                    GameRoot.Instance.SetIsGameOver(true);

                    currentHP = 0;
                    // Player를 죽음 상태로 변경
                    PlayerControl.Instance.currState = PlayerControl.state.DEAD;
                }

                // 아이템 효과 적용
                ActivateEpicItem34();

                PrintText(-behitDamage, Color.red);

                Destroy(collision);
            }
        }

        yield return null;
    }

    IEnumerator Imune()
    {
        isPlayerImune = true;
        yield return new WaitForSeconds(1.0f);
        isPlayerImune = false;
        calBeHitDamage = CalBeHitDamage();
    }

    void PrintText(int num, Color color)
    {
        // 받은 대미지 텍스트 출력
        GameObject text = Resources.Load<GameObject>("Prefabs/DamageText");
        TextMeshPro pro = text.GetComponent<TextMeshPro>();

        if (num == 0)
        {
            pro.text = "회피";
        }
        else
        {
            // 텍스트 및 색상 결정
            pro.text = num.ToString();
        }

        pro.color = color;

        GameObject copy = Instantiate(text);
        Vector3 randomPos = new Vector3(Random.Range(-0.4f, 0.4f), Random.Range(0.4f, 0.6f), 0f);
        copy.transform.position = this.gameObject.transform.position + randomPos;
    }

    // 근처에 몬스터를 인식하면 isTrigger = True
    private void DetectNearbyMonster()
    {
        // 현재 존재하는 몬스터들을 받아온다
        List<GameObject> Monsters = SpawnManager.Instance.GetCurrentMonsters();
        
        float closetDistance = float.MaxValue;
        float range = 1.6f;

        // 가장 가까이 있는 몬스터와의 거리를 측정한다
        foreach (GameObject monster in Monsters)
        {
            float dis = Vector2.Distance(this.transform.position, monster.transform.position);

            if (dis < range && dis < closetDistance)
            {
                closetDistance = dis;
            }
        }

        // 가장 가까운 몬스터가 range 내라면 트리거 on, 아닐 경우 off
        if (closetDistance < range)
            playerCollider.isTrigger = true;
        else
            playerCollider.isTrigger = false;
    }

    void ActivateRareItem33(MonsterControl monsterControl)
    {
        if (ItemManager.Instance.GetOwnRareItemList()[33] > 0)
        {
            int counterDamage = Mathf.FloorToInt((1 + RealtimeInfoManager.Instance.GetDMGPercent() / 100) * 
                                                 (1 + RealtimeInfoManager.Instance.GetFixedDMG() * 6f));

            Debug.Log("반격 대미지 : " + counterDamage);

            monsterControl.PrintText(monsterControl.transform, Color.cyan, counterDamage);
            monsterControl.SetMonsterCurrentHP(monsterControl.GetMonsterCurrentHP() - counterDamage);
        }
    }

    // EpicItem16 보유 중, 회피 성공 시 50% 확률로 체력 5 회복
    void ActivateEpicItem16()
    {
        if (ItemManager.Instance.GetOwnEpicItemList()[16] > 0)
        {
            float random = Random.Range(0f, 100f);
            if (random < 50f)
            {
                float playerCurrentHP = RealtimeInfoManager.Instance.GetCurrentHP();

                playerCurrentHP += 5f;
                if (playerCurrentHP >= RealtimeInfoManager.Instance.GetHP())
                    playerCurrentHP = RealtimeInfoManager.Instance.GetHP();

                RealtimeInfoManager.Instance.SetCurrentHP(playerCurrentHP);

                Color color = Color.white;
                ColorUtility.TryParseHtmlString("#1FDE38", out color);
                PrintText(5, color);
            }
        }
    }

    // EpicItem34 보유 중, 피격 시 해당 라운드 대미지% -2%
    void ActivateEpicItem34()
    {
        int count = ItemManager.Instance.GetOwnEpicItemList()[34];

        if (count > 0)
        {
            float DMGPercent = RealtimeInfoManager.Instance.GetDMGPercent() - 2f * count ;
            RealtimeInfoManager.Instance.SetDMGPercent(DMGPercent);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        SetAttackedMonster(collider.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        SetAttackedMonster(null);
    }

    public void SetAttackedMonster(GameObject monster)
    {
        this.attackedMonster = monster;
    }

    public GameObject GetAttackedMonster()
    {
        return attackedMonster;
    }
}
