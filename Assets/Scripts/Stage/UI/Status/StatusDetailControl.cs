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
            case "최대체력":
                name = "최대 체력";
                break;
            case "회복력":
                name = "회복력";
                break;
            case "생명력흡수":
                name = "생명력 흡수";
                break;
            case "대미지%":
                name = "대미지%";
                break;
            case "추가대미지":
                name = "추가 대미지";
                break;
            case "공격속도":
                name = "공격속도";
                break;
            case "치명타확률":
                name = "치명타 확률";
                break;
            case "범위":
                name = "범위%";
                break;
            case "회피":
                name = "회피";
                break;
            case "방어력":
                name = "방어력";
                break;
            case "이동속도":
                name = "이동속도%";
                break;
            case "행운":
                name = "행운";
                break;
            case "수확":
                name = "수확";
                break;
            default:
                break;
        }

        return name;
    }

    private string SetDetailProText(string statName)
    {
        // 플레이어 정보
        PlayerInfo playerInfo = PlayerInfo.Instance;
        string detail = "";

        switch (statName)
        {
            case "최대체력":
                detail = "최대 " + playerInfo.GetHP() + "만큼의 대미지를 받을 수 있습니다.";
                break;
            case "회복력":
                detail = "매 10초마다 체력 " + playerInfo.GetRecovery() + "을 회복합니다. \n" +
                         "(체력 " + playerInfo.GetRecovery() / 10 + "/s)";
                // 회복력 -4 ~ 0일 때
                if (playerInfo.GetRecovery() <= 0)
                {
                    detail = "매 10초마다 체력 " + 0 + "을 회복합니다. \n" +
                         "(체력 " + 0 + "/s)";
                }
                
                // 회복력 -5 이하일 때
                if (playerInfo.GetRecovery() <= -5f)
                {
                    detail = "매 10초마다 체력 " + -5 + " 감소합니다. \n" +
                         "(체력 " + -0.5 + "/s)";
                }
                
                break;
            case "생명력흡수":
                detail = "공격 시 " + playerInfo.GetHPDrain() + "% 확률로 체력 1을 회복합니다.";
                break;
            case "대미지%":
                detail = "공격 시 대미지가 " + Mathf.FloorToInt(playerInfo.GetDMGPercent() * 10f) / 10 + "% 증가합니다.";
                break;
            case "추가대미지":
                detail = "공격 시 대미지가 증가합니다. \n" +
                         "(무기 계수 x " + playerInfo.GetFixedDMG() + ")";
                break;
            case "공격속도":
                detail = "공격 주기가 " + Mathf.FloorToInt(playerInfo.GetATKSpeed() * 10f) / 10 + "% 더 빨라집니다.";
                break;
            case "치명타확률":
                detail = "공격 시 " + Mathf.FloorToInt(playerInfo.GetCritical() * 10f) / 10 + "% 확률로 2배 증가된 피해를 입힙니다.";
                break;
            case "범위":
                detail = "공격 사거리가 " + playerInfo.GetRange() + "% 증가합니다.";
                break;
            case "회피":
                detail = playerInfo.GetEvasion() + "% 확률로 공격을 회피합니다 (최대 60%).";
                break;
            case "방어력":
                float tmp = 100 * playerInfo.GetArmor() / (Mathf.Abs(playerInfo.GetArmor()) + 10);
                detail = "받는 피해가 " + tmp + "% 감소합니다.";
                break;
            case "이동속도":
                detail = playerInfo.GetMovementSpeedPercent() + "% 더 빠르게 이동합니다.";
                break;
            case "행운":
                detail = "적 처치 시 상자와 우유 드랍 확률이 " + Mathf.FloorToInt(playerInfo.GetLuck() * 10f) / 10 + "% 증가합니다. " +
                         "또한 상점 품목과 레벨 업 시 높은 등급이 자주 등장합니다.";
                break;
            case "수확":
                detail = "라운드 종료 시 " + playerInfo.GetHarvest() + "만큼의 와플과 경험치를 얻습니다.\n" +
                         "매 라운드 시작 시 5%만큼 증가합니다.";
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
