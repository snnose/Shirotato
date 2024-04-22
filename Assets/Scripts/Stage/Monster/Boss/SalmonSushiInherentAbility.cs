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
            // �ִ� ü���� ���� ���Ϸ� ü���� �����ϰų�
            // ���� �� 45�ʰ� �����ٸ� �ɷ� �ߵ�
            if (this.GetComponent<MonsterControl>().GetMonsterCurrentHP() <= this.GetComponent<MonsterInfo>().GetMonsterHP() / 2 ||
                timer >= timeLimit)
            {
                StartCoroutine(startAbility);
                startAbility = null;
            }
        }
    }

    // �� 0.25�ʸ��� ������ �������� ����ü�� �߻��Ѵ�
    private IEnumerator StartAbility()
    {
        while (!GameRoot.Instance.GetIsRoundClear())
        {
            yield return new WaitForSeconds(0.25f);

            // ������ �������� ����ü �߻�
            Vector2 fireVector = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            // ���� ������ �������ش�
            fireVector.Normalize();

            // ����ü�� �߻��ϰ� ������� �Է��Ѵ�
            GameObject copy = Instantiate(projectile, this.transform.position, this.transform.rotation);
            yield return null;
            copy.GetComponent<Rigidbody2D>().AddForce(fireVector * 5f, ForceMode2D.Impulse);

            ProjectileControl projectileControl = copy.GetComponent<ProjectileControl>();
            projectileControl.SetProjectileDamage(monsterInfo.damage);

            yield return null;
        }
    }
}
