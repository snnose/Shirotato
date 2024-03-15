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
        // 라운도가 종료되면 플레이어에게 끌려 가 사라진다.
        // 대신 이렇게 획득한 와플은 다음 라운드에 와플을 습득 시 추가 와플을 얻도록 한다.
        AttractToPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int currentWaffle = PlayerInfo.Instance.GetCurrentWaffle();
        int storedWaffle = PlayerInfo.Instance.GetStoredWaffle();
        float currentExp = ExpManager.Instance.GetCurrentExp();

        // 라운드 진행 중 플레이어와 닿을 경우
        if (collision.gameObject == PlayerControl.Instance.GetPlayer()
            && !GameRoot.Instance.GetIsRoundClear())
        {
            // 보유 와플 개수 + 1
            PlayerInfo.Instance.SetCurrentWaffle(++currentWaffle);
            // 현재 경험치 + 1 * 경험치 배율
            ExpManager.Instance.SetCurrentExp(currentExp + (2 * PlayerInfo.Instance.GetExpGain()));
            // 레벨업 코루틴 실행 (경험치가 충족되면 레벨업)
            ExpManager.Instance.levelUp = (ExpManager.Instance.LevelUp());

            // 저장된 와플이 하나 이상이면 추가로 얻는다
            if (PlayerInfo.Instance.GetStoredWaffle() > 0)
            {
                PlayerInfo.Instance.SetCurrentWaffle(++currentWaffle);
                PlayerInfo.Instance.SetStoredWaffle(--storedWaffle);

                RenewWaffleAmount.Instance.renewStoredWaffleAmount = RenewWaffleAmount.Instance.RenewStoredWaffleAmount();
            }

            // NormalItem35 보유 시 아이템 효과 발생
            if (ItemManager.Instance.GetOwnNormalItemList()[35] > 0)
            {
                ActivateNormalItem35();
            }

            // Waffle UI 갱신
            RenewWaffleAmount.Instance.renewCurrentWaffleAmount = RenewWaffleAmount.Instance.RenewCurrentWaffleAmount();
            Destroy(this.gameObject);
        }
        // 라운드 종료 후 플레이어와 닿을 경우 저장 와플 + 1
        if (collision.gameObject == PlayerControl.Instance.GetPlayer()
            && GameRoot.Instance.GetIsRoundClear())
        {
            PlayerInfo.Instance.SetStoredWaffle(++storedWaffle);
            // Waffle UI 갱신
            RenewWaffleAmount.Instance.renewStoredWaffleAmount = RenewWaffleAmount.Instance.RenewStoredWaffleAmount();

            Destroy(this.gameObject);
        }
    }

    private void ActivateNormalItem35()
    {
        // 난수를 돌려 효과 발생 여부를 체크
        float random = Random.Range(0f, 100f);

        // 랜덤한 적에게 행운의 25%만큼의 대미지를 가한다.
        if (random < 25f * ItemManager.Instance.GetOwnNormalItemList()[35])
        {
            // 대미지 계산
            int damage = Mathf.FloorToInt(RealtimeInfoManager.Instance.GetLuck() * 0.25f);
            if (damage <= 0)
                damage = 1;

            // 현재 스폰된 적 중에서 하나를 골라 대미지를 입힌다
            int ran = Random.Range(0, SpawnManager.Instance.GetCurrentMonsters().Count);
            MonsterInfo monsterInfo = SpawnManager.Instance.GetCurrentMonsters()[ran].GetComponent<MonsterInfo>();
            monsterInfo.SetMonsterHP(monsterInfo.GetMonsterHP() - damage);

            // 텍스트 출력
            PrintText(monsterInfo.transform, damage);
        }
    }

    // 와플이 플레이어에게 끌려가는 함수
    private void AttractToPlayer()
    {
        Vector2 playerPos = PlayerControl.Instance.GetPlayer().transform.position;

        // 라운드 진행 중이라면
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

        // 라운드가 종료 됐다면
        if (GameRoot.Instance.GetIsRoundClear())
        {
            // 플레이어 위치 조정 (보이는 것 보다 x값 -0.1f만큼 밀려있음)
            Vector2 newPos = new Vector2(playerPos.x - 0.1f, playerPos.y);

            // 와플이 플레이어에게 끌려간다
            this.transform.position =
                Vector2.Lerp(this.transform.position, playerPos, 0.005f);
        }
    }

    void PrintText(Transform transform, int num)
    {
        // 대미지 텍스트 출력
        GameObject text = Resources.Load<GameObject>("Prefabs/DamageText");
        TextMeshPro textPro = text.GetComponent<TextMeshPro>();

        // 텍스트 및 색상 결정
        textPro.text = num.ToString();

        Color color = Color.white;
        textPro.color = color;

        GameObject copy = Instantiate(text);
        Vector3 randomPos = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(0.4f, 0.6f), 0f);
        copy.transform.position = transform.position + randomPos;
    }
}
