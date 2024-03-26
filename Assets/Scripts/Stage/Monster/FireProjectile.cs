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

        // 플레이어와 몬스터의 위치를 바탕으로 발사 방향 계산
        Vector2 playerPos = PlayerControl.Instance.transform.position;
        Vector2 monsterPos = this.transform.position;

        Vector2 fireVector = playerPos - monsterPos;
        // 조준 오차를 적용해준다
        fireVector = ApplyAimingError(fireVector);

        // 투사체를 발사하고 대미지를 입력한다
        GameObject copy = Instantiate(projectile, this.transform.position, this.transform.rotation);
        yield return null;
        copy.GetComponent<Rigidbody2D>().AddForce(fireVector * 5f, ForceMode2D.Impulse);

        ProjectileControl projectileControl = copy.GetComponent<ProjectileControl>();
        projectileControl.SetProjectileDamage(monsterInfo.damage);

        // 코루틴 재장전
        fire = Fire();

        yield return null;
    }

    // 조준 오차를 적용한다
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
