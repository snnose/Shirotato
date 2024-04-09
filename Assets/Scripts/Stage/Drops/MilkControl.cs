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
        AttractToPlayer(1.5f);

        // ���� ���� �� �������
        if (GameRoot.Instance.GetIsRoundClear())
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾�� �´��� ���
        if (collision.gameObject == PlayerControl.Instance.GetPlayer())
        {
            // RareItem35�� �ִٸ� ������ ȿ�� �ߵ�
            ActivateRareItem35();

            // �÷��̾��� ü���� ȸ���Ѵ�
            float currentHP = RealtimeInfoManager.Instance.GetCurrentHP();
            float maxHP = RealtimeInfoManager.Instance.GetHP();

            // ���� ü���� �ִ� ü�°� ������ LegendItem18 ȿ�� �ߵ� (�ִ� 10��)
            if (currentHP == maxHP &&
                ItemManager.Instance.GetOwnLegendItemList()[18] > 0 &&
                RealtimeInfoManager.Instance.legendItem18Stack <= 10)
            {
                ActivateLegendItem18();
                currentHP++; maxHP++;
            }

            // ������ ������ �ִ� �������� �ִٸ� ȸ������ �����ȴ�. (�⺻ 3)
            float healing = 3.0f - (1.0f * ItemManager.Instance.GetOwnNormalItemList()[34])
                                 + (1.0f * ItemManager.Instance.GetOwnNormalItemList()[41])
                                 + (1.0f * ItemManager.Instance.GetOwnNormalItemList()[46]);
            currentHP += healing;
            // �ִ� ü���� �ʰ����� �ʴ´�
            if (currentHP >= maxHP)
                currentHP = maxHP;

            RealtimeInfoManager.Instance.SetCurrentHP(currentHP);

            // �ؽ�Ʈ�� ����Ѵ�
            PrintText(collision.transform, int.Parse(healing.ToString()));
            Destroy(this.gameObject);
        }
    }

    private void AttractToPlayer(float range)
    {
        Vector2 playerPos = PlayerControl.Instance.GetPlayer().transform.position;

        // ���� ���� ���̶��
        if (!GameRoot.Instance.GetIsRoundClear())
        {
            float dis = Vector2.Distance(this.transform.position, playerPos);

            if (dis < range)
            {
                this.transform.position =
                Vector2.Lerp(this.transform.position, playerPos, 0.1f);
            }
        }

        // ���尡 ���� �ƴٸ�
        if (GameRoot.Instance.GetIsRoundClear())
        {
            // �÷��̾� ��ġ ���� (���̴� �� ���� x�� -0.1f��ŭ �з�����)
            Vector2 newPos = new Vector2(playerPos.x - 0.1f, playerPos.y);

            // ������ �÷��̾�� ��������
            this.transform.position =
                Vector2.Lerp(this.transform.position, playerPos, 0.08f);
        }
    }

    void ActivateRareItem35()
    {
        if (ItemManager.Instance.GetOwnRareItemList()[35] > 0)
        {
            float random = Random.Range(0f, 100f);

            // ������ ���� * 25% Ȯ���� (15 + �ִ� ü�� 150%) ������� ������
            if (random < 25f * ItemManager.Instance.GetOwnRareItemList()[35])
            {
                random = Random.Range(0, SpawnManager.Instance.GetCurrentMonsters().Count);
                MonsterControl monster = SpawnManager.Instance.GetCurrentMonsters()[Mathf.FloorToInt(random)].GetComponent<MonsterControl>();

                int damage = 15 + Mathf.FloorToInt(RealtimeInfoManager.Instance.GetHP() * 1.5f);
                monster.PrintText(monster.transform, Color.cyan, damage);
                monster.SetMonsterCurrentHP(monster.GetMonsterCurrentHP() - damage);
            }
        }
    }

    // LegendItem18 ȿ�� �ߵ�
    // �ִ� ü���� ���¿��� ���� ȹ�� �� �ִ� ü�� +1 (���帶�� �ִ� 10)
    void ActivateLegendItem18()
    {
        if (ItemManager.Instance.GetOwnLegendItemList()[18] > 0)
        {
            float maxHP = PlayerInfo.Instance.GetHP() + 1f;
            PlayerInfo.Instance.SetHP(maxHP);
            RealtimeInfoManager.Instance.SetHP(maxHP);
            RealtimeInfoManager.Instance.SetCurrentHP(maxHP);
            RealtimeInfoManager.Instance.legendItem18Stack++;
        }
    }

    void PrintText(Transform transform, int num)
    {
        // ����� �ؽ�Ʈ ���
        GameObject text = Resources.Load<GameObject>("Prefabs/DamageText");
        TextMeshPro textPro = text.GetComponent<TextMeshPro>();

        // �ؽ�Ʈ �� ���� ����
        textPro.text = "+" + num.ToString();

        Color color = Color.white;
        ColorUtility.TryParseHtmlString("#1FDE38", out color);
        textPro.color = color;

        GameObject copy = Instantiate(text);
        Vector3 randomPos = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(0.4f, 0.6f), 0f);
        copy.transform.position = transform.position + randomPos;
    }
}
