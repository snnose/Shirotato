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

    private IEnumerator calBeHitDamage; // �ǰ� �� ����� ��� �Լ�

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
        // �÷��̾ ���Ϳ� �浹�ߴٸ� (������ �ƴ� ��)
        if (GetAttackedMonster() != null && !isPlayerImune)
        {
            StartCoroutine(Imune());
            StartCoroutine(calBeHitDamage);
        }
    }

    IEnumerator CalBeHitDamage()
    {
        // �浹�� ���Ϳ� ������ �о�´�
        if (GetAttackedMonster() != null)
        {
            GameObject monster = GetAttackedMonster();
            MonsterInfo monsterInfo = monster.GetComponent<MonsterInfo>();

            // �޴� ������� ��� �� ���� ü���� �����Ѵ�
            int behitDamage = Mathf.CeilToInt(monsterInfo.ATK *
                                                            (1 - playerInfo.GetArmor() /
                                                                            (playerInfo.GetArmor() + 10)));

            float currentHP = GameRoot.Instance.GetCurrentHP();
            currentHP -= behitDamage;
            GameRoot.Instance.SetCurrentHP(currentHP);

            // �÷��̾��� ü���� 0 ���Ϸ� �������� 
            if (currentHP <= 0)
            {
                currentHP = 0;
                // Player�� ���� ���·� ����
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
        // ���� ����� �ؽ�Ʈ ���
        GameObject damageText = Resources.Load<GameObject>("Prefabs/DamageText");
        TextMeshPro damagePro = damageText.GetComponent<TextMeshPro>();

        // �ؽ�Ʈ �� ���� ����
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
