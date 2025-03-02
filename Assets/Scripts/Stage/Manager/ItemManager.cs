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

    // item1 = 게임 오브젝트
    private List<GameObject> shopItemList = new();
    // 상점 리스트에 무기가 있을 때, 해당 무기의 정보를 저장하는 리스트
    private List<WeaponInfo> shopWeaponInfoList = new();
    // 아이템 잠금 여부 리스트
    private List<bool> isLockItemList = new();
    // 현재 보유 아이템 리스트
    private List<GameObject> currentItemList = new();
    // 현재 보유 무기 리스트
    List<GameObject> currentWeaponList;

    // 현재 보유 아이템 리스트
    public List<(GameObject, int)> ownItemList = new();

    // 아이템 구매 횟수
    // 새로고침 시 0으로 초기화된다
    public int itemPurchaseCount = 0;

    // 아이템 새로고침 시 true로 전환.
    public bool isRenewItem = true;

    private int rerollCount = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        ownItemList.Clear();

        // 리스트 초기화
        //ownNormalItemList.Clear();
        //ownRareItemList.Clear();
        //ownEpicItemList.Clear();
        //ownLegendItemList.Clear();

        //shopItemList.Clear();
        //shopWeaponInfoList.Clear();
        //currentItemList.Clear();
        //currentWeaponList.Clear();

        for (int i = 0; i < 4; i++)
        {
            shopItemList.Add(null);
            shopWeaponInfoList.Add(null);
            // 아이템 잠금 리스트에 false를 채워 넣는다.
            isLockItemList.Add(false);
        }

        for (int i = 0; i < 50; i++)
        {
            ownNormalItemList.Add(0);
            ownRareItemList.Add(0);
            ownEpicItemList.Add(0);
            ownLegendItemList.Add(0);
        }

        isRenewItem = true;
    }

    void Start()
    {
        //ownEpicItemList[23] = 1;
    }

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
    public void RenewItemList()
    {
        List<float> itemProbabilities = new();

        // 아이템 등급을 확률에 따라 정한다.
        // 현재 진행 중인 라운드에 따라 확률은 변동된다.
        itemProbabilities = SetItemProbability();
        
        for (int i = 0; i < 4; i++)
        {
            // 해당 아이템 칸이 잠겼다면 건너뛴다
            if (isLockItemList[i])
                continue;

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
            int ran = UnityEngine.Random.Range(0, 100);
            
            // 난수 값이 35미만라면 무기
            if (ran < 35)
            {
                currentWeaponList = WeaponManager.Instance.GetCurrentWeaponList();
                // 결정된 레어도에 따라서 나오는 무기 개수가 달라진다
                // 0 ~ 11 = 일반 12 ~ 특수
                int weaponListCount = 12;
                if (rarity >= 2)
                {
                    weaponListCount = weaponList.Count;
                }

                // 한번 더 난수를 사용해 같은 무기가 나오도록 보정
                int weaponRan = UnityEngine.Random.Range(0, 100);
                // 난수 값이 35미만일 때 같은 무기 보정 적용
                if (weaponRan < 35)
                {
                    if (currentWeaponList.Count > 0)
                    {
                        // 현재 갖고 있는 무기 중에서 하나를 골라 상점 리스트에 넣는다.
                        int random = UnityEngine.Random.Range(0, currentWeaponList.Count);

                        // 결정된 레어도가 2보다 작은 상황에서 보유 무기 중 특수무기가 선택되면
                        if ((currentWeaponList[random].name == "매그넘" || currentWeaponList[random].name == "철쇄아")
                            && rarity < 2)
                        {
                            // 처음부터 다시
                            i--;
                            continue;
                        }

                        for (int j = 0; j < weaponList.Count; j++)
                        {
                            if (weaponList[j].name == currentWeaponList[random].name)
                            {
                                // 무기와 아이템을 설정
                                GameObject tmp = weaponList[j];
                                WeaponInfo tmpInfo = new WeaponInfo(weaponList[j].name);
                                tmpInfo.SetWeaponStatus(weaponList[j].name, rarity);
                                shopItemList[i] = tmp;
                                shopWeaponInfoList[i] = tmpInfo;

                                break;
                            }
                        }
                    }
                    else
                    {
                        // 무기 리스트 중에 하나를 골라 상점 리스트에 넣는다.
                        int random = UnityEngine.Random.Range(0, weaponListCount);
                        GameObject tmp = weaponList[random];
                        WeaponInfo tmpInfo = new WeaponInfo(weaponList[random].name);
                        tmpInfo.SetWeaponStatus(weaponList[random].name, rarity);
                        shopItemList[i] = tmp;
                        shopWeaponInfoList[i] = tmpInfo;
                    }
                }
                // 난수 값이 35 이상일 때
                else
                {
                    // 무기 리스트 중에 하나를 골라 상점 리스트에 넣는다.
                    int random = UnityEngine.Random.Range(0, weaponListCount);
                    GameObject tmp = weaponList[random];
                    WeaponInfo tmpInfo = new WeaponInfo(weaponList[random].name);
                    tmpInfo.SetWeaponStatus(weaponList[random].name, rarity);
                    shopItemList[i] = tmp;
                    shopWeaponInfoList[i] = tmpInfo;
                }
            }
            // 난수 값이 35 이상이라면 아이템
            if (ran >= 35)
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
                        random = CheckIsOwnLimit(rarity, UnityEngine.Random.Range(0, rareItemList.Count));
                        tmp = rareItemList[random];
                        shopItemList[i] = tmp;
                        break;
                    case 2:
                        random = CheckIsOwnLimit(rarity, UnityEngine.Random.Range(0, epicItemList.Count));
                        tmp = epicItemList[random];
                        shopItemList[i] = tmp;
                        break;
                    case 3:
                        random = CheckIsOwnLimit(rarity, UnityEngine.Random.Range(0, legendItemList.Count));
                        tmp = legendItemList[random];
                        shopItemList[i] = tmp;
                        break;
                    default:
                        break;
                }
            }

            Debug.Log("새로고침 횟수 : " + rerollCount + "/ 레어도 : " + rarity + "/ 아이템 이름 : " + shopItemList[i].name);
        }

        rerollCount++;
    }

    // 보유 제한을 초과하는 아이템은 뽑지 않도록 조정하는 함수
    int CheckIsOwnLimit(int rarity, int itemNumber)
    {
        bool isLimit = false;
        int tmp = itemNumber;

        while (true)
        {
            switch (rarity)
            {
                case 0:
                    // 아이템 리스트와 보유 아이템 리스트의 번호가 다르기 때문에 주의
                    if (tmp == 7 && ownNormalItemList[8] >= 0)  // 빈 공간
                        isLimit = true;
                    if (tmp == 35 && ownNormalItemList[36] >= 5)
                        isLimit = true;
                    if (tmp == 36 && ownNormalItemList[37] >= 3)
                        isLimit = true;
                    if (tmp == 37 && ownNormalItemList[38] >= 13)
                        isLimit = true;
                    if (tmp == 38 && ownNormalItemList[39] >= 5)
                        isLimit = true;
                    if (tmp == 39 && ownNormalItemList[40] >= 10)
                        isLimit = true;
                    if (tmp == 41 && ownNormalItemList[42] >= 4)
                        isLimit = true;
                    if (tmp == 42 && ownNormalItemList[43] >= 1)
                        isLimit = true;
                    if (tmp == 43 && ownNormalItemList[44] >= 1)
                        isLimit = true;

                    // 현재 아이템이 보유 제한이라면 다른 아이템으로 변경한다
                    if (isLimit)
                    {
                        tmp = UnityEngine.Random.Range(0, normalItemList.Count);

                        isLimit = false;
                        continue;
                    }
                    break;
                case 1:
                    // 미구현
                    if (tmp == 24 && ownRareItemList[25] == 0)
                        isLimit = true;
                    // 미구현 22
                    if (tmp == 25 && ownRareItemList[26] == 0)
                        isLimit = true;
                    if (tmp == 27 && ownRareItemList[28] >= 3)
                        isLimit = true;
                    if (tmp == 28 && ownRareItemList[29] >= 20)
                        isLimit = true;
                    if (tmp == 29 && ownRareItemList[30] >= 1)
                        isLimit = true;
                    if (tmp == 31 && ownRareItemList[32] >= 1)
                        isLimit = true;
                    if (tmp == 32 && ownRareItemList[33] >= 1)
                        isLimit = true;
                    if (tmp == 33 && ownRareItemList[34] >= 1)
                        isLimit = true;
                    if (tmp == 34 && ownRareItemList[35] >= 4)
                        isLimit = true;
                    if (tmp == 35 && ownRareItemList[36] >= 5)
                        isLimit = true;
                    if (tmp == 36 && ownRareItemList[37] >= 1)
                        isLimit = true;

                    if (isLimit)
                    {
                        tmp = UnityEngine.Random.Range(0, rareItemList.Count);

                        isLimit = false;
                        continue;
                    }
                    break;
                case 2:
                    if (tmp == 15 && ownEpicItemList[16] >= 1)
                        isLimit = true;
                    // 임시 미구현
                    if (tmp == 20 && ownEpicItemList[21] == 0)
                        isLimit = true;
                    if (tmp == 22 && ownEpicItemList[23] >= 1)
                        isLimit = true;
                    if (tmp == 23 && ownEpicItemList[24] >= 1)
                        isLimit = true;
                    if (tmp == 25 && ownEpicItemList[26] >= 1)
                        isLimit = true;
                    if (tmp == 26 && ownEpicItemList[27] >= 3)
                        isLimit = true;
                    if (tmp == 27 && ownEpicItemList[28] >= 5)
                        isLimit = true;
                    if (tmp == 28 && ownEpicItemList[29] >= 1)
                        isLimit = true;
                    if (tmp == 29 && ownEpicItemList[30] >= 1)
                        isLimit = true;
                    if (tmp == 30 && ownEpicItemList[31] >= 1)
                        isLimit = true;
                    if (tmp == 35 && ownEpicItemList[36] >= 1)
                        isLimit = true;

                    if (isLimit)
                    {
                        tmp = UnityEngine.Random.Range(0, epicItemList.Count);

                        isLimit = false;
                        continue;
                    }
                    break;
                case 3:
                    // 이미지 X
                    if (tmp == 13 && ownLegendItemList[14] == 0)
                        isLimit = true;
                    if (tmp == 14 && ownLegendItemList[15] >= 1)
                        isLimit = true;
                    if (tmp == 15 && ownLegendItemList[16] >= 1)
                        isLimit = true;
                    if (tmp == 16 && ownLegendItemList[17] >= 1)
                        isLimit = true;
                    if (tmp == 17 && ownLegendItemList[18] >= 1)
                        isLimit = true;
                    if (tmp == 19 && ownLegendItemList[20] >= 1)
                        isLimit = true;
                    if (tmp == 21 && ownLegendItemList[22] >= 1)
                        isLimit = true;
                    // 이미지 X
                    if (tmp == 22 && ownLegendItemList[23] == 0)
                        isLimit = true;
                    if (tmp == 24 && ownLegendItemList[25] >= 1)
                        isLimit = true;
                    // 이미지 X
                    if (tmp == 25 && ownLegendItemList[26] == 0)
                        isLimit = true;

                    if (isLimit)
                    {
                        tmp = UnityEngine.Random.Range(0, legendItemList.Count);

                        isLimit = false;
                        continue;
                    }
                    break;
                default:
                    break;
            }

            // 보유 제한에 걸리는 아이템이 아니라면 탈출
            if (!isLimit)
                break;
        }

        return tmp;
    }

    // 아이템 레어도 결정 함수
    List<float> SetItemProbability()
    {
        List<float> tmp = new List<float>(new float[] { 100, 0, 0, 0 });
        float luck = PlayerInfo.Instance.GetLuck();
        if (luck <= 0)
            luck = 0;

        int round = GameRoot.Instance.GetCurrentRound();
        // 전설 등급 확률 (최대 8%)
        tmp[3] = (round - 7) * (1 + luck / 100) / 4;
        if (tmp[3] <= 0)
            tmp[3] = 0;
        else if (tmp[3] >= 8)
            tmp[3] = 8;
        // 에픽 등급 확률 (최대 25%)
        tmp[2] = (1f - (tmp[3] * 0.01f)) * 2 * (round - 3) * (1 + luck / 100) + tmp[3];
        if (tmp[2] <= 0)
            tmp[2] = 0;
        else if (tmp[2] >= (1f - (tmp[3] * 0.01f)) * 25 + tmp[3])
            tmp[2] = (1f - (tmp[3] * 0.01f)) * 25 + tmp[3];
        // 레어 등급 확률 (최대 60%)
        tmp[1] = (1f - (tmp[3] * 0.01f) - (tmp[2] * 0.01f)) * 6 * round * (1 + luck / 100) + tmp[2] + tmp[3];
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
