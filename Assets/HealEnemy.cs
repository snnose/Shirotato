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

    // 매 3초마다 주변 몬스터 회복
    IEnumerator Healing()
    {
        healing = null;
        yield return new WaitForSeconds(3.0f);

        float properDistance = 2.0f;

        // 현재 존재하는 몬스터 목록을 불러온다
        List<GameObject> monsters = SpawnManager.Instance.GetCurrentMonsters();
        int count = monsters.Count;

        Vector2 healerPos = this.transform.position;
        float healingAmount = 100f + 10f * GameRoot.Instance.GetCurrentRound();

        // 모든 몬스터를 찾아 적정 범위 내 몬스터에게 힐
        for (int i = 0; i < count; i++)
        {
            Vector2 targetPos = monsters[i].transform.position;
            float distance = Vector2.Distance(healerPos, targetPos);

            if (distance < properDistance)
            {
                MonsterControl monsterControl = monsters[i].GetComponent<MonsterControl>();
                monsterControl.SetMonsterCurrentHP(monsterControl.GetMonsterCurrentHP() + healingAmount);

                // 힐링 이펙트를 화면에 띄운다
            }
        }

        healing = Healing();
    }
}
