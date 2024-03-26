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
        // ���� �浹�ϸ� ����ü �ı�
        if (collision.gameObject.TryGetComponent<WallControl>(out WallControl wall))
        {
            Destroy(this.gameObject);
        }

        // �÷��̾ �浹���� ���
        if (collision.gameObject.TryGetComponent<PlayerColideDetect>(out PlayerColideDetect playerColideDetect))
        {
            //Destroy(this.gameObject);
        }
    }
}
