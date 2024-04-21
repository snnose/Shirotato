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
        // 보유 시 관통 횟수 +1, 관통 대미지 - 20%
        ActivateNormalItem43();
        // 보유 시 관통 대미지 +15%
        ActivateRareItem31();
        // 보유 시 관통 횟수 +1
        ActivateEpicItem19();
        // 보유 시 탄성 횟수 +1
        ActivateLegendItem26();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 벽과 충돌하면 총알 파괴
        if (collision.gameObject.TryGetComponent<WallControl>(out WallControl wall))
        {
            //Debug.Log("충돌");
            Destroy(this.gameObject);
        }

        //collision.gameObject == GameObject.FindGameObjectWithTag("Monster")
        // 몬스터에 충돌했을 경우
        if (SpawnManager.Instance.GetCurrentMonsters().Contains(collision.gameObject))
        {
            GameObject hitedMonster = collision.gameObject;
            MonsterControl monsterControl = hitedMonster.GetComponent<MonsterControl>();

            float monsterHP = monsterControl.GetMonsterCurrentHP();

            // 치명타 적용
            float random = Random.Range(0f, 100f);
            if (random < RealtimeInfoManager.Instance.GetCritical())
                isCritical = true;

            // 생명력 흡수 적용
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
            // EpicItem32 보유 시, 공격을 맞은 적이 보스나 엘리트라면 +25% 추가 대미지
            finalDamage = ActivateEpicItem32(finalDamage, collision.GetComponent<MonsterInfo>());
            // 치명타라면 대미지가 2배 증가한다
            if (isCritical)
            {
                finalDamage *= 2.0f;
                finalDamage = ActivateLegendItem20(finalDamage, monsterControl);
            }

            // NormalItem42 보유 시 효과 발동
            // 공격한 적에게 확률에 따라 추가 대미지를 입힌다
            ActivateNormalItem42(monsterControl);
            // NormalItem44 보유 시 효과 발동
            // 공격한 적의 속도를 -10% 감소 시킨다
            ActivateNormalItem44(collision.GetComponent<MonsterInfo>());

            monsterHP -= finalDamage;
            monsterControl.SetMonsterCurrentHP(monsterHP);
            // 크리티컬로 적 처치 시
            if (isCritical && monsterHP <= 0)
            {
                // RareItem36 보유 시 효과 발동
                ActivateRareItem36();
                // EpicItem27 보유 시 효과 발동
                ActivateEpicItem27(collision.GetComponent<MonsterInfo>());
            }

            // 입힌 대미지 출력
            if (isCritical)
                PrintText(hitedMonster.transform.position, finalDamage, Color.yellow);
            else
                PrintText(hitedMonster.transform.position, finalDamage, Color.white);

            // 넉백 적용
            Rigidbody2D hitedMonsterRb2D = hitedMonster.GetComponent<Rigidbody2D>();
            Vector2 knockbackVector = PlayerControl.Instance.transform.position - hitedMonster.transform.position;
            hitedMonsterRb2D.AddForce(knockback * -knockbackVector.normalized * 0.5f, ForceMode2D.Impulse);

            // 튕김 횟수와 관통 횟수 모두 있다면 튕김이 우선시된다
            // 튕김 횟수가 1 이상이라면
            if (bounceCount > 0)
            {
                Bounce(collision.gameObject);
                bounceCount--;
            }
            // 튕김 횟수가 없다면
            else
            {
                // 관통 횟수가 1 이상이라면
                if (pierceCount > 0)
                {
                    // 횟수를 감소시키고 대미지를 절반으로 감소
                    this.pierceCount--;
                    this.damage = Mathf.FloorToInt(this.damage * pierceDamage);
                    if (this.damage <= 0)
                        this.damage = 1;
                }
                // 관통 횟수가 0이면
                else
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }

    // 튕김은 적에게 적중 후 다른 랜덤한 적에게 향하는 기능
    void Bounce(GameObject collision)
    {
        // 현재 존재하는 몬스터 목록을 받아온다.
        List<GameObject> Monsters;
        Monsters = SpawnManager.Instance.GetCurrentMonsters();
        // 랜덤한 몬스터를 선택한다
        int maxNum = Monsters.Count;
        // 몬스터가 단 한마리 존재하면 발동하지 않는다
        if (maxNum == 1)
            return;
        GameObject targetMonster = null;
        while (true)
        {
            int ran = Random.Range(0, maxNum);
            targetMonster = Monsters[Random.Range(0, maxNum)];
            // 타겟팅된 몬스터가 현재 피격된 몬스터와 다르면 탈출
            if (ran != collision.GetComponent<MonsterInfo>().GetMonsterNumber())
                break;
        }

        // 화살 -> 몬스터 벡터를 구한다
        Vector2 direction = targetMonster.transform.position - this.transform.position;
        // 화살의 속도를 초기화한 후
        this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        // 해당 몬스터가 있는 방향으로 다시 발사
        this.GetComponent<Rigidbody2D>().AddForce(direction.normalized * 50f, ForceMode2D.Impulse);
        // 회전 조정
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
                // 대미지 * (2 + 고정 대미지 50%)
                int additionalDamage = Mathf.FloorToInt(
                    (1f + (RealtimeInfoManager.Instance.GetDMGPercent() / 100)) *
                    (2f + 0.5f * RealtimeInfoManager.Instance.GetFixedDMG()));
                // 대미지는 최소 1
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
            // 이동속도 감소는 최대 3중첩
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

    // 크리티컬로 적 처치 시 확률에 따라 HP 1 회복
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

    // 크리티컬로 적 처치 시 와플 1개 추가 드랍
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

    // 크리티컬 발생 시 대상의 현재 체력 10% 추가 대미지 (보스나 엘리트는 1%)
    float ActivateLegendItem20(float damage, MonsterControl monsterControl)
    {
        float tmp = damage;

        if (ItemManager.Instance.GetOwnLegendItemList()[20] > 0)
        {
            float monsterCurrentHP = monsterControl.GetMonsterCurrentHP();

            // 몬스터 타입에 따라 추가 대미지가 변동되도록 조정해야함
            tmp += 0.1f * monsterCurrentHP;
        }

        return tmp;
    }

    // 투사체 탄성 횟수 +1
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
        // 대미지 텍스트 출력
        GameObject textObject = Resources.Load<GameObject>("Prefabs/DamageText");
        TextMeshPro tmPro = textObject.GetComponent<TextMeshPro>();

        // 텍스트 및 색상 결정
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
