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

    // ����� �ҽ�
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
        // ��ó�� ���Ͱ� ������ �浹 ���� ���°� �ȴ�
        //DetectNearbyMonster();

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
            GameObject collision = GetAttackedMonster();

            // monster�� ���ڶ�� ����ó��
            if (collision.name == "Potatoes")
            {
                StopCoroutine(calBeHitDamage);
                calBeHitDamage = CalBeHitDamage();
            }

            // ȸ�� �õ��� �Ѵ�
            float random = Random.Range(0, 100f);
            // ȸ�� ������ 60
            int evadeNum = RealtimeInfoManager.Instance.GetEvasion();
            if (evadeNum >= 60)
                evadeNum = 60;

            // ���� ���� ȸ�� ��ġ���� ���� ���Դٸ� ȸ�� ����
            if (random < evadeNum && collision.TryGetComponent<MonsterInfo>(out MonsterInfo monsterInfo))
            {
                // RareItem33 ���� �� ������ ȿ�� �ߵ�
                // ȸ�� ���� �� �ݰ� ������� ������
                ActivateRareItem33(collision.GetComponent<MonsterControl>());
                // EpicItem16 ���� �� ������ ȿ�� �ߵ�
                // ȸ�� ���� �� 50% Ȯ���� ü�� 5�� ȸ���Ѵ�
                ActivateEpicItem16();
                isEvade = true;
                PrintText(0, Color.white);
            }

            // �浹�� ������Ʈ�� �����̰�, ȸ�� ���� �� ������� �޴´�.
            if (collision.TryGetComponent<MonsterInfo>(out monsterInfo)
                && !isEvade)
            {
                // �޴� ������� ��� �� ���� ü���� �����Ѵ�
                // �޴� ����� ���ҷ�
                float damageReduction = 100 - 100 * playerInfo.GetArmor() / (Mathf.Abs(playerInfo.GetArmor()) + 10);
                int behitDamage = Mathf.FloorToInt(monsterInfo.damage * damageReduction / 100);

                if (behitDamage <= 0)
                    behitDamage = 1;

                float currentHP = RealtimeInfoManager.Instance.GetCurrentHP();
                currentHP -= behitDamage;
                RealtimeInfoManager.Instance.SetCurrentHP(currentHP);

                // �÷��̾��� ü���� 0 ���Ϸ� �������� 
                if (currentHP <= 0)
                {
                    //�÷��̾� ������� ���� ����
                    GameRoot.Instance.SetIsGameOver(true);

                    currentHP = 0;
                    // Player�� ���� ���·� ����
                    PlayerControl.Instance.currState = PlayerControl.state.DEAD;
                }

                // ������ ȿ�� ����
                ActivateEpicItem34();

                PrintText(-behitDamage, Color.red);
            }

            // �浹�� ������Ʈ�� ����ü�� ���
            if (collision.TryGetComponent<ProjectileControl>(out ProjectileControl projectileControl)
                && !isEvade)
            {
                // �޴� ������� ��� �� ���� ü���� �����Ѵ�
                int behitDamage = Mathf.FloorToInt(projectileControl.GetProjectileDamage() *
                                                                (1 - playerInfo.GetArmor() /
                                                                                (Mathf.Abs(playerInfo.GetArmor()) + 10)));

                float currentHP = RealtimeInfoManager.Instance.GetCurrentHP();
                currentHP -= behitDamage;
                RealtimeInfoManager.Instance.SetCurrentHP(currentHP);

                // �÷��̾��� ü���� 0 ���Ϸ� �������� 
                if (currentHP <= 0)
                {
                    //�÷��̾� ������� ���� ����
                    GameRoot.Instance.SetIsGameOver(true);

                    currentHP = 0;
                    // Player�� ���� ���·� ����
                    PlayerControl.Instance.currState = PlayerControl.state.DEAD;
                }

                // ������ ȿ�� ����
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
        // ���� ����� �ؽ�Ʈ ���
        GameObject text = Resources.Load<GameObject>("Prefabs/DamageText");
        TextMeshPro pro = text.GetComponent<TextMeshPro>();

        if (num == 0)
        {
            pro.text = "ȸ��";
        }
        else
        {
            // �ؽ�Ʈ �� ���� ����
            pro.text = num.ToString();
        }

        pro.color = color;

        GameObject copy = Instantiate(text);
        Vector3 randomPos = new Vector3(Random.Range(-0.4f, 0.4f), Random.Range(0.4f, 0.6f), 0f);
        copy.transform.position = this.gameObject.transform.position + randomPos;
    }

    // ��ó�� ���͸� �ν��ϸ� isTrigger = True
    private void DetectNearbyMonster()
    {
        // ���� �����ϴ� ���͵��� �޾ƿ´�
        List<GameObject> Monsters = SpawnManager.Instance.GetCurrentMonsters();
        
        float closetDistance = float.MaxValue;
        float range = 1.6f;

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

    void ActivateRareItem33(MonsterControl monsterControl)
    {
        if (ItemManager.Instance.GetOwnRareItemList()[33] > 0)
        {
            int counterDamage = Mathf.FloorToInt((1 + RealtimeInfoManager.Instance.GetDMGPercent() / 100) * 
                                                 (1 + RealtimeInfoManager.Instance.GetFixedDMG() * 6f));

            Debug.Log("�ݰ� ����� : " + counterDamage);

            monsterControl.PrintText(monsterControl.transform, Color.cyan, counterDamage);
            monsterControl.SetMonsterCurrentHP(monsterControl.GetMonsterCurrentHP() - counterDamage);
        }
    }

    // EpicItem16 ���� ��, ȸ�� ���� �� 50% Ȯ���� ü�� 5 ȸ��
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

    // EpicItem34 ���� ��, �ǰ� �� �ش� ���� �����% -2%
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
