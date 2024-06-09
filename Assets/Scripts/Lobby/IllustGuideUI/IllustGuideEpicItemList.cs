using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IllustGuideEpicItemList : MonoBehaviour
{
    public List<GameObject> epicItemList = new();

    private GameObject content;

    private void Awake()
    {
        content = this.transform.GetChild(0).GetChild(0).gameObject;

        InitItemList();
    }

    private void InitItemList()
    {
        int count = epicItemList.Count;

        for (int i = 0; i < count; i++)
        {
            // ��, �� ����
            int row = i / 6;
            int col = i % 6;

            // r�� c�� ĭ �Ҵ�
            GameObject room = content.transform.GetChild(row).GetChild(col).gameObject;

            // ItemInfo ����
            room.AddComponent<ItemInfo>();
            room.GetComponent<ItemInfo>().SetItemInfo(epicItemList[i]);

            // ĭ�� ù��° �ڽ��� Grade, �ι�° �ڽ��� Item �̹���
            room.transform.GetChild(0).GetComponent<Image>().color =
                epicItemList[i].transform.GetChild(0).GetComponent<SpriteRenderer>().color;

            room.transform.GetChild(1).GetComponent<Image>().sprite =
                epicItemList[i].transform.GetChild(1).GetComponent<SpriteRenderer>().sprite;
        }
    }
}
