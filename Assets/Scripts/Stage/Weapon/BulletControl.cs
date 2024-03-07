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
        // ���Ϳ� �浹���� ���
        if (SpawnManager.Instance.GetCurrentMonsters().Contains(collision.gameObject))
        {
            GameObject hitedMonster = collision.gameObject;
            MonsterControl monsterControl = hitedMonster.GetComponent<MonsterControl>();

            float hp = monsterControl.GetMonsterCurrentHP() - damage;
            monsterControl.SetMonsterCurrentHP(hp);

            // ���� ����� ���
            PrintText(hitedMonster.transform, damage);

            // ���� Ƚ���� 1 �̻��̶��
            if (pierceCount > 0)
            {
                // Ƚ���� ���ҽ�Ű�� ������� �������� ����
                this.pierceCount--;
                this.damage /= 2;
            }
            // ���� Ƚ���� 0�̸�
            else
            {
                Destroy(this.gameObject);
            }
        }

        // ���� �浹�ϸ� �Ѿ� �ı�
        if (collision.gameObject == GameObject.FindGameObjectWithTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ���� �浹�ϸ� �Ѿ� �ı�
        if (collision.gameObject == GameObject.FindGameObjectWithTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }

    void PrintText(Transform transform, int damage)
    {
        // ����� �ؽ�Ʈ ���
        GameObject damageText = Resources.Load<GameObject>("Prefabs/DamageText");
        TextMeshPro damagePro = damageText.GetComponent<TextMeshPro>();

        // �ؽ�Ʈ �� ���� ����
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
