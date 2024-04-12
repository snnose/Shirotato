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
        // 게임 오버 시 오브젝트 파괴
        if (GameRoot.Instance.GetIsGameOver())
            Destroy(this.gameObject);

        // 라운드가 종료되면 플레이어에게 끌려 가 사라진다.
        // 대신 이렇게 획득한 와플은 다음 라운드에 와플을 습득 시 추가 와플을 얻도록 한다.
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

        // 라운드 진행 중 플레이어와 닿을 경우
        if (collision.gameObject == GameObject.FindGameObjectWithTag("Player")
            && !GameRoot.Instance.GetIsRoundClear())
        {
            // 보유 와플 개수 + 1
            PlayerInfo.Instance.SetCurrentWaffle(++currentWaffle);
            // RareItem29 보유 시 확률에 따라 추가 와플 획득
            ActivateRareItem29(currentWaffle);
            // 현재 경험치 + 1 * 경험치 배율
            ExpManager.Instance.SetCurrentExp(currentExp + (1 * (1 + PlayerInfo.Instance.GetExpGain() / 100)));
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
            // NormalItem38 보유 시 아이템 효과 발생
            if (ItemManager.Instance.GetOwnNormalItemList()[38] > 0)
            {
                ActivateNormalItem38();
            }

            // Waffle UI 갱신
            RenewWaffleAmount.Instance.renewCurrentWaffleAmount = RenewWaffleAmount.Instance.RenewCurrentWaffleAmount();

            Destroy(this.gameObject);
            WaffleSoundManager.Instance.PlayWaffleSound();
        }
        // 라운드 종료 후 플레이어와 닿을 경우 저장 와플 + 1
        if (collision.gameObject == GameObject.FindGameObjectWithTag("Player")
            && GameRoot.Instance.GetIsRoundClear())
        {
            PlayerInfo.Instance.SetStoredWaffle(++storedWaffle);
            // Waffle UI 갱신
            RenewWaffleAmount.Instance.renewStoredWaffleAmount = RenewWaffleAmount.Instance.RenewStoredWaffleAmount();

            Destroy(this.gameObject);
            WaffleSoundManager.Instance.PlayWaffleSound();
        }
    }

    private void ActivateNormalItem35()
    {
        // 아이템 보유 개수만큼 반복한다
        for (int i = 0; i < ItemManager.Instance.GetOwnNormalItemList()[35]; i++)
        {
            // 난수를 돌려 효과 발생 여부를 체크
            float random = Random.Range(0f, 100f);

            // 랜덤한 적에게 행운의 25%만큼의 대미지를 가한다.
            if (random < 25f)
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
                PrintText(monsterInfo.transform, damage, Color.white);
            }
        }
    }

    private void ActivateNormalItem36()
    {
        if (ItemManager.Instance.GetOwnNormalItemList()[36] > 0)
        {
            float random = Random.Range(0f, 100f);

            // NormalItem36 보유 시 와플 드랍 시 확률에 따라 플레이어에게 바로 끌려온다
            if (random < 20f * ItemManager.Instance.GetOwnNormalItemList()[36])
            {
                isAttractImmediatly = true;
            }
        }
    }

    private void ActivateNormalItem38()
    {
        float random = Random.Range(0f, 100f);

        // Normaltem38 보유 시, 와플 드랍 시 확률에 따라 플레이어의 체력 1 회복
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

        // RareItem29 보유 시, 와플 획득 시 확률에 따라 2배로 획득
        if (random < 5f * ItemManager.Instance.GetOwnRareItemList()[29])
        {
            PlayerInfo.Instance.SetCurrentWaffle(++currentWaffle);
        }
    }

    // LegendItem27 보유 시 와플이 드랍되는 즉시 플레이어에게 끌려온다
    private void ActivateLegendItem27()
    {
        if (ItemManager.Instance.GetOwnLegendItemList()[27] > 0)
        {
            isAttractImmediatly = true;
        }
    }


    // 와플이 플레이어에게 끌려가는 함수
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
                Vector2.Lerp(this.transform.position, playerPos, 0.02f);
        }
    }

    void PrintHealingText(Transform transform, int num)
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

    void PrintText(Transform transform, int num, Color color)
    {
        // 대미지 텍스트 출력
        GameObject text = Resources.Load<GameObject>("Prefabs/DamageText");
        TextMeshPro textPro = text.GetComponent<TextMeshPro>();

        // 텍스트 및 색상 결정
        textPro.text = num.ToString();
        textPro.color = color;

        GameObject copy = Instantiate(text);
        Vector3 randomPos = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(0.4f, 0.6f), 0f);
        copy.transform.position = transform.position + randomPos;
    }
}
