using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UpgradeRerollButton : MonoBehaviour
{
    // 버튼
    private Button rerollButton;
    private TextMeshProUGUI rerollText;
    private int currentCost = 2;
    int rerollCount = 0;

    private void Awake()
    {
        rerollButton = this.transform.GetComponent<Button>();
        rerollText = this.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        rerollButton.onClick.AddListener(OnClickRerollButton);
        rerollText.text = "초기화 -" + 2;
    }

    void Update()
    {
        
    }

    private void OnClickRerollButton()
    {
        // 가진 와플이 요구 와플보다 많으면
        if (PlayerInfo.Instance.GetCurrentWaffle() > currentCost)
        {
            rerollCount++;
            UpgradeManager.Instance.renewUpgradeList = UpgradeManager.Instance.RenewUpgradeList();

            // 보유 와플 소모
            PlayerInfo.Instance.SetCurrentWaffle(PlayerInfo.Instance.GetCurrentWaffle() - currentCost);
            StartCoroutine(RenewWaffleAmount.Instance.RenewCurrentWaffleAmount());

            // 리롤 비용 조정
            currentCost = CalCurrentCost();
            // 텍스트 변경
            SetTextToCurrentCost();
        }
    }

    // 현재 리롤 비용을 계산하는 기능
    private int CalCurrentCost()
    {
        int increaseCost = Mathf.FloorToInt(GameRoot.Instance.GetCurrentRound() * 0.5f);
        if (increaseCost < 1)
            increaseCost = 1;

        increaseCost = increaseCost * (rerollCount + 1);

        int currentCost = GameRoot.Instance.GetCurrentRound() + increaseCost;

        return currentCost;
    }

    // 현재 요구하는 비용에 맞게 텍스트 변경
    public void SetTextToCurrentCost()
    {
        rerollText.text = "초기화 -" + currentCost;
    }
}
