using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 무기 장착을 담당하는 스크립트
public class WeaponManager : MonoBehaviour
{
    // Singleton
    private static WeaponManager instance = null;

    public static WeaponManager Instance
    {
        get
        {
            if (null == instance)
                return null;

            return instance;
        }
    }

    private GameObject playerBox;

    public List<GameObject> currentWeaponList = new();
    public List<WeaponInfo> currentWeaponInfoList = new();
    private List<Vector2> weaponPos = new() { new Vector2(-1.2f, -0.5f), new Vector2(1.2f, -0.5f), new Vector2(-1.2f, -0.0f),
                                              new Vector2(1f, -1.1f), new Vector2(1.2f, -0.0f), new Vector2(-1f, -1.1f)};

    public IEnumerator equipWeapons;
    public IEnumerator destroyWeapons;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        playerBox = GameObject.FindGameObjectWithTag("GameController");

        GameObject startWeapon = LoadStartWeapon(RoundSetting.Instance.GetStartWeapon());
        // 시작 무기 착용
        currentWeaponList.Add(startWeapon);
        currentWeaponInfoList.Add(new WeaponInfo(startWeapon.name, 0));
        currentWeaponList[0].GetComponent<StoredWeaponNumber>().SetWeaponNumber(currentWeaponInfoList[0].GetWeaponNumber());
    }

    // Start is called before the first frame update
    void Start()
    {
        equipWeapons = EquipWeapons();
    }

    // Update is called once per frame
    void Update()
    {
        // 라운드 시작 시 무기 장착하고 끝날 때 무기 파괴
        if (equipWeapons != null)
        {
            StartCoroutine(equipWeapons);
        }

        if (destroyWeapons != null)
        {
            StartCoroutine(destroyWeapons);
        }
    }

    // 시작 무기를 불러온다
    private GameObject LoadStartWeapon(string weaponName)
    {
        string path = "Prefabs/Weapons/" + weaponName;
        GameObject startWeapon = Resources.Load<GameObject>(path);

        return startWeapon;
    }

    // 무기 장착 함수
    public IEnumerator EquipWeapons()
    {
        int repeat = currentWeaponList.Count;
        int reversal = 0;

        for (int i = 0; i < repeat; i++)
        {
            reversal = 1;
            if (i % 2 == 1)
                reversal = 0;
            currentWeaponList[i].GetComponent<StoredWeaponNumber>().SetWeaponNumber(currentWeaponInfoList[i].GetWeaponNumber());
            GameObject copy = Instantiate(currentWeaponList[i], weaponPos[i], Quaternion.Euler(0f, -180f * reversal, 0f)) as GameObject;

            copy.transform.SetParent(playerBox.transform, false);
            yield return null;
        }

        yield return null;
    }

    // 무기 파괴 함수
    public IEnumerator DestroyWeapons()
    {
        int repeat = currentWeaponList.Count;
        for (int i = 0; i < repeat; i++)
        {
            Destroy(playerBox.transform.GetChild(i + 1).gameObject);
        }

        yield return null;
    }

    // 아이템 첫 구매 때 활성화
    // 무기 구매 및 판매 때 비활성화 후 활성화하는 식으로
    public void ActivateLegendItem19()
    {
        // 각기 다른 무기가 있을 때 마다 공격속도 -3% * 아이템 개수
        int itemCount = ItemManager.Instance.GetOwnLegendItemList()[19];
        if (itemCount > 0)
        {
            // 서로 다른 무기가 있는 경우의 수는 최소 0 ~ 최대 5 
            int differentWeaponCount = FindSameWeapon();
            PlayerInfo.Instance.SetATKSpeed(PlayerInfo.Instance.GetATKSpeed() - 3f * differentWeaponCount * itemCount);
        }
    }

    public void ActivateLegendItem28()
    {
        // 각기 다른 무기가 있을 때 마다 공격속도 +6% * 아이템 개수
        int itemCount = ItemManager.Instance.GetOwnLegendItemList()[28];
        if (itemCount > 0)
        {
            // 서로 다른 무기가 있는 경우의 수는 최소 0 ~ 최대 5 
            int differentWeaponCount = FindSameWeapon();
            PlayerInfo.Instance.SetATKSpeed(PlayerInfo.Instance.GetATKSpeed() + 6f * differentWeaponCount * itemCount);
        }
    }

    // LegendItem19 아이템 효과를 비활성화
    // 감소된 공격속도를 다시 원래의 공격속도로 돌려놓는다
    public void InActivateLegendItem19()
    {
        // 각기 다른 무기가 있을 때 마다 공격속도 -3% * 아이템 개수
        int itemCount = ItemManager.Instance.GetOwnLegendItemList()[19];
        if (itemCount > 0)
        {
            // 서로 다른 무기가 있는 경우의 수는 최소 0 ~ 최대 5 
            int differentWeaponCount = FindSameWeapon();
            PlayerInfo.Instance.SetATKSpeed(PlayerInfo.Instance.GetATKSpeed() + 3f * differentWeaponCount * itemCount);
        }
        
    }

    // LegendItem28 아이템 효과를 비활성화
    // 증가한 공격속도를 다시 원래의 공격속도로 돌려놓는다
    public void InActivateLegendItem28()
    {
        // 각기 다른 무기가 있을 때 마다 공격속도 +6% * 아이템 개수
        int itemCount = ItemManager.Instance.GetOwnLegendItemList()[28];
        if (itemCount > 0)
        {
            // 무기 리스트를 탐색해서 무기 이름을 검사한다
            // 서로 다른 무기가 있는 경우의 수는 최소 0 ~ 최대 5 
            int differentWeaponCount = FindSameWeapon();
            PlayerInfo.Instance.SetATKSpeed(PlayerInfo.Instance.GetATKSpeed() - 6f * differentWeaponCount * itemCount);
        }
    }

    private int FindSameWeapon()
    {
        List<string> weaponNames = new();
        int currentWeaponCount = currentWeaponInfoList.Count;
        // 무기 리스트를 탐색해서 무기 이름을 검사한다
        for (int i = 0; i < currentWeaponCount; i++)
        {
            bool isHaveSameWeapon = false;
            string weaponName = currentWeaponInfoList[i].weaponName;

            // 무기 이름 리스트가 비어있으면 바로 넣는다
            if (weaponNames.Count == 0)
            {
                weaponNames.Add(weaponName);
                continue;
            }

            // 무기 이름 리스트를 탐색해 현재 무기 이름과 같은 게 있는지 검사한다
            for (int j = 0; j < weaponNames.Count; j++)
            {
                if (weaponName == weaponNames[j])
                {
                    isHaveSameWeapon = true;
                    break;
                }
            }

            // 같은 무기 이름이 없다면 다른 무기이므로 추가
            if (!isHaveSameWeapon)
                weaponNames.Add(weaponName);
        }

        int tmp = weaponNames.Count;
        if (tmp == 0)
            tmp = 1;

        return tmp;
    }

    public List<GameObject> GetCurrentWeaponList()
    {
        return this.currentWeaponList;
    }

    public List<WeaponInfo> GetCurrentWeaponInfoList()
    {
        return this.currentWeaponInfoList;
    }
}
