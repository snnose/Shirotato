using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusDetailControl : MonoBehaviour
{
    private static StatusDetailControl instance;
    public static StatusDetailControl Instance
    {
        get
        {
            if (null == instance)
                return null;
            else
                return instance;
        }
    }

    private Image statImage;
    private TextMeshProUGUI statNamePro;
    private TextMeshProUGUI detailPro;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        statImage = this.gameObject.transform.GetChild(1).GetComponent<Image>();
        statNamePro = this.gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        detailPro = this.gameObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
    }

    public void SetStatusDetail(string statName)
    {
        string path = "Sprites/Stat/" + statName;
        //statImage.sprite = Resources.Load<Sprite>(path);
        statNamePro.text = SetStatNameProText(statName);
        detailPro.text = SetDetailProText(statName);
    }

    private string SetStatNameProText(string statName)
    {
        string name = "";

        switch (statName)
        {
            case "HP":
                name = "�ִ� ü��";
                break;
            case "Recovery":
                name = "ȸ����";
                break;
            case "HPDrain":
                name = "����� ���";
                break;
            case "DamagePercent":
                name = "�����%";
                break;
            case "DamageFixed":
                name = "���� �����";
                break;
            case "ATKSpeed":
                name = "���ݼӵ�";
                break;
            case "Critical":
                name = "ġ��Ÿ Ȯ��";
                break;
            case "Range%":
                name = "����%";
                break;
            case "Evasion":
                name = "ȸ��";
                break;
            case "Armor":
                name = "����";
                break;
            case "MovementSpeed%":
                name = "�̵��ӵ�%";
                break;
            case "Luck":
                name = "���";
                break;
            case "Harvest":
                name = "��Ȯ";
                break;
            default:
                break;
        }

        return name;
    }

    private string SetDetailProText(string statName)
    {
        // �÷��̾� ����
        PlayerInfo playerInfo = PlayerInfo.Instance;
        string detail = "";

        switch (statName)
        {
            case "HP":
                detail = "�ִ� " + playerInfo.GetHP() + "��ŭ�� ������� ���� �� �ֽ��ϴ�.";
                break;
            case "Recovery":
                detail = "�� 10�ʸ��� ü�� " + playerInfo.GetRecovery() + "�� ȸ���մϴ�. \n" +
                         "(ü�� " + playerInfo.GetRecovery() / 10 + "/s)";
                break;
            case "HPDrain":
                detail = "���� �� " + playerInfo.GetHPDrain() + "% Ȯ���� ü�� 1�� ȸ���մϴ�.";
                break;
            case "DamagePercent":
                detail = "���� �� ������� " + playerInfo.GetDMGPercent() + "% �����մϴ�.";
                break;
            case "DamageFixed":
                detail = "���� �� ������� " + playerInfo.GetFixedDMG() + " �����մϴ�";
                break;
            case "ATKSpeed":
                detail = "���� �ֱⰡ " + playerInfo.GetATKSpeed() + "% �� �������ϴ�.";
                break;
            case "Critical":
                detail = "���� �� " + playerInfo.GetCritical() + "% Ȯ���� 2�� ������ ���ظ� �����ϴ�.";
                break;
            case "Range%":
                detail = "���� ��Ÿ��� " + playerInfo.GetRange() + "% �����մϴ�.";
                break;
            case "Evasion":
                detail = playerInfo.GetEvasion() + "% Ȯ���� ������ ȸ���մϴ�.";
                break;
            case "Armor":
                detail = "�޴� ���ذ� " + Mathf.FloorToInt(playerInfo.GetArmor() / (Mathf.Abs(playerInfo.GetArmor()) + 10)) + "% �����մϴ�.";
                break;
            case "MovementSpeed%":
                detail = playerInfo.GetMovementSpeedPercent() + "% �� ������ �̵��մϴ�.";
                break;
            case "Luck":
                detail = "�� óġ �� ���ڿ� ���� ��� Ȯ���� " + playerInfo.GetLuck() + "% �����մϴ�. " +
                         "���� ���� ǰ��� ���� �� �� ���� ����� ���� �����մϴ�.";
                break;
            case "Harvest":
                detail = "���� ���� �� " + playerInfo.GetHarvest() + "��ŭ�� ���ð� ����ġ�� ����ϴ�.\n" +
                         "�� ���� ���� �� 5%��ŭ �����մϴ�.";
                break;
            default:
                break;
        }

        return detail;
    }

    public void SetDetailUIPosition(Vector2 pos)
    {
        this.transform.position = pos;
    }
}
