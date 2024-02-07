using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopOwnItemListControl : MonoBehaviour
{
    private GameObject ownItemListContent;
    // Item1 = ������, Item2 = ����
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

        // ���� �������� �� �� �̻��̶��
        if (ownItemList.Count > 0)
        {
            // ���� �������� ���� �ִ� �� ã�´�.
            for (int i = 0; i < ownItemList.Count; i++)
            {
                // �ִٸ� ���� +1
                if (ownItemList[i] == item)
                {
                    isOverlapItem = true;
                    // �ش� ������Ʈ�� �ؽ�Ʈ�� �����ϸ� �ȴ�.
                    int r = i / 6;
                    int c = i % 6;
                    GameObject itemRoom = ownItemListContent.transform.GetChild(r).GetChild(c).gameObject;

                    string tmp = itemRoom.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text;
                    int num = tmp[1] + 1;
                    itemRoom.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "x" + num;
                }
            }
        }
        
        // �ߺ��� �������� �ƴϸ� ����Ʈ�� �߰�
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
