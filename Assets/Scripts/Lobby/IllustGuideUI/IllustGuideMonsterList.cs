using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IllustGuideMonsterList : MonoBehaviour
{
    public List<GameObject> monsterList = new();

    private GameObject content;

    private void Awake()
    {
        content = this.transform.GetChild(0).GetChild(0).gameObject;

        InitMonsterList();
    }

    private void InitMonsterList()
    {
        int count = monsterList.Count;

        for (int i = 0; i < count; i++)
        {
            // 행, 열 지정
            int row = i / 6;
            int col = i % 6;

            // r행 c열 칸 할당
            GameObject room = content.transform.GetChild(row).GetChild(col).gameObject;

            room.transform.GetChild(1).GetComponent<Image>().sprite =
                monsterList[i].transform.GetComponent<SpriteRenderer>().sprite;
        }
    }
}
