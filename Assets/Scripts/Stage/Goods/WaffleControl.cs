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
            ExpManager.Instance.SetCurrentExp(currentExp + (1 * PlayerInfo.Instance.GetExpGain()));
            // 레벨업 코루틴 실행 (경험치가 충족되면 레벨업)
            ExpManager.Instance.levelUp = (ExpManager.Instance.LevelUp());

            // 저장된 와플이 하나 이상이면 추가로 얻는다
            if (PlayerInfo.Instance.GetStoredWaffle() > 0)
            {
                PlayerInfo.Instance.SetCurrentWaffle(++currentWaffle);
                PlayerInfo.Instance.SetStoredWaffle(--storedWaffle);

                RenewWaffleAmount.Instance.renewStoredWaffleAmount = RenewWaffleAmount.Instance.RenewStoredWaffleAmount();
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

    // 라운드 종료 시 플레이어에게 끌려가는 함수
    private void AttractToPlayer()
    {
        // 라운드가 종료 됐다면
        if (GameRoot.Instance.GetIsRoundClear())
        {
            // 플레이어 위치 조정 (보이는 것 보다 x값 -0.1f만큼 밀려있음)
            Vector2 playerPos = PlayerControl.Instance.GetPlayer().transform.position;
            Vector2 newPos = new Vector2(playerPos.x - 0.1f, playerPos.y);

            // 와플이 플레이어에게 끌려간다
            this.transform.position =
                Vector2.Lerp(this.transform.position, playerPos, 0.005f);
        }
    }
}
