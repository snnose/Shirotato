using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UpgradeRerollButton : MonoBehaviour, IPointerEnterHandler
{
    // ��ư
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
        rerollText.text = "�ʱ�ȭ -" + 2;
    }

    void Update()
    {
        
    }

    private void OnClickRerollButton()
    {
        // ��ư Ŭ�� �� ���� ���
        ButtonSoundManager.Instance.PlayOnClickButtonSound1();

        // ���� ������ �䱸 ���ú��� ������
        if (PlayerInfo.Instance.GetCurrentWaffle() > currentCost)
        {
            rerollCount++;
            UpgradeManager.Instance.renewUpgradeList = UpgradeManager.Instance.RenewUpgradeList();

            // ���� ���� �Ҹ�
            PlayerInfo.Instance.SetCurrentWaffle(PlayerInfo.Instance.GetCurrentWaffle() - currentCost);
            StartCoroutine(RenewWaffleAmount.Instance.RenewCurrentWaffleAmount());

            // ���� ��� ����
            currentCost = CalCurrentCost();
            // �ؽ�Ʈ ����
            SetTextToCurrentCost();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // ������ ���� �� ���� ���
        ButtonSoundManager.Instance.PlayOnPointerEnterSound1();
    }

    public void InitReroll()
    {
        this.rerollCount = 0;

        int increaseCost = Mathf.FloorToInt((rerollCount + 1) * GameRoot.Instance.GetCurrentRound() * 0.5f);
        if (increaseCost < 1)
            increaseCost = 1;

        this.currentCost = GameRoot.Instance.GetCurrentRound() + increaseCost;
        rerollText.text = "�ʱ�ȭ -" + currentCost;
    }

    // ���� ���� ����� ����ϴ� ���
    private int CalCurrentCost()
    {
        int increaseCost = Mathf.FloorToInt(GameRoot.Instance.GetCurrentRound() * 0.5f);
        if (increaseCost < 1)
            increaseCost = 1;

        increaseCost = increaseCost * (rerollCount + 1);

        int currentCost = GameRoot.Instance.GetCurrentRound() + increaseCost;

        return currentCost;
    }

    // ���� �䱸�ϴ� ��뿡 �°� �ؽ�Ʈ ����
    public void SetTextToCurrentCost()
    {
        rerollText.text = "�ʱ�ȭ -" + currentCost;
    }
}