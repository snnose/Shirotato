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
        playerInfo = this.gameObject.GetComponent<PlayerInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        // 플레이어가 몬스터와 충돌했다면 (무적이 아닐 시)
        if (GetAttackedMonster() != null && !isPlayerImune)
        {
            StartCoroutine(Imune());
            StartCoroutine(calBeHitDamage);
        }
    }

    IEnumerator CalBeHitDamage()
    {
        // 충돌한 몬스터와 정보를 읽어온다
        if (GetAttackedMonster() != null)
        {
            GameObject monster = GetAttackedMonster();
            MonsterInfo monsterInfo = monster.GetComponent<MonsterInfo>();

            // 받는 대미지를 계산 후 현재 체력을 차감한다
            int behitDamage = Mathf.CeilToInt(monsterInfo.ATK *
                                                            (1 - playerInfo.GetArmor() /
                                                                            (playerInfo.GetArmor() + 10)));

            float currentHP = GameRoot.Instance.GetCurrentHP();
            currentHP -= behitDamage;
            GameRoot.Instance.SetCurrentHP(currentHP);

            // 플레이어의 체력이 0 이하로 떨어지면 
            if (currentHP <= 0)
            {
                currentHP = 0;
                // Player를 죽음 상태로 변경
                this.gameObject.transform.parent.GetComponent<PlayerControl>().currState = PlayerControl.state.DEAD;
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
        copy.transform.position = this.gameObject.transform.position + randomPos;
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        SetAttackedMonster(collider.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
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
