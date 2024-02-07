using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletControl : MonoBehaviour
{
    private int damage = 0;

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

            PrintText(hitedMonster.transform, damage);

            Destroy(this.gameObject);
        }
        else if (collision.gameObject == GameObject.FindGameObjectWithTag("Wall"))
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
}
