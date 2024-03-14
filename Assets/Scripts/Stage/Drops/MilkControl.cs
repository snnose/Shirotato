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
        // ���� ���� �� �������
        if (GameRoot.Instance.GetIsRoundClear())
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾�� �´��� ���
        if (collision.gameObject == PlayerControl.Instance.GetPlayer())
        {
            // �÷��̾��� ü���� ȸ���Ѵ�
            float currentHP = RealtimeInfoManager.Instance.GetCurrentHP();
            float maxHP = RealtimeInfoManager.Instance.GetHP();

            currentHP += 3.0f;
            // �ִ� ü���� �ʰ����� �ʴ´�
            if (currentHP >= maxHP)
                currentHP = maxHP;

            RealtimeInfoManager.Instance.SetCurrentHP(currentHP);

            // �ؽ�Ʈ�� ����Ѵ�
            PrintText(collision.transform, 3);
            Destroy(this.gameObject);
        }
    }

    void PrintText(Transform transform, int num)
    {
        // ����� �ؽ�Ʈ ���
        GameObject text = Resources.Load<GameObject>("Prefabs/DamageText");
        TextMeshPro textPro = text.GetComponent<TextMeshPro>();

        // �ؽ�Ʈ �� ���� ����
        textPro.text = "+" + num.ToString();
        textPro.color = Color.green;

        GameObject copy = Instantiate(text);
        Vector3 randomPos = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(0.4f, 0.6f), 0f);
        copy.transform.position = transform.position + randomPos;
    }
}
