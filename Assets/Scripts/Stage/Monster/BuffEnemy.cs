using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffEnemy : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        StartCoroutine(Action());
    }

    IEnumerator Action()
    {
        yield return StartCoroutine(BuffToEnemy());
    }

    IEnumerator BuffToEnemy()
    {
        yield return new WaitForSeconds(3.5f);

        while (true)
        {
            // ���� ������ ���� ����� �ҷ��´�
            List<GameObject> monsters = SpawnManager.Instance.GetCurrentMonsters();
            int count = monsters.Count;

            // ������ ���Ͱ� �ڽ� ȥ�ڶ�� ���� �ߵ� x
            if (count == 1)
                break;

            // ������ ���͸� �����Ѵ�
            int ranNum = Random.Range(0, count);
            // ���õ� ���Ͱ� �Ϲ� ���� �ƴ϶�� �ٽ� ����
            if (monsters[ranNum].GetComponent<MonsterInfo>().type != "General")
                continue;
            // ���õ� ���Ͱ� ������ ���¶�� �ٽ� ����
            if (monsters[ranNum].GetComponent<MonsterInfo>().isBuffed)
                continue;

            // ���õ� ���� ����
            // ü�� 150%, ����� 25%, �̼� 50% ����
            MonsterInfo monsterInfo = monsters[ranNum].GetComponent<MonsterInfo>();
            MonsterControl monsterControl = monsters[ranNum].GetComponent<MonsterControl>();
            monsterInfo.SetMonsterHP(monsterInfo.GetMonsterHP() * 2.5f);
            monsterControl.SetMonsterCurrentHP(monsterControl.GetMonsterCurrentHP() * 2.5f);
            monsterInfo.SetMonsterDamage(monsterInfo.damage * 1.25f);
            monsterInfo.SetMonsterMovementSpeed(monsterInfo.GetMonsterMovementSpeed() * 1.5f);
            monsterInfo.isBuffed = true;

            // �׵θ��� ���� ���� ���� �Ѵ�
            SpriteOutline spriteOutline = monsters[ranNum].GetComponent<SpriteOutline>();
            spriteOutline.outlineSize = 8;

            // ������ ���������� �ο��ߴٸ� ���� Ż��
            break;
        }

        yield return null;
    }
}
