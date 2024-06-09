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
        // 감자 이외 모든 몬스터
        // 근처에 플레이어가 있으면 충돌 무시 상태가 된다
        if (monsterInfo.type != "Potato" && currentHP > 0)
            DetectNearbyPlayer();

        // 몬스터가 죽을 때의 처리
        if (currentHP <= 0 && vanishing != null)
        {
            // 부착된 컴포넌트 제거
            RemoveComponent();
            // 몬스터가 빙글빙글 돌면서 작아진 후에 파괴된다 
            StartCoroutine(vanishing);
            // 현재 생존 몬스터 리스트에서 삭제
            SpawnManager.Instance.GetCurrentMonsters().Remove(this.gameObject);
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
            vanishing = null;  
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
        //if (collision.gameObject == GameObject.FindGameObjectWithTag("Player"))
            //this.monsterCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 탄에 피격된다면
        if (collision.gameObject == GameObject.FindGameObjectWithTag("Bullet"))
        {
            // 피격되면 피격 사운드 출력
            monsterBeHitedSound.pitch = Random.Range(0.95f, 1.05f);
            monsterBeHitedSound.Play();
        }

        // 무기에 피격 될 경우
        if (collision.gameObject == GameObject.FindGameObjectWithTag("Weapon"))
        {
            // 해당 무기의 공격 판정 여부에 따라 사운드 출력이 결정된다
            // 충돌 감지된 무기마다 부착된 스크립트가 다르므로
            // 각 무기의 스크립트에서 공격 판정 여부를 검사한다
            bool ret = false;

            switch (collision.gameObject.GetComponent<SpriteRenderer>().sprite.name)
            {
                case "고양이손":
                    ret = collision.gameObject.GetComponent<MeleeWeaponControl>().GetIsAttackPossible();
                    break;

                case "망치":
                    ret = collision.gameObject.GetComponent<HammerControl>().GetIsAttackPossible();
                    break;

                case "솔":
                    ret = collision.gameObject.GetComponent<BrushControl>().GetIsAttackPossible();
                    break;

                case "방망이":
                    ret = collision.gameObject.GetComponent<BatControl>().GetIsAttackPossible();
                    break;

                case "사탕칼":
                    ret = collision.gameObject.GetComponent<CandyKnifeControl>().GetIsAttackPossible();
                    break;

                case "화도일문자":
                    ret = collision.gameObject.GetComponent<SwordControl>().GetIsAttackPossible();
                    break;

                case "철쇄아":
                    ret = collision.gameObject.GetComponent<TetsusaigaControl>().GetIsAttackPossible();
                    break;

                default:
                    ret = false;
                    break;
            }

            // 공격 판정이 true일 때 피격되면 사운드 출력
            if (ret)
            {
                monsterBeHitedSound.pitch = Random.Range(0.95f, 1.05f);
                monsterBeHitedSound.Play();
            }
            
        }
    }

    // 근처에 플레이어를 인식하면 isTrigger = True
    private void DetectNearbyPlayer()
    {
        // 플레이어를 받아온다
        GameObject player = PlayerControl.Instance.gameObject;

        float closetDistance = float.MaxValue;
        float range = 2f;

        float dis = Vector2.Distance(this.transform.position, player.transform.position);

        if (dis < range && dis < closetDistance)
        {
            closetDistance = dis;
        }

        // range 내라면 트리거 on, 아닐 경우 off
        if (closetDistance < range)
            monsterCollider.isTrigger = true;
        else
            monsterCollider.isTrigger = false;
    }

    private IEnumerator Vanishing()
    {
        float rotationSpeed = 360f;
        float scaleSpeed = 2f;      // 크기 축소 속도
        float timeElapsed = 0f;

        // 0.5초에 걸쳐 회전 및 축소된다
        while (timeElapsed < 0.5f)
        {
            transform.localScale *= 1f - scaleSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // 오브젝트 파괴
        Destroy(this.gameObject);
    }

    // 처치당할 때 부착된 컴포넌트를 제거
    private void RemoveComponent()
    {
        // Rigidbody2D 제거
        if (this.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb2D))
        {
            Destroy(GetComponent<Rigidbody2D>());
        }
        // CapsuleCollider2D 제거
        if (this.TryGetComponent<CapsuleCollider2D>(out CapsuleCollider2D capsuleCollider2D))
            Destroy(GetComponent<CapsuleCollider2D>());

        // CircleCollider2D 제거
        if (this.TryGetComponent<CircleCollider2D>(out CircleCollider2D circleCollider2D))
            Destroy(GetComponent<CircleCollider2D>());

        // PolygonCollider2D 제거
        if (this.TryGetComponent<PolygonCollider2D>(out PolygonCollider2D polygonCollider2D))
            Destroy(GetComponent<PolygonCollider2D>());

        // 돌진하는 기능 제거
        if (this.TryGetComponent<ChargeToPlayer>(out ChargeToPlayer chargeToPlayer))
            Destroy(GetComponent<ChargeToPlayer>());

        // 플레이어 추적 기능 제거
        if (this.TryGetComponent<ChasePlayer>(out ChasePlayer chasePlayer))
            Destroy(GetComponent<ChasePlayer>());

        // 투사체 발사 기능 제거
        if (this.TryGetComponent<FireProjectile>(out FireProjectile fireProjectile))
            Destroy(GetComponent<FireProjectile>());

        // 감자튀김 고유 기능 제거 (피격 시 랜덤 방향 투사체)
        //if (this.TryGetComponent<FrenchFriesInherentAbility>(out FrenchFriesInherentAbility frenchFriesInherentAbility))
            //Destroy(GetComponent<FrenchFriesInherentAbility>());

        // 플레이어 주변을 배회하는 기능 제거
        if (this.TryGetComponent<MoveAroundPlayer>(out MoveAroundPlayer moveAroundPlayer))
            Destroy(GetComponent<MoveAroundPlayer>());

        // 몬스터를 따라가는 기능 제거
        if (this.TryGetComponent<MoveToEnemies>(out MoveToEnemies moveToEnemies))
            Destroy(GetComponent<MoveToEnemies>());

        // 플레이어에게 도망가는 기능 제거
        if (this.TryGetComponent<RunAwayFromPlayer>(out RunAwayFromPlayer runAwayFromPlayer))
            Destroy(GetComponent<RunAwayFromPlayer>());
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
        Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
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
