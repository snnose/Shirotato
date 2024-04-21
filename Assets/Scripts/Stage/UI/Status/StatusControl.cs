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

    // 상점 UI의 능력치를 갱신
    void RenewStatus()
    {
        // 최대 체력
        statInfo.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text =
                                                        PlayerInfo.Instance.GetHP().ToString();
        // 회복력
        statInfo.transform.GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            PlayerInfo.Instance.GetRecovery().ToString();
        // 생명력 흡수
        statInfo.transform.GetChild(2).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            PlayerInfo.Instance.GetHPDrain().ToString();
        // 대미지%
        statInfo.transform.GetChild(3).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            Mathf.FloorToInt(PlayerInfo.Instance.GetDMGPercent()).ToString();
        // 고정 대미지
        statInfo.transform.GetChild(4).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            PlayerInfo.Instance.GetFixedDMG().ToString();
        // 공격속도
        statInfo.transform.GetChild(5).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            PlayerInfo.Instance.GetATKSpeed().ToString();
        // 치명타 확률
        statInfo.transform.GetChild(6).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            PlayerInfo.Instance.GetCritical().ToString();
        // 범위
        statInfo.transform.GetChild(7).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            PlayerInfo.Instance.GetRange().ToString();
        // 회피 확률
        statInfo.transform.GetChild(8).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            PlayerInfo.Instance.GetEvasion().ToString();
        // 방어력
        statInfo.transform.GetChild(9).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            PlayerInfo.Instance.GetArmor().ToString();
        // 이동속도 %
        statInfo.transform.GetChild(10).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            PlayerInfo.Instance.GetMovementSpeedPercent().ToString();
        // 행운
        statInfo.transform.GetChild(11).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            Mathf.FloorToInt(PlayerInfo.Instance.GetLuck()).ToString();
        // 수확
        statInfo.transform.GetChild(12).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            PlayerInfo.Instance.GetHarvest().ToString();
    }
}
