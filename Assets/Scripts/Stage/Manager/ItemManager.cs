using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // Singleton
    private static ItemManager instance = null;

    public static ItemManager Instance
    {
        get
        {
            if (null == instance)
                return null;

            return instance;
        }
    }
    public List<GameObject> normalItemList;
    public List<GameObject> rareItemList;
    public List<GameObject> epicItemList;
    public List<GameObject> legendItemList;

    public List<GameObject> weaponList;

    private List<int> ownNormalItemList = new();
    private List<int> ownRareItemList = new();
    private List<int> ownEpicItemList = new();
    private List<int> ownLegendItemList = new();

    // item1 = ���� ������Ʈ
    private List<GameObject> shopItemList = new();
    // ���� ����Ʈ�� ���Ⱑ ���� ��, �ش� ������ ������ �����ϴ� ����Ʈ
    private List<WeaponInfo> shopWeaponInfoList = new();
    // ������ ��� ���� ����Ʈ
    private List<bool> isLockItemList = new();
    // ���� ���� ������ ����Ʈ
    private List<GameObject> currentItemList = new();
    // ���� ���� ���� ����Ʈ
    List<GameObject> currentWeaponList;

    // ������ ���ΰ�ħ �� true�� ��ȯ.
    public bool isRenewItem = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        for (int i = 0; i < 4; i++)
        {
            shopItemList.Add(null);
            shopWeaponInfoList.Add(null);
            // ������ ��� ����Ʈ�� false�� ä�� �ִ´�.
            isLockItemList.Add(false);
        }

        for (int i = 0; i < 50; i++)
        {
            ownNormalItemList.Add(0);
        }
    }

    void Start()
    {
        ownNormalItemList[43] = 1;
    }

    void Update()
    {
        // ������ ����Ʈ ������ �ȵƴٸ�
        if (!isRenewItem)
        {
            // ���� ���� ������ ����Ʈ�� �����Ѵ�.
            RenewItemList();
            isRenewItem = true;
        }
    }

    // ������ ����Ʈ ���� �Լ�
    public void RenewItemList()
    {
        List<float> itemProbabilities = new();

        // ������ ����� Ȯ���� ���� ���Ѵ�.
        // ���� ���� ���� ���忡 ���� Ȯ���� �����ȴ�.
        itemProbabilities = SetItemProbability();
        
        for (int i = 0; i < 4; i++)
        {
            // �ش� ������ ĭ�� ���ٸ� �ǳʶڴ�
            if (isLockItemList[i])
                continue;

            // ������ ����� ������ ����� �����Ѵ�.
            float itemRandom = UnityEngine.Random.Range(0.0f, 100.0f);
            int rarity = -1;
            // ������ ��� ����Ʈ�� �ں��� Ž���Ѵ�.
            for (int j = 3; j >= 0; j--)
            {
                // ��� ����Ʈ�� ���ں��� ���ٸ� ��� ����
                if (itemRandom < itemProbabilities[j])
                {
                    rarity = j;
                    break;
                }
            }

            // ����, �������� Ȯ���� ���� ���Ѵ�.
            int ran = UnityEngine.Random.Range(1, 100);
            
            // ���� ���� 35���϶�� ����
            if (ran <= 35)
            {
                currentWeaponList = WeaponManager.Instance.GetCurrentWeaponList();
                // �ѹ� �� ������ ����� ���� ���Ⱑ �������� ����
                int weaponRan = UnityEngine.Random.Range(1, 100);
                // ���� ���� 35������ �� ���� ���� ���� ����
                if (weaponRan <= 35)
                {
                    // ���� ���� �ִ� ���� �߿��� �ϳ��� ��� ���� ����Ʈ�� �ִ´�.
                    int random = UnityEngine.Random.Range(0, currentWeaponList.Count - 1);

                    for (int j = 0; j < weaponList.Count; j++)
                    {
                        if (weaponList[j].name == currentWeaponList[random].name)
                        {
                            GameObject tmp = weaponList[j];
                            WeaponInfo tmpInfo = new WeaponInfo(weaponList[j].name);
                            tmpInfo.SetWeaponStatus(weaponList[j].name, rarity);
                            shopItemList[i] = tmp;                            
                            shopWeaponInfoList[i] = tmpInfo;

                            break;
                        }
                    }
                }
                // ���� ���� 36 �̻��� ��
                else
                {
                    // ���� ����Ʈ �߿� �ϳ��� ��� ���� ����Ʈ�� �ִ´�.
                    int random = UnityEngine.Random.Range(0, weaponList.Count);
                    GameObject tmp = weaponList[random];
                    WeaponInfo tmpInfo = new WeaponInfo(weaponList[random].name);
                    tmpInfo.SetWeaponStatus(weaponList[random].name, rarity);
                    shopItemList[i] = tmp;
                    shopWeaponInfoList[i] = tmpInfo;
                }
            }
            // ���� ���� 35 �ʰ���� ������
            else
            {
                GameObject tmp;
                int random = 0;
                switch (rarity)
                {
                    case 0:
                        random = CheckIsOwnLimit(rarity, UnityEngine.Random.Range(0, normalItemList.Count));
                        tmp = normalItemList[random];
                        shopItemList[i] = tmp;
                        break;
                    case 1:
                        random = UnityEngine.Random.Range(0, rareItemList.Count);
                        tmp = rareItemList[random];
                        shopItemList[i] = tmp;
                        break;
                    case 2:
                        random = UnityEngine.Random.Range(0, epicItemList.Count);
                        tmp = epicItemList[random];
                        shopItemList[i] = tmp;
                        break;
                    case 3:
                        random = UnityEngine.Random.Range(0, legendItemList.Count);
                        tmp = legendItemList[random];
                        shopItemList[i] = tmp;
                        break;
                    default:
                        break;
                }
            }
        }
    }

    // ���� ������ �ʰ��ϴ� �������� ���� �ʵ��� �����ϴ� �Լ�
    int CheckIsOwnLimit(int rarity, int itemNumber)
    {
        bool isLimit = false;
        int tmp = itemNumber;

        while (true)
        {
            switch (rarity)
            {
                case 0:
                    // ������ ����Ʈ�� ���� ������ ����Ʈ�� ��ȣ�� �ٸ��� ������ ����
                    if (tmp == 35 && ownNormalItemList[36] == 5)
                        isLimit = true;
                    if (tmp == 36 && ownNormalItemList[37] == 3)
                        isLimit = true;
                    if (tmp == 37 && ownNormalItemList[38] == 13)
                        isLimit = true;
                    if (tmp == 38 && ownNormalItemList[39] == 5)
                        isLimit = true;
                    if (tmp == 39 && ownNormalItemList[40] == 10)
                        isLimit = true;
                    if (tmp == 41 && ownNormalItemList[42] == 4)
                        isLimit = true;
                    if (tmp == 42 && ownNormalItemList[43] == 1)
                        isLimit = true;
                    if (tmp == 43 && ownNormalItemList[44] == 1)
                        isLimit = true;

                    // ���� �������� ���� �����̶�� �ٸ� ���������� �����Ѵ�
                    if (isLimit)
                    {
                        tmp = UnityEngine.Random.Range(0, normalItemList.Count);

                        isLimit = false;
                        continue;
                    }
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    break;
            }

            // ���� ���ѿ� �ɸ��� �������� �ƴ϶�� Ż��
            if (!isLimit)
                break;
        }

        return tmp;
    }

    // ������ ��� ���� �Լ�
    List<float> SetItemProbability()
    {
        List<float> tmp = new List<float>(new float[] { 100, 0, 0, 0 });

        int round = GameRoot.Instance.GetCurrentRound();
        // ���� ��� Ȯ�� (�ִ� 8%)
        tmp[3] = (round - 7) / 4 * ((1 + PlayerInfo.Instance.GetLuck()) / 100);
        if (tmp[3] <= 0)
            tmp[3] = 0;
        else if (tmp[3] >= 8)
            tmp[3] = 8;
        // ���� ��� Ȯ�� (�ִ� 25%)
        tmp[2] = (1f - (tmp[3] * 0.01f)) * 2 * (round - 3) * ((1 + PlayerInfo.Instance.GetLuck()) / 100) + tmp[3];
        if (tmp[2] <= 0)
            tmp[2] = 0;
        else if (tmp[2] >= (1f - (tmp[3] * 0.01f)) * 25 + tmp[3])
            tmp[2] = (1f - (tmp[3] * 0.01f)) * 25 + tmp[3];
        // ���� ��� Ȯ�� (�ִ� 60%)
        tmp[1] = (1f - (tmp[3] * 0.01f) - (tmp[2] * 0.01f)) * 6 * round * ((1 + PlayerInfo.Instance.GetLuck()) / 100) + tmp[2] + tmp[3];
        if (tmp[1] >= (1f - (tmp[3] * 0.01f) - (tmp[2] * 0.01f)) * 60 + tmp[2] + tmp[3])
            tmp[1] = (1f - (tmp[3] * 0.01f) - (tmp[2] * 0.01f)) * 60 + tmp[2] + tmp[3];

        return tmp;
    }

    public void SetShopItemList(List<GameObject> shopItemList)
    {
        this.shopItemList = shopItemList;
    }

    public void SetShopWeaponInfoList(List<WeaponInfo> weaponInfoList)
    {
        this.shopWeaponInfoList = weaponInfoList;
    }

    public void SetIsRenewItem(bool isRenewItem)
    {
        this.isRenewItem = isRenewItem;
    }

    public void SetIsLockItemList(List<bool> list)
    {
        this.isLockItemList = list;
    }

    public void SetOwnNormalItemList(List<int> list)
    {
        this.ownNormalItemList = list;
    }

    public void SetOwnRareItemList(List<int> list)
    {
        this.ownRareItemList = list;
    }

    public void SetOwnEpicItemList(List<int> list)
    {
        this.ownEpicItemList = list;
    }

    public void SetOwnLegendItemList(List<int> list)
    {
        this.ownLegendItemList = list;
    }

    public List<bool> GetIsLockItemList()
    {
        return this.isLockItemList;
    }

    public List<GameObject> GetShopItemList()
    {
        return this.shopItemList;
    }

    public List<WeaponInfo> GetShopWeaponInfoList()
    {
        return this.shopWeaponInfoList;
    }

    public List<GameObject> GetCurrentItemList()
    {
        return this.currentItemList;
    }

    public List<int> GetOwnNormalItemList()
    {
        return this.ownNormalItemList;
    }

    public List<int> GetOwnRareItemList()
    {
        return this.ownRareItemList;
    }

    public List<int> GetOwnEpicItemList()
    {
        return this.ownEpicItemList;
    }

    public List<int> GetOwnLegendItemList()
    {
        return this.ownLegendItemList;
    }

    public bool GetIsRenewItem()
    {
        return this.isRenewItem;
    }
}
