using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeListControl : MonoBehaviour
{
    private List<GameObject> upgradeRoomList = new();

    public IEnumerator renewUpgradeRoom;

    private void Awake()
    {
        // �� ���׷��̵� UI ĭ�� ����Ʈ�� �߰�
        for (int i = 0; i < 4; i++)
        {
            GameObject upgradeRoom = this.gameObject.transform.GetChild(i).gameObject;
            upgradeRoomList.Add(upgradeRoom);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (renewUpgradeRoom != null)
            StartCoroutine(renewUpgradeRoom);
    }

    public IEnumerator RenewUpgradeRoom()
    {
        // ������ ���׷��̵� ����� �ҷ��´�
        List<(int, int)> upgradeList = UpgradeManager.Instance.GetUpgradeList();

        for (int i = 0; i < 4; i++)
        {
            (int, int) currentUpgrade = upgradeList[i];
            SetUpgradeRoomInfo(upgradeRoomList[i], currentUpgrade.Item1, currentUpgrade.Item2);
        }
        yield return null;
    }

    // ���� ���׷��̵忡 �´� ������ ����Ѵ�
    private void SetUpgradeRoomInfo(GameObject upgradeRoom, int upgrade, int rarity)
    {
        Image upgradeGrade = upgradeRoom.transform.GetChild(1).GetComponent<Image>();
        Image upgradeImage = upgradeRoom.transform.GetChild(2).GetComponent<Image>();
        TextMeshProUGUI upgradeName = upgradeRoom.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI upgradeStatus = upgradeRoom.transform.GetChild(4).GetComponent<TextMeshProUGUI>();

        Color color = Color.white;
        switch (rarity)
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
        upgradeGrade.color = color;

        switch (upgrade)
        {
            case 0:
                //upgradeImage = Resources.Load("");
                upgradeName.text = "����";
                upgradeStatus.text = "�ִ� ü�� +" + 3 * (1 + rarity);
                break;
            case 1:
                //upgradeImage = Resources.Load("");
                upgradeName.text = "��";
                upgradeStatus.text = "ȸ���� +" + (2 + rarity);
                break;
            case 2:
                //upgradeImage = Resources.Load("");
                upgradeName.text = "��";
                upgradeStatus.text = "����� ���% +" + (1 + rarity);
                break;
            case 3:
                //upgradeImage = Resources.Load("");
                upgradeName.text = "��";
                upgradeStatus.text = "�����% +" + 5 * (1 + rarity);
                break;
            case 4:
                //upgradeImage = Resources.Load("");
                upgradeName.text = "���";
                upgradeStatus.text = "�߰� ����� +" + (1 + rarity);
                break;
            case 5:
                //upgradeImage = Resources.Load("");
                upgradeName.text = "�ݻ�Ű�";
                upgradeStatus.text = "���ݼӵ�% +" + 5 * (1 + rarity);
                break;
            case 6:
                //upgradeImage = Resources.Load("");
                upgradeName.text = "�հ���";
                upgradeStatus.text = "ġ��Ÿ Ȯ�� +" + 3 * (1 + rarity);
                break;
            case 7:
                //upgradeImage = Resources.Load("");
                upgradeName.text = "��";
                upgradeStatus.text = "����% +" + 3 * (1 + rarity);
                break;
            case 8:
                //upgradeImage = Resources.Load("");
                upgradeName.text = "��";
                upgradeStatus.text = "ȸ�� Ȯ�� +" + 3 * (1 + rarity);
                break;
            case 9:
                //upgradeImage = Resources.Load("");
                upgradeName.text = "���";
                upgradeStatus.text = "���� +" + (1 + rarity);
                break;
            case 10:
                //upgradeImage = Resources.Load("");
                upgradeName.text = "�ٸ�";
                upgradeStatus.text = "�̵��ӵ�% +" + 3 * (1 + rarity);
                break;
            case 11:
                //upgradeImage = Resources.Load("");
                upgradeName.text = "��";
                upgradeStatus.text = "��� +" + 5 * (1 + rarity);
                break;
            case 12:
                //upgradeImage = Resources.Load("");
                upgradeName.text = "��";
                upgradeStatus.text = "��Ȯ +" + (5 + 3 * rarity);
                break;
            default:
                break;
        }
    }
}
