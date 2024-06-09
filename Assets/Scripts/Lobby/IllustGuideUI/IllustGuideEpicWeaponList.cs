using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IllustGuideEpicWeaponList : MonoBehaviour
{
    public List<GameObject> epicWeaponList = new();

    private GameObject content;

    private void Awake()
    {
        content = this.transform.GetChild(0).GetChild(0).gameObject;

        InitWeaponList();
    }

    private void InitWeaponList()
    {
        int count = epicWeaponList.Count;

        for (int i = 0; i < count; i++)
        {
            // ��, �� ����
            int row = i / 6;
            int col = i % 6;

            // r�� c�� ĭ �Ҵ�
            GameObject room = content.transform.GetChild(row).GetChild(col).gameObject;

            room.transform.GetChild(1).GetComponent<Image>().sprite =
                epicWeaponList[i].transform.GetComponent<SpriteRenderer>().sprite;
        }
    }
}
