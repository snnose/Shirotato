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
        DetectNearbyMonster();

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
            GameObject monster = GetAttackedMonster();

            // 회피 시도를 한다
            float random = Random.Range(0, 100f);
            // 회피 상한은 60
            int evadeNum = RealtimeInfoManager.Instance.GetEvasion();
            if (evadeNum >= 60)
                evadeNum = 60;

            // 난수 값이 회피 수치보다 낮게 나왔다면 회피 성공
            if (random < evadeNum)
            {
                isEvade = true;
                PrintText(0);
            }

            // 충돌한 오브젝트가 몬스터이고, 회피 실패 시 대미지를 받는다.
            if (monster.TryGetComponent<MonsterInfo>(out MonsterInfo monsterInfo)
                && !isEvade)
            {
                // 받는 대미지를 계산 후 현재 체력을 차감한다
                int behitDamage = Mathf.FloorToInt(monsterInfo.damage *
                                                                (1 - playerInfo.GetArmor() /
                                                                                (playerInfo.GetArmor() + 10)));

                float currentHP = RealtimeInfoManager.Instance.GetCurrentHP();
                currentHP -= behitDamage;
                RealtimeInfoManager.Instance.SetCurrentHP(currentHP);

                // 플레이어의 체력이 0 이하로 떨어지면 
                if (currentHP <= 0)
                {
                    currentHP = 0;
                    // Player를 죽음 상태로 변경
                    this.gameObject.transform.parent.GetComponent<PlayerControl>().currState = PlayerControl.state.DEAD;
                }

                PrintText(-behitDamage);
            }
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

    void PrintText(int damage)
    {
        // 받은 대미지 텍스트 출력
        GameObject damageText = Resources.Load<GameObject>("Prefabs/DamageText");
        TextMeshPro damagePro = damageText.GetComponent<TextMeshPro>();

        if (damage == 0)
        {
            damagePro.text = "회피";
            damagePro.color = Color.white;
        }
        else
        {
            // 텍스트 및 색상 결정
            damagePro.text = damage.ToString();
            damagePro.color = Color.red;
        }

        GameObject copy = Instantiate(damageText);
        Vector3 randomPos = new Vector3(Random.Range(-0.4f, 0.4f), Random.Range(0.4f, 0.6f), 0f);
        copy.transform.position = this.gameObject.transform.position + randomPos;
    }

    // 근처에 몬스터를 인식하면 isTrigger = True
    private void DetectNearbyMonster()
    {
        // 현재 존재하는 몬스터들을 받아온다
        List<GameObject> Monsters = SpawnManager.Instance.GetCurrentMonsters();
        
        float closetDistance = float.MaxValue;
        float range = 1.3f;

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
