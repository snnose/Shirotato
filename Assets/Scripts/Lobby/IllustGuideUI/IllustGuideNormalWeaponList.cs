using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IllustGuideNormalWeaponList : MonoBehaviour
{
    public List<GameObject> normalWeaponList = new();

    private GameObject content;

    private void Awake()
    {
        content = this.transform.GetChild(0).GetChild(0).gameObject;

        InitWeaponList();
    }

    private void InitWeaponList()
    {
        int count = normalWeaponList.Count;

        for (int i = 0; i < count; i++)
        {
            // 행, 열 지정
            int row = i / 6;
            int col = i % 6;

            // r행 c열 칸 할당
            GameObject room = content.transform.GetChild(row).GetChild(col).gameObject;

            room.transform.GetChild(1).GetComponent<Image>().sprite =
                normalWeaponList[i].transform.GetComponent<SpriteRenderer>().sprite;
        }
    }
}
