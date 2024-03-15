using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
            ExpManager.Instance.SetCurrentExp(currentExp + (2 * PlayerInfo.Instance.GetExpGain()));
            // ������ �ڷ�ƾ ���� (����ġ�� �����Ǹ� ������)
            ExpManager.Instance.levelUp = (ExpManager.Instance.LevelUp());

            // ����� ������ �ϳ� �̻��̸� �߰��� ��´�
            if (PlayerInfo.Instance.GetStoredWaffle() > 0)
            {
                PlayerInfo.Instance.SetCurrentWaffle(++currentWaffle);
                PlayerInfo.Instance.SetStoredWaffle(--storedWaffle);

                RenewWaffleAmount.Instance.renewStoredWaffleAmount = RenewWaffleAmount.Instance.RenewStoredWaffleAmount();
            }

            // NormalItem35 ���� �� ������ ȿ�� �߻�
            if (ItemManager.Instance.GetOwnNormalItemList()[35] > 0)
            {
                ActivateNormalItem35();
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

    private void ActivateNormalItem35()
    {
        // ������ ���� ������ŭ �ݺ��Ѵ�
        for (int i = 0; i < ItemManager.Instance.GetOwnNormalItemList()[35]; i++)
        {
            // ������ ���� ȿ�� �߻� ���θ� üũ
            float random = Random.Range(0f, 100f);

            // ������ ������ ����� 25%��ŭ�� ������� ���Ѵ�.
            if (random < 25f)
            {
                // ����� ���
                int damage = Mathf.FloorToInt(RealtimeInfoManager.Instance.GetLuck() * 0.25f);
                if (damage <= 0)
                    damage = 1;

                // ���� ������ �� �߿��� �ϳ��� ��� ������� ������
                int ran = Random.Range(0, SpawnManager.Instance.GetCurrentMonsters().Count);
                MonsterInfo monsterInfo = SpawnManager.Instance.GetCurrentMonsters()[ran].GetComponent<MonsterInfo>();
                monsterInfo.SetMonsterHP(monsterInfo.GetMonsterHP() - damage);

                // �ؽ�Ʈ ���
                PrintText(monsterInfo.transform, damage);
            }
        }
    }

    // ������ �÷��̾�� �������� �Լ�
    private void AttractToPlayer()
    {
        Vector2 playerPos = PlayerControl.Instance.GetPlayer().transform.position;

        // ���� ���� ���̶��
        if (!GameRoot.Instance.GetIsRoundClear())
        {
            float range = 2.5f * (1 + (PlayerInfo.Instance.GetRootingRange() / 100));
            float dis = Vector2.Distance(this.transform.position, playerPos);
                
            if (dis < range)
            {
                this.transform.position =
                Vector2.Lerp(this.transform.position, playerPos, 0.02f);
            }
        }

        // ���尡 ���� �ƴٸ�
        if (GameRoot.Instance.GetIsRoundClear())
        {
            // �÷��̾� ��ġ ���� (���̴� �� ���� x�� -0.1f��ŭ �з�����)
            Vector2 newPos = new Vector2(playerPos.x - 0.1f, playerPos.y);

            // ������ �÷��̾�� ��������
            this.transform.position =
                Vector2.Lerp(this.transform.position, playerPos, 0.005f);
        }
    }

    void PrintText(Transform transform, int num)
    {
        // ����� �ؽ�Ʈ ���
        GameObject text = Resources.Load<GameObject>("Prefabs/DamageText");
        TextMeshPro textPro = text.GetComponent<TextMeshPro>();

        // �ؽ�Ʈ �� ���� ����
        textPro.text = num.ToString();

        Color color = Color.white;
        textPro.color = color;

        GameObject copy = Instantiate(text);
        Vector3 randomPos = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(0.4f, 0.6f), 0f);
        copy.transform.position = transform.position + randomPos;
    }
}
