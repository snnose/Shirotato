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
    private List<Vector2> weaponPos = new() { new Vector2(-2.0f, 0f), new Vector2(2.0f, 0f), };

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
        GameObject copy = Instantiate(weapon, weaponPos[0], Quaternion.Euler(0f, -180f, 0f)) as GameObject;
        copy.transform.SetParent(playerBox.transform, false);
        currentWeaponList.Add(copy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<GameObject> GetCurrentWeaponList()
    {
        return this.currentWeaponList;
    }
}
