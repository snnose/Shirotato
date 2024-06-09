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
            // 행, 열 지정
            int row = i / 6;
            int col = i % 6;

            // r행 c열 칸 할당
            GameObject room = content.transform.GetChild(row).GetChild(col).gameObject;

            // ItemInfo 부착
            room.AddComponent<ItemInfo>();
            room.GetComponent<ItemInfo>().SetItemInfo(legendItemList[i]);

            // 칸의 첫번째 자식은 Grade, 두번째 자식은 Item 이미지
            room.transform.GetChild(0).GetComponent<Image>().color =
                legendItemList[i].transform.GetChild(0).GetComponent<SpriteRenderer>().color;

            room.transform.GetChild(1).GetComponent<Image>().sprite =
                legendItemList[i].transform.GetChild(1).GetComponent<SpriteRenderer>().sprite;
        }
    }
}
