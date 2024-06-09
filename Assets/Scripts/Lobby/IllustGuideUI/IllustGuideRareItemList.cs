using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IllustGuideRareItemList : MonoBehaviour
{
    public List<GameObject> rareItemList = new();

    private GameObject content;

    private void Awake()
    {
        content = this.transform.GetChild(0).GetChild(0).gameObject;

        InitItemList();
    }

    private void InitItemList()
    {
        int count = rareItemList.Count;

        for (int i = 0; i < count; i++)
        {
            // ��, �� ����
            int row = i / 6;
            int col = i % 6;

            // r�� c�� ĭ �Ҵ�
            GameObject room = content.transform.GetChild(row).GetChild(col).gameObject;

            // ItemInfo ����
            room.AddComponent<ItemInfo>();
            room.GetComponent<ItemInfo>().SetItemInfo(rareItemList[i]);

            // ĭ�� ù��° �ڽ��� Grade, �ι�° �ڽ��� Item �̹���
            room.transform.GetChild(0).GetComponent<Image>().color =
                rareItemList[i].transform.GetChild(0).GetComponent<SpriteRenderer>().color;

            room.transform.GetChild(1).GetComponent<Image>().sprite =
                rareItemList[i].transform.GetChild(1).GetComponent<SpriteRenderer>().sprite;
        }
    }
}
