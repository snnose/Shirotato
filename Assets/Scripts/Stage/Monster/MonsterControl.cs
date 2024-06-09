using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonsterControl : MonoBehaviour
{
    private GameObject waffle;
    private GameObject milk;
    private GameObject box;

    private Rigidbody2D monsterRb2D;
    private Collider2D monsterCollider;
    private MonsterInfo monsterInfo;

    private AudioSource monsterBeHitedSound;

    private float currentHP = 0;

    IEnumerator vanishing;

    void Awake()
    {
        waffle = Resources.Load<GameObject>("Prefabs/Drops/Waffle");
        milk = Resources.Load<GameObject>("Prefabs/Drops/Milk");
        box = Resources.Load<GameObject>("Prefabs/Drops/Box");
        monsterRb2D = this.GetComponent<Rigidbody2D>();
        monsterCollider = this.GetComponent<Collider2D>();
        monsterInfo = this.GetComponent<MonsterInfo>();
        monsterBeHitedSound = this.GetComponent<AudioSource>();
    }
    
    void Start()
    {
        monsterBeHitedSound.volume = 0.15f * ConfigManager.Instance.masterVolume * ConfigManager.Instance.effectVolume;
        currentHP = monsterInfo.GetMonsterHP();
        vanishing = Vanishing();
    }

    void Update()
    {
        // ���� �̿� ��� ����
        // ��ó�� �÷��̾ ������ �浹 ���� ���°� �ȴ�
        if (monsterInfo.type != "Potato" && currentHP > 0)
            DetectNearbyPlayer();

        // ���Ͱ� ���� ���� ó��
        if (currentHP <= 0 && vanishing != null)
        {
            // ������ ������Ʈ ����
            RemoveComponent();
            // ���Ͱ� ���ۺ��� ���鼭 �۾��� �Ŀ� �ı��ȴ� 
            StartCoroutine(vanishing);
            // ���� ���� ���� ����Ʈ���� ����
            SpawnManager.Instance.GetCurrentMonsters().Remove(this.gameObject);
            // RareItem27 ���� �� ȿ�� �ߵ�
            ActivateRareItem27();
            // EpicItem18 ���� �� ȿ�� �ߵ�
            ActivateEpicItem18();
            // TofuLarge�� ���� ��� TofuSmall 3���� ����
            if (this.TryGetComponent<SpawnTofuSmall>(out SpawnTofuSmall spawnTofuSmall))
            {
                StartCoroutine(SpawnManager.Instance.StartSpawnTofuSmall(this.transform.position));
            }
            // ����, �Ҹ�ǰ, ���� ��� ó��
            Drop();
            vanishing = null;  
        }

        // ���� ���� ���� ó��
        if (GameRoot.Instance.GetIsRoundClear())
        {
            SpawnManager.Instance.GetCurrentMonsters().Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject == GameObject.FindGameObjectWithTag("Player"))
            //this.monsterCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ź�� �ǰݵȴٸ�
        if (collision.gameObject == GameObject.FindGameObjectWithTag("Bullet"))
        {
            // �ǰݵǸ� �ǰ� ���� ���
            monsterBeHitedSound.pitch = Random.Range(0.95f, 1.05f);
            monsterBeHitedSound.Play();
        }

        // ���⿡ �ǰ� �� ���
        if (collision.gameObject == GameObject.FindGameObjectWithTag("Weapon"))
        {
            // �ش� ������ ���� ���� ���ο� ���� ���� ����� �����ȴ�
            // �浹 ������ ���⸶�� ������ ��ũ��Ʈ�� �ٸ��Ƿ�
            // �� ������ ��ũ��Ʈ���� ���� ���� ���θ� �˻��Ѵ�
            bool ret = false;

            switch (collision.gameObject.GetComponent<SpriteRenderer>().sprite.name)
            {
                case "����̼�":
                    ret = collision.gameObject.GetComponent<MeleeWeaponControl>().GetIsAttackPossible();
                    break;

                case "��ġ":
                    ret = collision.gameObject.GetComponent<HammerControl>().GetIsAttackPossible();
                    break;

                case "��":
                    ret = collision.gameObject.GetComponent<BrushControl>().GetIsAttackPossible();
                    break;

                case "�����":
                    ret = collision.gameObject.GetComponent<BatControl>().GetIsAttackPossible();
                    break;

                case "����Į":
                    ret = collision.gameObject.GetComponent<CandyKnifeControl>().GetIsAttackPossible();
                    break;

                case "ȭ���Ϲ���":
                    ret = collision.gameObject.GetComponent<SwordControl>().GetIsAttackPossible();
                    break;

                case "ö���":
                    ret = collision.gameObject.GetComponent<TetsusaigaControl>().GetIsAttackPossible();
                    break;

                default:
                    ret = false;
                    break;
            }

            // ���� ������ true�� �� �ǰݵǸ� ���� ���
            if (ret)
            {
                monsterBeHitedSound.pitch = Random.Range(0.95f, 1.05f);
                monsterBeHitedSound.Play();
            }
            
        }
    }

    // ��ó�� �÷��̾ �ν��ϸ� isTrigger = True
    private void DetectNearbyPlayer()
    {
        // �÷��̾ �޾ƿ´�
        GameObject player = PlayerControl.Instance.gameObject;

        float closetDistance = float.MaxValue;
        float range = 2f;

        float dis = Vector2.Distance(this.transform.position, player.transform.position);

        if (dis < range && dis < closetDistance)
        {
            closetDistance = dis;
        }

        // range ����� Ʈ���� on, �ƴ� ��� off
        if (closetDistance < range)
            monsterCollider.isTrigger = true;
        else
            monsterCollider.isTrigger = false;
    }

    private IEnumerator Vanishing()
    {
        float rotationSpeed = 360f;
        float scaleSpeed = 2f;      // ũ�� ��� �ӵ�
        float timeElapsed = 0f;

        // 0.5�ʿ� ���� ȸ�� �� ��ҵȴ�
        while (timeElapsed < 0.5f)
        {
            transform.localScale *= 1f - scaleSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // ������Ʈ �ı�
        Destroy(this.gameObject);
    }

    // óġ���� �� ������ ������Ʈ�� ����
    private void RemoveComponent()
    {
        // Rigidbody2D ����
        if (this.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb2D))
        {
            Destroy(GetComponent<Rigidbody2D>());
        }
        // CapsuleCollider2D ����
        if (this.TryGetComponent<CapsuleCollider2D>(out CapsuleCollider2D capsuleCollider2D))
            Destroy(GetComponent<CapsuleCollider2D>());

        // CircleCollider2D ����
        if (this.TryGetComponent<CircleCollider2D>(out CircleCollider2D circleCollider2D))
            Destroy(GetComponent<CircleCollider2D>());

        // PolygonCollider2D ����
        if (this.TryGetComponent<PolygonCollider2D>(out PolygonCollider2D polygonCollider2D))
            Destroy(GetComponent<PolygonCollider2D>());

        // �����ϴ� ��� ����
        if (this.TryGetComponent<ChargeToPlayer>(out ChargeToPlayer chargeToPlayer))
            Destroy(GetComponent<ChargeToPlayer>());

        // �÷��̾� ���� ��� ����
        if (this.TryGetComponent<ChasePlayer>(out ChasePlayer chasePlayer))
            Destroy(GetComponent<ChasePlayer>());

        // ����ü �߻� ��� ����
        if (this.TryGetComponent<FireProjectile>(out FireProjectile fireProjectile))
            Destroy(GetComponent<FireProjectile>());

        // ����Ƣ�� ���� ��� ���� (�ǰ� �� ���� ���� ����ü)
        //if (this.TryGetComponent<FrenchFriesInherentAbility>(out FrenchFriesInherentAbility frenchFriesInherentAbility))
            //Destroy(GetComponent<FrenchFriesInherentAbility>());

        // �÷��̾� �ֺ��� ��ȸ�ϴ� ��� ����
        if (this.TryGetComponent<MoveAroundPlayer>(out MoveAroundPlayer moveAroundPlayer))
            Destroy(GetComponent<MoveAroundPlayer>());

        // ���͸� ���󰡴� ��� ����
        if (this.TryGetComponent<MoveToEnemies>(out MoveToEnemies moveToEnemies))
            Destroy(GetComponent<MoveToEnemies>());

        // �÷��̾�� �������� ��� ����
        if (this.TryGetComponent<RunAwayFromPlayer>(out RunAwayFromPlayer runAwayFromPlayer))
            Destroy(GetComponent<RunAwayFromPlayer>());
    }

    // ���� ��� �� ��� ó���� �Ѵ�
    private void Drop()
    {
        Vector2 dropPos;

        // �ش� ������ ��� ��� �� ��ŭ ���� ���
        int dropWaffleCount = monsterInfo.GetWaffleDropCount();
        for (int i = 0; i < dropWaffleCount; i++)
        {
            // ����Ǵ� ������Ʈ ��ġ�� ������ �ο�
            float RandomX = Random.Range(-0.5f, 0.5f);
            float RandomY = Random.Range(-0.5f, 0.5f);

            dropPos = this.transform.position + new Vector3(RandomX, RandomY);
            GameObject copy = Instantiate(waffle, dropPos, Quaternion.identity);
        }

        // �ش� ������ �Ҹ�ǰ ��� Ȯ���� ���� ����, ���� ��� ����
        float consumableRandom = Random.Range(0.0f, 1.0f);
        float lootRandom = Random.Range(0.0f, 1.0f);

        // �÷��̾��� ����� ����� ������ ��� Ȯ������ ������ ������ ���
        if (consumableRandom <= monsterInfo.GetMonsterConsumableDropRate() * (1f + (PlayerInfo.Instance.GetLuck() / 100)))
        {
            dropPos = this.transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
            // ���� ��� Ȯ������ ����Ѵ�
            if (lootRandom <= monsterInfo.GetMonsterLootDropRate() * (1f + (PlayerInfo.Instance.GetLuck() / 100)))
            {
                GameObject copy = Instantiate(box, dropPos, Quaternion.identity);
            }
            // ���ڰ� ������� ������ ������ ���
            else
            {
                GameObject copy = Instantiate(milk, dropPos, Quaternion.identity);
            }
        }
    }

    // ���� óġ �� Ȯ���� ���� �� ����� ������� ������ ������ ���
    void ActivateRareItem27()
    {
        int count = ItemManager.Instance.GetOwnRareItemList()[27];

        // RareItem27�� �����ߴٸ�
        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                int random = Random.Range(0, 101);
                // 25% Ȯ���� ������ ȿ���� �ߵ��Ѵ�
                if (random < 25f)
                {
                    // ������ �� ����
                    random = Random.Range(0, SpawnManager.Instance.GetCurrentMonsters().Count);
                    GameObject monster = SpawnManager.Instance.GetCurrentMonsters()[random];

                    // ����� 25% ��ŭ ������� ���Ѵ�
                    float monsterHP = monster.GetComponent<MonsterInfo>().GetMonsterHP();
                    int damage = Mathf.FloorToInt(RealtimeInfoManager.Instance.GetLuck() * 0.25f);
                    if (damage <= 0)
                        damage = 1;

                    monsterHP -= damage;
                    monster.GetComponent<MonsterInfo>().SetMonsterHP(monsterHP);

                    // �ؽ�Ʈ ���
                    PrintText(monster.transform, Color.white, damage);
                }
            }
        }
    }

    // �� óġ �� ������ �������� (1 + ���� ����� 100%) ������� ������ ����ü �߻�
    private void ActivateEpicItem18()
    {
        int count = ItemManager.Instance.GetOwnEpicItemList()[18];

        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                // ������ ���� ��ŭ ����ü�� �߻��Ѵ�
                FireEpicItem18Bullet();
            }
        }
    }

    void FireEpicItem18Bullet()
    {
        // �Ѿ� ����
        GameObject bullet = Resources.Load<GameObject>("Prefabs/Weapons/Bullet");
        GameObject copy = Instantiate(bullet, this.transform.position, this.transform.rotation);

        // ������ ����� ���
        // (1 + �����%) * (1 + ���� ����� 100%)
        int damage = Mathf.FloorToInt((1 + RealtimeInfoManager.Instance.GetDMGPercent() / 100) *
                                              (1 + RealtimeInfoManager.Instance.GetFixedDMG()));

        // �Ѿ˿� ������� ���� Ƚ�� ����
        copy.GetComponent<BulletControl>().SetDamage(damage);
        copy.GetComponent<BulletControl>().SetPierceCount(0);

        // ���� �������� �߻�
        Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        copy.GetComponent<Rigidbody2D>().AddForce(direction.normalized * 50f, ForceMode2D.Impulse);
        // �߻�� �������� �Ѿ��� ���ϵ��� ȸ���� ����
        copy.transform.rotation = Quaternion.Euler(copy.transform.rotation.x, 
                                                   copy.transform.rotation.y, 
                                                   Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }

    public void PrintText(Transform transform, Color color, int num)
    {
        // �ؽ�Ʈ ���
        GameObject textObject = Resources.Load<GameObject>("Prefabs/DamageText");
        TextMeshPro tmPro = textObject.GetComponent<TextMeshPro>();

        // �ؽ�Ʈ �� ���� ����
        tmPro.text = num.ToString();
        tmPro.color = color;

        GameObject copy = Instantiate(textObject);
        Vector3 randomPos = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(0.4f, 0.6f), 0f);
        copy.transform.position = transform.position + randomPos;
    }

    public void SetMonsterCurrentHP(float hp)
    {
        this.currentHP = hp;

        // �ִ� ü�º��� ���� ��� ����
        if (hp >= this.GetComponent<MonsterInfo>().GetMonsterHP())
            this.currentHP = this.GetComponent<MonsterInfo>().GetMonsterHP();
    }

    public float GetMonsterCurrentHP()
    {
        return this.currentHP;
    }
}
