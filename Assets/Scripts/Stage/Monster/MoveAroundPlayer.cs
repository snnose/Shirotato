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

    // ���� �������� �����ϴ� ���
    IEnumerator ChooseNextMoving()
    {
        float properDistance = 4f;

        if (this.GetComponent<SpriteRenderer>().name == "EggFry")
            properDistance = 2f;

        Vector2 playerPos = PlayerControl.Instance.transform.position;
        Vector2 monsterPos = this.transform.position;

        float distance = Vector2.Distance(playerPos, monsterPos);


        // ���� �Ÿ� �����̸� �÷��̾�� �ִ��� �־����� �������� ����ģ��
        if (distance <= properDistance)
        {
            // ���� �����ؼ� �� �� ������ ���������� �����ؾ���
            //StartCoroutine(RunAway(playerPos, monsterPos));
            yield return StartCoroutine(RunAway(playerPos, monsterPos));
        }
        // ���� �Ÿ� ���̸� �÷��̾ �߰�
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

    // ���� ����� �� ������ ����
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
