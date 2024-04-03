using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    private WeaponInfo weaponInfo;
    private List<GameObject> Monsters;

    public int weaponNumber = -1;
    public bool isCoolDown = false;

    private int bulletCount = -1;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        weaponInfo = WeaponManager.Instance.GetCurrentWeaponInfoList()[weaponNumber];

        // ���Ⱑ ��������� ��ź�� ���´�
        if (weaponInfo.weaponName == "Revolver")
        {
            bulletCount = 6;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ���� ���̰� �÷��̾ ���� �ʾ��� ��
        if (!GameRoot.Instance.GetIsRoundClear())
        {
            Monsters = SpawnManager.Instance.GetCurrentMonsters();

            GameObject closetMonster = GetClosetMonster();

            // ���� ����� ���͸� ã�Ҵٸ�
            if (closetMonster != null)
            {
                TrackingClosetMonster(closetMonster);

                // ���Ⱑ ��Ÿ���� �ƴ϶�� ����Ѵ�
                if (!isCoolDown)
                {
                    StartCoroutine(Attack(closetMonster));
                }
            }
        }
    }

    // ����� ���� ����
    void TrackingClosetMonster(GameObject closetMonster)
    {
        float rotateY = 0f;
        float rotateZ = 0f;
        Vector2 vec = new Vector2(closetMonster.transform.position.x - this.transform.position.x,
                                  closetMonster.transform.position.y - this.transform.position.y);
        Vector2 norm = vec.normalized;

        rotateZ = Mathf.Atan2(norm.y, norm.x) * Mathf.Rad2Deg;

        // �����ʿ� ���Ͱ� ���� ��
        if (rotateZ < 90f || rotateZ > -90f)
        {
            rotateY = 0f;
        }

        // ���ʿ� ���Ͱ� ���� ��
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
        // �Ѿ� ����
        GameObject bullet = Resources.Load<GameObject>("Prefabs/Weapons/Bullet");
        GameObject copy = Instantiate(bullet, this.transform.position, this.transform.rotation);

        // ������ ����� ���
        int damage = Mathf.FloorToInt(
            (weaponInfo.damage + Mathf.FloorToInt(RealtimeInfoManager.Instance.GetFixedDMG()))
                                                * ((RealtimeInfoManager.Instance.GetDMGPercent() + 100) / 100));

        // ������ ��Ÿ�� ��� (���� ��ų ���Ӱ� ���� ����)
        // ���� �⺻ ��Ÿ�� - (�⺻ ��Ÿ�� * (���ݼӵ� / (100 + ���ݼӵ�)))
        float coolDown = weaponInfo.coolDown - 
                       weaponInfo.coolDown * RealtimeInfoManager.Instance.GetATKSpeed() / (100 + RealtimeInfoManager.Instance.GetATKSpeed());

        // �Ѿ˿� ������� ���� Ƚ�� ����
        copy.GetComponent<BulletControl>().SetDamage(damage);
        copy.GetComponent<BulletControl>().SetPierceCount(weaponInfo.pierceCount);

        // ����� ���Ϳ��� �߻�
        Vector2 direction = closetMonster.transform.position - copy.transform.position;
        copy.GetComponent<Rigidbody2D>().AddForce(direction.normalized * 65f, ForceMode2D.Impulse);

        // ���Ⱑ ��������� ������ ź�� �Һ��Ѵ�
        if (weaponInfo.weaponName == "Revolver")
        {
            bulletCount--;
        }

        // ���Ⱑ �������̰� ������ ź�� ��� ��ٸ� �������Ѵ�
        if (weaponInfo.weaponName == "Revolver" &&
            bulletCount <= 0)
        {
            bulletCount = 6;
            // ������ �ð� 5�� ����
            coolDown *= 5f;
        }

        isCoolDown = true;
        yield return new WaitForSeconds(coolDown);
        isCoolDown = false;
    }

    // ����� ���͸� ã�´�
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

    public void SetWeaponNumber(int weaponNumber)
    {
        this.weaponNumber = weaponNumber;
    }
}
