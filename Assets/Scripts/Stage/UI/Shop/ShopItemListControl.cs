using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemListControl : MonoBehaviour
{
    public List<GameObject> itemList;

    public bool isRenewItemInfo = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        RenewItemInfo();
    }

    public void RenewItemInfo()
    {
        // ���� ������ ����Ʈ�� ���� �ƴٸ�
        if (ItemManager.Instance.GetIsRenewItem() && !isRenewItemInfo)
        {
            // �� ���Կ� ������ ������ ����Ѵ�.
            for (int i = 0; i < 4; i++)
            {
                // ������ �̹��� ����
                itemList[i].transform.GetChild(1).GetComponent<Image>().sprite =
                    ItemManager.Instance.GetShopItemList()[i].Item1.GetComponent<SpriteRenderer>().sprite;
                // ������ �̸� ����
                itemList[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text =
                    ItemManager.Instance.GetShopItemList()[i].Item1.name;

                // ����, �����ۿ� ���� ���� ������ �ٸ���.
                // ������ ���
                if (ItemManager.Instance.GetShopItemList()[i].Item3)
                {
                    SetWeaponInfoText(i);
                }
                // �������� ���
                else
                {
                    SetItemInfoText(i);
                }
            }

            isRenewItemInfo = true;
        }
    }

    void SetWeaponInfoText(int i)
    {
        TextMeshProUGUI weaponInfoText = itemList[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        WeaponInfo weaponInfo = ItemManager.Instance.GetShopItemList()[i].Item1.GetComponent<WeaponInfo>();
        weaponInfoText.text = "����� : " + weaponInfo.damage + '\n' +
                              "���ݼӵ� : " + Mathf.Round(1 / weaponInfo.coolDown * 100) / 100 + "/s \n" +
                              "���� : " + weaponInfo.range;

        TextMeshProUGUI weaponPrice = itemList[i].transform.GetChild(4).GetChild(2).GetComponent<TextMeshProUGUI>();
        weaponPrice.text = (weaponInfo.price + GameRoot.Instance.GetCurrentRound() +
                            Mathf.FloorToInt(weaponInfo.price * GameRoot.Instance.GetCurrentRound() / 10)).ToString();
    }

    void SetItemInfoText(int i)
    {
        TextMeshProUGUI itemInfoText = itemList[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        ItemInfo itemInfo = ItemManager.Instance.GetShopItemList()[i].Item1.GetComponent<ItemInfo>();
        itemInfoText.text = "";

        TextMeshProUGUI itemPrice = itemList[i].transform.GetChild(4).GetChild(2).GetComponent<TextMeshProUGUI>();
        itemPrice.text = (itemInfo.price + GameRoot.Instance.GetCurrentRound() +
                            Mathf.FloorToInt(itemInfo.price * GameRoot.Instance.GetCurrentRound() / 10)).ToString();

        // ���� ����
        if (itemInfo.DMGPercent != 0)
            itemInfoText.text += "����� " + itemInfo.DMGPercent + "%\n";
        if (itemInfo.ATKSpeed != 0)
            itemInfoText.text += "���ݼӵ� " + itemInfo.ATKSpeed + "%\n";
        if (itemInfo.FixedDMG != 0)
            itemInfoText.text += "���� ����� " + itemInfo.FixedDMG + '\n';
        if (itemInfo.Critical != 0)
            itemInfoText.text += "ġ��Ÿ " + itemInfo.Critical + "%\n";
        if (itemInfo.Range != 0)
            itemInfoText.text += "���� " + itemInfo.Range + "%\n";

        // ��� ����
        if (itemInfo.HP != 0)
            itemInfoText.text += "�ִ� ü�� " + itemInfo.HP + '\n';
        if (itemInfo.Recovery != 0)
            itemInfoText.text += "ü�� ȸ�� " + itemInfo.Recovery + '\n';
        if (itemInfo.Armor != 0)
            itemInfoText.text += "���� " + itemInfo.Armor + '\n';
        if (itemInfo.Evasion != 0)
            itemInfoText.text += "ȸ�� " + itemInfo.Evasion + "%\n";
        // ��ƿ ����
        if (itemInfo.MovementSpeed != 0)
            itemInfoText.text += "�̵��ӵ� " + itemInfo.MovementSpeed + "%\n";
        if (itemInfo.RootingRange != 0)
            itemInfoText.text += "ȹ�� ���� " + itemInfo.RootingRange + "%\n";
        if (itemInfo.Luck != 0)
            itemInfoText.text += "��� " + itemInfo.Luck + '\n';
    }

    public void SetItemListActive()
    {
        for(int i = 0; i < 4; i++)
        {
            itemList[i].SetActive(true);
        }
    }

    public void SetIsRenewInfo(bool ret)
    {
        this.isRenewItemInfo = ret;
    }
}
