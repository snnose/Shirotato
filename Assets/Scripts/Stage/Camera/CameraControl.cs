using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject player;
    private Vector3 posDiff = Vector3.zero;

    void Start()
    {
        this.posDiff = this.transform.position - player.transform.position;
        posDiff.z = 0;
    }

    void FixedUpdate()
    {
        Vector3 newPos =
            new Vector3(player.transform.position.x,
                        player.transform.position.y,
                          this.transform.position.z);

        // ���� ������� �� �� ������ �� ���̵��� ī�޶� ����
        if (player.transform.position.x < -10)
            newPos.x = -10;
        if (player.transform.position.x > 10)
            newPos.x = 10;
        if (player.transform.position.y < -10)
            newPos.y = -10;
        if (player.transform.position.y > 10)
            newPos.y = 10;

        //this.transform.position = newPos + posDiff;

        this.transform.position =
                    Vector3.Lerp(this.transform.position, newPos + posDiff, Time.deltaTime * 12.5f);
    }
}
