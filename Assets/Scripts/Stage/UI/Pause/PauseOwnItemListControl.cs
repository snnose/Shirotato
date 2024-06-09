using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseOwnItemListControl : MonoBehaviour
{
    public static List<(GameObject, int)> ownItemList;

    private GameObject ownItemListContent;

    private void Awake()
    {
        //ownItemList = ShopOwnItemListControl.ownItemList;
        ownItemList.Clear();
        ownItemListContent = this.gameObject.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public IEnumerator RenewOwnItemList()
    {
        Debug.Log("진입");
        int ownItemCount = ownItemList.Count;

        if (ownItemCount > 0)
        {
            for (int i = 0; i < ownItemCount; i++)
            {
                GameObject item = ownItemList[i].Item1;

                int r = i / 6;  // 행
                int c = i % 6;  // 열
                //Debug.Log(r + "행 " + c + "열");
                GameObject itemRoom = ownItemListContent.transform.GetChild(r).GetChild(c).gameObject;

                // 등록하려는 아이템의 등급 이미지를 넣는다.
                itemRoom.transform.GetChild(0).GetComponent<Image>().color =
                    item.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
                // 등록하려는 아이템의 이미지를 넣는다.
                itemRoom.transform.GetChild(1).GetComponent<Image>().sprite =
                    item.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite;
                // 등록하려는 아이템의 개수 입력
                itemRoom.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "x" + ownItemList[i].Item2;
            }
        }
        yield return null;
    }
}
