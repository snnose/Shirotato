using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stab : MonoBehaviour
{
    public IEnumerator StabMovement(Vector2 dir, int frame)
    {
        Vector2 initPos = this.transform.localPosition;
        Vector2 destPos = initPos + dir;

        float moveSpeed = 6f / frame;

        for (int i = 0; i < frame / 2; i++)
        {
            this.transform.localPosition = Vector2.Lerp(this.transform.localPosition, destPos, moveSpeed);
            yield return null;
        }

        for (int i = 0; i < frame / 2; i++)
        {
            this.transform.localPosition = Vector2.Lerp(this.transform.localPosition, initPos, moveSpeed);
            yield return null;
        }

        // 제자리로 돌아온다
        this.transform.localPosition = initPos;
        yield return null;
    }
}
