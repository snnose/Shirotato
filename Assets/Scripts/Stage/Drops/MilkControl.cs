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

        // 라운드 종료 시 사라진다
        if (GameRoot.Instance.GetIsRoundClear())
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 맞닿을 경우
        if (collision.gameObject == PlayerControl.Instance.GetPlayer())
        {
            // RareItem35가 있다면 아이템 효과 발동
            ActivateRareItem35();

            // 플레이어의 체력을 회복한다
            float currentHP = RealtimeInfoManager.Instance.GetCurrentHP();
            float maxHP = RealtimeInfoManager.Instance.GetHP();

            // 현재 체력이 최대 체력과 같으면 LegendItem18 효과 발동 (최대 10번)
            if (currentHP == maxHP &&
                ItemManager.Instance.GetOwnLegendItemList()[18] > 0 &&
                RealtimeInfoManager.Instance.legendItem18Stack <= 10)
            {
                ActivateLegendItem18();
                currentHP++; maxHP++;
            }

            // 우유에 영향을 주는 아이템이 있다면 회복량이 변동된다. (기본 3)
            float healing = 3.0f - (1.0f * ItemManager.Instance.GetOwnNormalItemList()[34])
                                 + (1.0f * ItemManager.Instance.GetOwnNormalItemList()[41])
                                 + (1.0f * ItemManager.Instance.GetOwnNormalItemList()[46]);
            currentHP += healing;
            // 최대 체력을 초과하지 않는다
            if (currentHP >= maxHP)
                currentHP = maxHP;

            RealtimeInfoManager.Instance.SetCurrentHP(currentHP);

            // 텍스트를 출력한다
            PrintText(collision.transform, int.Parse(healing.ToString()));
            Destroy(this.gameObject);
        }
    }

    private void AttractToPlayer(float range)
    {
        Vector2 playerPos = PlayerControl.Instance.GetPlayer().transform.position;

        // 라운드 진행 중이라면
        if (!GameRoot.Instance.GetIsRoundClear())
        {
            float dis = Vector2.Distance(this.transform.position, playerPos);

            if (dis < range)
            {
                this.transform.position =
                Vector2.Lerp(this.transform.position, playerPos, 0.1f);
            }
        }

        // 라운드가 종료 됐다면
        if (GameRoot.Instance.GetIsRoundClear())
        {
            // 플레이어 위치 조정 (보이는 것 보다 x값 -0.1f만큼 밀려있음)
            Vector2 newPos = new Vector2(playerPos.x - 0.1f, playerPos.y);

            // 와플이 플레이어에게 끌려간다
            this.transform.position =
                Vector2.Lerp(this.transform.position, playerPos, 0.08f);
        }
    }

    void ActivateRareItem35()
    {
        if (ItemManager.Instance.GetOwnRareItemList()[35] > 0)
        {
            float random = Random.Range(0f, 100f);

            // 아이템 개수 * 25% 확률로 (15 + 최대 체력 150%) 대미지를 입힌다
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

    // LegendItem18 효과 발동
    // 최대 체력인 상태에서 우유 획득 시 최대 체력 +1 (라운드마다 최대 10)
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
        // 대미지 텍스트 출력
        GameObject text = Resources.Load<GameObject>("Prefabs/DamageText");
        TextMeshPro textPro = text.GetComponent<TextMeshPro>();

        // 텍스트 및 색상 결정
        textPro.text = "+" + num.ToString();

        Color color = Color.white;
        ColorUtility.TryParseHtmlString("#1FDE38", out color);
        textPro.color = color;

        GameObject copy = Instantiate(text);
        Vector3 randomPos = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(0.4f, 0.6f), 0f);
        copy.transform.position = transform.position + randomPos;
    }
}
