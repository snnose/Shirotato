using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletControl : MonoBehaviour
{
    private int damage = 0;
    private int pierceCount = 0;
    private bool isCritical = false;

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
        // ���� �浹�ϸ� �Ѿ� �ı�
        if (collision.gameObject.TryGetComponent<WallControl>(out WallControl wall))
        {
            Debug.Log("�浹");
            Destroy(this.gameObject);
        }

        //collision.gameObject == GameObject.FindGameObjectWithTag("Monster")
        // ���Ϳ� �浹���� ���
        if (SpawnManager.Instance.GetCurrentMonsters().Contains(collision.gameObject))
        {
            GameObject hitedMonster = collision.gameObject;
            MonsterControl monsterControl = hitedMonster.GetComponent<MonsterControl>();

            // ġ��Ÿ ����
            float random = Random.Range(0f, 100f);
            if (random < RealtimeInfoManager.Instance.GetCritical())
                isCritical = true;

            // ����� ��� ����
            random = Random.Range(0f, 100f);
            if (random < RealtimeInfoManager.Instance.GetHPDrain()
                && RealtimeInfoManager.Instance.GetCurrentHP() < RealtimeInfoManager.Instance.GetHP())
            {
                float currentHP = RealtimeInfoManager.Instance.GetCurrentHP();
                currentHP += 1f;
                RealtimeInfoManager.Instance.SetCurrentHP(currentHP);
                PrintText(PlayerControl.Instance.transform, 1);
            }

            float finalDamage = damage;
            if (isCritical)
            {
                finalDamage *= 1.5f;
            }

            float hp = monsterControl.GetMonsterCurrentHP() - finalDamage;
            monsterControl.SetMonsterCurrentHP(hp);

            // ���� ����� ���
            PrintText(hitedMonster.transform, finalDamage, isCritical);

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
    }

    void PrintText(Transform transform, float damage, bool isCritical)
    {
        // ����� �ؽ�Ʈ ���
        GameObject textObject = Resources.Load<GameObject>("Prefabs/DamageText");
        TextMeshPro tmPro = textObject.GetComponent<TextMeshPro>();

        // �ؽ�Ʈ �� ���� ����
        tmPro.text = damage.ToString();
        Color color = Color.white;
        if (isCritical)
        {
            color = Color.yellow;
        }
        tmPro.color = color;

        GameObject copy = Instantiate(textObject);
        Vector3 randomPos = new Vector3(Random.Range(-0.4f, 0.4f), Random.Range(0.4f, 0.6f), 0f);
        copy.transform.position = transform.position + randomPos;
    }

    void PrintText(Transform transform, int num)
    {
        // ����� �ؽ�Ʈ ���
        GameObject textObject = Resources.Load<GameObject>("Prefabs/DamageText");
        TextMeshPro tmPro = textObject.GetComponent<TextMeshPro>();

        // �ؽ�Ʈ �� ���� ����
        tmPro.text = num.ToString();
        Color color = Color.white;
        ColorUtility.TryParseHtmlString("#1FDE38", out color);
        tmPro.color = color;

        GameObject copy = Instantiate(textObject);
        Vector3 randomPos = new Vector3(Random.Range(-0.4f, 0.4f), Random.Range(0.4f, 0.6f), 0f);
        copy.transform.position = transform.position + randomPos;
    }

    void TryHPDrain()
    {
        float random = Random.Range(0f, 100f);

        if (random < RealtimeInfoManager.Instance.GetCritical())
        {
            float currentHP = RealtimeInfoManager.Instance.GetCurrentHP();
            if (currentHP < RealtimeInfoManager.Instance.GetHP())
            {
                currentHP += 1f;

            }
        }
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
