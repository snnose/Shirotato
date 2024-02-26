using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemListControl : MonoBehaviour
{
    public List<GameObject> itemList;

    public bool isRenewItemInfo = false;
    //private WeaponInfo weaponInfo;

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
                GameObject currentThing = ItemManager.Instance.GetShopItemList()[i];
                // ������ ���
                if (currentThing.TryGetComponent<WeaponInfo>(out WeaponInfo weaponInfo))
                {
                    // ���� ��� �̹��� ����
                    itemList[i].transform.GetChild(0).GetComponent<Image>().color =
                        DecideGradeColor(weaponInfo.GetWeaponGrade());
                    // ���� �̹��� ����
                    itemList[i].transform.GetChild(1).GetComponent<Image>().sprite =
                        currentThing.GetComponent<SpriteRenderer>().sprite;
                    // �̸� �ؽ�Ʈ ����
                    itemList[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text =
                        weaponInfo.weaponName;

                    SetWeaponInfoText(i);
                }
                // �������� ���
                else
                {
                    // ������ ��� �̹��� ����
                    itemList[i].transform.GetChild(0).GetComponent<Image>().color =
                    currentThing.transform.GetChild(0).GetComponent<SpriteRenderer>().color;

                    // ������ �̹��� ����
                    itemList[i].transform.GetChild(1).GetComponent<Image>().sprite =
                        currentThing.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite;
                    // ������ �̸� ����
                    itemList[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text =
                        currentThing.GetComponent<ItemInfo>().itemName;

                    SetItemInfoText(i);
                }
            }

            isRenewItemInfo = true;
        }
    }

    void SetWeaponInfoText(int i)
    {
        TextMeshProUGUI weaponInfoText = itemList[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        WeaponInfo weaponInfo = ItemManager.Instance.GetShopItemList()[i].GetComponent<WeaponInfo>();
        weaponInfoText.text = "����� : " + weaponInfo.damage + '\n' +
                              "���ݼӵ� : " + Mathf.Round(1 / weaponInfo.coolDown * 100) / 100 + "/s \n" +
                              "���� : " + weaponInfo.range;

        TextMeshProUGUI weaponPrice = itemList[i].transform.GetChild(4).GetChild(2).GetComponent<TextMeshProUGUI>();
        weaponPrice.text = (weaponInfo.price + GameRoot.Instance.GetCurrentRound() +
                            Mathf.FloorToInt(weaponInfo.price * GameRoot.Instance.GetCurrentRound() / 10)).ToString();
    }

    // �������� �ɷ�ġ�� ���� �ؽ�Ʈ�� �����Ѵ�.
    void SetItemInfoText(int i)
    {
        TextMeshProUGUI itemInfoText = itemList[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        ItemInfo itemInfo = ItemManager.Instance.GetShopItemList()[i].GetComponent<ItemInfo>();
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

    // rank�� ���� ��ũ ������ ��ȯ�ϴ� �Լ� (��, ��, ��, ��)
    private Color DecideGradeColor(int grade)
    {
        Color color = Color.white;
        switch (grade)
        {
            case 0:
                color = Color.white;
                break;
            case 1:
                color = new Color(120, 166, 214);
                break;
            case 2:
                color = new Color(161, 120, 214);
                break;
            case 3:
                color = new Color(233, 137, 76);
                break;
            default:
                color = Color.black;
                break;
        }

        return color;
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
