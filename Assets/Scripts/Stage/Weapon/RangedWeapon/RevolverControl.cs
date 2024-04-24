using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolverControl : MonoBehaviour, IRangedWeaponControl
{
    public int weaponNumber { get; set; }
    public WeaponInfo weaponInfo { get; set; }
    public bool isCoolDown { get; set; }

    AudioSource shootSound;

    private void Awake()
    {
        shootSound = this.GetComponent<AudioSource>();
    }

    // 리볼버는 장탄 수 6발을 갖는다
    private int bulletCount = 6;
    void Start()
    {
        shootSound.volume = 0.1f * ConfigManager.Instance.masterVolume * ConfigManager.Instance.effectVolume;
        weaponNumber = this.GetComponent<StoredWeaponNumber>().GetWeaponNumber();
        weaponInfo = WeaponManager.Instance.GetCurrentWeaponInfoList()[weaponNumber];
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
                TrackingClosetMonster(closetMonster);

                // 무기가 쿨타임이 아니라면 사격한다
                if (!isCoolDown)
                {
                    // 한번에 여러 발을 쏘는 무기가 있을 경우를 고려
                    for (int i = 0; i < weaponInfo.GetShootBulletCount(); i++)
                    {
                        StartCoroutine(Attack(closetMonster));
                    }
                }
            }
        }
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
        }

        // 왼쪽에 몬스터가 있을 때
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

    public IEnumerator Attack(GameObject closetMonster)
    {
        PlayShootSound();

        // 총알 생성
        GameObject bullet = Resources.Load<GameObject>("Prefabs/Weapons/Bullet");
        GameObject copy = Instantiate(bullet, this.transform.position, this.transform.rotation);

        // 무기의 대미지 계산
        // (무기 대미지 + 고정 대미지 * 무기 계수) * 대미지%
        int damage = Mathf.FloorToInt(
            (weaponInfo.damage + weaponInfo.damageCoeff * RealtimeInfoManager.Instance.GetFixedDMG())
                                                * ((RealtimeInfoManager.Instance.GetDMGPercent() + 100) / 100));

        // 대미지는 최소 1
        if (damage <= 0)
            damage = 1;

        // 무기의 쿨타임 계산 (롤의 스킬 가속과 같은 공식)
        // 무기 기본 쿨타임 - (기본 쿨타임 * (공격속도 / (100 + 공격속도)))
        float coolDown = weaponInfo.coolDown -
                       weaponInfo.coolDown * RealtimeInfoManager.Instance.GetATKSpeed() / (100 + RealtimeInfoManager.Instance.GetATKSpeed());

        // 총알에 대미지와 넉백, 관통 횟수, 관통 대미지 설정
        copy.GetComponent<BulletControl>().SetDamage(damage);
        copy.GetComponent<BulletControl>().SetKnockback(weaponInfo.knockback);
        copy.GetComponent<BulletControl>().SetPierceCount(weaponInfo.pierceCount);
        copy.GetComponent<BulletControl>().SetPierceDamage(weaponInfo.GetPierceDamage());

        // 가까운 몬스터에게 발사
        Vector2 direction = closetMonster.transform.position - copy.transform.position;
        copy.GetComponent<Rigidbody2D>().AddForce(direction.normalized * 65f, ForceMode2D.Impulse);

        // 장전된 탄을 소비한다      
        bulletCount--;

        // 장전된 탄을 모두 썼다면 재장전한다
        if (bulletCount <= 0)
        {
            bulletCount = 6;
            // 재장전 시간 5배 증가
            coolDown *= 5f;
        }

        isCoolDown = true;
        yield return new WaitForSeconds(coolDown);
        isCoolDown = false;
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

    private void PlayShootSound()
    {
        shootSound.Play();
    }
}
