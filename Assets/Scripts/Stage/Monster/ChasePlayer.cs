using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    private Rigidbody2D monsterRb2D;
    private MonsterInfo monsterInfo;

    public Coroutine chasingPlayer;

    void Start()
    {
        monsterRb2D = this.GetComponent<Rigidbody2D>();
        monsterInfo = this.GetComponent<MonsterInfo>();

        StartChasing();
    }

    void Update()
    {

    }

    private IEnumerator ChasingPlayer()
    {
        while (!GameRoot.Instance.GetIsRoundClear())
        {
            Vector2 playerPos = PlayerControl.Instance.transform.position;
            Vector2 monsterPos = this.transform.position;

            Vector2 movement = playerPos - monsterPos;
            movement.Normalize();

            monsterRb2D.velocity = movement * monsterInfo.GetMonsterMovementSpeed();

            yield return null;
        }
    }

    public void StartChasing()
    {
        chasingPlayer = StartCoroutine(ChasingPlayer());
    }

    public void StopChasing()
    {
        StopCoroutine(chasingPlayer);
    }
}
