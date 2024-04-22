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
        // 플레이어와 맞닿으면 박스 획득
        if (collision.gameObject == GameObject.FindGameObjectWithTag("Player"))
        {
            // 현재 라운드에 얻은 박스 개수 추가
            int boxCount = GameRoot.Instance.GetBoxCount();
            GameRoot.Instance.SetBoxCount(++boxCount);
            // 박스 획득 UI 활성화
            GetBoxUIControl.Instance.SetActive(true);

            // 아이템 보유 시 효과 발동
            if (ItemManager.Instance.GetOwnNormalItemList()[37] > 0)
            {
                // 상자 습득 시 15 * 아이템 개수 만큼 와플 획득
                ActivateNormalItem37();
            }

            // 화면 우상단에 UI 플로팅

            // 사운드 출력
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
        // 라운드가 종료 됐다면
        if (GameRoot.Instance.GetIsRoundClear())
        {
            Vector2 playerPos = PlayerControl.Instance.GetPlayer().transform.position;

            // 플레이어 위치 조정 (보이는 것 보다 x값 -0.1f만큼 밀려있음)
            Vector2 newPos = new Vector2(playerPos.x - 0.1f, playerPos.y);

            // 상자가 플레이어에게 끌려간다
            this.transform.position =
                Vector2.Lerp(this.transform.position, playerPos, 0.15f);
        }
    }
}
