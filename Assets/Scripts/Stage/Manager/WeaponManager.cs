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
    private GameObject weapon;

    public List<GameObject> currentWeaponList = new();
    private List<Vector2> weaponPos = new() { new Vector2(-2.0f, 0f), new Vector2(2.0f, 0f), new Vector2(-2.0f, 2.0f),
                                              new Vector2(2.0f, -2.0f), new Vector2(-2.0f, -2.0f), new Vector2(2.0f, 2.0f)};

    public IEnumerator equipWeapons;
    public IEnumerator destroyWeapons;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        playerBox = GameObject.FindGameObjectWithTag("GameController");
        weapon = Resources.Load<GameObject>("Prefabs/Pistol");
    }

    // Start is called before the first frame update
    void Start()
    {
        currentWeaponList.Add(weapon);
        equipWeapons = EquipWeapons();
    }

    // Update is called once per frame
    void Update()
    {
        // ���� ���� �� ���� �����ϰ� ���� �� ���� �ı�?
        if (equipWeapons != null)
        {
            StartCoroutine(equipWeapons);
        }

        if (destroyWeapons != null)
        {
            StartCoroutine(destroyWeapons);
        }
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

    public List<GameObject> GetCurrentWeaponList()
    {
        return this.currentWeaponList;
    }
}
