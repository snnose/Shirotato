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

    // ���� �������� �����ϴ� ���
    IEnumerator ChooseNextMoving()
    {
        // ���� �̿� ������ ���Ͱ� �ִٸ� �������� �̵�
        if (SpawnManager.Instance.GetCurrentMonsters().Count > 1)
        {
            // ���� �����ؼ� �� �� ������ ���������� �����ؾ���
            //StartCoroutine(RunAway(playerPos, monsterPos));
            yield return StartCoroutine(MoveToDirection());
        }
        // ���θ� �����ߴٸ� ����ģ��
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

            // �ڱ� �ڽ��� �����ߴٸ� �ٽ� ����
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

        // ���� ����� ����� ������ ����
        // ������ ���� �����ٸ�
        if (this.transform.position.x >= 16)
        {
            movementX = Random.Range(-1.0f, 0f);
        }
        // ���� ���� �����ٸ�
        else if (this.transform.position.x <= -16)
        {
            movementX = Random.Range(0f, 1.0f);
        }

        // ���� ���� �����ٸ�
        if (this.transform.position.y >= 13)
        {
            movementY = Random.Range(-1.0f, 0f);
        }
        // �Ʒ��� ���� �����ٸ�
        else if (this.transform.position.y <= -13)
        {
            movementY = Random.Range(0f, 1.0f);
        }

        return new Vector2(movementX, movementY);
    }
}
