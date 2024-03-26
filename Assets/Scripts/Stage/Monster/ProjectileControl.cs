using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileControl : MonoBehaviour
{
    private float damage = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        if (GameRoot.Instance.GetIsRoundClear())
            Destroy(this.gameObject);
    }

    public void SetProjectileDamage(float damage)
    {
        this.damage = damage;
    }

    public float GetProjectileDamage()
    {
        return this.damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 벽과 충돌하면 투사체 파괴
        if (collision.gameObject.TryGetComponent<WallControl>(out WallControl wall))
        {
            Destroy(this.gameObject);
        }

        // 플레이어에 충돌했을 경우
        if (collision.gameObject.TryGetComponent<PlayerColideDetect>(out PlayerColideDetect playerColideDetect))
        {
            //Destroy(this.gameObject);
        }
    }
}
