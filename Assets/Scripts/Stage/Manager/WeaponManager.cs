using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ������ ����ϴ� ��ũ��Ʈ
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
        // ���� ���� ����
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
        // ���� ���� �� ���� �����ϰ� ���� �� ���� �ı�
        if (equipWeapons != null)
        {
            StartCoroutine(equipWeapons);
        }

        if (destroyWeapons != null)
        {
            StartCoroutine(destroyWeapons);
        }
    }

    // ���� ���⸦ �ҷ��´�
    private GameObject LoadStartWeapon(string weaponName)
    {
        string path = "Prefabs/Weapons/" + weaponName;
        GameObject startWeapon = Resources.Load<GameObject>(path);

        return startWeapon;
    }

    // ���� ���� �Լ�
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

    // ���� �ı� �Լ�
    public IEnumerator DestroyWeapons()
    {
        int repeat = currentWeaponList.Count;
        for (int i = 0; i < repeat; i++)
        {
            Destroy(playerBox.transform.GetChild(i + 1).gameObject);
        }

        yield return null;
    }

    // ������ ù ���� �� Ȱ��ȭ
    // ���� ���� �� �Ǹ� �� ��Ȱ��ȭ �� Ȱ��ȭ�ϴ� ������
    public void ActivateLegendItem19()
    {
        // ���� �ٸ� ���Ⱑ ���� �� ���� ���ݼӵ� -3% * ������ ����
        int itemCount = ItemManager.Instance.GetOwnLegendItemList()[19];
        if (itemCount > 0)
        {
            // ���� �ٸ� ���Ⱑ �ִ� ����� ���� �ּ� 0 ~ �ִ� 5 
            int differentWeaponCount = FindSameWeapon();
            PlayerInfo.Instance.SetATKSpeed(PlayerInfo.Instance.GetATKSpeed() - 3f * differentWeaponCount * itemCount);
        }
    }

    public void ActivateLegendItem28()
    {
        // ���� �ٸ� ���Ⱑ ���� �� ���� ���ݼӵ� +6% * ������ ����
        int itemCount = ItemManager.Instance.GetOwnLegendItemList()[28];
        if (itemCount > 0)
        {
            // ���� �ٸ� ���Ⱑ �ִ� ����� ���� �ּ� 0 ~ �ִ� 5 
            int differentWeaponCount = FindSameWeapon();
            PlayerInfo.Instance.SetATKSpeed(PlayerInfo.Instance.GetATKSpeed() + 6f * differentWeaponCount * itemCount);
        }
    }

    // LegendItem19 ������ ȿ���� ��Ȱ��ȭ
    // ���ҵ� ���ݼӵ��� �ٽ� ������ ���ݼӵ��� �������´�
    public void InActivateLegendItem19()
    {
        // ���� �ٸ� ���Ⱑ ���� �� ���� ���ݼӵ� -3% * ������ ����
        int itemCount = ItemManager.Instance.GetOwnLegendItemList()[19];
        if (itemCount > 0)
        {
            // ���� �ٸ� ���Ⱑ �ִ� ����� ���� �ּ� 0 ~ �ִ� 5 
            int differentWeaponCount = FindSameWeapon();
            PlayerInfo.Instance.SetATKSpeed(PlayerInfo.Instance.GetATKSpeed() + 3f * differentWeaponCount * itemCount);
        }
        
    }

    // LegendItem28 ������ ȿ���� ��Ȱ��ȭ
    // ������ ���ݼӵ��� �ٽ� ������ ���ݼӵ��� �������´�
    public void InActivateLegendItem28()
    {
        // ���� �ٸ� ���Ⱑ ���� �� ���� ���ݼӵ� +6% * ������ ����
        int itemCount = ItemManager.Instance.GetOwnLegendItemList()[28];
        if (itemCount > 0)
        {
            // ���� ����Ʈ�� Ž���ؼ� ���� �̸��� �˻��Ѵ�
            // ���� �ٸ� ���Ⱑ �ִ� ����� ���� �ּ� 0 ~ �ִ� 5 
            int differentWeaponCount = FindSameWeapon();
            PlayerInfo.Instance.SetATKSpeed(PlayerInfo.Instance.GetATKSpeed() - 6f * differentWeaponCount * itemCount);
        }
    }

    private int FindSameWeapon()
    {
        List<string> weaponNames = new();
        int currentWeaponCount = currentWeaponInfoList.Count;
        // ���� ����Ʈ�� Ž���ؼ� ���� �̸��� �˻��Ѵ�
        for (int i = 0; i < currentWeaponCount; i++)
        {
            bool isHaveSameWeapon = false;
            string weaponName = currentWeaponInfoList[i].weaponName;

            // ���� �̸� ����Ʈ�� ��������� �ٷ� �ִ´�
            if (weaponNames.Count == 0)
            {
                weaponNames.Add(weaponName);
                continue;
            }

            // ���� �̸� ����Ʈ�� Ž���� ���� ���� �̸��� ���� �� �ִ��� �˻��Ѵ�
            for (int j = 0; j < weaponNames.Count; j++)
            {
                if (weaponName == weaponNames[j])
                {
                    isHaveSameWeapon = true;
                    break;
                }
            }

            // ���� ���� �̸��� ���ٸ� �ٸ� �����̹Ƿ� �߰�
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
