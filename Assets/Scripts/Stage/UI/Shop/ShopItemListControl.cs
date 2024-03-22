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
                // ������ ��� (�ش� ĭ�� ���� ������ ������ ����)
                if (ItemManager.Instance.GetShopWeaponInfoList()[i] != null)
                {
                    WeaponInfo weaponInfo = ItemManager.Instance.GetShopWeaponInfoList()[i];
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
        WeaponInfo weaponInfo = ItemManager.Instance.GetShopWeaponInfoList()[i];
        int damage = Mathf.FloorToInt(
            (weaponInfo.damage + Mathf.FloorToInt(PlayerInfo.Instance.GetFixedDMG()))
                                                * ((PlayerInfo.Instance.GetDMGPercent() + 100) / 100));

        float coolDown = weaponInfo.coolDown - weaponInfo.coolDown * PlayerInfo.Instance.GetATKSpeed() / (100 + PlayerInfo.Instance.GetATKSpeed());
        float atkSpeed = Mathf.Round(1 / coolDown * 100) / 100;
        float range = Mathf.Floor(weaponInfo.range * ((PlayerInfo.Instance.GetRange() + 100) / 100) * 100) / 100;

        weaponInfoText.text = "����� : " + damage + '\n' +
                              "���ݼӵ� : " + atkSpeed + "/s \n" +
                              "���� : " + range;

        TextMeshProUGUI weaponPrice = itemList[i].transform.GetChild(4).GetChild(2).GetComponent<TextMeshProUGUI>();
        // ������ �⺻ ���� + ���� ���� + (������ �⺻ ���� * ���� ����) / 10
        int price = Mathf.FloorToInt(weaponInfo.price + GameRoot.Instance.GetCurrentRound() +
                                    (weaponInfo.price * GameRoot.Instance.GetCurrentRound() / 10) *
                                    (1f - 0.05f * ItemManager.Instance.GetOwnNormalItemList()[39]));

        weaponPrice.text = price.ToString();
    }

    // �������� �ɷ�ġ�� ���� �ؽ�Ʈ�� �����Ѵ�.
    void SetItemInfoText(int i)
    {
        TextMeshProUGUI itemInfoText = itemList[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        ItemInfo itemInfo = ItemManager.Instance.GetShopItemList()[i].GetComponent<ItemInfo>();
        itemInfoText.text = "";

        TextMeshProUGUI itemPrice = itemList[i].transform.GetChild(4).GetChild(2).GetComponent<TextMeshProUGUI>();
        itemPrice.text = Mathf.FloorToInt(itemInfo.price + GameRoot.Instance.GetCurrentRound() +
                                         (itemInfo.price * GameRoot.Instance.GetCurrentRound() / 10) *
                                         (1f - 0.05f * ItemManager.Instance.GetOwnNormalItemList()[39])).ToString();

        // ������ ���� ������ ��� ��´�
        string tmpText = "";
        int plusCount = 0;
        int minusCount = 0;

        // ���� ����
        if (itemInfo.DMGPercent > 0)
        {
            tmpText += "����� +" + itemInfo.DMGPercent + "%\n";
            plusCount++;
        }
        if (itemInfo.ATKSpeed > 0)
        {
            tmpText += "���ݼӵ� +" + itemInfo.ATKSpeed + "%\n";
            plusCount++;
        }
        if (itemInfo.FixedDMG > 0)
        {
            tmpText += "���� ����� +" + itemInfo.FixedDMG + '\n';
            plusCount++;
        }
        if (itemInfo.Critical > 0)
        {
            tmpText += "ġ��Ÿ Ȯ��+" + itemInfo.Critical + "%\n";
            plusCount++;
        }
        if (itemInfo.Range > 0)
        {
            tmpText += "���� +" + itemInfo.Range + "%\n";
            plusCount++;
        }

        // ��� ����
        if (itemInfo.HP > 0)
        {
            tmpText += "�ִ� ü�� +" + itemInfo.HP + '\n';
            plusCount++;
        }
        if (itemInfo.Recovery > 0)
        {
            tmpText += "ȸ���� +" + itemInfo.Recovery + '\n';
            plusCount++;
        }
        if (itemInfo.HPDrain > 0)
        {
            tmpText += "����� ��� +" + itemInfo.HPDrain + "%\n";
            plusCount++;
        }
        if (itemInfo.Armor > 0)
        {
            tmpText += "���� +" + itemInfo.Armor + '\n';
            plusCount++;
        }
        if (itemInfo.Evasion > 0)
        {
            tmpText += "ȸ�� Ȯ�� +" + itemInfo.Evasion + "%\n";
            plusCount++;
        }

        // ��ƿ ����
        if (itemInfo.MovementSpeedPercent > 0)
        {
            tmpText += "�̵��ӵ� +" + itemInfo.MovementSpeedPercent + "%\n";
            plusCount++;
        }
        if (itemInfo.RootingRange > 0)
        {
            tmpText += "ȹ�� ���� +" + itemInfo.RootingRange + "%\n";
            plusCount++;
        }
        if (itemInfo.Luck > 0)
        {
            tmpText += "��� +" + itemInfo.Luck + '\n';
            plusCount++;
        }
        if (itemInfo.Harvest > 0)
        {
            tmpText += "��Ȯ +" + itemInfo.Harvest + '\n';
            plusCount++;
        }
        if (itemInfo.ExpGain > 0)
        {
            tmpText += "����ġ ȹ�� " + itemInfo.ExpGain + "%\n";
            plusCount++;
        }

        // ������ Ư�� ȿ��
        if (itemInfo.positiveSpecial != "")
        {
            tmpText += itemInfo.positiveSpecial + "\n";
            plusCount++;
        }

        // ���� ����
        if (itemInfo.DMGPercent < 0)
        {
            tmpText += "����� " + itemInfo.DMGPercent + "%\n";
            minusCount++;
        }
        if (itemInfo.ATKSpeed < 0)
        {
            tmpText += "���ݼӵ� " + itemInfo.ATKSpeed + "%\n";
            minusCount++;
        }
        if (itemInfo.FixedDMG < 0)
        {
            tmpText += "���� ����� " + itemInfo.FixedDMG + '\n';
            minusCount++;
        }
        if (itemInfo.Critical < 0)
        {
            tmpText += "ġ��Ÿ Ȯ��" + itemInfo.Critical + "%\n";
            minusCount++;
        }
        if (itemInfo.Range < 0)
        {
            tmpText += "���� " + itemInfo.Range + "%\n";
            minusCount++;
        }

        // ��� ����
        if (itemInfo.HP < 0)
        {
            tmpText += "�ִ� ü�� " + itemInfo.HP + '\n';
            minusCount++;
        }
        if (itemInfo.Recovery < 0)
        {
            tmpText += "ȸ���� " + itemInfo.Recovery + '\n';
            minusCount++;
        }
        if (itemInfo.HPDrain < 0)
        {
            tmpText += "����� ��� " + itemInfo.HPDrain + "%\n";
            minusCount++;
        }
        if (itemInfo.Armor < 0)
        {
            tmpText += "���� " + itemInfo.Armor + '\n';
            minusCount++;
        }
        if (itemInfo.Evasion < 0)
        {
            tmpText += "ȸ�� Ȯ�� " + itemInfo.Evasion + "%\n";
            minusCount++;
        }

        // ��ƿ ����
        if (itemInfo.MovementSpeedPercent < 0)
        {
            tmpText += "�̵��ӵ� " + itemInfo.MovementSpeedPercent + "%\n";
            minusCount++;
        }
        if (itemInfo.RootingRange < 0)
        {
            tmpText += "ȹ�� ���� " + itemInfo.RootingRange + "%\n";
            minusCount++;
        }
        if (itemInfo.Luck < 0)
        {
            tmpText += "��� " + itemInfo.Luck + '\n';
            minusCount++;
        }
        if (itemInfo.Harvest < 0)
        {
            tmpText += "��Ȯ " + itemInfo.Harvest + '\n';
            minusCount++;
        }
        if (itemInfo.ExpGain < 0)
        {
            tmpText += "����ġ ȹ�� " + itemInfo.ExpGain + "%\n";
            minusCount++;
        }

        // ������ Ư�� ȿ��
        if (itemInfo.negativeSpecial != "")
        {
            tmpText += itemInfo.negativeSpecial + "\n";
            minusCount++;
        }

        // �ؽ�Ʈ�� �� �������� ������
        string[] lines = tmpText.Split('\n');
        string finalText = ""; // ���� �ؽ�Ʈ

        // �ɷ�ġ�� ����ϸ� �ؽ�Ʈ�� �ʷϻ����� ����
        for (int j = 0; j < plusCount; j++)
        {
            string coloredLine = "";
            // +�� ���ڸ� ���� �����Ѵ�
            for (int k = 0; k < lines[j].Length; k++)
            {
                // #1FDE38 << ���� �ʷϻ�
                if (lines[j][k] == '+' || lines[j][k] == '%')
                    coloredLine += $"<color=#1FDE38>{lines[j][k]}</color>";
                else if (lines[j][k] > 47 && lines[j][k] < 58)
                    coloredLine += $"<color=#1FDE38>{lines[j][k]}</color>";
                else
                    coloredLine += lines[j][k];
            }

            // ���� �ؽ�Ʈ�� �߰�
            finalText += coloredLine;
            finalText += "\n";
        }

        // �ɷ�ġ�� �϶��ϸ� �ؽ�Ʈ�� ���������� ����
        for (int j = plusCount; j < plusCount + minusCount; j++)
        {
            string coloredLine = "";

            // -�� ���ڸ� ���� �����Ѵ�
            for (int k = 0; k < lines[j].Length; k++)
            {
                if (lines[j][k] == '-' || lines[j][k] == '%')
                    coloredLine += $"<color=#{ColorUtility.ToHtmlStringRGB(Color.red)}>{lines[j][k]}</color>";
                else if (lines[j][k] > 47 && lines[j][k] < 58)
                    coloredLine += $"<color=#{ColorUtility.ToHtmlStringRGB(Color.red)}>{lines[j][k]}</color>";
                else
                    coloredLine += lines[j][k];
            }

            // ���� �ؽ�Ʈ�� �߰�
            finalText += coloredLine;
            finalText += "\n";
        }

        // TextMeshProUGUI�� �Ҵ�
        itemInfoText.text = finalText;
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
            // �Ķ���
            case 1:
                color = new Color(120, 166, 214);
                ColorUtility.TryParseHtmlString("#78A6D6", out color);
                break;
            // �����
            case 2:
                color = new Color(161, 120, 214);
                ColorUtility.TryParseHtmlString("#A178D6", out color);
                break;
            // ��Ȳ��
            case 3:
                color = new Color(233, 137, 76);
                ColorUtility.TryParseHtmlString("#E9894C", out color);
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
