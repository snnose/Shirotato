using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusControl : MonoBehaviour
{
    public GameObject statInfo;

    private void Awake()
    {
        statInfo = this.transform.GetChild(2).gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RenewStatus();
    }

    // ���� UI�� �ɷ�ġ�� ����
    void RenewStatus()
    {
        // �ִ� ü��
        statInfo.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text =
                                                        PlayerInfo.Instance.GetHP().ToString();
        // ȸ����
        statInfo.transform.GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            PlayerInfo.Instance.GetRecovery().ToString();
        // ����� ���
        statInfo.transform.GetChild(2).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            PlayerInfo.Instance.GetHPDrain().ToString();
        // �����%
        statInfo.transform.GetChild(3).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            Mathf.FloorToInt(PlayerInfo.Instance.GetDMGPercent()).ToString();
        // ���� �����
        statInfo.transform.GetChild(4).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            PlayerInfo.Instance.GetFixedDMG().ToString();
        // ���ݼӵ�
        statInfo.transform.GetChild(5).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            PlayerInfo.Instance.GetATKSpeed().ToString();
        // ġ��Ÿ Ȯ��
        statInfo.transform.GetChild(6).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            PlayerInfo.Instance.GetCritical().ToString();
        // ����
        statInfo.transform.GetChild(7).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            PlayerInfo.Instance.GetRange().ToString();
        // ȸ�� Ȯ��
        statInfo.transform.GetChild(8).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            PlayerInfo.Instance.GetEvasion().ToString();
        // ����
        statInfo.transform.GetChild(9).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            PlayerInfo.Instance.GetArmor().ToString();
        // �̵��ӵ� %
        statInfo.transform.GetChild(10).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            PlayerInfo.Instance.GetMovementSpeedPercent().ToString();
        // ���
        statInfo.transform.GetChild(11).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            Mathf.FloorToInt(PlayerInfo.Instance.GetLuck()).ToString();
        // ��Ȯ
        statInfo.transform.GetChild(12).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            PlayerInfo.Instance.GetHarvest().ToString();
    }
}
