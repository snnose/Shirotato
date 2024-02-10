using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopOwnWeaponListControl : MonoBehaviour
{
    private GameObject ownWeaponListContent;

    public IEnumerator renewOwnWeaponList;

    private void Awake()
    {
        ownWeaponListContent = this.gameObject.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        renewOwnWeaponList = RenewOwnWeaponList();
    }

    // Update is called once per frame
    void Update()
    {
        if (renewOwnWeaponList != null)
        {
            StartCoroutine(renewOwnWeaponList);
        }
    }

    public IEnumerator RenewOwnWeaponList()
    {
        List<GameObject> currWeaponList = WeaponManager.Instance.GetCurrentWeaponList();

        int num = currWeaponList.Count;
        for (int i = 0; i < num; i++)
        {
            int r = i / 3;
            int c = i % 3;
            GameObject weaponRoom =
                ownWeaponListContent.transform.GetChild(r).GetChild(c).gameObject;

            weaponRoom.transform.GetChild(1).gameObject.GetComponent<Image>().sprite =
                currWeaponList[i].GetComponent<SpriteRenderer>().sprite;
        }
        
        yield return null;
    }
}
