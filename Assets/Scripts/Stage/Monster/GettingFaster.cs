using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GettingFaster : MonoBehaviour
{
    MonsterInfo monsterInfo;

    Coroutine fasterEverySecond;

    void Start()
    {
        monsterInfo = this.GetComponent<MonsterInfo>();
        fasterEverySecond = StartCoroutine(FasterEverySecond());
    }

    
    void Update()
    {
        
    }

    // �� �� �ӵ��� �������� �Լ�
    IEnumerator FasterEverySecond()
    {
        while (!GameRoot.Instance.GetIsRoundClear())
        {
            yield return new WaitForSeconds(1.0f);

            float monsterSpeed = monsterInfo.GetMonsterMovementSpeed() + 0.2f;
            if (monsterSpeed >= 12f)
                monsterSpeed = 12f;
            monsterInfo.SetMonsterMovementSpeed(monsterSpeed);
        }
    }
}
