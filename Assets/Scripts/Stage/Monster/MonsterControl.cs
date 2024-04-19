using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonsterControl : MonoBehaviour
{
    private GameObject player;
    private GameObject waffle;
    private GameObject milk;
    private GameObject box;

    private Rigidbody2D monsterRb2D;
    private Collider2D monsterCollider;
    private MonsterInfo monsterInfo;

    private float currentHP = 0;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        waffle = Resources.Load<GameObject>("Prefabs/Drops/Waffle");
        milk = Resources.Load<GameObject>("Prefabs/Drops/Milk");
        box = Resources.Load<GameObject>("Prefabs/Drops/Box");
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
        // 몬스터가 죽을 때의 처리
        if (currentHP <= 0)
        {
            // RareItem27 보유 시 효과 발동
            ActivateRareItem27();
            // EpicItem18 보유 시 효과 발동
            ActivateEpicItem18();
            // TofuLarge가 죽은 경우 TofuSmall 3마리 스폰
            if (this.TryGetComponent<SpawnTofuSmall>(out SpawnTofuSmall spawnTofuSmall))
            {
                StartCoroutine(SpawnManager.Instance.StartSpawnTofuSmall(this.transform.position));
            }
            // 와플, 소모품, 상자 드랍 처리
            Drop();
        }

        // 라운드 종료 시의 처리
        if (GameRoot.Instance.GetIsRoundClear())
        {
            SpawnManager.Instance.GetCurrentMonsters().Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        // 현재 생존 몬스터 리스트에서 삭제
        SpawnManager.Instance.GetCurrentMonsters().Remove(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == GameObject.FindGameObjectWithTag("Player"))
            this.monsterCollider.isTrigger = true;
    }

    // 몬스터 사망 시 드랍 처리를 한다
    private void Drop()
    {
        Vector2 dropPos;

        // 해당 몬스터의 재료 드랍 수 만큼 와플 드랍
        int dropWaffleCount = monsterInfo.GetWaffleDropCount();
        for (int i = 0; i < dropWaffleCount; i++)
        {
            // 드랍되는 오브젝트 위치에 랜덤성 부여
            float RandomX = Random.Range(-0.5f, 0.5f);
            float RandomY = Random.Range(-0.5f, 0.5f);

            dropPos = this.transform.position + new Vector3(RandomX, RandomY);
            GameObject copy = Instantiate(waffle, dropPos, Quaternion.identity);
        }

        // 해당 몬스터의 소모품 드랍 확률에 따라 우유, 상자 드랍 결정
        float consumableRandom = Random.Range(0.0f, 1.0f);
        float lootRandom = Random.Range(0.0f, 1.0f);

        // 플레이어의 행운이 적용된 몬스터의 드랍 확률보다 난수가 낮으면 드랍
        if (consumableRandom <= monsterInfo.GetMonsterConsumableDropRate() * (1f + (PlayerInfo.Instance.GetLuck() / 100)))
        {
            dropPos = this.transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
            // 상자 드랍 확률까지 고려한다
            if (lootRandom <= monsterInfo.GetMonsterLootDropRate() * (1f + (PlayerInfo.Instance.GetLuck() / 100)))
            {
                GameObject copy = Instantiate(box, dropPos, Quaternion.identity);
            }
            // 상자가 드랍되지 않으면 우유를 드랍
            else
            {
                GameObject copy = Instantiate(milk, dropPos, Quaternion.identity);
            }
        }
        Destroy(this.gameObject);
    }

    // 몬스터 처치 시 확률에 따라 행운에 비례한 대미지를 입히는 아이템 기능
    void ActivateRareItem27()
    {
        int count = ItemManager.Instance.GetOwnRareItemList()[27];

        // RareItem27을 보유했다면
        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                int random = Random.Range(0, 101);
                // 25% 확률로 아이템 효과가 발동한다
                if (random < 25f)
                {
                    // 랜덤한 적 선정
                    random = Random.Range(0, SpawnManager.Instance.GetCurrentMonsters().Count);
                    GameObject monster = SpawnManager.Instance.GetCurrentMonsters()[random];

                    // 행운의 25% 만큼 대미지를 가한다
                    float monsterHP = monster.GetComponent<MonsterInfo>().GetMonsterHP();
                    int damage = Mathf.FloorToInt(RealtimeInfoManager.Instance.GetLuck() * 0.25f);
                    if (damage <= 0)
                        damage = 1;

                    monsterHP -= damage;
                    monster.GetComponent<MonsterInfo>().SetMonsterHP(monsterHP);

                    // 텍스트 출력
                    PrintText(monster.transform, Color.white, damage);
                }
            }
        }
    }

    // 적 처치 시 랜덤한 방향으로 (1 + 고정 대미지 100%) 대미지를 입히는 투사체 발사
    private void ActivateEpicItem18()
    {
        int count = ItemManager.Instance.GetOwnEpicItemList()[18];

        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                // 아이템 개수 만큼 투사체를 발사한다
                FireEpicItem18Bullet();
            }
        }
    }

    void FireEpicItem18Bullet()
    {
        // 총알 생성
        GameObject bullet = Resources.Load<GameObject>("Prefabs/Weapons/Bullet");
        GameObject copy = Instantiate(bullet, this.transform.position, this.transform.rotation);

        // 아이템 대미지 계산
        // (1 + 대미지%) * (1 + 고정 대미지 100%)
        int damage = Mathf.FloorToInt((1 + RealtimeInfoManager.Instance.GetDMGPercent() / 100) *
                                              (1 + RealtimeInfoManager.Instance.GetFixedDMG()));

        // 총알에 대미지와 관통 횟수 설정
        copy.GetComponent<BulletControl>().SetDamage(damage);
        copy.GetComponent<BulletControl>().SetPierceCount(0);

        // 랜덤 방향으로 발사
        Vector2 direction = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
        copy.GetComponent<Rigidbody2D>().AddForce(direction.normalized * 50f, ForceMode2D.Impulse);
        // 발사된 방향으로 총알이 향하도록 회전값 조정
        copy.transform.rotation = Quaternion.Euler(copy.transform.rotation.x, 
                                                   copy.transform.rotation.y, 
                                                   Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }

    public void PrintText(Transform transform, Color color, int num)
    {
        // 텍스트 출력
        GameObject textObject = Resources.Load<GameObject>("Prefabs/DamageText");
        TextMeshPro tmPro = textObject.GetComponent<TextMeshPro>();

        // 텍스트 및 색상 결정
        tmPro.text = num.ToString();
        tmPro.color = color;

        GameObject copy = Instantiate(textObject);
        Vector3 randomPos = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(0.4f, 0.6f), 0f);
        copy.transform.position = transform.position + randomPos;
    }

    public void SetMonsterCurrentHP(float hp)
    {
        this.currentHP = hp;

        // 최대 체력보다 높을 경우 방지
        if (hp >= this.GetComponent<MonsterInfo>().GetMonsterHP())
            this.currentHP = this.GetComponent<MonsterInfo>().GetMonsterHP();
    }

    public float GetMonsterCurrentHP()
    {
        return this.currentHP;
    }
}
