using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopStatusControl : MonoBehaviour
{
    public GameObject statInfo;

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
        // 대미지%
        statInfo.transform.GetChild(2).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            PlayerInfo.Instance.GetDMGPercent().ToString();
        // 고정 대미지
        statInfo.transform.GetChild(3).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            PlayerInfo.Instance.GetFixedDMG().ToString();
        // 공격속도
        statInfo.transform.GetChild(4).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            PlayerInfo.Instance.GetATKSpeed().ToString();
        // 치명타 확률
        statInfo.transform.GetChild(5).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            PlayerInfo.Instance.GetCritical().ToString();
        // 범위
        statInfo.transform.GetChild(6).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            PlayerInfo.Instance.GetRange().ToString();
        // 회피 확률
        statInfo.transform.GetChild(7).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            PlayerInfo.Instance.GetEvasion().ToString();
        // 방어력
        statInfo.transform.GetChild(8).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            PlayerInfo.Instance.GetArmor().ToString();
        // 이동속도
        statInfo.transform.GetChild(9).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            PlayerInfo.Instance.GetMovementSpeed().ToString();
        // 행운
        statInfo.transform.GetChild(10).GetChild(2).GetComponent<TextMeshProUGUI>().text =
            PlayerInfo.Instance.GetLuck().ToString();
    }
}
