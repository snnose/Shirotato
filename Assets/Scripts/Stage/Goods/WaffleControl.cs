using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaffleControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ���� ����Ǹ� �÷��̾�� ���� �� �������.
        // ��� �̷��� ȹ���� ������ ���� ���忡 ������ ���� �� �߰� ������ �򵵷� �Ѵ�.
        AttractToPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int currentWaffle = PlayerInfo.Instance.GetCurrentWaffle();
        int storedWaffle = PlayerInfo.Instance.GetStoredWaffle();
        float currentExp = ExpManager.Instance.GetCurrentExp();

        // ���� ���� �� �÷��̾�� ���� ���
        if (collision.gameObject == PlayerControl.Instance.GetPlayer()
            && !GameRoot.Instance.GetIsRoundClear())
        {
            // ���� ���� ���� + 1
            PlayerInfo.Instance.SetCurrentWaffle(++currentWaffle);
            // ���� ����ġ + 1 * ����ġ ����
            ExpManager.Instance.SetCurrentExp(currentExp + (1 * PlayerInfo.Instance.GetExpGain()));
            // ������ �ڷ�ƾ ���� (����ġ�� �����Ǹ� ������)
            ExpManager.Instance.levelUp = (ExpManager.Instance.LevelUp());

            // ����� ������ �ϳ� �̻��̸� �߰��� ��´�
            if (PlayerInfo.Instance.GetStoredWaffle() > 0)
            {
                PlayerInfo.Instance.SetCurrentWaffle(++currentWaffle);
                PlayerInfo.Instance.SetStoredWaffle(--storedWaffle);

                RenewWaffleAmount.Instance.renewStoredWaffleAmount = RenewWaffleAmount.Instance.RenewStoredWaffleAmount();
            }
            // Waffle UI ����
            RenewWaffleAmount.Instance.renewCurrentWaffleAmount = RenewWaffleAmount.Instance.RenewCurrentWaffleAmount();
            Destroy(this.gameObject);
        }
        // ���� ���� �� �÷��̾�� ���� ��� ���� ���� + 1
        if (collision.gameObject == PlayerControl.Instance.GetPlayer()
            && GameRoot.Instance.GetIsRoundClear())
        {
            PlayerInfo.Instance.SetStoredWaffle(++storedWaffle);
            // Waffle UI ����
            RenewWaffleAmount.Instance.renewStoredWaffleAmount = RenewWaffleAmount.Instance.RenewStoredWaffleAmount();
            Destroy(this.gameObject);
        }
    }

    // ���� ���� �� �÷��̾�� �������� �Լ�
    private void AttractToPlayer()
    {
        // ���尡 ���� �ƴٸ�
        if (GameRoot.Instance.GetIsRoundClear())
        {
            // �÷��̾� ��ġ ���� (���̴� �� ���� x�� -0.1f��ŭ �з�����)
            Vector2 playerPos = PlayerControl.Instance.GetPlayer().transform.position;
            Vector2 newPos = new Vector2(playerPos.x - 0.1f, playerPos.y);

            // ������ �÷��̾�� ��������
            this.transform.position =
                Vector2.Lerp(this.transform.position, playerPos, 0.005f);
        }
    }
}
