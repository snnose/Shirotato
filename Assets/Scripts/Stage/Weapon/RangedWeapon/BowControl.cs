using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowControl : MonoBehaviour, IRangedWeaponControl
{
    public int weaponNumber { get; set; }
    public WeaponInfo weaponInfo { get; set; }
    public bool isCoolDown { get; set; }

    Sprite chargingBow;
    Sprite emptyBow;
    Vector2 direction;
    AudioSource shootSound;

    private void Awake()
    {
        //shootSound = this.GetComponent<AudioSource>();
        chargingBow = Resources.Load<Sprite>("Sprites/Weapons/ChargingBow");
        emptyBow = Resources.Load<Sprite>("Sprites/Weapons/Bow");
    }

    void Start()
    {
        this.weaponNumber = this.GetComponent<StoredWeaponNumber>().GetWeaponNumber();
        weaponInfo = WeaponManager.Instance.GetCurrentWeaponInfoList()[weaponNumber];

        isCoolDown = false;
        this.GetComponent<SpriteRenderer>().sprite = chargingBow;
    }

    void Update()
    {
        // ���� ���̰� �÷��̾ ���� �ʾ��� ��
        if (!GameRoot.Instance.GetIsRoundClear())
        {
            GameObject closetMonster = GetClosetMonster();

            // ���� ����� ���͸� ã�Ҵٸ�
            if (closetMonster != null)
            {
                TrackingClosetMonster(closetMonster);

                // ���Ⱑ ��Ÿ���� �ƴ϶�� ����Ѵ�
                if (!isCoolDown)
                {
                    // �ѹ��� ���� ���� ��� ���Ⱑ ���� ��츦 ����
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
        float rotateZ;
        Vector2 vec = new Vector2(closetMonster.transform.position.x - this.transform.position.x,
                                  closetMonster.transform.position.y - this.transform.position.y);
        direction = vec.normalized;

        rotateZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

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

    public IEnumerator Attack(GameObject closetMonster)
    {
        //PlayShootSound();
        // ȭ���� �߻��ϸ� ȭ���� ����ִ� �̹����� ��ü�Ѵ�
        this.GetComponent<SpriteRenderer>().sprite = emptyBow;

        // �Ѿ� ����
        GameObject arrow = Resources.Load<GameObject>("Prefabs/Weapons/Arrow");
        GameObject copy = Instantiate(arrow, this.transform.position, this.transform.rotation);

        // ������ ����� ���
        // (���� ����� + ���� ����� * ���� ���) * �����%
        int damage = Mathf.FloorToInt(
            (weaponInfo.damage + weaponInfo.damageCoeff * RealtimeInfoManager.Instance.GetFixedDMG())
                                                * ((RealtimeInfoManager.Instance.GetDMGPercent() + 100) / 100));

        // ������� �ּ� 1
        if (damage <= 0)
            damage = 1;

        // ������ ��Ÿ�� ��� (���� ��ų ���Ӱ� ���� ����)
        // ���� �⺻ ��Ÿ�� - (�⺻ ��Ÿ�� * (���ݼӵ� / (100 + ���ݼӵ�)))
        float coolDown = weaponInfo.coolDown -
                       weaponInfo.coolDown * RealtimeInfoManager.Instance.GetATKSpeed() / (100 + RealtimeInfoManager.Instance.GetATKSpeed());

        // �Ѿ˿� ������� �˹�, ���� Ƚ��, ���� ����� ����
        copy.GetComponent<ArrowControl>().SetDamage(damage);
        copy.GetComponent<ArrowControl>().SetKnockback(weaponInfo.knockback);
        copy.GetComponent<ArrowControl>().SetPierceCount(weaponInfo.pierceCount);
        copy.GetComponent<ArrowControl>().SetPierceDamage(weaponInfo.GetPierceDamage());
        copy.GetComponent<ArrowControl>().SetBounceCount(weaponInfo.bounceCount);

        // ����� ���Ϳ��� �߻�
        Vector2 direction = closetMonster.transform.position - copy.transform.position;
        copy.GetComponent<Rigidbody2D>().AddForce(direction.normalized * 50f, ForceMode2D.Impulse);

        isCoolDown = true;
        yield return new WaitForSeconds(coolDown);
        isCoolDown = false;
        // ���� ��Ÿ���� ���Ҵٸ� ȭ���� ������ �̹����� ��ü
        this.GetComponent<SpriteRenderer>().sprite = chargingBow;
    }

    public GameObject GetClosetMonster()
    {
        // ���� �����ϴ� ���� ����� �޾ƿ´�.
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