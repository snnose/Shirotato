using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxControl : MonoBehaviour
{
    private BoxSoundManager boxSoundManager;

    private void Awake()
    {
        boxSoundManager = GameObject.FindGameObjectWithTag("AudioManager").
                                transform.GetChild(0).GetChild(2).GetComponent<BoxSoundManager>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        AttractToPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾�� �´����� �ڽ� ȹ��
        if (collision.gameObject == GameObject.FindGameObjectWithTag("Player"))
        {
            // ���� ���忡 ���� �ڽ� ���� �߰�
            int boxCount = GameRoot.Instance.GetBoxCount();
            GameRoot.Instance.SetBoxCount(++boxCount);
            // �ڽ� ȹ�� UI Ȱ��ȭ
            GetBoxUIControl.Instance.SetActive(true);

            // ������ ���� �� ȿ�� �ߵ�
            if (ItemManager.Instance.GetOwnNormalItemList()[37] > 0)
            {
                // ���� ���� �� 15 * ������ ���� ��ŭ ���� ȹ��
                ActivateNormalItem37();
            }

            // ȭ�� ���ܿ� UI �÷���

            // ���� ���
            boxSoundManager.PlayBoxSound();

            Destroy(this.gameObject);
        }
    }

    private void ActivateNormalItem37()
    {
        int currentWaffle = PlayerInfo.Instance.GetCurrentWaffle();
        currentWaffle += 15 * ItemManager.Instance.GetOwnNormalItemList()[37];
        PlayerInfo.Instance.SetCurrentWaffle(currentWaffle);

        RenewWaffleAmount.Instance.renewCurrentWaffleAmount = RenewWaffleAmount.Instance.RenewCurrentWaffleAmount();
    }

    private void AttractToPlayer()
    {
        // ���尡 ���� �ƴٸ�
        if (GameRoot.Instance.GetIsRoundClear())
        {
            Vector2 playerPos = PlayerControl.Instance.GetPlayer().transform.position;

            // �÷��̾� ��ġ ���� (���̴� �� ���� x�� -0.1f��ŭ �з�����)
            Vector2 newPos = new Vector2(playerPos.x - 0.1f, playerPos.y);

            // ���ڰ� �÷��̾�� ��������
            this.transform.position =
                Vector2.Lerp(this.transform.position, playerPos, 0.15f);
        }
    }
}
