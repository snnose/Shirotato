using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RealtimeStatusControl : MonoBehaviour
{
    public GameObject statInfo;

    void Start()
    {
        statInfo = this.transform.GetChild(2).gameObject;
    }

    void Update()
    {
        RenewStatus();
    }

    // UI�� �ɷ�ġ�� �ǽð����� ����Ǵ� �ɷ�ġ�� ����
    void RenewStatus()
    {
        // �ִ� ü��
        statInfo.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            RealtimeInfoManager.Instance.GetHP().ToString();
        // ȸ����
        statInfo.transform.GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            RealtimeInfoManager.Instance.GetRecovery().ToString();
        // ����� ���
        statInfo.transform.GetChild(2).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            RealtimeInfoManager.Instance.GetHPDrain().ToString();
        // �����%
        statInfo.transform.GetChild(3).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            RealtimeInfoManager.Instance.GetDMGPercent().ToString();
        // ���� �����
        statInfo.transform.GetChild(4).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            RealtimeInfoManager.Instance.GetFixedDMG().ToString();
        // ���ݼӵ�
        statInfo.transform.GetChild(5).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            RealtimeInfoManager.Instance.GetATKSpeed().ToString();
        // ġ��Ÿ Ȯ��
        statInfo.transform.GetChild(6).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            Mathf.FloorToInt(RealtimeInfoManager.Instance.GetCritical()).ToString();
        // ����
        statInfo.transform.GetChild(7).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            RealtimeInfoManager.Instance.GetRange().ToString();
        // ȸ�� Ȯ��
        int evade = RealtimeInfoManager.Instance.GetEvasion();
        if (evade >= 60)
            evade = 60;
        statInfo.transform.GetChild(8).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            evade.ToString();
        // ����
        statInfo.transform.GetChild(9).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            RealtimeInfoManager.Instance.GetArmor().ToString();
        // �̵��ӵ� %
        statInfo.transform.GetChild(10).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            RealtimeInfoManager.Instance.GetMovementSpeedPercent().ToString();
        // ���
        statInfo.transform.GetChild(11).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            Mathf.FloorToInt(RealtimeInfoManager.Instance.GetLuck()).ToString();
        // ��Ȯ
        statInfo.transform.GetChild(12).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            RealtimeInfoManager.Instance.GetHarvest().ToString();
    }
}
