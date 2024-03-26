using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    MonsterInfo monsterInfo;
    GameObject projectile;

    IEnumerator fire;

    void Start()
    {
        monsterInfo = this.GetComponent<MonsterInfo>();
        projectile = Resources.Load<GameObject>("Prefabs/Monsters/MonsterProjectile");
        fire = Fire();
    }

    void Update()
    {
        if (fire != null)
            StartCoroutine(fire);
    }

    IEnumerator Fire()
    {
        yield return StartCoroutine(FireProjectileToPlayer());
    }

    IEnumerator FireProjectileToPlayer()
    {
        yield return new WaitForSeconds(4.0f);

        // �÷��̾�� ������ ��ġ�� �������� �߻� ���� ���
        Vector2 playerPos = PlayerControl.Instance.transform.position;
        Vector2 monsterPos = this.transform.position;

        Vector2 fireVector = playerPos - monsterPos;
        // ���� ������ �������ش�
        fireVector = ApplyAimingError(fireVector);

        // ����ü�� �߻��ϰ� ������� �Է��Ѵ�
        GameObject copy = Instantiate(projectile, this.transform.position, this.transform.rotation);
        yield return null;
        copy.GetComponent<Rigidbody2D>().AddForce(fireVector * 5f, ForceMode2D.Impulse);

        ProjectileControl projectileControl = copy.GetComponent<ProjectileControl>();
        projectileControl.SetProjectileDamage(monsterInfo.damage);

        // �ڷ�ƾ ������
        fire = Fire();

        yield return null;
    }

    // ���� ������ �����Ѵ�
    Vector2 ApplyAimingError(Vector2 fireVector)
    {
        Vector2 tmp;
        float errorX = Random.Range(-1.5f, 1.5f);
        float errorY = Random.Range(-1.5f, 1.5f);

        tmp = new Vector2(fireVector.x + errorX, fireVector.y + errorY);
        tmp.Normalize();

        return tmp;
    }
}
