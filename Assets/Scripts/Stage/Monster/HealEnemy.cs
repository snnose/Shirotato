using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEnemy : MonoBehaviour
{
    IEnumerator healing;

    // Start is called before the first frame update
    void Start()
    {
        healing = Healing();
    }

    // Update is called once per frame
    void Update()
    {
        if (healing != null)
        {
            StartCoroutine(healing);
        }
    }

    // �� 3�ʸ��� �ֺ� ���� ȸ��
    IEnumerator Healing()
    {
        healing = null;
        yield return new WaitForSeconds(3.0f);

        float properDistance = 2.0f;

        // ���� �����ϴ� ���� ����� �ҷ��´�
        List<GameObject> monsters = SpawnManager.Instance.GetCurrentMonsters();
        int count = monsters.Count;

        Vector2 healerPos = this.transform.position;
        float healingAmount = 100f + 10f * GameRoot.Instance.GetCurrentRound();

        // ��� ���͸� ã�� ���� ���� �� ���Ϳ��� ��
        for (int i = 0; i < count; i++)
        {
            Vector2 targetPos = monsters[i].transform.position;
            float distance = Vector2.Distance(healerPos, targetPos);

            if (distance < properDistance)
            {
                MonsterControl monsterControl = monsters[i].GetComponent<MonsterControl>();
                monsterControl.SetMonsterCurrentHP(monsterControl.GetMonsterCurrentHP() + healingAmount);

                // ���� ����Ʈ�� ȭ�鿡 ����
                GameObject healEffect = Resources.Load<GameObject>("Prefabs/Effect/������Ʈ");
                GameObject copy = Instantiate(healEffect);

                copy.transform.parent = monsters[i].transform;
                copy.transform.localPosition = new Vector3(0f, 0f, -1);
            }
        }

        healing = Healing();
    }
}
