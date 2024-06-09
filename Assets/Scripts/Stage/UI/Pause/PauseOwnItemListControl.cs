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
}
