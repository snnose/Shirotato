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
        // 현재 소유 무기의 정보를 가져온다.
        List<WeaponInfo> currWeaponInfoList = WeaponManager.Instance.GetCurrentWeaponInfoList();

        // UI 갱신
        int num = currWeaponList.Count;
        for (int i = 0; i < 6; i++)
        {
            int r = i / 3;
            int c = i % 3;

            GameObject weaponRoom =
                    ownWeaponListContent.transform.GetChild(r).GetChild(c).gameObject;
            // i가 보유 무기 수보다 작다면 무기 정보 입력 및 갱신
            if (i < num)
            {
                // 무기 칸의 배경 이미지(등급)를 변경한다.
                weaponRoom.transform.GetChild(0).gameObject.GetComponent<Image>().color =
                    DecideGradeColor(currWeaponInfoList[i].GetWeaponGrade());
                Debug.Log(i + "번 무기 번호 : " + currWeaponInfoList[i].GetWeaponNumber());
                // 무기 칸의 이미지(무기)를 변경한다.
                weaponRoom.transform.GetChild(1).gameObject.GetComponent<Image>().sprite =
                    currWeaponList[i].GetComponent<SpriteRenderer>().sprite;
            }
            // 그 외는 빈칸으로 처리한다.
            else
            {
                // 무기 칸의 배경 이미지를 흰색으로 변경한다.
                weaponRoom.transform.GetChild(0).gameObject.GetComponent<Image>().color =
                    DecideGradeColor(0);
                // 무기 칸의 이미지를 비운다.
                weaponRoom.transform.GetChild(1).gameObject.GetComponent<Image>().sprite =
                    null;
            }
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
            // 파랑색
            case 1:
                color = new Color(120, 166, 214);
                ColorUtility.TryParseHtmlString("#78A6D6", out color);
                break;
            // 보라색
            case 2:
                color = new Color(161, 120, 214);
                ColorUtility.TryParseHtmlString("#A178D6", out color);
                break;
            // 주황색
            case 3:
                color = new Color(233, 137, 76);
                ColorUtility.TryParseHtmlString("#E9894C", out color);
                break;
            default:
                color = Color.black;
                break;
        }

        return color;
    }
}
