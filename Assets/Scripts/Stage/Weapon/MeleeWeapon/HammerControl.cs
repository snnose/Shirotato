using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HammerControl : MonoBehaviour, IMeleeWeaponControl
{
    public int weaponNumber { get; set; }
    public WeaponInfo weaponInfo { get; set; }
    public bool isCoolDown { get; set; }

    private bool isAttackPossible;

    public int damage = 0;
    float coolDown = 1;
    int knockback = 0;

    private void Awake()
    {
        this.weaponNumber = this.GetComponent<StoredWeaponNumber>().GetWeaponNumber();
        weaponInfo = WeaponManager.Instance.GetCurrentWeaponInfoList()[weaponNumber];
    }

    void Start()
    {
        knockback = weaponInfo.knockback;
        isCoolDown = false;
        isAttackPossible = false;
    }

    void Update()
    {
        // ���� ���̰� �÷��̾ ���� �ʾ��� ��
        if (!GameRoot.Instance.GetIsRoundClear())
        {
            GameObject closetMonster = GetClosetMonster();

            // ���� ����� ���͸� ã�Ҵٸ�
            if (closetMonster != null)
            {
                // ���Ⱑ ��Ÿ���� �ƴ϶�� ����Ѵ�
                if (!isCoolDown)
                {
                    StartCoroutine(Attack(closetMonster));
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���Ⱑ ���Ϳ� ��Ҵٸ�
        if (collision.TryGetComponent<MonsterControl>(out MonsterControl monsterControl) &&
            isAttackPossible)
        {
            GameObject hitedMonster = collision.gameObject;
            float monsterHP = monsterControl.GetMonsterCurrentHP();
            bool isCritical = false;

            // ����� ���
            // ũ��Ƽ�� �߻� �� ���
            // ġ��Ÿ ����
            float random = Random.Range(0f, 100f);
            if (random < RealtimeInfoManager.Instance.GetCritical())
            {
                isCritical = true;
            }
            else
            {
                isCritical = false;
            }

            // ����� ��� �õ�
            TryHPDrain();

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
            PrintText(hitedMonster.transform.position, finalDamage, Color.white);

            // �˹� ����
            Rigidbody2D hitedMonsterRb2D = hitedMonster.GetComponent<Rigidbody2D>();
            Vector2 knockbackVector = PlayerControl.Instance.transform.position - hitedMonster.transform.position;
            hitedMonsterRb2D.AddForce(knockback * -knockbackVector.normalized * 0.5f, ForceMode2D.Impulse);
        }
    }

    public IEnumerator Attack(GameObject closetMonster)
    {
        // ��Ÿ�� on
        isCoolDown = true;
        // ���� ���� on
        isAttackPossible = true;
        // ������ ����� ���
        // (���� ����� + ���� ����� * ���� ���) * �����%
        damage = Mathf.FloorToInt(
            (weaponInfo.damage + weaponInfo.damageCoeff * RealtimeInfoManager.Instance.GetFixedDMG())
                                                * ((RealtimeInfoManager.Instance.GetDMGPercent() + 100) / 100));
        // ������� �ּ� 1
        if (damage <= 0)
            damage = 1;

        // ������ ��Ÿ�� ��� (���� ��ų ���Ӱ� ���� ����)
        // ���� �⺻ ��Ÿ�� - (�⺻ ��Ÿ�� * (���ݼӵ� / (100 + ���ݼӵ�)))
        coolDown = weaponInfo.coolDown -
                       weaponInfo.coolDown * RealtimeInfoManager.Instance.GetATKSpeed() / (100 + RealtimeInfoManager.Instance.GetATKSpeed());

        // ����� ���Ϳ��� �ֵθ���
        Vector2 direction = closetMonster.transform.position - this.transform.position;
        yield return StartCoroutine(this.GetComponent<Swing>().
                        SwingMovement(direction.normalized, weaponInfo.range * 0.75f, Mathf.FloorToInt(this.coolDown * 60f)));
        // ���� ���� off
        isAttackPossible = false;

        yield return new WaitForSeconds(coolDown);
        isCoolDown = false;
    }

    public void TrackingClosetMonster(GameObject closetMonster)
    {
        float rotateY = 0f;
        float rotateZ = 0f;
        Vector2 vec = new Vector2(closetMonster.transform.position.x - this.transform.position.x,
                                  closetMonster.transform.position.y - this.transform.position.y);
        Vector2 norm = vec.normalized;

        rotateZ = Mathf.Atan2(norm.y, norm.x) * Mathf.Rad2Deg;

        // �����ʿ� ���Ͱ� ���� ��
        if (rotateZ < 90f || rotateZ > -90f)
        {
            rotateY = 0f;
        }

        // ���ʿ� ���Ͱ� ���� ��
        if (rotateZ >= 90f || rotateZ <= -90f)
        {
            rotateY = 180f;
            if (rotateZ >= 90f)
                rotateZ = 180f - rotateZ;
            else if (rotateZ <= -90f)
                rotateZ = -(180f + rotateZ);
        }

        this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, rotateY, rotateZ);
    }

    public GameObject GetClosetMonster()
    {
        // ���� �����ϴ� ���� ����� �޾ƿ´�.
        List<GameObject> Monsters = new();
        Monsters = SpawnManager.Instance.GetCurrentMonsters();

        GameObject closetMonster = null;
        float closetDistance = float.MaxValue;

        float range = Mathf.Floor(weaponInfo.range * ((RealtimeInfoManager.Instance.GetRange() + 100) / 100) * 100) / 100;

        foreach (GameObject monster in Monsters)
        {
            float dis = Vector2.Distance(this.transform.position, monster.transform.position);

            if (dis < range && dis < closetDistance)
            {
                closetMonster = monster;
                closetDistance = dis;
            }
        }

        return closetMonster;
    }

    void TryHPDrain()
    {
        float random = Random.Range(0f, 100f);
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

    void ActivateNormalItem44(MonsterInfo monsterInfo)
    {
        if (ItemManager.Instance.GetOwnNormalItemList()[44] > 0)
        {
            float monsterSpeed = monsterInfo.GetMonsterMovementSpeed();
            monsterInfo.SetMonsterMovementSpeed(monsterSpeed * 0.9f);
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
                ColorUtility.TryParseHtmlString("#1FDE38", out color);
                PrintText(PlayerControl.Instance.transform.position, 1, color);
            }
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

    // ���� ����Ʈ, ������ �� ����� 25% ����
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

    void PrintText(Vector3 position, float num, Color color)
    {
        // ����� �ؽ�Ʈ ���
        GameObject textObject = Resources.Load<GameObject>("Prefabs/DamageText");
        TextMeshPro tmPro = textObject.GetComponent<TextMeshPro>();

        GameObject copy = Instantiate(textObject);
        tmPro.text = num.ToString();
        tmPro.color = color;

        Vector3 randomPos = new Vector3(Random.Range(-0.4f, 0.4f), Random.Range(0.4f, 0.6f), 0f);
        copy.transform.position = position + randomPos;
    }
}
