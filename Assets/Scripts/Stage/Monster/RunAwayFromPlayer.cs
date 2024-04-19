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

    // ���� �������� �����ϴ� ���
    IEnumerator ChooseNextMoving()
    {
        yield return StartCoroutine(MoveRandomly());
        /*
        // ���� �Ÿ� �����̸� �÷��̾�� �ִ��� �־����� �������� ����ģ��
        if (distance <= properDistance)
        {
            // ���� �����ؼ� �� �� ������ ���������� �����ؾ���
            //StartCoroutine(RunAway(playerPos, monsterPos));
            yield return StartCoroutine(MoveRandomly());
        }
        // ���� �Ÿ� ���̸� ���� ������ �������� �����ð� �̵�
        else
        {
            yield return StartCoroutine(MoveRandomly());
        }
        */
    }

    IEnumerator MoveRandomly()
    {
        float movementX = Random.Range(-1.0f, 1.0f);
        float movementY = Random.Range(-1.0f, 1.0f);

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

        Vector2 movement = new Vector2(movementX, movementY);
        movement.Normalize();

        for (int i = 0; i < 15; i++)
        {
            monsterRb2D.velocity = movement * monsterInfo.GetMonsterMovementSpeed();
            yield return new WaitForSeconds(0.1f);
        }
        
        chooseNextMoving = ChooseNextMoving();
    }
}
