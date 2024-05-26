using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToEnemies : MonoBehaviour
{
    private Rigidbody2D monsterRb2D;
    private MonsterInfo monsterInfo;

    private Vector2 movement;

    IEnumerator chooseNextMoving;

    void Start()
    {
        monsterRb2D = this.GetComponent<Rigidbody2D>();
        monsterInfo = this.GetComponent<MonsterInfo>();

        chooseNextMoving = ChooseNextMoving();
    }

    private void Update()
    {
        if (chooseNextMoving != null)
            StartCoroutine(chooseNextMoving);
    }

    // 다음 움직임을 결정하는 기능
    IEnumerator ChooseNextMoving()
    {
        // 본인 이외 생존한 몬스터가 있다면 몬스터한테 이동
        if (SpawnManager.Instance.GetCurrentMonsters().Count > 1)
        {
            // 추후 보완해서 좀 더 괜찮은 움직임으로 설계해야함
            //StartCoroutine(RunAway(playerPos, monsterPos));
            yield return StartCoroutine(MoveToDirection());
        }
        // 본인만 생존했다면 도망친다
        else
        {
            yield return StartCoroutine(RunAway());
        }
    }

    IEnumerator RunAway()
    {
        Vector2 playerPos = PlayerControl.Instance.transform.position;
        Vector2 monsterPos = this.transform.position;

        float relativePosX = monsterPos.x - playerPos.x;
        float relativePosY = monsterPos.y - playerPos.y;

        Vector2 movement = VectorCorrection(new Vector2(relativePosY, relativePosX));
        movement.Normalize();

        monsterRb2D.velocity = movement * monsterInfo.GetMonsterMovementSpeed();
        yield return new WaitForSeconds(0.3f);

        chooseNextMoving = ChooseNextMoving();
    }

    private IEnumerator MoveToDirection()
    {
        List<GameObject> monsters = SpawnManager.Instance.GetCurrentMonsters();
        int ranNum;
        while (true)
        {
            ranNum = Random.Range(0, monsters.Count);

            // 자기 자신을 선택했다면 다시 선택
            if (monsters[ranNum] == this.gameObject)
                continue;

            break;
        }

        Vector2 healerPos = this.transform.position;
        Vector2 selectedMonsterPos = monsters[ranNum].transform.position;
        movement = selectedMonsterPos - healerPos;
        movement.Normalize();
        monsterRb2D.velocity = movement * monsterInfo.GetMonsterMovementSpeed();

        yield return new WaitForSeconds(Random.Range(0.2f, 0.3f));

        chooseNextMoving = ChooseNextMoving();
    }

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
