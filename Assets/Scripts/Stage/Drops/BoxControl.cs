using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxControl : MonoBehaviour
{
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
        // �÷��̾�� �´����� �ڽ� ȹ��
        if (collision.gameObject == PlayerControl.Instance.gameObject)
        {
            // ���� ���忡 ���� �ڽ� ���� �߰�
            int boxCount = GameRoot.Instance.GetBoxCount();
            GameRoot.Instance.SetBoxCount(++boxCount);

            // ȭ�� ���ܿ� UI �÷���

            Destroy(this.gameObject);
        }
    }
}
