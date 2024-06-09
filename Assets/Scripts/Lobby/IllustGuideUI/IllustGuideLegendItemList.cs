using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IllustGuideLegendItemList : MonoBehaviour
{
    public List<GameObject> legendItemList = new();

    private GameObject content;

    private void Awake()
    {
        content = this.transform.GetChild(0).GetChild(0).gameObject;

        InitItemList();
    }

    private void InitItemList()
    {
        int count = legendItemList.Count;

        for (int i = 0; i < count; i++)
        {
            // ��, �� ����
            int row = i / 6;
            int col = i % 6;

            // r�� c�� ĭ �Ҵ�
            GameObject room = content.transform.GetChild(row).GetChild(col).gameObject;

            // ItemInfo ����
            room.AddComponent<ItemInfo>();
            room.GetComponent<ItemInfo>().SetItemInfo(legendItemList[i]);

            // ĭ�� ù��° �ڽ��� Grade, �ι�° �ڽ��� Item �̹���
            room.transform.GetChild(0).GetComponent<Image>().color =
                legendItemList[i].transform.GetChild(0).GetComponent<SpriteRenderer>().color;

            room.transform.GetChild(1).GetComponent<Image>().sprite =
                legendItemList[i].transform.GetChild(1).GetComponent<SpriteRenderer>().sprite;
        }
    }
}
