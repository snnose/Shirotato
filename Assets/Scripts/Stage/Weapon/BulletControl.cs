using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletControl : MonoBehaviour
{
    private int damage = 0;
    private int pierceCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //collision.gameObject == GameObject.FindGameObjectWithTag("Monster")
        // 몬스터에 충돌했을 경우
        if (SpawnManager.Instance.GetCurrentMonsters().Contains(collision.gameObject))
        {
            GameObject hitedMonster = collision.gameObject;
            MonsterControl monsterControl = hitedMonster.GetComponent<MonsterControl>();

            float hp = monsterControl.GetMonsterCurrentHP() - damage;
            monsterControl.SetMonsterCurrentHP(hp);

            // 입힌 대미지 출력
            PrintText(hitedMonster.transform, damage);

            // 관통 횟수가 1 이상이라면
            if (pierceCount > 0)
            {
                // 횟수를 감소시키고 대미지를 절반으로 감소
                this.pierceCount--;
                this.damage /= 2;
            }
            // 관통 횟수가 0이면
            else
            {
                Destroy(this.gameObject);
            }
        }

        // 벽과 충돌하면 총알 파괴
        if (collision.gameObject == GameObject.FindGameObjectWithTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 벽과 충돌하면 총알 파괴
        if (collision.gameObject == GameObject.FindGameObjectWithTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }

    void PrintText(Transform transform, int damage)
    {
        // 대미지 텍스트 출력
        GameObject damageText = Resources.Load<GameObject>("Prefabs/DamageText");
        TextMeshPro damagePro = damageText.GetComponent<TextMeshPro>();

        // 텍스트 및 색상 결정
        damagePro.text = damage.ToString();
        damagePro.color = Color.white;

        GameObject copy = Instantiate(damageText);
        Vector3 randomPos = new Vector3(Random.Range(-0.4f, 0.4f), Random.Range(0.4f, 0.6f), 0f);
        copy.transform.position = transform.position + randomPos;
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    public void SetPierceCount(int count)
    {
        this.pierceCount = count;
    }
}
