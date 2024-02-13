using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterControl : MonoBehaviour
{
    private GameObject player;
    private GameObject waffle;

    private Rigidbody2D monsterRb2D;
    private MonsterInfo monsterInfo;

    private float currentHP = 0;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        waffle = Resources.Load<GameObject>("Prefabs/Waffle");
        monsterRb2D = this.GetComponent<Rigidbody2D>();
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
            // �ش� ������ ��� ��� �� ��ŭ ���� ���
            int dropWaffleCount = this.GetComponent<MonsterInfo>().GetDropCount();
            for (int i = 0; i < dropWaffleCount; i++)
            {
                float RandomX = Random.Range(-0.1f, 0.1f);
                float RandomY = Random.Range(-0.1f, 0.1f);

                Vector3 dropPos = this.transform.position + new Vector3(RandomX, RandomY, 0f);
                GameObject copy = Instantiate(waffle, dropPos, Quaternion.identity);
            }
            Destroy(this.gameObject);
        }

        // ���� ���� ���� ó��
        if (GameRoot.Instance.GetIsRoundClear())
        {
            SpawnManager.Instance.GetCurrentMonsters().Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }

    private void FollowingPlayer()
    {
        Vector2 playerPos = player.transform.position;
        Vector2 monsterPos = this.transform.position;

        Vector2 movement = playerPos - monsterPos;
        movement.Normalize();

        monsterRb2D.velocity = movement * monsterInfo.GetMovementSpeed();
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
