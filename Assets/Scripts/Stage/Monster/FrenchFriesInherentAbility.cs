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
        // 총알 혹은 화살에 피격됐다면
        if (collision.TryGetComponent<BulletControl>(out BulletControl bulletControl) ||
            collision.TryGetComponent<ArrowControl>(out ArrowControl arrowControl))
        {
            // 피격되고 죽지 않았다면 랜덤한 방향으로 투사체 발사
            if (this.GetComponent<MonsterControl>().GetMonsterCurrentHP() >= 0)
                StartCoroutine(fire);
        }
    }

    IEnumerator Fire()
    {
        yield return StartCoroutine(FireRandomDirection());
        // 쿨타임 0.03초 (너무 과한 투사체 발사 방지)
        yield return new WaitForSeconds(0.03f);
        fire = Fire();
    }

    private IEnumerator FireRandomDirection()
    {
        // 발사 방향을 정한다
        Vector2 fireDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));

        // 투사체를 발사하고 대미지를 입력한다
        GameObject copy = Instantiate(projectile, this.transform.position, this.transform.rotation);
        yield return null;
        copy.GetComponent<Rigidbody2D>().AddForce(fireDirection.normalized * 5f, ForceMode2D.Impulse);

        ProjectileControl projectileControl = copy.GetComponent<ProjectileControl>();
        projectileControl.SetProjectileDamage(monsterInfo.damage);

        yield return null;
    }
}
