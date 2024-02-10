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

    // item1 = ���� ������Ʈ, item2 = ���, item3 = ����, ������ ���� (true = ����)
    private List<Tuple<GameObject, int, bool>> shopItemList = new();
    // ���� ���� ������ ����Ʈ
    private List<GameObject> currentItemList = new();

    // ������ ���ΰ�ħ �� true�� ��ȯ.
    private bool isRenewItem = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
    void RenewItemList()
    {
        List<float> itemProbabilities = new();

        // ������ ����� Ȯ���� ���� ���Ѵ�.
        // ���� ���� ���� ���忡 ���� Ȯ���� �����ȴ�.
        itemProbabilities = SetItemProbability();
        
        for (int i = 0; i < 4; i++)
        {
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
                // �ѹ� �� ������ ����� ���� ���Ⱑ �������� ����
                int weaponRan = UnityEngine.Random.Range(1, 100);
                if (weaponRan <= 35)
                {
                    // ���� ���� �ִ� ���� �߿��� �ϳ��� ��� ���� ����Ʈ�� �ִ´�.
                    List<GameObject> currentWeaponList = WeaponManager.Instance.GetCurrentWeaponList();
                    int random = UnityEngine.Random.Range(0, currentWeaponList.Count - 1);

                    for (int j = 0; j < weaponList.Count; j++)
                    {
                        if (weaponList[j].name == currentWeaponList[random].name)
                        {
                            Tuple<GameObject, int, bool> tmp = new(weaponList[j], rarity, true);
                            shopItemList.Add(tmp);
                            break;
                        }
                    }
                }
                else
                {
                    // ���� ����Ʈ �߿� �ϳ��� ��� ���� ����Ʈ�� �ִ´�.
                    int random = UnityEngine.Random.Range(0, weaponList.Count);
                    Tuple<GameObject, int, bool> tmp = new(weaponList[random], rarity, true);
                    shopItemList.Add(tmp);
                }
            }
            // ���� ���� 35 �ʰ���� ������
            else
            {
                Tuple<GameObject, int, bool> tmp;
                int random = 0;
                switch (rarity)
                {
                    case 0:
                        random = UnityEngine.Random.Range(0, normalItemList.Count);
                        tmp = new(normalItemList[random], 0, false);
                        shopItemList.Add(tmp);
                        break;
                    case 1:
                        random = UnityEngine.Random.Range(0, rareItemList.Count);
                        tmp = new(rareItemList[random], 0, false);
                        shopItemList.Add(tmp);
                        break;
                    case 2:
                        random = UnityEngine.Random.Range(0, epicItemList.Count);
                        tmp = new(epicItemList[random], 0, false);
                        shopItemList.Add(tmp);
                        break;
                    case 3:
                        random = UnityEngine.Random.Range(0, legendItemList.Count);
                        tmp = new(legendItemList[random], 0, false);
                        shopItemList.Add(tmp);
                        break;
                    default:
                        break;
                }
            }
        }
    }
    // ������ ��� ���� �Լ�
    List<float> SetItemProbability()
    {
        List<float> tmp = new List<float>(new float[] { 100, 0, 0, 0 });

        int round = GameRoot.Instance.GetCurrentRound();
        // ���� ��� Ȯ�� (�ִ� 8%)
        tmp[3] = (round - 7) / 4;
        if (tmp[3] <= 0)
            tmp[3] = 0;
        else if (tmp[3] >= 8)
            tmp[3] = 8;
        // ���� ��� Ȯ�� (�ִ� 25%)
        tmp[2] = (1f - (tmp[3] * 0.01f)) * 2 * (round - 3) + tmp[3];
        if (tmp[2] <= 0)
            tmp[2] = 0;
        else if (tmp[2] >= (1f - (tmp[3] * 0.01f)) * 25 + tmp[3])
            tmp[2] = (1f - (tmp[3] * 0.01f)) * 25 + tmp[3];
        // ���� ��� Ȯ�� (�ִ� 60%)
        tmp[1] = (1f - (tmp[3] * 0.01f) - (tmp[2] * 0.01f)) * 6 * round + tmp[2] + tmp[3];
        if (tmp[1] >= (1f - (tmp[3] * 0.01f) - (tmp[2] * 0.01f)) * 60 + tmp[2] + tmp[3])
            tmp[1] = (1f - (tmp[3] * 0.01f) - (tmp[2] * 0.01f)) * 60 + tmp[2] + tmp[3];
        
        return tmp;
    }

    public void SetIsRenewItem(bool isRenewItem)
    {
        this.isRenewItem = isRenewItem;
    }

    public List<Tuple<GameObject, int, bool>> GetShopItemList()
    {
        return this.shopItemList;
    }

    public List<GameObject> GetCurrentItemList()
    {
        return this.currentItemList;
    }

    public bool GetIsRenewItem()
    {
        return this.isRenewItem;
    }
}
