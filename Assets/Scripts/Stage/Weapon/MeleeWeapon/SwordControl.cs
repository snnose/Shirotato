using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwordControl : MonoBehaviour, IMeleeWeaponControl
{
    public int weaponNumber { get; set; }
    public WeaponInfo weaponInfo { get; set; }
    public bool isCoolDown { get; set; }

    private bool isAttackPossible;
    private bool attackMode;

    public int damage = 0;
    float coolDown = 1;
    int knockback = 0;
    Quaternion firstRotation;

    private void Awake()
    {
        this.weaponNumber = this.GetComponent<StoredWeaponNumber>().GetWeaponNumber();
        weaponInfo = WeaponManager.Instance.GetCurrentWeaponInfoList()[weaponNumber];

        attackMode = false;
        firstRotation = this.transform.rotation;
    }

    void Start()
    {
        knockback = weaponInfo.knockback;
        isCoolDown = false;
        isAttackPossible = false;
    }

    void Update()
    {
        // 라운드 중이고 플레이어가 죽지 않았을 때
        if (!GameRoot.Instance.GetIsRoundClear())
        {
            GameObject closetMonster = GetClosetMonster();

            // 가장 가까운 몬스터를 찾았다면
            if (closetMonster != null)
            {
                // 무기가 쿨타임이 아니라면 사격한다
                if (!isCoolDown)
                {
                    StartCoroutine(Attack(closetMonster));
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 무기가 몬스터와 닿았다면
        if (collision.TryGetComponent<MonsterControl>(out MonsterControl monsterControl) &&
            isAttackPossible)
        {
            GameObject hitedMonster = collision.gameObject;
            float monsterHP = monsterControl.GetMonsterCurrentHP();
            bool isCritical = false;

            // 대미지 계산
            // 크리티컬 발생 한 경우
            // 치명타 적용
            float random = Random.Range(0f, 100f);
            if (random < RealtimeInfoManager.Instance.GetCritical())
            {
                isCritical = true;
            }
            else
            {
                isCritical = false;
            }

            // 생명력 흡수 시도
            TryHPDrain();

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
        }
    }

    public IEnumerator Attack(GameObject closetMonster)
    {
        // 쿨타임 on
        isCoolDown = true;
        // 공격 판정 on
        isAttackPossible = true;
        // 무기의 대미지 계산
        // (무기 대미지 + 고정 대미지 * 무기 계수) * 대미지%
        damage = Mathf.FloorToInt(
            (weaponInfo.damage + weaponInfo.damageCoeff * RealtimeInfoManager.Instance.GetFixedDMG())
                                                * ((RealtimeInfoManager.Instance.GetDMGPercent() + 100) / 100));
        // 대미지는 최소 1
        if (damage <= 0)
            damage = 1;

        // 무기의 쿨타임 계산 (롤의 스킬 가속과 같은 공식)
        // 무기 기본 쿨타임 - (기본 쿨타임 * (공격속도 / (100 + 공격속도)))
        coolDown = weaponInfo.coolDown -
                       weaponInfo.coolDown * RealtimeInfoManager.Instance.GetATKSpeed() / (100 + RealtimeInfoManager.Instance.GetATKSpeed());

        // 가까운 몬스터에게 휘두른다
        Vector2 direction = closetMonster.transform.position - this.transform.position;
        // 휘두르기 - 찌르기 순으로 반복해 공격한다
        if (!attackMode)
        {
            yield return StartCoroutine(this.GetComponent<Swing>().
                            SwingMovement(direction.normalized, weaponInfo.range * 0.65f, Mathf.FloorToInt(this.coolDown * 60f)));
            // 찌르기로 전환
            attackMode = true;
        }
        // 찌르기
        else
        {
            // 대상을 바라보도록 한다
            TrackingClosetMonster(closetMonster);
            yield return StartCoroutine(this.GetComponent<Stab>().
                            StabMovement(direction.normalized * weaponInfo.range, Mathf.FloorToInt(this.coolDown * 60f)));
            // 공격이 끝나면 초기 회전으로 초기화
            this.transform.rotation = firstRotation;
            // 휘두르기로 전환
            attackMode = false;
        }
        // 공격 판정 off
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

        // 오른쪽에 몬스터가 있을 때
        if (rotateZ < 90f || rotateZ > -90f)
        {
            rotateY = 0f;
            rotateZ -= 45f;
        }

        // 왼쪽에 몬스터가 있을 때
        if (rotateZ >= 90f || rotateZ <= -90f)
        {
            rotateY = 180f;
            // 좌상단
            if (rotateZ >= 90f)
                rotateZ = 180f - rotateZ - 45f;
            else if (rotateZ <= -90f)
                rotateZ = -(180f + rotateZ + 45f);
        }

        this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, rotateY, rotateZ);
    }

    public GameObject GetClosetMonster()
    {
        // 현재 존재하는 몬스터 목록을 받아온다.
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

    void ActivateNormalItem44(MonsterInfo monsterInfo)
    {
        if (ItemManager.Instance.GetOwnNormalItemList()[44] > 0)
        {
            float monsterSpeed = monsterInfo.GetMonsterMovementSpeed();
            monsterInfo.SetMonsterMovementSpeed(monsterSpeed * 0.9f);
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
                ColorUtility.TryParseHtmlString("#1FDE38", out color);
                PrintText(PlayerControl.Instance.transform.position, 1, color);
            }
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
                int waffleDropCount = monsterInfo.GetWaffleDropCount() + 1;
                monsterInfo.SetMonsterWaffleDropCount(waffleDropCount);
            }
        }
    }

    // 엘리트, 보스 몹 공격 시 25% 추가 대미지
    float ActivateEpicItem32(float damage, MonsterInfo monsterInfo)
    {
        float tmp = damage;
        int count = ItemManager.Instance.GetOwnEpicItemList()[32];

        if (count > 0)
        {
            // 일반 몹이 아닐 경우
            if (monsterInfo.type != "General")
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
            if (monsterControl.gameObject.GetComponent<MonsterInfo>().type != "Boss")
                tmp += 0.1f * monsterCurrentHP;
            else
                tmp += 0.01f * monsterCurrentHP;
        }

        return tmp;
    }

    void PrintText(Vector3 position, float damage, Color color)
    {
        // 대미지 텍스트 출력
        GameObject textObject = Resources.Load<GameObject>("Prefabs/DamageText");
        TextMeshPro tmPro = textObject.GetComponent<TextMeshPro>();
        tmPro.text = "";

        Color green = Color.white; ColorUtility.TryParseHtmlString("#1FDE38", out green);

        // 텍스트 및 색상 결정
        if (color.Equals(green))
            tmPro.text += "+";
        tmPro.text += damage.ToString();
        tmPro.color = color;

        GameObject copy = Instantiate(textObject);
        Vector3 randomPos = new Vector3(Random.Range(-0.4f, 0.4f), Random.Range(0.4f, 0.6f), 0f);
        copy.transform.position = position + randomPos;
    }

    public bool GetIsAttackPossible()
    {
        return this.isAttackPossible;
    }
}
