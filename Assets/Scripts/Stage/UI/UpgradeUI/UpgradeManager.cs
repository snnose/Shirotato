using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // 0 ~ 10�� ���� ���� ���׷��̵� ����Ʈ
    // 0 = �ִ� ü��, 1 = ȸ����, 2 = ����� %, 3 = ���� �����
    // 4 = ���ݼӵ�%, 5 = ġ��Ÿ, 6 = ����%, 7 = ȸ�� Ȯ��
    // 8 = ����,    9 = �̼�%, 10 = ���
    // item1 = upgrade, item2 = rarity
    List<(int, int)> upgradeList = new();
    // �ߺ��� ���׷��̵� ������ ����Ʈ
    List<int> currentUpgradeList = new();

    private UpgradeListControl upgradeListControl;

    private int currentUpgradeLevel = 0;
    public IEnumerator renewUpgradeList;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        upgradeListControl = this.gameObject.transform.GetChild(0).GetComponent<UpgradeListControl>();
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            (int, int) tmp = (-1, -1);
            upgradeList.Add(tmp);
        }

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

        // ���� ���׷��̵� ������ ���� Ȯ���� �����ȴ�
        upgradeProbabilities = SetUpgradeProbability();

        // �� 4���� ���׷��̵带 �����Ѵ�.
        for (int i = 0; i < 4; i++)
        {
            // ������ ����� ����� �����Ѵ�
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

            // 0 ~ 10 ������ ���� ����
            int upgradeRandom = UnityEngine.Random.Range(0, 11);
            // ���� �ߺ��� ���׷��̵尡 ���Դٸ�
            for (int j = 0; j < upgradeList.Count; j++)
            {
                int tmp = upgradeList[j].Item1;
                if (upgradeRandom == tmp)
                {
                    // �ٽ� ������ ��˻�
                    upgradeRandom = UnityEngine.Random.Range(0, 11);
                    j = 0;
                    continue;
                }
            }

            (int, int) upgrade = (upgradeRandom, rarity);
            upgradeList[i] = upgrade;
        }
        // ���׷��̵� UI ���� �ڷ�ƾ
        upgradeListControl.renewUpgradeRoom = upgradeListControl.RenewUpgradeRoom();
        currentUpgradeList.Clear();
        yield return null;
    }

    // ���׷��̵� ��� ���� �Լ�
    List<float> SetUpgradeProbability()
    {
        List<float> tmp = new List<float>(new float[] { 100, 0, 0, 0 });

        // ���� ��� Ȯ�� (�ִ� 8%)
        tmp[3] = (currentUpgradeLevel - 7) / 4 * ((1 + PlayerInfo.Instance.GetLuck()) / 100);
        if (tmp[3] <= 0)
            tmp[3] = 0;
        else if (tmp[3] >= 8)
            tmp[3] = 8;
        // ���� ��� Ȯ�� (�ִ� 25%)
        tmp[2] = (1f - (tmp[3] * 0.01f)) * 2 * (currentUpgradeLevel - 3) * ((1 + PlayerInfo.Instance.GetLuck()) / 100) + tmp[3];
        if (tmp[2] <= 0)
            tmp[2] = 0;
        else if (tmp[2] >= (1f - (tmp[3] * 0.01f)) * 25 + tmp[3])
            tmp[2] = (1f - (tmp[3] * 0.01f)) * 25 + tmp[3];
        // ���� ��� Ȯ�� (�ִ� 60%)
        tmp[1] = (1f - (tmp[3] * 0.01f) - (tmp[2] * 0.01f)) * 6 * currentUpgradeLevel * ((1 + PlayerInfo.Instance.GetLuck()) / 100) + tmp[2] + tmp[3];
        if (tmp[1] >= (1f - (tmp[3] * 0.01f) - (tmp[2] * 0.01f)) * 60 + tmp[2] + tmp[3])
            tmp[1] = (1f - (tmp[3] * 0.01f) - (tmp[2] * 0.01f)) * 60 + tmp[2] + tmp[3];

        return tmp;
    }

    public void SetCurrentUpgradeLevel(int level)
    {
        this.currentUpgradeLevel = level;
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
