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

    // item1 = 게임 오브젝트, item2 = 등급, item3 = 무기, 아이템 여부 (true = 무기)
    private List<Tuple<GameObject, int, bool>> shopItemList = new();
    // 현재 보유 아이템 리스트
    private List<GameObject> currentItemList = new();

    // 아이템 새로고침 시 true로 전환.
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
        // 아이템 리스트 갱신이 안됐다면
        if (!isRenewItem)
        {
            // 현재 상점 아이템 리스트를 갱신한다.
            RenewItemList();
            isRenewItem = true;
        }
    }

    // 아이템 리스트 갱신 함수
    void RenewItemList()
    {
        List<float> itemProbabilities = new();

        // 아이템 등급을 확률에 따라 정한다.
        // 현재 진행 중인 라운드에 따라 확률은 변동된다.
        itemProbabilities = SetItemProbability();
        
        for (int i = 0; i < 4; i++)
        {
            // 난수를 사용해 아이템 레어도를 결정한다.
            float itemRandom = UnityEngine.Random.Range(0.0f, 100.0f);
            int rarity = -1;
            // 아이템 레어도 리스트의 뒤부터 탐색한다.
            for (int j = 3; j >= 0; j--)
            {
                // 레어도 리스트의 숫자보다 낮다면 레어도 결정
                if (itemRandom < itemProbabilities[j])
                {
                    rarity = j;
                    break;
                }
            }

            // 무기, 아이템을 확률에 따라 정한다.
            int ran = UnityEngine.Random.Range(1, 100);
            
            // 난수 값이 35이하라면 무기
            if (ran <= 35)
            {
                // 한번 더 난수를 사용해 같은 무기가 나오도록 보정
                int weaponRan = UnityEngine.Random.Range(1, 100);
                if (weaponRan <= 35)
                {
                    // 현재 갖고 있는 무기 중에서 하나를 골라 상점 리스트에 넣는다.
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
                    // 무기 리스트 중에 하나를 골라 상점 리스트에 넣는다.
                    int random = UnityEngine.Random.Range(0, weaponList.Count);
                    Tuple<GameObject, int, bool> tmp = new(weaponList[random], rarity, true);
                    shopItemList.Add(tmp);
                }
            }
            // 난수 값이 35 초과라면 아이템
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
    // 아이템 레어도 결정 함수
    List<float> SetItemProbability()
    {
        List<float> tmp = new List<float>(new float[] { 100, 0, 0, 0 });

        int round = GameRoot.Instance.GetCurrentRound();
        // 전설 등급 확률 (최대 8%)
        tmp[3] = (round - 7) / 4;
        if (tmp[3] <= 0)
            tmp[3] = 0;
        else if (tmp[3] >= 8)
            tmp[3] = 8;
        // 에픽 등급 확률 (최대 25%)
        tmp[2] = (1f - (tmp[3] * 0.01f)) * 2 * (round - 3) + tmp[3];
        if (tmp[2] <= 0)
            tmp[2] = 0;
        else if (tmp[2] >= (1f - (tmp[3] * 0.01f)) * 25 + tmp[3])
            tmp[2] = (1f - (tmp[3] * 0.01f)) * 25 + tmp[3];
        // 레어 등급 확률 (최대 60%)
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
