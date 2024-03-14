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
        playerInfo = PlayerInfo.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        // ��ó�� ���Ͱ� ������ �浹 ���� ���°� �ȴ�
        DetectNearbyMonster();

        // �÷��̾ ���Ϳ� �浹�ߴٸ� (������ �ƴ� ��)
        if (GetAttackedMonster() != null && !isPlayerImune)
        {
            // ���� ���°� �ǰ�
            StartCoroutine(Imune());
            // ������� �޴´�
            StartCoroutine(calBeHitDamage);
        }
    }

    IEnumerator CalBeHitDamage()
    {
        // �浹�� ���Ϳ� ������ �о�´�
        if (GetAttackedMonster() != null)
        {
            bool isEvade = false;
            GameObject monster = GetAttackedMonster();

            // ȸ�� �õ��� �Ѵ�
            float random = Random.Range(0, 100f);
            // ȸ�� ������ 60
            int evadeNum = RealtimeInfoManager.Instance.GetEvasion();
            if (evadeNum >= 60)
                evadeNum = 60;

            // ���� ���� ȸ�� ��ġ���� ���� ���Դٸ� ȸ�� ����
            if (random < evadeNum)
            {
                isEvade = true;
                PrintText(0);
            }

            // �浹�� ������Ʈ�� �����̰�, ȸ�� ���� �� ������� �޴´�.
            if (monster.TryGetComponent<MonsterInfo>(out MonsterInfo monsterInfo)
                && !isEvade)
            {
                // �޴� ������� ��� �� ���� ü���� �����Ѵ�
                int behitDamage = Mathf.FloorToInt(monsterInfo.damage *
                                                                (1 - playerInfo.GetArmor() /
                                                                                (playerInfo.GetArmor() + 10)));

                float currentHP = RealtimeInfoManager.Instance.GetCurrentHP();
                currentHP -= behitDamage;
                RealtimeInfoManager.Instance.SetCurrentHP(currentHP);

                // �÷��̾��� ü���� 0 ���Ϸ� �������� 
                if (currentHP <= 0)
                {
                    currentHP = 0;
                    // Player�� ���� ���·� ����
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
        // ���� ����� �ؽ�Ʈ ���
        GameObject damageText = Resources.Load<GameObject>("Prefabs/DamageText");
        TextMeshPro damagePro = damageText.GetComponent<TextMeshPro>();

        if (damage == 0)
        {
            damagePro.text = "ȸ��";
            damagePro.color = Color.white;
        }
        else
        {
            // �ؽ�Ʈ �� ���� ����
            damagePro.text = damage.ToString();
            damagePro.color = Color.red;
        }

        GameObject copy = Instantiate(damageText);
        Vector3 randomPos = new Vector3(Random.Range(-0.4f, 0.4f), Random.Range(0.4f, 0.6f), 0f);
        copy.transform.position = this.gameObject.transform.position + randomPos;
    }

    // ��ó�� ���͸� �ν��ϸ� isTrigger = True
    private void DetectNearbyMonster()
    {
        // ���� �����ϴ� ���͵��� �޾ƿ´�
        List<GameObject> Monsters = SpawnManager.Instance.GetCurrentMonsters();
        
        float closetDistance = float.MaxValue;
        float range = 1.3f;

        // ���� ������ �ִ� ���Ϳ��� �Ÿ��� �����Ѵ�
        foreach (GameObject monster in Monsters)
        {
            float dis = Vector2.Distance(this.transform.position, monster.transform.position);

            if (dis < range && dis < closetDistance)
            {
                closetDistance = dis;
            }
        }

        // ���� ����� ���Ͱ� range ����� Ʈ���� on, �ƴ� ��� off
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
