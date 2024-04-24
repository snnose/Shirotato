using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{
    public IEnumerator SwingMovement(Vector2 dir, float range, int frame)
    {
        Quaternion initRotation = this.transform.localRotation;
        Vector3 initPosition = this.transform.localPosition;

        float playerRotationY = this.transform.parent.rotation.y;

        // 몬스터를 바라보는 방향 각 계산
        float rotateZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //Quaternion attackStartRotation = Quaternion.Euler(0f, initRotation.y, rotateZ + 90f);

        // 처음 Z 회전 각을 토대로 공격 시작 위치를 구한다
        Vector3 attackStartPosition = DegToVec2(rotateZ + 90f, range);
        //Debug.Log("몬스터 방향 : " + dir + ", " + "공격 시작 위치 : " + attackStartPosition);
        float moveSpeed = 18f / frame;

        if (playerRotationY == 0f)
        {
            // 무기가 공격 시작 위치로 이동하면서 천천히 회전한다
            for (int i = 0; i < frame / 6; i++)
            {
                rotateZ += 90f / (frame / 6f);

                this.transform.localPosition = Vector2.Lerp(this.transform.localPosition, attackStartPosition, moveSpeed);
                this.transform.localRotation = Quaternion.Euler(0f, initRotation.y, rotateZ);

                yield return new WaitForSeconds(0.0167f);
            }

            // 공격 시작 위치로 이동했다면 반대 방향으로 반원을 그리면서 회전한다
            for (int i = 0; i < frame / 3; i++)
            {
                rotateZ -= 180f / (frame / 3f);

                this.transform.localPosition = Vector2.Lerp(this.transform.localPosition, DegToVec2(rotateZ, range), moveSpeed);
                this.transform.localRotation = Quaternion.Euler(0f, initRotation.y, rotateZ);

                yield return new WaitForSeconds(0.0167f);
            }
        }
        else
        {
            for (int i = 0; i < frame / 6; i++)
            {
                rotateZ += 90f / (frame / 6f);

                this.transform.localPosition = Vector2.Lerp(this.transform.localPosition, attackStartPosition, moveSpeed);
                this.transform.localRotation = Quaternion.Euler(0f, -initRotation.y, -rotateZ);

                yield return new WaitForSeconds(0.0167f);
            }

            for (int i = 0; i < frame / 3; i++)
            {
                rotateZ -= 180f / (frame / 3f);

                this.transform.localPosition = Vector2.Lerp(this.transform.localPosition, -DegToVec2(-rotateZ, range), moveSpeed);
                this.transform.localRotation = Quaternion.Euler(0f, -initRotation.y, -rotateZ);

                yield return new WaitForSeconds(0.0167f);
            }
        }

        this.transform.localPosition = initPosition;
        this.transform.localRotation = initRotation;

        yield return null;
    }

    Vector2 DegToVec2(float rotateZ, float range)
    {
        // Z 회전 각을 토대로 공격 시작 위치를 구한다
        float radian = rotateZ * Mathf.Deg2Rad;
        float startX = range * Mathf.Cos(radian);
        float startY = range * Mathf.Sin(radian);
        Vector3 position = new Vector3(startX, startY);

        return position;
    }
}
