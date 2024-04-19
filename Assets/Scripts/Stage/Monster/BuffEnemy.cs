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
            // 현재 생존한 몬스터 목록을 불러온다
            List<GameObject> monsters = SpawnManager.Instance.GetCurrentMonsters();
            int count = monsters.Count;

            // 생존한 몬스터가 자신 혼자라면 버프 발동 x
            if (count == 1)
                break;

            // 랜덤한 몬스터를 선택한다
            int ranNum = Random.Range(0, count);
            // 선택된 몬스터가 일반 몹이 아니라면 다시 선택
            if (monsters[ranNum].GetComponent<MonsterInfo>().type != "General")
                continue;
            // 선택된 몬스터가 버프된 상태라면 다시 선택
            if (monsters[ranNum].GetComponent<MonsterInfo>().isBuffed)
                continue;

            // 선택된 몬스터 버프
            // 체력 150%, 대미지 25%, 이속 50% 증가
            MonsterInfo monsterInfo = monsters[ranNum].GetComponent<MonsterInfo>();
            MonsterControl monsterControl = monsters[ranNum].GetComponent<MonsterControl>();
            monsterInfo.SetMonsterHP(monsterInfo.GetMonsterHP() * 2.5f);
            monsterControl.SetMonsterCurrentHP(monsterControl.GetMonsterCurrentHP() * 2.5f);
            monsterInfo.SetMonsterDamage(monsterInfo.damage * 1.25f);
            monsterInfo.SetMonsterMovementSpeed(monsterInfo.GetMonsterMovementSpeed() * 1.5f);
            monsterInfo.isBuffed = true;

            // 테두리에 붉은 색을 띄우게 한다
            SpriteOutline spriteOutline = monsters[ranNum].GetComponent<SpriteOutline>();
            spriteOutline.outlineSize = 8;

            // 버프를 성공적으로 부여했다면 루프 탈출
            break;
        }

        yield return null;
    }
}
