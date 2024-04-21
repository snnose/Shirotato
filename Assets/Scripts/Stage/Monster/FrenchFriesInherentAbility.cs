using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrenchFriesInherentAbility : MonoBehaviour
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
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �Ѿ� Ȥ�� ȭ�쿡 �ǰݵƴٸ�
        if (collision.TryGetComponent<BulletControl>(out BulletControl bulletControl) ||
            collision.TryGetComponent<ArrowControl>(out ArrowControl arrowControl))
        {
            // �ǰݵǰ� ���� �ʾҴٸ� ������ �������� ����ü �߻�
            if (this.GetComponent<MonsterControl>().GetMonsterCurrentHP() >= 0)
                StartCoroutine(fire);
        }
    }

    IEnumerator Fire()
    {
        yield return StartCoroutine(FireRandomDirection());
        // ��Ÿ�� 0.03�� (�ʹ� ���� ����ü �߻� ����)
        yield return new WaitForSeconds(0.03f);
        fire = Fire();
    }

    private IEnumerator FireRandomDirection()
    {
        // �߻� ������ ���Ѵ�
        Vector2 fireDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));

        // ����ü�� �߻��ϰ� ������� �Է��Ѵ�
        GameObject copy = Instantiate(projectile, this.transform.position, this.transform.rotation);
        yield return null;
        copy.GetComponent<Rigidbody2D>().AddForce(fireDirection.normalized * 5f, ForceMode2D.Impulse);

        ProjectileControl projectileControl = copy.GetComponent<ProjectileControl>();
        projectileControl.SetProjectileDamage(monsterInfo.damage);

        yield return null;
    }
}
