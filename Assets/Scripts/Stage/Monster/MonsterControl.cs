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

        // 몬스터가 죽을 때의 처리
        if (currentHP <= 0)
        {
            // 현재 생존 몬스터 리스트에서 삭제
            SpawnManager.Instance.GetCurrentMonsters().Remove(this.gameObject);
            Drop();
        }

        // 라운드 종료 시의 처리
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

    // 몬스터 사망 시 드랍 처리를 한다
    private void Drop()
    {
        // 드랍되는 오브젝트 위치에 랜덤성 부여
        float RandomX = Random.Range(-0.1f, 0.1f);
        float RandomY = Random.Range(-0.1f, 0.1f);

        Vector2 dropPos = this.transform.position + new Vector3(RandomX, RandomY);

        // 해당 몬스터의 재료 드랍 수 만큼 와플 드랍
        int dropWaffleCount = monsterInfo.GetWaffleDropCount();
        for (int i = 0; i < dropWaffleCount; i++)
        {
            GameObject copy = Instantiate(waffle, dropPos, Quaternion.identity);
        }

        // 해당 몬스터의 소모품 드랍 확률에 따라 우유, 상자 드랍 결정
        float consumableRandom = Random.Range(0.0f, 1.0f);
        float lootRandom = Random.Range(0.0f, 1.0f);

        // 플레이어의 행운이 적용된 몬스터의 드랍 확률보다 난수가 낮으면 드랍
        if (consumableRandom <= monsterInfo.GetMonsterConsumableDropRate() * (1f + (PlayerInfo.Instance.GetLuck() / 100)))
        {
            // 상자 드랍 확률까지 고려한다
            if (lootRandom <= monsterInfo.GetMonsterLootDropRate() * (1f + (PlayerInfo.Instance.GetLuck() / 100)))
            {
                //GameObject copy = Instantiate(box, dropPos, Quaternion.identity);
            }
            // 상자가 드랍되지 않으면 우유를 드랍
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
