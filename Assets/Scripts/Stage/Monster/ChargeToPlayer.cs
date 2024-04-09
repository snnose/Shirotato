using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeToPlayer : MonoBehaviour
{
    private Rigidbody2D monsterRb2D;
    ChasePlayer chasePlayer;

    bool isCoolDown = true;

    IEnumerator charging;  

    void Start()
    {
        chasePlayer = this.GetComponent<ChasePlayer>();
        monsterRb2D = this.GetComponent<Rigidbody2D>();
        // ���� �� ������ ��Ÿ��
        StartCoroutine(CoolDown());
    }

    void Update()
    {
        if (!isCoolDown)
            StartCoroutine(Charging());
    }

    IEnumerator CoolDown()
    {
        // ��Ÿ���� 2.5�� ~ 3.5�� ����
        float coolDown = Random.Range(2.5f, 3.5f);
        yield return new WaitForSeconds(coolDown);

        isCoolDown = false;
    }

    IEnumerator StartCharge()
    {
        yield return StartCoroutine(Charging());
    }

    IEnumerator Charging()
    {
        Vector2 playerPos = PlayerControl.Instance.transform.position;
        Vector2 monsterPos = this.transform.position;

        float distance = Vector2.Distance(playerPos, monsterPos);
        Vector2 chargeVector = playerPos - monsterPos;

        if (distance <= 7f)
        {
            isCoolDown = true;

            // �÷��̾� �߰��� �����
            chasePlayer.StopChasing();
            monsterRb2D.velocity = new Vector2(0, 0);
            // 0.75�� ���� ������ �������鼭 ����� �� �����Ѵ�
            // ���� ���� �߿��� �˹���� �ʴ´�.
            this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            yield return new WaitForSeconds(0.75f);

            // ��Ⱑ ������ ���� ���� ����
            this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

            // 0.5�� ���� ����
            monsterRb2D.AddForce(chargeVector.normalized * 25f, ForceMode2D.Impulse);

            yield return new WaitForSeconds(0.5f);

            chasePlayer.StartChasing();

            // ��Ÿ���� ������
            // ��Ÿ���� 2.5�� ~ 3.5�� ����
            float coolDown = Random.Range(2.5f, 3.5f);

            yield return new WaitForSeconds(coolDown);

            isCoolDown = false;
        }
        yield return null;
    }
}
