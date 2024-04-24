using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stab : MonoBehaviour
{
    public IEnumerator StabMovement(Vector2 dir, int frame)
    {
        Vector2 initPos = this.transform.localPosition;
        Vector2 destPos = initPos + dir;

        float playerRotationY = this.transform.parent.rotation.y;

        float moveSpeed = 10f / frame;

        if (playerRotationY == 0f)
        {
            destPos = initPos + dir;

            for (int i = 0; i < frame / 2; i++)
            {
                this.transform.localPosition = Vector2.Lerp(this.transform.localPosition, destPos, moveSpeed);
                yield return new WaitForSeconds(0.0167f);
            }

            for (int i = 0; i < frame / 2; i++)
            {
                this.transform.localPosition = Vector2.Lerp(this.transform.localPosition, initPos, moveSpeed);
                yield return new WaitForSeconds(0.0167f);
            }
        }
        else
        {
            destPos = initPos - dir;
            destPos.y = -destPos.y;

            for (int i = 0; i < frame / 2; i++)
            {
                this.transform.localPosition = Vector2.Lerp(this.transform.localPosition, destPos, moveSpeed);
                yield return new WaitForSeconds(0.0167f);
            }

            for (int i = 0; i < frame / 2; i++)
            {
                this.transform.localPosition = Vector2.Lerp(this.transform.localPosition, initPos, moveSpeed);
                yield return new WaitForSeconds(0.0167f);
            }
        }

        // 제자리로 돌아온다
        this.transform.localPosition = initPos;
        yield return null;
    }
}
