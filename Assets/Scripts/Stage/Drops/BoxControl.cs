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
        AttractToPlayer();
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

    private void AttractToPlayer()
    {
        // ���尡 ���� �ƴٸ�
        if (GameRoot.Instance.GetIsRoundClear())
        {
            Vector2 playerPos = PlayerControl.Instance.GetPlayer().transform.position;

            // �÷��̾� ��ġ ���� (���̴� �� ���� x�� -0.1f��ŭ �з�����)
            Vector2 newPos = new Vector2(playerPos.x - 0.1f, playerPos.y);

            // ������ �÷��̾�� ��������
            this.transform.position =
                Vector2.Lerp(this.transform.position, playerPos, 0.008f);
        }
    }
}
