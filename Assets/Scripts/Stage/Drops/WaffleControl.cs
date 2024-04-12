using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaffleControl : MonoBehaviour
{
    bool isAttractImmediatly = false;
    float rootingRange;

    private AudioSource waffleAudioSource;

    private void Awake()
    {
        waffleAudioSource = this.GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    { 
        rootingRange = RealtimeInfoManager.Instance.GetRootingRange();
        
        ActivateNormalItem36();
        ActivateLegendItem27();
    }

    // Update is called once per frame
    void Update()
    {
        // ���� ���� �� ������Ʈ �ı�
        if (GameRoot.Instance.GetIsGameOver())
            Destroy(this.gameObject);

        // ���尡 ����Ǹ� �÷��̾�� ���� �� �������.
        // ��� �̷��� ȹ���� ������ ���� ���忡 ������ ���� �� �߰� ������ �򵵷� �Ѵ�.
        if (isAttractImmediatly)
            AttractToPlayer(100f);
        else
            AttractToPlayer(1.5f * (1 + (rootingRange / 100)));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int currentWaffle = PlayerInfo.Instance.GetCurrentWaffle();
        int storedWaffle = PlayerInfo.Instance.GetStoredWaffle();
        float currentExp = ExpManager.Instance.GetCurrentExp();

        // ���� ���� �� �÷��̾�� ���� ���
        if (collision.gameObject == GameObject.FindGameObjectWithTag("Player")
            && !GameRoot.Instance.GetIsRoundClear())
        {
            // ���� ���� ���� + 1
            PlayerInfo.Instance.SetCurrentWaffle(++currentWaffle);
            // RareItem29 ���� �� Ȯ���� ���� �߰� ���� ȹ��
            ActivateRareItem29(currentWaffle);
            // ���� ����ġ + 1 * ����ġ ����
            ExpManager.Instance.SetCurrentExp(currentExp + (1 * (1 + PlayerInfo.Instance.GetExpGain() / 100)));
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
            // NormalItem38 ���� �� ������ ȿ�� �߻�
            if (ItemManager.Instance.GetOwnNormalItemList()[38] > 0)
            {
                ActivateNormalItem38();
            }

            // Waffle UI ����
            RenewWaffleAmount.Instance.renewCurrentWaffleAmount = RenewWaffleAmount.Instance.RenewCurrentWaffleAmount();

            Destroy(this.gameObject);
            WaffleSoundManager.Instance.PlayWaffleSound();
        }
        // ���� ���� �� �÷��̾�� ���� ��� ���� ���� + 1
        if (collision.gameObject == GameObject.FindGameObjectWithTag("Player")
            && GameRoot.Instance.GetIsRoundClear())
        {
            PlayerInfo.Instance.SetStoredWaffle(++storedWaffle);
            // Waffle UI ����
            RenewWaffleAmount.Instance.renewStoredWaffleAmount = RenewWaffleAmount.Instance.RenewStoredWaffleAmount();

            Destroy(this.gameObject);
            WaffleSoundManager.Instance.PlayWaffleSound();
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
                PrintText(monsterInfo.transform, damage, Color.white);
            }
        }
    }

    private void ActivateNormalItem36()
    {
        if (ItemManager.Instance.GetOwnNormalItemList()[36] > 0)
        {
            float random = Random.Range(0f, 100f);

            // NormalItem36 ���� �� ���� ��� �� Ȯ���� ���� �÷��̾�� �ٷ� �����´�
            if (random < 20f * ItemManager.Instance.GetOwnNormalItemList()[36])
            {
                isAttractImmediatly = true;
            }
        }
    }

    private void ActivateNormalItem38()
    {
        float random = Random.Range(0f, 100f);

        // Normaltem38 ���� ��, ���� ��� �� Ȯ���� ���� �÷��̾��� ü�� 1 ȸ��
        if (random < 8f * ItemManager.Instance.GetOwnNormalItemList()[38])
        {
            float currentHP = RealtimeInfoManager.Instance.GetCurrentHP();
            if (currentHP < RealtimeInfoManager.Instance.GetHP())
            {
                currentHP += 1;
                RealtimeInfoManager.Instance.SetCurrentHP(currentHP);
            }

            PrintHealingText(PlayerControl.Instance.transform, 1);
        }
    }

    private void ActivateRareItem29(int currentWaffle)
    {
        float random = Random.Range(0f, 100f);

        // RareItem29 ���� ��, ���� ȹ�� �� Ȯ���� ���� 2��� ȹ��
        if (random < 5f * ItemManager.Instance.GetOwnRareItemList()[29])
        {
            PlayerInfo.Instance.SetCurrentWaffle(++currentWaffle);
        }
    }

    // LegendItem27 ���� �� ������ ����Ǵ� ��� �÷��̾�� �����´�
    private void ActivateLegendItem27()
    {
        if (ItemManager.Instance.GetOwnLegendItemList()[27] > 0)
        {
            isAttractImmediatly = true;
        }
    }


    // ������ �÷��̾�� �������� �Լ�
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
                Vector2.Lerp(this.transform.position, playerPos, 0.02f);
        }
    }

    void PrintHealingText(Transform transform, int num)
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

    void PrintText(Transform transform, int num, Color color)
    {
        // ����� �ؽ�Ʈ ���
        GameObject text = Resources.Load<GameObject>("Prefabs/DamageText");
        TextMeshPro textPro = text.GetComponent<TextMeshPro>();

        // �ؽ�Ʈ �� ���� ����
        textPro.text = num.ToString();
        textPro.color = color;

        GameObject copy = Instantiate(text);
        Vector3 randomPos = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(0.4f, 0.6f), 0f);
        copy.transform.position = transform.position + randomPos;
    }
}
