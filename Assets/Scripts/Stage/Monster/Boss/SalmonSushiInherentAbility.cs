using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalmonSushiInherentAbility : MonoBehaviour
{
    IEnumerator startAbility;
    float timeLimit = 1f;
    float timer = 0f;

    MonsterInfo monsterInfo;
    GameObject projectile;

    void Start()
    {
        monsterInfo = this.GetComponent<MonsterInfo>();
        projectile = Resources.Load<GameObject>("Prefabs/Monsters/MonsterProjectile");

        startAbility = StartAbility();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        // 
        if (startAbility != null)
        {
            // 최대 체력의 절반 이하로 체력이 감소하거나
            // 스폰 후 45초가 지났다면 능력 발동
            if (this.GetComponent<MonsterControl>().GetMonsterCurrentHP() <= this.GetComponent<MonsterInfo>().GetMonsterHP() / 2 ||
                timer >= timeLimit)
            {
                StartCoroutine(startAbility);
                startAbility = null;
            }
        }
    }

    // 매 0.25초마다 랜덤한 방향으로 투사체를 발사한다
    private IEnumerator StartAbility()
    {
        while (!GameRoot.Instance.GetIsRoundClear())
        {
            yield return new WaitForSeconds(0.25f);

            // 랜덤한 방향으로 투사체 발사
            Vector2 fireVector = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            // 조준 오차를 적용해준다
            fireVector.Normalize();

            // 투사체를 발사하고 대미지를 입력한다
            GameObject copy = Instantiate(projectile, this.transform.position, this.transform.rotation);
            yield return null;
            copy.GetComponent<Rigidbody2D>().AddForce(fireVector * 5f, ForceMode2D.Impulse);

            ProjectileControl projectileControl = copy.GetComponent<ProjectileControl>();
            projectileControl.SetProjectileDamage(monsterInfo.damage);

            yield return null;
        }
    }
}
