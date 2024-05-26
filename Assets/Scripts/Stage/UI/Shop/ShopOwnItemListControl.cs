using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopOwnItemListControl : MonoBehaviour
{
    private GameObject ownItemListContent;
    // Item1 = 아이템
    public static List<(GameObject, int)> ownItemList = new();

    public IEnumerator renewOwnItemList;

    private void Awake()
    {
        ownItemListContent = this.gameObject.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (renewOwnItemList != null)
        {
            StartCoroutine(renewOwnItemList);
        }
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

    public IEnumerator RenewOwnItemList(GameObject item)
    {
        yield return null;
        bool isOverlapItem = false;

        // 소유 아이템이 한 개 이상이라면
        if (ownItemList.Count > 0)
        {
            // 같은 아이템을 갖고 있는 지 찾는다.
            for (int i = 0; i < ownItemList.Count; i++)
            {
                // 있다면 수량 +1
                if (ownItemList[i].Item1.name == item.name)
                {
                    // 중복 아이템 트리거 True
                    isOverlapItem = true;
                    // 보유 아이템 UI의 위치를 찾는다
                    int r = i / 6;  // 행
                    int c = i % 6;  // 열
                    GameObject itemRoom = ownItemListContent.transform.GetChild(r).GetChild(c).gameObject;
                    TextMeshProUGUI itemNumberText = itemRoom.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();

                    string tmp = itemNumberText.text;
                    // 아스키 코드를 이용해서 보유 아이템 개수 증가
                    int num = tmp[1] - 47;
                    itemNumberText.text = "x" + num;

                    ownItemList[i] = (item, num);
                    break;
                }
            }
        }
        
        // 중복된 아이템이 아니면 리스트에 추가
        if (!isOverlapItem)
        {
            ownItemList.Add((item, 1));
            int r = (ownItemList.Count - 1) / 6;    // 0행 ~ n행
            int c = (ownItemList.Count - 1) % 6;    // 0열 ~ 5열
            GameObject itemRoom = ownItemListContent.transform.GetChild(r).GetChild(c).gameObject;

            // 보유 아이템 칸에 ItemInfo 스크립트 추가
            itemRoom.AddComponent<ItemInfo>();
            // 등록하려는 아이템의 정보를 입력한다.
            itemRoom.GetComponent<ItemInfo>().SetItemInfo(item);
            // 등록하려는 아이템의 등급 이미지를 넣는다.
            itemRoom.transform.GetChild(0).gameObject.GetComponent<Image>().color =
                item.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
            // 등록하려는 아이템의 이미지를 넣는다.
            itemRoom.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = 
                item.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite;
            // 등록하려는 아이템의 개수 입력
            itemRoom.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = "x1";
        }

        yield return null;
    }
}
