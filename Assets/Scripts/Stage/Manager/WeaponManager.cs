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
    private GameObject weapon;

    public List<GameObject> currentWeaponList = new();
    private List<Vector2> weaponPos = new() { new Vector2(-2.0f, 0f), new Vector2(2.0f, 0f), };

    private IEnumerator equipWeapons;
    private IEnumerator destroyWeapons;

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
        GameObject copy = Instantiate(weapon, weaponPos[0], Quaternion.Euler(0f, -180f, 0f)) as GameObject;
        copy.transform.SetParent(playerBox.transform, false);
    }

    // Update is called once per frame
    void Update()
    {
        // 라운드 시작 시 무기 장착하고 끝날 때 무기 파괴?
        if (equipWeapons != null)
        {
            StartCoroutine(EquipWeapons());
        }

        if (destroyWeapons != null)
        {
            StartCoroutine(DestroyWeapons());
        }
    }

    // 무기 장착 함수
    public IEnumerator EquipWeapons()
    {
        yield return null;
    }

    // 무기 파괴 함수
    public IEnumerator DestroyWeapons()
    {
        yield return null;
    }

    public IEnumerator GetEquipWeapons()
    {
        return this.equipWeapons;
    }

    public List<GameObject> GetCurrentWeaponList()
    {
        return this.currentWeaponList;
    }
}
