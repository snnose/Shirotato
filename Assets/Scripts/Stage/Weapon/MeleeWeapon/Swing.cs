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

        // ���͸� �ٶ󺸴� ���� �� ���
        float rotateZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //Quaternion attackStartRotation = Quaternion.Euler(0f, initRotation.y, rotateZ + 90f);

        // ó�� Z ȸ�� ���� ���� ���� ���� ��ġ�� ���Ѵ�
        Vector3 attackStartPosition = DegToVec2(rotateZ + 90f, range);
        //Debug.Log("���� ���� : " + dir + ", " + "���� ���� ��ġ : " + attackStartPosition);
        float moveSpeed = 18f / frame;

        if (playerRotationY == 0f)
        {
            // ���Ⱑ ���� ���� ��ġ�� �̵��ϸ鼭 õõ�� ȸ���Ѵ�
            for (int i = 0; i < frame / 6; i++)
            {
                rotateZ += 90f / (frame / 6f);

                this.transform.localPosition = Vector2.Lerp(this.transform.localPosition, attackStartPosition, moveSpeed);
                this.transform.localRotation = Quaternion.Euler(0f, initRotation.y, rotateZ);

                yield return new WaitForSeconds(0.0167f);
            }

            // ���� ���� ��ġ�� �̵��ߴٸ� �ݴ� �������� �ݿ��� �׸��鼭 ȸ���Ѵ�
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
        // Z ȸ�� ���� ���� ���� ���� ��ġ�� ���Ѵ�
        float radian = rotateZ * Mathf.Deg2Rad;
        float startX = range * Mathf.Cos(radian);
        float startY = range * Mathf.Sin(radian);
        Vector3 position = new Vector3(startX, startY);

        return position;
    }
}
