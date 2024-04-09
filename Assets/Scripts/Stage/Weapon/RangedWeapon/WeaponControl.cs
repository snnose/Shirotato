using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    private WeaponInfo weaponInfo;
    private List<GameObject> Monsters;

    private int weaponNumber = -1;
    private bool isCoolDown = false;

    private int bulletCount = -1;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        this.weaponNumber = this.GetComponent<StoredWeaponNumber>().GetWeaponNumber();
        weaponInfo = WeaponManager.Instance.GetCurrentWeaponInfoList()[weaponNumber];

        // 무기가 리볼버라면 장탄을 갖는다
        if (weaponInfo.weaponName == "Revolver")
        {
            bulletCount = 6;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 라운드 중이고 플레이어가 죽지 않았을 때
        if (!GameRoot.Instance.GetIsRoundClear())
        {
            Monsters = SpawnManager.Instance.GetCurrentMonsters();

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

    // 가까운 몬스터 추적
    void TrackingClosetMonster(GameObject closetMonster)
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

    IEnumerator Attack(GameObject closetMonster)
    {
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
        // 무기가 샷건이라면 발사하는 방향에 오차를 준다
        if (weaponInfo.weaponName == "Shotgun")
        { 
            direction = direction.normalized + new Vector2(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f));
        }
        copy.GetComponent<Rigidbody2D>().AddForce(direction.normalized * 65f, ForceMode2D.Impulse);

        // 무기가 리볼버라면 장전된 탄을 소비한다
        if (weaponInfo.weaponName == "Revolver")
        {
            bulletCount--;
        }

        // 무기가 리볼버이고 장전된 탄을 모두 썼다면 재장전한다
        if (weaponInfo.weaponName == "Revolver" &&
            bulletCount <= 0)
        {
            bulletCount = 6;
            // 재장전 시간 5배 증가
            coolDown *= 5f;
        }

        isCoolDown = true;
        yield return new WaitForSeconds(coolDown);
        isCoolDown = false;
    }

    // 가까운 몬스터를 찾는다
    GameObject GetClosetMonster()
    {
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
}
