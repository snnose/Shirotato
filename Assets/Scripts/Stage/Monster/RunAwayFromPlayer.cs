using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAwayFromPlayer : MonoBehaviour
{
    private Rigidbody2D monsterRb2D;
    private MonsterInfo monsterInfo;

    IEnumerator chooseNextMoving;

    void Start()
    {
        monsterRb2D = this.GetComponent<Rigidbody2D>();
        monsterInfo = this.GetComponent<MonsterInfo>();
        chooseNextMoving = ChooseNextMoving();
    }

    // Update is called once per frame
    void Update()
    {
        if (chooseNextMoving != null)
            StartCoroutine(chooseNextMoving);
    }

    // 다음 움직임을 결정하는 기능
    IEnumerator ChooseNextMoving()
    {
        float properDistance = 9f;
        
        Vector2 playerPos = PlayerControl.Instance.transform.position;
        Vector2 monsterPos = this.transform.position;

        float distance = Vector2.Distance(playerPos, monsterPos);

        // 일정 거리 가까이면 플레이어에서 최대한 멀어지는 방향으로 도망친다
        if (distance <= properDistance)
        {
            // 추후 보완해서 좀 더 괜찮은 움직임으로 설계해야함
            //StartCoroutine(RunAway(playerPos, monsterPos));
            StartCoroutine(MoveRandomly());
        }
        // 일정 거리 밖이면 매초 랜덤한 방향으로 일정시간 이동
        else
        {
            StartCoroutine(MoveRandomly());
        }

        yield return null;
    }

    IEnumerator RunAway(Vector2 playerPos, Vector2 monsterPos)
    {
        float relativePosX = monsterPos.x - playerPos.x;
        float relativePosY = monsterPos.y - playerPos.y;

        Vector2 movement = new Vector2(relativePosY, relativePosX);
        movement.Normalize();
        
        monsterRb2D.velocity = movement * monsterInfo.GetMonsterMovementSpeed();
        yield return new WaitForSeconds(0.5f);

        chooseNextMoving = ChooseNextMoving();
        yield return null;
    }

    IEnumerator MoveRandomly()
    {
        float movementX = Random.Range(-1.0f, 1.0f);
        float movementY = Random.Range(-1.0f, 1.0f);

        // 벽에 가까울 경우의 움직임 조정
        // 오른쪽 벽에 가깝다면
        if (this.transform.position.x >= 16)
        {
            movementX = Random.Range(-1.0f, 0f);
        }
        // 왼쪽 벽에 가깝다면
        else if (this.transform.position.x <= -16)
        {
            movementX = Random.Range(0f, 1.0f);
        }

        // 위쪽 벽에 가깝다면
        if (this.transform.position.y >= 13)
        {
            movementY = Random.Range(-1.0f, 0f);
        }
        // 아래쪽 벽에 가깝다면
        else if (this.transform.position.y <= -13)
        {
            movementY = Random.Range(0f, 1.0f);
        }

        Vector2 movement = new Vector2(movementX, movementY);
        movement.Normalize();

        monsterRb2D.velocity = movement * monsterInfo.GetMonsterMovementSpeed();
        yield return new WaitForSeconds(1.5f);
        
        chooseNextMoving = ChooseNextMoving();
        yield return null;
    }
}
