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
        statImage.sprite = Resources.Load<Sprite>(path);
        statNamePro.text = SetStatNameProText(statName);
        detailPro.text = SetDetailProText(statName);
    }

    private string SetStatNameProText(string statName)
    {
        string name = "";

        switch (statName)
        {
            case "�ִ�ü��":
                name = "�ִ� ü��";
                break;
            case "ȸ����":
                name = "ȸ����";
                break;
            case "��������":
                name = "����� ���";
                break;
            case "�����%":
                name = "�����%";
                break;
            case "�߰������":
                name = "�߰� �����";
                break;
            case "���ݼӵ�":
                name = "���ݼӵ�";
                break;
            case "ġ��ŸȮ��":
                name = "ġ��Ÿ Ȯ��";
                break;
            case "����":
                name = "����%";
                break;
            case "ȸ��":
                name = "ȸ��";
                break;
            case "����":
                name = "����";
                break;
            case "�̵��ӵ�":
                name = "�̵��ӵ�%";
                break;
            case "���":
                name = "���";
                break;
            case "��Ȯ":
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
            case "�ִ�ü��":
                detail = "�ִ� " + playerInfo.GetHP() + "��ŭ�� ������� ���� �� �ֽ��ϴ�.";
                break;
            case "ȸ����":
                detail = "�� 10�ʸ��� ü�� " + playerInfo.GetRecovery() + "�� ȸ���մϴ�. \n" +
                         "(ü�� " + playerInfo.GetRecovery() / 10 + "/s)";
                // ȸ���� -4 ~ 0�� ��
                if (playerInfo.GetRecovery() <= 0)
                {
                    detail = "�� 10�ʸ��� ü�� " + 0 + "�� ȸ���մϴ�. \n" +
                         "(ü�� " + 0 + "/s)";
                }
                
                // ȸ���� -5 ������ ��
                if (playerInfo.GetRecovery() <= -5f)
                {
                    detail = "�� 10�ʸ��� ü�� " + -5 + " �����մϴ�. \n" +
                         "(ü�� " + -0.5 + "/s)";
                }
                
                break;
            case "��������":
                detail = "���� �� " + playerInfo.GetHPDrain() + "% Ȯ���� ü�� 1�� ȸ���մϴ�.";
                break;
            case "�����%":
                detail = "���� �� ������� " + Mathf.FloorToInt(playerInfo.GetDMGPercent() * 10f) / 10 + "% �����մϴ�.";
                break;
            case "�߰������":
                detail = "���� �� ������� �����մϴ�. \n" +
                         "(���� ��� x " + playerInfo.GetFixedDMG() + ")";
                break;
            case "���ݼӵ�":
                detail = "���� �ֱⰡ " + Mathf.FloorToInt(playerInfo.GetATKSpeed() * 10f) / 10 + "% �� �������ϴ�.";
                break;
            case "ġ��ŸȮ��":
                detail = "���� �� " + Mathf.FloorToInt(playerInfo.GetCritical() * 10f) / 10 + "% Ȯ���� 2�� ������ ���ظ� �����ϴ�.";
                break;
            case "����":
                detail = "���� ��Ÿ��� " + playerInfo.GetRange() + "% �����մϴ�.";
                break;
            case "ȸ��":
                detail = playerInfo.GetEvasion() + "% Ȯ���� ������ ȸ���մϴ� (�ִ� 60%).";
                break;
            case "����":
                float tmp = 100 * playerInfo.GetArmor() / (Mathf.Abs(playerInfo.GetArmor()) + 10);
                detail = "�޴� ���ذ� " + tmp + "% �����մϴ�.";
                break;
            case "�̵��ӵ�":
                detail = playerInfo.GetMovementSpeedPercent() + "% �� ������ �̵��մϴ�.";
                break;
            case "���":
                detail = "�� óġ �� ���ڿ� ���� ��� Ȯ���� " + Mathf.FloorToInt(playerInfo.GetLuck() * 10f) / 10 + "% �����մϴ�. " +
                         "���� ���� ǰ��� ���� �� �� ���� ����� ���� �����մϴ�.";
                break;
            case "��Ȯ":
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
