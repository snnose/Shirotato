using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IllustGuideLegendWeaponList : MonoBehaviour
{
    public List<GameObject> legendWeaponList = new();

    private GameObject content;

    private void Awake()
    {
        content = this.transform.GetChild(0).GetChild(0).gameObject;

        InitWeaponList();
    }

    private void InitWeaponList()
    {
        int count = legendWeaponList.Count;

        for (int i = 0; i < count; i++)
        {
            // ��, �� ����
            int row = i / 6;
            int col = i % 6;

            // r�� c�� ĭ �Ҵ�
            GameObject room = content.transform.GetChild(row).GetChild(col).gameObject;

            room.transform.GetChild(1).GetComponent<Image>().sprite =
                legendWeaponList[i].transform.GetComponent<SpriteRenderer>().sprite;
        }
    }
}
