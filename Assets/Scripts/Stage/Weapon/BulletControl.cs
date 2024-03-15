using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletControl : MonoBehaviour
{
    private int damage = 0;
    private int pierceCount = 0;
    private bool isCritical = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 벽과 충돌하면 총알 파괴
        if (collision.gameObject.TryGetComponent<WallControl>(out WallControl wall))
        {
            Debug.Log("충돌");
            Destroy(this.gameObject);
        }

        //collision.gameObject == GameObject.FindGameObjectWithTag("Monster")
        // 몬스터에 충돌했을 경우
        if (SpawnManager.Instance.GetCurrentMonsters().Contains(collision.gameObject))
        {
            GameObject hitedMonster = collision.gameObject;
            MonsterControl monsterControl = hitedMonster.GetComponent<MonsterControl>();

            // 치명타 적용
            float random = Random.Range(0f, 100f);
            if (random < RealtimeInfoManager.Instance.GetCritical())
                isCritical = true;

            // 생명력 흡수 적용
            random = Random.Range(0f, 100f);
            if (random < RealtimeInfoManager.Instance.GetHPDrain()
                && RealtimeInfoManager.Instance.GetCurrentHP() < RealtimeInfoManager.Instance.GetHP())
            {
                float currentHP = RealtimeInfoManager.Instance.GetCurrentHP();
                currentHP += 1f;
                RealtimeInfoManager.Instance.SetCurrentHP(currentHP);
                PrintText(PlayerControl.Instance.transform, 1);
            }

            float finalDamage = damage;
            if (isCritical)
            {
                finalDamage *= 1.5f;
            }

            float hp = monsterControl.GetMonsterCurrentHP() - finalDamage;
            monsterControl.SetMonsterCurrentHP(hp);

            // 입힌 대미지 출력
            PrintText(hitedMonster.transform, finalDamage, isCritical);

            // 관통 횟수가 1 이상이라면
            if (pierceCount > 0)
            {
                // 횟수를 감소시키고 대미지를 절반으로 감소
                this.pierceCount--;
                this.damage /= 2;
            }
            // 관통 횟수가 0이면
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    void PrintText(Transform transform, float damage, bool isCritical)
    {
        // 대미지 텍스트 출력
        GameObject textObject = Resources.Load<GameObject>("Prefabs/DamageText");
        TextMeshPro tmPro = textObject.GetComponent<TextMeshPro>();

        // 텍스트 및 색상 결정
        tmPro.text = damage.ToString();
        Color color = Color.white;
        if (isCritical)
        {
            color = Color.yellow;
        }
        tmPro.color = color;

        GameObject copy = Instantiate(textObject);
        Vector3 randomPos = new Vector3(Random.Range(-0.4f, 0.4f), Random.Range(0.4f, 0.6f), 0f);
        copy.transform.position = transform.position + randomPos;
    }

    void PrintText(Transform transform, int num)
    {
        // 대미지 텍스트 출력
        GameObject textObject = Resources.Load<GameObject>("Prefabs/DamageText");
        TextMeshPro tmPro = textObject.GetComponent<TextMeshPro>();

        // 텍스트 및 색상 결정
        tmPro.text = num.ToString();
        Color color = Color.white;
        ColorUtility.TryParseHtmlString("#1FDE38", out color);
        tmPro.color = color;

        GameObject copy = Instantiate(textObject);
        Vector3 randomPos = new Vector3(Random.Range(-0.4f, 0.4f), Random.Range(0.4f, 0.6f), 0f);
        copy.transform.position = transform.position + randomPos;
    }

    void TryHPDrain()
    {
        float random = Random.Range(0f, 100f);

        if (random < RealtimeInfoManager.Instance.GetCritical())
        {
            float currentHP = RealtimeInfoManager.Instance.GetCurrentHP();
            if (currentHP < RealtimeInfoManager.Instance.GetHP())
            {
                currentHP += 1f;

            }
        }
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    public void SetPierceCount(int count)
    {
        this.pierceCount = count;
    }
}
