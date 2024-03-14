using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MilkControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 라운드 종료 시 사라진다
        if (GameRoot.Instance.GetIsRoundClear())
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 맞닿을 경우
        if (collision.gameObject == PlayerControl.Instance.GetPlayer())
        {
            // 플레이어의 체력을 회복한다
            float currentHP = RealtimeInfoManager.Instance.GetCurrentHP();
            float maxHP = RealtimeInfoManager.Instance.GetHP();

            currentHP += 3.0f;
            // 최대 체력을 초과하지 않는다
            if (currentHP >= maxHP)
                currentHP = maxHP;

            RealtimeInfoManager.Instance.SetCurrentHP(currentHP);

            // 텍스트를 출력한다
            PrintText(collision.transform, 3);
            Destroy(this.gameObject);
        }
    }

    void PrintText(Transform transform, int num)
    {
        // 대미지 텍스트 출력
        GameObject text = Resources.Load<GameObject>("Prefabs/DamageText");
        TextMeshPro textPro = text.GetComponent<TextMeshPro>();

        // 텍스트 및 색상 결정
        textPro.text = "+" + num.ToString();
        textPro.color = Color.green;

        GameObject copy = Instantiate(text);
        Vector3 randomPos = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(0.4f, 0.6f), 0f);
        copy.transform.position = transform.position + randomPos;
    }
}
