using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    private static UpgradeManager instance;
    public static UpgradeManager Instance
    {
        get
        {
            if (null == instance)
                return null;

            return instance;
        }
    }
    // 0 ~ 10의 값을 갖는 업그레이드 리스트
    // 0 = 최대 체력, 1 = 회복력, 2 = 대미지 %, 3 = 고정 대미지
    // 4 = 공격속도%, 5 = 치명타, 6 = 범위%, 7 = 회피 확률
    // 8 = 방어력,    9 = 이속%, 10 = 행운
    // item1 = upgrade, item2 = rarity
    List<(int, int)> upgradeList = new();
    // 중복된 업그레이드 방지용 리스트
    List<int> currentUpgradeList = new();

    private UpgradeListControl upgradeListControl;

    private int currentUpgradeLevel = 0;
    public IEnumerator renewUpgradeList;

    // 버튼
    private Button rerollButton;
    private TextMeshProUGUI rerollText;
    private int currentCost = 2;
    int rerollCount = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        upgradeListControl = this.gameObject.transform.GetChild(1).GetComponent<UpgradeListControl>();
        rerollButton = this.transform.GetChild(3).GetComponent<Button>();
        rerollText = this.transform.GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            (int, int) tmp = (-1, -1);
            upgradeList.Add(tmp);
        }

        rerollButton.onClick.AddListener(OnClickRerollButton);
        rerollText.text = "초기화 -" + 2;

        this.transform.position = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (renewUpgradeList != null)
        {
            StartCoroutine(renewUpgradeList);
        }
    }

    public IEnumerator RenewUpgradeList()
    {
        List<float> upgradeProbabilities = new();

        // 현재 업그레이드 레벨에 따라 확률이 변동된다
        upgradeProbabilities = SetUpgradeProbability();

        // 총 4개의 업그레이드를 결정한다.
        for (int i = 0; i < 4; i++)
        {
            // 난수를 사용해 레어도를 결정한다
            float rarityRandom = UnityEngine.Random.Range(0.0f, 100.0f);
            int rarity = -1;

            for (int j = 3; j >= 0; j--)
            {
                if (rarityRandom < upgradeProbabilities[j])
                {
                    rarity = j;
                    break;
                }
            }

            // 0 ~ 12 사이의 값을 고른다
            int upgradeRandom = UnityEngine.Random.Range(0, 13);
            // 만약 중복된 업그레이드가 나왔다면
            for (int j = 0; j < upgradeList.Count; j++)
            {
                int tmp = upgradeList[j].Item1;
                if (upgradeRandom == tmp)
                {
                    // 다시 굴리고 재검사
                    upgradeRandom = UnityEngine.Random.Range(0, 13);
                    j = -1;
                    continue;
                }
            }

            (int, int) upgrade = (upgradeRandom, rarity);
            upgradeList[i] = upgrade;
        }
        // 업그레이드 UI 갱신 코루틴
        upgradeListControl.renewUpgradeRoom = upgradeListControl.RenewUpgradeRoom();
        currentUpgradeList.Clear();
        yield return null;
    }

    // 업그레이드 레어도 결정 함수
    List<float> SetUpgradeProbability()
    {
        List<float> tmp = new List<float>(new float[] { 100, 0, 0, 0 });

        // 전설 등급 확률 (최대 8%)
        tmp[3] = (currentUpgradeLevel - 7) / 4 * ((1 + PlayerInfo.Instance.GetLuck()) / 100);
        if (tmp[3] <= 0)
            tmp[3] = 0;
        else if (tmp[3] >= 8)
            tmp[3] = 8;
        // 에픽 등급 확률 (최대 25%)
        tmp[2] = (1f - (tmp[3] * 0.01f)) * 2 * (currentUpgradeLevel - 3) * ((1 + PlayerInfo.Instance.GetLuck()) / 100) + tmp[3];
        if (tmp[2] <= 0)
            tmp[2] = 0;
        else if (tmp[2] >= (1f - (tmp[3] * 0.01f)) * 25 + tmp[3])
            tmp[2] = (1f - (tmp[3] * 0.01f)) * 25 + tmp[3];
        // 레어 등급 확률 (최대 60%)
        tmp[1] = (1f - (tmp[3] * 0.01f) - (tmp[2] * 0.01f)) * 6 * currentUpgradeLevel * ((1 + PlayerInfo.Instance.GetLuck()) / 100) + tmp[2] + tmp[3];
        if (tmp[1] >= (1f - (tmp[3] * 0.01f) - (tmp[2] * 0.01f)) * 60 + tmp[2] + tmp[3])
            tmp[1] = (1f - (tmp[3] * 0.01f) - (tmp[2] * 0.01f)) * 60 + tmp[2] + tmp[3];

        return tmp;
    }

    private void OnClickRerollButton()
    {
        // 가진 와플이 요구 와플보다 많으면
        if (PlayerInfo.Instance.GetCurrentWaffle() > currentCost)
        {
            rerollCount++;
            renewUpgradeList = RenewUpgradeList();

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

    public void SetCurrentUpgradeLevel(int level)
    {
        this.currentUpgradeLevel = level;
    }

    public void InitReroll()
    {
        this.rerollCount = 0;

        int increaseCost = Mathf.FloorToInt((rerollCount + 1) * GameRoot.Instance.GetCurrentRound() * 0.5f);
        if (increaseCost < 1)
            increaseCost = 1;

        this.currentCost = GameRoot.Instance.GetCurrentRound() + increaseCost;
    }

    public int GetCurrentUpgradeLevel()
    {
        return this.currentUpgradeLevel;
    }

    public List<(int, int)> GetUpgradeList()
    {
        return this.upgradeList;
    }
}
