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
                name = "최대 체력";
                break;
            case "Recovery":
                name = "회복력";
                break;
            case "HPDrain":
                name = "생명력 흡수";
                break;
            case "DamagePercent":
                name = "대미지%";
                break;
            case "DamageFixed":
                name = "고정 대미지";
                break;
            case "ATKSpeed":
                name = "공격속도";
                break;
            case "Critical":
                name = "치명타 확률";
                break;
            case "Range%":
                name = "범위%";
                break;
            case "Evasion":
                name = "회피";
                break;
            case "Armor":
                name = "방어력";
                break;
            case "MovementSpeed%":
                name = "이동속도%";
                break;
            case "Luck":
                name = "행운";
                break;
            case "Harvest":
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
            case "HP":
                detail = "최대 " + playerInfo.GetHP() + "만큼의 대미지를 받을 수 있습니다.";
                break;
            case "Recovery":
                detail = "매 10초마다 체력 " + playerInfo.GetRecovery() + "을 회복합니다. \n" +
                         "(체력 " + playerInfo.GetRecovery() / 10 + "/s)";
                break;
            case "HPDrain":
                detail = "공격 시 " + playerInfo.GetHPDrain() + "% 확률로 체력 1을 회복합니다.";
                break;
            case "DamagePercent":
                detail = "공격 시 대미지가 " + playerInfo.GetDMGPercent() + "% 증가합니다.";
                break;
            case "DamageFixed":
                detail = "공격 시 대미지가 " + playerInfo.GetFixedDMG() + " 증가합니다";
                break;
            case "ATKSpeed":
                detail = "공격 주기가 " + playerInfo.GetATKSpeed() + "% 더 빨라집니다.";
                break;
            case "Critical":
                detail = "공격 시 " + playerInfo.GetCritical() + "% 확률로 2배 증가된 피해를 입힙니다.";
                break;
            case "Range%":
                detail = "공격 사거리가 " + playerInfo.GetRange() + "% 증가합니다.";
                break;
            case "Evasion":
                detail = playerInfo.GetEvasion() + "% 확률로 공격을 회피합니다.";
                break;
            case "Armor":
                detail = "받는 피해가 " + Mathf.FloorToInt(playerInfo.GetArmor() / (Mathf.Abs(playerInfo.GetArmor()) + 10)) + "% 감소합니다.";
                break;
            case "MovementSpeed%":
                detail = playerInfo.GetMovementSpeedPercent() + "% 더 빠르게 이동합니다.";
                break;
            case "Luck":
                detail = "적 처치 시 상자와 우유 드랍 확률이 " + playerInfo.GetLuck() + "% 증가합니다. " +
                         "또한 상점 품목과 레벨 업 시 높은 등급이 자주 등장합니다.";
                break;
            case "Harvest":
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
