using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetsusaigaSwing : MonoBehaviour
{
    GameObject bullet;
    private void Awake()
    {
        bullet = Resources.Load<GameObject>("Prefabs/Weapons/Bullet");
    }

    public IEnumerator SwingMovement(Vector2 dir, float range, int frame)
    {
        Quaternion initRotation = this.transform.localRotation;
        Vector3 initPosition = this.transform.localPosition;
        float waitSeconds = 0.005f;

        float playerRotationY = this.transform.parent.rotation.y;

        // 몬스터를 바라보는 방향 각 계산
        float rotateZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //Quaternion attackStartRotation = Quaternion.Euler(0f, initRotation.y, rotateZ + 90f);

        // 처음 Z 회전 각을 토대로 공격 시작 위치를 구한다
        Vector3 attackStartPosition = DegToVec2(rotateZ + 90f, range);
        //Debug.Log("몬스터 방향 : " + dir + ", " + "공격 시작 위치 : " + attackStartPosition);
        float moveSpeed = 45f / frame;

        if (playerRotationY == 0f)
        {
            attackStartPosition = DegToVec2(rotateZ + 90f, range);

            // 무기가 공격 시작 위치로 이동하면서 천천히 회전한다
            for (int i = 0; i < frame / 6; i++)
            {
                rotateZ += 90f / (frame / 6f);

                this.transform.localPosition = Vector2.Lerp(this.transform.localPosition, attackStartPosition, moveSpeed);
                this.transform.localRotation = Quaternion.Euler(0f, initRotation.y, rotateZ);

                yield return new WaitForSeconds(waitSeconds);
            }

            // 공격 시작 위치로 이동했다면 반대 방향으로 반원을 그리면서 회전한다
            for (int i = 0; i < frame / 3; i++)
            {
                rotateZ -= 180f / (frame / 3f);

                // 바람의 상처 시전
                if (i % 2 == 0)
                    WindScar(rotateZ);

                this.transform.localPosition = Vector2.Lerp(this.transform.localPosition, DegToVec2(rotateZ, range), moveSpeed);
                this.transform.localRotation = Quaternion.Euler(0f, initRotation.y, rotateZ);

                yield return new WaitForSeconds(waitSeconds);
            }
        }
        /*
        else
        {
            attackStartPosition = DegToVec2(-rotateZ + 90f, range);

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
        */

        this.transform.localPosition = initPosition;
        this.transform.localRotation = initRotation;

        yield return null;
    }

    // 바람의 상처
    private void WindScar(float rotateZ)
    {
        // 회전하면서 바람의 상처 쓰는 것 처럼 탄 발사
        GameObject copy = Instantiate(bullet, this.transform.position, this.transform.rotation);
        // 탄 대미지 계산 (기존 무기 대미지의 1/3)
        int damage = this.GetComponent<TetsusaigaControl>().damage;

        damage /= 3;

        if (damage <= 0)
            damage = 1;

        copy.GetComponent<BulletControl>().SetDamage(damage);

        // 회전 각도에 따라 발사각이 달라진다
        Vector2 direction = DegToVec2(rotateZ, 1f);
        copy.GetComponent<Rigidbody2D>().AddForce(direction.normalized * 65f, ForceMode2D.Impulse);
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
