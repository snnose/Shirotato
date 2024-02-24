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
        // 현재 소유 무기 목록을 가져온다.
        List<GameObject> currWeaponList = WeaponManager.Instance.GetCurrentWeaponList();

        // UI 갱신
        int num = currWeaponList.Count;
        for (int i = 0; i < num; i++)
        {
            int r = i / 3;
            int c = i % 3;
            GameObject weaponRoom =
                ownWeaponListContent.transform.GetChild(r).GetChild(c).gameObject;
            // 무기 칸의 이미지(무기)를 변경한다.
            weaponRoom.transform.GetChild(1).gameObject.GetComponent<Image>().sprite =
                currWeaponList[i].GetComponent<SpriteRenderer>().sprite;
            // 무기 칸의 배경 이미지(등급)를 변경한다.
            weaponRoom.transform.GetChild(0).gameObject.GetComponent<Image>().color =
                DecideGradeColor(currWeaponList[i].GetComponent<WeaponInfo>().grade);
        }
        
        yield return null;
    }

    // rank에 따라 랭크 색깔을 반환하는 함수 (흰, 파, 보, 주)
    private Color DecideGradeColor(int grade)
    {
        Color color = Color.white;
        switch (grade)
        {
            case 0:
                color = Color.white;
                break;
            case 1:
                color = new Color(120, 166, 214);
                break;
            case 2:
                color = new Color(161, 120, 214);
                break;
            case 3:
                color = new Color(233, 137, 76);
                break;
            default:
                color = Color.black;
                break;
        }

        return color;
    }
}
