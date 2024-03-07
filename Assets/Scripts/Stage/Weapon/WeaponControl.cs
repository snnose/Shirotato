using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    private WeaponInfo weaponInfo;
    private List<GameObject> Monsters;

    // Weapon info

    public int weaponNumber = -1;

    public bool isCoolDown = false;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        weaponInfo = WeaponManager.Instance.GetCurrentWeaponInfoList()[weaponNumber];
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
                    //Debug.Log("진입");
                    StartCoroutine(Attack(closetMonster));
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
        GameObject bullet = Resources.Load<GameObject>("Prefabs/Bullet");
        GameObject copy = Instantiate(bullet, this.transform.position, this.transform.rotation);

        // 무기의 대미지 계산
        int damage = Mathf.FloorToInt(
            (weaponInfo.damage + Mathf.FloorToInt(PlayerInfo.Instance.GetFixedDMG()))
                                                * ((PlayerInfo.Instance.GetDMGPercent() + 100) / 100));

        // 무기의 쿨타임 계산
        float coolDown = weaponInfo.coolDown - 
                       weaponInfo.coolDown * PlayerInfo.Instance.GetATKSpeed() / (100 + PlayerInfo.Instance.GetATKSpeed());

        // 총알에 대미지와 관통 횟수 설정
        copy.GetComponent<BulletControl>().SetDamage(damage);
        copy.GetComponent<BulletControl>().SetPierceCount(weaponInfo.pierceCount);

        // 가까운 몬스터에게 발사
        Vector2 direction = closetMonster.transform.position - copy.transform.position;
        copy.GetComponent<Rigidbody2D>().AddForce(direction.normalized * 50f, ForceMode2D.Impulse);

        isCoolDown = true;
        yield return new WaitForSeconds(weaponInfo.coolDown);
        isCoolDown = false;
    }

    // 가까운 몬스터를 찾는다
    GameObject GetClosetMonster()
    {
        GameObject closetMonster = null;
        float closetDistance = float.MaxValue;

        float range = Mathf.Floor(weaponInfo.range * ((PlayerInfo.Instance.GetRange() + 100) / 100) * 100) / 100;

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

    public void SetWeaponNumber(int weaponNumber)
    {
        this.weaponNumber = weaponNumber;
    }
}
