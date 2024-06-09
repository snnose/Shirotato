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

    // UI의 능력치를 실시간으로 적용되는 능력치로 갱신
    void RenewStatus()
    {
        // 최대 체력
        statInfo.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            RealtimeInfoManager.Instance.GetHP().ToString();
        // 회복력
        statInfo.transform.GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            RealtimeInfoManager.Instance.GetRecovery().ToString();
        // 생명력 흡수
        statInfo.transform.GetChild(2).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            RealtimeInfoManager.Instance.GetHPDrain().ToString();
        // 대미지%
        statInfo.transform.GetChild(3).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            RealtimeInfoManager.Instance.GetDMGPercent().ToString();
        // 고정 대미지
        statInfo.transform.GetChild(4).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            RealtimeInfoManager.Instance.GetFixedDMG().ToString();
        // 공격속도
        statInfo.transform.GetChild(5).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            RealtimeInfoManager.Instance.GetATKSpeed().ToString();
        // 치명타 확률
        statInfo.transform.GetChild(6).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            Mathf.FloorToInt(RealtimeInfoManager.Instance.GetCritical()).ToString();
        // 범위
        statInfo.transform.GetChild(7).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            RealtimeInfoManager.Instance.GetRange().ToString();
        // 회피 확률
        int evade = RealtimeInfoManager.Instance.GetEvasion();
        if (evade >= 60)
            evade = 60;
        statInfo.transform.GetChild(8).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            evade.ToString();
        // 방어력
        statInfo.transform.GetChild(9).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            RealtimeInfoManager.Instance.GetArmor().ToString();
        // 이동속도 %
        statInfo.transform.GetChild(10).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            RealtimeInfoManager.Instance.GetMovementSpeedPercent().ToString();
        // 행운
        statInfo.transform.GetChild(11).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            Mathf.FloorToInt(RealtimeInfoManager.Instance.GetLuck()).ToString();
        // 수확
        statInfo.transform.GetChild(12).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            RealtimeInfoManager.Instance.GetHarvest().ToString();
    }
}
