using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopOwnItemListControl : MonoBehaviour
{
    private GameObject ownItemListContent;
    // Item1 = ������
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
        Debug.Log("����");
        int ownItemCount = ownItemList.Count;

        if (ownItemCount > 0)
        {
            for (int i = 0; i < ownItemCount; i++)
            {
                GameObject item = ownItemList[i].Item1;

                int r = i / 6;  // ��
                int c = i % 6;  // ��
                //Debug.Log(r + "�� " + c + "��");
                GameObject itemRoom = ownItemListContent.transform.GetChild(r).GetChild(c).gameObject;

                // ����Ϸ��� �������� ��� �̹����� �ִ´�.
                itemRoom.transform.GetChild(0).GetComponent<Image>().color =
                    item.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
                // ����Ϸ��� �������� �̹����� �ִ´�.
                itemRoom.transform.GetChild(1).GetComponent<Image>().sprite =
                    item.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite;
                // ����Ϸ��� �������� ���� �Է�
                itemRoom.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "x" + ownItemList[i].Item2;
            }
        }
        yield return null;
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
                if (ownItemList[i].Item1.name == item.name)
                {
                    // �ߺ� ������ Ʈ���� True
                    isOverlapItem = true;
                    // ���� ������ UI�� ��ġ�� ã�´�
                    int r = i / 6;  // ��
                    int c = i % 6;  // ��
                    GameObject itemRoom = ownItemListContent.transform.GetChild(r).GetChild(c).gameObject;
                    TextMeshProUGUI itemNumberText = itemRoom.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();

                    string tmp = itemNumberText.text;
                    // �ƽ�Ű �ڵ带 �̿��ؼ� ���� ������ ���� ����
                    int num = tmp[1] - 47;
                    itemNumberText.text = "x" + num;

                    ownItemList[i] = (item, num);
                    break;
                }
            }
        }
        
        // �ߺ��� �������� �ƴϸ� ����Ʈ�� �߰�
        if (!isOverlapItem)
        {
            ownItemList.Add((item, 1));
            int r = (ownItemList.Count - 1) / 6;    // 0�� ~ n��
            int c = (ownItemList.Count - 1) % 6;    // 0�� ~ 5��
            GameObject itemRoom = ownItemListContent.transform.GetChild(r).GetChild(c).gameObject;

            // ���� ������ ĭ�� ItemInfo ��ũ��Ʈ �߰�
            itemRoom.AddComponent<ItemInfo>();
            // ����Ϸ��� �������� ������ �Է��Ѵ�.
            itemRoom.GetComponent<ItemInfo>().SetItemInfo(item);
            // ����Ϸ��� �������� ��� �̹����� �ִ´�.
            itemRoom.transform.GetChild(0).gameObject.GetComponent<Image>().color =
                item.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
            // ����Ϸ��� �������� �̹����� �ִ´�.
            itemRoom.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = 
                item.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite;
            // ����Ϸ��� �������� ���� �Է�
            itemRoom.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = "x1";
        }

        yield return null;
    }
}
