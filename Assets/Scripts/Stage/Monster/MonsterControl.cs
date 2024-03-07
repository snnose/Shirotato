using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterControl : MonoBehaviour
{
    private GameObject player;
    private GameObject waffle;
    private GameObject milk;

    private Rigidbody2D monsterRb2D;
    private Collider2D monsterCollider;
    private MonsterInfo monsterInfo;

    private float currentHP = 0;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        waffle = Resources.Load<GameObject>("Prefabs/Goods/Waffle");
        milk = Resources.Load<GameObject>("Prefabs/Goods/Milk");
        monsterRb2D = this.GetComponent<Rigidbody2D>();
        monsterCollider = this.GetComponent<Collider2D>();
        monsterInfo = this.GetComponent<MonsterInfo>();
    }
    
    void Start()
    {
        currentHP = monsterInfo.GetMonsterHP();
    }

    void Update()
    {
        FollowingPlayer();

        // ���Ͱ� ���� ���� ó��
        if (currentHP <= 0)
        {
            // ���� ���� ���� ����Ʈ���� ����
            SpawnManager.Instance.GetCurrentMonsters().Remove(this.gameObject);
            Drop();
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
        if (collision.gameObject == GameObject.FindGameObjectWithTag("Player"))
            this.monsterCollider.isTrigger = true;
    }

    private void FollowingPlayer()
    {
        Vector2 playerPos = player.transform.position;
        Vector2 monsterPos = this.transform.position;

        Vector2 movement = playerPos - monsterPos;
        movement.Normalize();

        monsterRb2D.velocity = movement * monsterInfo.GetMonsterMovementSpeed();
    }

    // ���� ��� �� ��� ó���� �Ѵ�
    private void Drop()
    {
        // ����Ǵ� ������Ʈ ��ġ�� ������ �ο�
        float RandomX = Random.Range(-0.1f, 0.1f);
        float RandomY = Random.Range(-0.1f, 0.1f);

        Vector2 dropPos = this.transform.position + new Vector3(RandomX, RandomY);

        // �ش� ������ ��� ��� �� ��ŭ ���� ���
        int dropWaffleCount = monsterInfo.GetWaffleDropCount();
        for (int i = 0; i < dropWaffleCount; i++)
        {
            GameObject copy = Instantiate(waffle, dropPos, Quaternion.identity);
        }

        // �ش� ������ �Ҹ�ǰ ��� Ȯ���� ���� ����, ���� ��� ����
        float consumableRandom = Random.Range(0.0f, 1.0f);
        float lootRandom = Random.Range(0.0f, 1.0f);

        // �÷��̾��� ����� ����� ������ ��� Ȯ������ ������ ������ ���
        if (consumableRandom <= monsterInfo.GetMonsterConsumableDropRate() * (1f + (PlayerInfo.Instance.GetLuck() / 100)))
        {
            // ���� ��� Ȯ������ ����Ѵ�
            if (lootRandom <= monsterInfo.GetMonsterLootDropRate() * (1f + (PlayerInfo.Instance.GetLuck() / 100)))
            {
                //GameObject copy = Instantiate(box, dropPos, Quaternion.identity);
            }
            // ���ڰ� ������� ������ ������ ���
            else
            {
                GameObject copy = Instantiate(milk, dropPos, Quaternion.identity);
            }
        }
        Destroy(this.gameObject);
    }

    public void SetMonsterCurrentHP(float hp)
    {
        this.currentHP = hp;
    }

    public float GetMonsterCurrentHP()
    {
        return this.currentHP;
    }
}
