using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAroundPlayer : MonoBehaviour
{
    private Rigidbody2D monsterRb2D;
    private MonsterInfo monsterInfo;

    IEnumerator chooseNextMoving;

    // Start is called before the first frame update
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
        float properDistance = 4f;

        if (this.GetComponent<SpriteRenderer>().name == "EggFry")
            properDistance = 2f;

        Vector2 playerPos = PlayerControl.Instance.transform.position;
        Vector2 monsterPos = this.transform.position;

        float distance = Vector2.Distance(playerPos, monsterPos);


        // 일정 거리 가까이면 플레이어에서 최대한 멀어지는 방향으로 도망친다
        if (distance <= properDistance)
        {
            // 추후 보완해서 좀 더 괜찮은 움직임으로 설계해야함
            //StartCoroutine(RunAway(playerPos, monsterPos));
            yield return StartCoroutine(RunAway(playerPos, monsterPos));
        }
        // 일정 거리 밖이면 플레이어를 추격
        else
        {
            yield return StartCoroutine(ChasingPlayer());
        }
    }

    IEnumerator RunAway(Vector2 playerPos, Vector2 monsterPos)
    {
        float relativePosX = monsterPos.x - playerPos.x;
        float relativePosY = monsterPos.y - playerPos.y;

        Vector2 movement = VectorCorrection(new Vector2(relativePosY, relativePosX));
        movement.Normalize();

        monsterRb2D.velocity = movement * monsterInfo.GetMonsterMovementSpeed();
        yield return new WaitForSeconds(0.1f);

        chooseNextMoving = ChooseNextMoving();
    }

    private IEnumerator ChasingPlayer()
    {

        Vector2 playerPos = PlayerControl.Instance.transform.position;
        Vector2 monsterPos = this.transform.position;

        Vector2 movement = playerPos - monsterPos;
        movement.Normalize();

        monsterRb2D.velocity = movement * monsterInfo.GetMonsterMovementSpeed();

        yield return new WaitForSeconds(0.1f);

        chooseNextMoving = ChooseNextMoving();
    }

    /*
    IEnumerator MoveRandomly()
    {
        float movementX = Random.Range(-1.0f, 1.0f);
        float movementY = Random.Range(-1.0f, 1.0f);

        Vector2 movement = VectorCorrection(new Vector2(movementX, movementY));
        movement.Normalize();

        for (int i = 0; i < 15; i++)
        {
            monsterRb2D.velocity = movement * monsterInfo.GetMonsterMovementSpeed();
            yield return new WaitForSeconds(0.1f);
        }

        chooseNextMoving = ChooseNextMoving();
    }
    */

    // 벽과 가까울 때 움직임 조정
    Vector2 VectorCorrection(Vector2 movement)
    {
        float movementX = movement.x;
        float movementY = movement.y;

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

        return new Vector2(movementX, movementY);
    }
}
