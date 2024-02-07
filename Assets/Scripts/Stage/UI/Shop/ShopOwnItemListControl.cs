using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopOwnItemListControl : MonoBehaviour
{
    private GameObject ownItemListContent;
    // Item1 = 아이템, Item2 = 수량
    List<GameObject> ownItemList = new();
    int ownListSize = 0;

    public IEnumerator renewOwnItemList;

    // Start is called before the first frame update
    private void Awake()
    {
        ownItemListContent = this.gameObject.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (renewOwnItemList != null)
        {
            StartCoroutine(renewOwnItemList);
        }
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
                if (ownItemList[i] == item)
                {
                    isOverlapItem = true;
                    // 해당 오브젝트의 텍스트를 변경하면 된다.
                    int r = i / 6;
                    int c = i % 6;
                    GameObject itemRoom = ownItemListContent.transform.GetChild(r).GetChild(c).gameObject;

                    string tmp = itemRoom.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text;
                    int num = tmp[1] + 1;
                    itemRoom.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "x" + num;
                }
            }
        }
        
        // 중복된 아이템이 아니면 리스트에 추가
        if (!isOverlapItem)
        {
            ownItemList.Add(item);
            int r = ownItemList.Count / 6;
            int c = (ownItemList.Count - 1) % 6;
            GameObject itemRoom = ownItemListContent.transform.GetChild(r).GetChild(c).gameObject;

            itemRoom.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;
            itemRoom.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "x1";
        }
    }
}
