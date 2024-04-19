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
        // 스폰 시 돌진은 쿨타임
        StartCoroutine(CoolDown());
    }

    void Update()
    {
        if (!isCoolDown)
            StartCoroutine(Charging());
    }

    IEnumerator CoolDown()
    {
        // 쿨타임은 2.5초 ~ 3.5초 사이
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

            // 플레이어 추격을 멈춘다
            chasePlayer.StopChasing();
            monsterRb2D.velocity = new Vector2(0, 0);
            // 0.5초 동안 서서히 빨개지면서 대기한 후 시전한다
            // 돌진 시전 중에는 넉백되지 않는다.
            yield return StartCoroutine(WaitToCharge());

            // 0.5초 동안 돌진
            this.GetComponent<Collider2D>().isTrigger = true;
            this.GetComponent<Rigidbody2D>().mass = 10;
            monsterRb2D.AddForce(chargeVector.normalized * 250f, ForceMode2D.Impulse);

            yield return new WaitForSeconds(0.5f);

            this.GetComponent<Collider2D>().isTrigger = false;
            this.GetComponent<Rigidbody2D>().mass = 1;

            chasePlayer.StartChasing();

            // 쿨타임을 돌린다
            // 쿨타임은 2.5초 ~ 3.5초 사이
            float coolDown = Random.Range(2.5f, 3.5f);

            yield return new WaitForSeconds(coolDown);

            isCoolDown = false;
        }
        yield return null;
    }

    IEnumerator WaitToCharge()
    {
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        Color color = this.GetComponent<SpriteRenderer>().color;
        Color originColor = color;

        for (int i = 0; i < 50; i++)
        {
            color.g = 1f - (i * 0.015f);
            color.b = 1f - (i * 0.015f);
            this.GetComponent<SpriteRenderer>().color = color;

            yield return new WaitForSeconds(0.01f);
        }

        // 돌진 대기가 끝나면 다시 색을 원상태로 돌려놓는다.
        this.GetComponent<SpriteRenderer>().color = originColor;

        // 대기가 끝나면 고정 상태 해제
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
}
