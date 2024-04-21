using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArrowControl : MonoBehaviour
{
    private int damage = 0;
    private int pierceCount = 0;
    private int bounceCount = 0;

    private float pierceDamage = 0.5f;
    private bool isCritical = false;

    public int knockback = 0;

    // Start is called before the first frame update
    void Start()
    {
        // ���� �� ���� Ƚ�� +1, ���� ����� - 20%
        ActivateNormalItem43();
        // ���� �� ���� ����� +15%
        ActivateRareItem31();
        // ���� �� ���� Ƚ�� +1
        ActivateEpicItem19();
        // ���� �� ź�� Ƚ�� +1
        ActivateLegendItem26();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� �浹�ϸ� �Ѿ� �ı�
        if (collision.gameObject.TryGetComponent<WallControl>(out WallControl wall))
        {
            //Debug.Log("�浹");
            Destroy(this.gameObject);
        }

        //collision.gameObject == GameObject.FindGameObjectWithTag("Monster")
        // ���Ϳ� �浹���� ���
        if (SpawnManager.Instance.GetCurrentMonsters().Contains(collision.gameObject))
        {
            GameObject hitedMonster = collision.gameObject;
            MonsterControl monsterControl = hitedMonster.GetComponent<MonsterControl>();

            float monsterHP = monsterControl.GetMonsterCurrentHP();

            // ġ��Ÿ ����
            float random = Random.Range(0f, 100f);
            if (random < RealtimeInfoManager.Instance.GetCritical())
                isCritical = true;

            // ����� ��� ����
            random = Random.Range(0f, 100f);
            if (random < RealtimeInfoManager.Instance.GetHPDrain()
                && RealtimeInfoManager.Instance.GetCurrentHP() < RealtimeInfoManager.Instance.GetHP())
            {
                float currentHP = RealtimeInfoManager.Instance.GetCurrentHP();
                currentHP += 1f;
                RealtimeInfoManager.Instance.SetCurrentHP(currentHP);

                Color color = Color.white;
                ColorUtility.TryParseHtmlString("#1FDE38", out color);
                PrintText(PlayerControl.Instance.transform.position, 1, color);
            }

            float finalDamage = damage;
            // EpicItem32 ���� ��, ������ ���� ���� ������ ����Ʈ��� +25% �߰� �����
            finalDamage = ActivateEpicItem32(finalDamage, collision.GetComponent<MonsterInfo>());
            // ġ��Ÿ��� ������� 2�� �����Ѵ�
            if (isCritical)
            {
                finalDamage *= 2.0f;
                finalDamage = ActivateLegendItem20(finalDamage, monsterControl);
            }

            // NormalItem42 ���� �� ȿ�� �ߵ�
            // ������ ������ Ȯ���� ���� �߰� ������� ������
            ActivateNormalItem42(monsterControl);
            // NormalItem44 ���� �� ȿ�� �ߵ�
            // ������ ���� �ӵ��� -10% ���� ��Ų��
            ActivateNormalItem44(collision.GetComponent<MonsterInfo>());

            monsterHP -= finalDamage;
            monsterControl.SetMonsterCurrentHP(monsterHP);
            // ũ��Ƽ�÷� �� óġ ��
            if (isCritical && monsterHP <= 0)
            {
                // RareItem36 ���� �� ȿ�� �ߵ�
                ActivateRareItem36();
                // EpicItem27 ���� �� ȿ�� �ߵ�
                ActivateEpicItem27(collision.GetComponent<MonsterInfo>());
            }

            // ���� ����� ���
            if (isCritical)
                PrintText(hitedMonster.transform.position, finalDamage, Color.yellow);
            else
                PrintText(hitedMonster.transform.position, finalDamage, Color.white);

            // �˹� ����
            Rigidbody2D hitedMonsterRb2D = hitedMonster.GetComponent<Rigidbody2D>();
            Vector2 knockbackVector = PlayerControl.Instance.transform.position - hitedMonster.transform.position;
            hitedMonsterRb2D.AddForce(knockback * -knockbackVector.normalized * 0.5f, ForceMode2D.Impulse);

            // ƨ�� Ƚ���� ���� Ƚ�� ��� �ִٸ� ƨ���� �켱�õȴ�
            // ƨ�� Ƚ���� 1 �̻��̶��
            if (bounceCount > 0)
            {
                Bounce(collision.gameObject);
                bounceCount--;
            }
            // ƨ�� Ƚ���� ���ٸ�
            else
            {
                // ���� Ƚ���� 1 �̻��̶��
                if (pierceCount > 0)
                {
                    // Ƚ���� ���ҽ�Ű�� ������� �������� ����
                    this.pierceCount--;
                    this.damage = Mathf.FloorToInt(this.damage * pierceDamage);
                    if (this.damage <= 0)
                        this.damage = 1;
                }
                // ���� Ƚ���� 0�̸�
                else
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }

    // ƨ���� ������ ���� �� �ٸ� ������ ������ ���ϴ� ���
    void Bounce(GameObject collision)
    {
        // ���� �����ϴ� ���� ����� �޾ƿ´�.
        List<GameObject> Monsters;
        Monsters = SpawnManager.Instance.GetCurrentMonsters();
        // ������ ���͸� �����Ѵ�
        int maxNum = Monsters.Count;
        // ���Ͱ� �� �Ѹ��� �����ϸ� �ߵ����� �ʴ´�
        if (maxNum == 1)
            return;
        GameObject targetMonster = null;
        while (true)
        {
            int ran = Random.Range(0, maxNum);
            targetMonster = Monsters[Random.Range(0, maxNum)];
            // Ÿ���õ� ���Ͱ� ���� �ǰݵ� ���Ϳ� �ٸ��� Ż��
            if (ran != collision.GetComponent<MonsterInfo>().GetMonsterNumber())
                break;
        }

        // ȭ�� -> ���� ���͸� ���Ѵ�
        Vector2 direction = targetMonster.transform.position - this.transform.position;
        // ȭ���� �ӵ��� �ʱ�ȭ�� ��
        this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        // �ش� ���Ͱ� �ִ� �������� �ٽ� �߻�
        this.GetComponent<Rigidbody2D>().AddForce(direction.normalized * 50f, ForceMode2D.Impulse);
        // ȸ�� ����
        float rotateZ = Mathf.Atan2(direction.normalized.y, direction.normalized.x) * Mathf.Rad2Deg;

        this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, rotateZ);
    }

    void ActivateNormalItem42(MonsterControl monsterControl)
    {
        float monsterHP = monsterControl.GetMonsterCurrentHP();

        if (ItemManager.Instance.GetOwnNormalItemList()[42] > 0)
        {
            float random = Random.Range(0f, 100f);

            if (random < 25f * ItemManager.Instance.GetOwnNormalItemList()[42])
            {
                // ����� * (2 + ���� ����� 50%)
                int additionalDamage = Mathf.FloorToInt(
                    (1f + (RealtimeInfoManager.Instance.GetDMGPercent() / 100)) *
                    (2f + 0.5f * RealtimeInfoManager.Instance.GetFixedDMG()));
                // ������� �ּ� 1
                if (additionalDamage <= 0)
                    additionalDamage = 1;

                monsterHP -= additionalDamage;
                monsterControl.SetMonsterCurrentHP(monsterHP);

                PrintText(monsterControl.transform.position + new Vector3(0f, 0.75f, 0f), additionalDamage, Color.cyan);
            }
        }
    }

    void ActivateNormalItem43()
    {
        if (ItemManager.Instance.GetOwnNormalItemList()[43] > 0)
        {
            this.pierceCount += 1;
            this.pierceDamage -= 0.2f;
        }
    }

    void ActivateNormalItem44(MonsterInfo monsterInfo)
    {
        if (ItemManager.Instance.GetOwnNormalItemList()[44] > 0)
        {
            float monsterSpeed = monsterInfo.GetMonsterMovementSpeed();
            // �̵��ӵ� ���Ҵ� �ִ� 3��ø
            if (monsterInfo.normalItem44Count <= 3)
            {
                monsterInfo.SetMonsterMovementSpeed(monsterSpeed * 0.9f);
                monsterInfo.normalItem44Count++;
            }
        }
    }

    void ActivateRareItem31()
    {
        if (ItemManager.Instance.GetOwnRareItemList()[31] > 0)
        {
            this.pierceDamage += 0.15f;
        }
    }

    // ũ��Ƽ�÷� �� óġ �� Ȯ���� ���� HP 1 ȸ��
    void ActivateRareItem36()
    {
        if (ItemManager.Instance.GetOwnRareItemList()[36] > 0)
        {
            float random = Random.Range(0f, 100f);
            if (random < 20 * ItemManager.Instance.GetOwnRareItemList()[36])
            {
                float playerCurrentHP = RealtimeInfoManager.Instance.GetCurrentHP();

                playerCurrentHP++;
                if (playerCurrentHP >= RealtimeInfoManager.Instance.GetHP())
                    playerCurrentHP = RealtimeInfoManager.Instance.GetHP();

                RealtimeInfoManager.Instance.SetCurrentHP(playerCurrentHP);

                Color color = Color.white;
                ColorUtility.TryParseHtmlString("1FDE38", out color);
                PrintText(PlayerControl.Instance.transform.position, 1, color);
            }
        }
    }

    void ActivateEpicItem19()
    {
        if (ItemManager.Instance.GetOwnEpicItemList()[19] > 0)
        {
            this.pierceCount += 1;
        }
    }

    // ũ��Ƽ�÷� �� óġ �� ���� 1�� �߰� ���
    void ActivateEpicItem27(MonsterInfo monsterInfo)
    {
        int count = ItemManager.Instance.GetOwnEpicItemList()[27];

        if (count > 0)
        {
            float random = Random.Range(0f, 100f);
            if (random < 33f * count)
            {
                int waffleDropCount = monsterInfo.GetWaffleDropCount();
                monsterInfo.SetMonsterWaffleDropCount(waffleDropCount);
            }
        }
    }

    float ActivateEpicItem32(float damage, MonsterInfo monsterInfo)
    {
        float tmp = damage;
        int count = ItemManager.Instance.GetOwnEpicItemList()[32];

        if (count > 0)
        {
            tmp = Mathf.FloorToInt(damage * 1.25f * count);
        }

        return tmp;
    }

    // ũ��Ƽ�� �߻� �� ����� ���� ü�� 10% �߰� ����� (������ ����Ʈ�� 1%)
    float ActivateLegendItem20(float damage, MonsterControl monsterControl)
    {
        float tmp = damage;

        if (ItemManager.Instance.GetOwnLegendItemList()[20] > 0)
        {
            float monsterCurrentHP = monsterControl.GetMonsterCurrentHP();

            // ���� Ÿ�Կ� ���� �߰� ������� �����ǵ��� �����ؾ���
            tmp += 0.1f * monsterCurrentHP;
        }

        return tmp;
    }

    // ����ü ź�� Ƚ�� +1
    void ActivateLegendItem26()
    {
        int itemCount = ItemManager.Instance.GetOwnLegendItemList()[26];

        if (itemCount > 0)
        {
            this.bounceCount += itemCount;
        }
    }

    void PrintText(Vector3 position, float damage, Color color)
    {
        // ����� �ؽ�Ʈ ���
        GameObject textObject = Resources.Load<GameObject>("Prefabs/DamageText");
        TextMeshPro tmPro = textObject.GetComponent<TextMeshPro>();

        // �ؽ�Ʈ �� ���� ����
        tmPro.text = damage.ToString();
        tmPro.color = color;

        GameObject copy = Instantiate(textObject);
        Vector3 randomPos = new Vector3(Random.Range(-0.4f, 0.4f), Random.Range(0.4f, 0.6f), 0f);
        copy.transform.position = position + randomPos;
    }

    void TryHPDrain()
    {
        float random = Random.Range(0f, 100f);

        if (random < RealtimeInfoManager.Instance.GetCritical())
        {
            float currentHP = RealtimeInfoManager.Instance.GetCurrentHP();
            if (currentHP < RealtimeInfoManager.Instance.GetHP())
            {
                currentHP += 1f;

            }
        }
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    public void SetPierceCount(int count)
    {
        this.pierceCount = count;
    }

    public void SetBounceCount(int count)
    {
        this.bounceCount = count;
    }

    public void SetPierceDamage(float pierceDamage)
    {
        this.pierceDamage = pierceDamage;
    }

    public void SetKnockback(int knockback)
    {
        this.knockback = knockback;
    }
}
