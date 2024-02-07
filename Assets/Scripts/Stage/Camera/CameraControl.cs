using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject player;
    private Vector3 posDiff = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        this.posDiff = this.transform.position - player.transform.position;
        posDiff.z = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 newPos =
            new Vector3(player.transform.position.x,
                        player.transform.position.y,
                          this.transform.position.z);

        // 벽에 가까워질 때 카메라 보정
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
