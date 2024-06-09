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
        // 각 업그레이드 UI 칸을 리스트에 추가
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
        // 선정된 업그레이드 목록을 불러온다
        List<(int, int)> upgradeList = UpgradeManager.Instance.GetUpgradeList();

        for (int i = 0; i < 4; i++)
        {
            (int, int) currentUpgrade = upgradeList[i];
            SetUpgradeRoomInfo(upgradeRoomList[i], currentUpgrade.Item1, currentUpgrade.Item2);
        }
        yield return null;
    }

    // 현재 업그레이드에 맞는 정보를 출력한다
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
            // 파랑색
            case 1:
                color = new Color(120, 166, 214);
                ColorUtility.TryParseHtmlString("#78A6D6", out color);
                break;
            // 보라색
            case 2:
                color = new Color(161, 120, 214);
                ColorUtility.TryParseHtmlString("#A178D6", out color);
                break;
            // 주황색
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
                upgradeName.text = "심장";
                upgradeStatus.text = "최대 체력 +" + 3 * (1 + rarity);
                break;
            case 1:
                //upgradeImage = Resources.Load("");
                upgradeName.text = "폐";
                upgradeStatus.text = "회복력 +" + (2 + rarity);
                break;
            case 2:
                //upgradeImage = Resources.Load("");
                upgradeName.text = "이";
                upgradeStatus.text = "생명력 흡수% +" + (1 + rarity);
                break;
            case 3:
                //upgradeImage = Resources.Load("");
                upgradeName.text = "팔";
                upgradeStatus.text = "대미지% +" + 5 * (1 + rarity);
                break;
            case 4:
                //upgradeImage = Resources.Load("");
                upgradeName.text = "어깨";
                upgradeStatus.text = "추가 대미지 +" + (1 + rarity);
                break;
            case 5:
                //upgradeImage = Resources.Load("");
                upgradeName.text = "반사신경";
                upgradeStatus.text = "공격속도% +" + 5 * (1 + rarity);
                break;
            case 6:
                //upgradeImage = Resources.Load("");
                upgradeName.text = "손가락";
                upgradeStatus.text = "치명타 확률 +" + 3 * (1 + rarity);
                break;
            case 7:
                //upgradeImage = Resources.Load("");
                upgradeName.text = "눈";
                upgradeStatus.text = "범위% +" + 3 * (1 + rarity);
                break;
            case 8:
                //upgradeImage = Resources.Load("");
                upgradeName.text = "등";
                upgradeStatus.text = "회피 확률 +" + 3 * (1 + rarity);
                break;
            case 9:
                //upgradeImage = Resources.Load("");
                upgradeName.text = "흉부";
                upgradeStatus.text = "방어력 +" + (1 + rarity);
                break;
            case 10:
                //upgradeImage = Resources.Load("");
                upgradeName.text = "다리";
                upgradeStatus.text = "이동속도% +" + 3 * (1 + rarity);
                break;
            case 11:
                //upgradeImage = Resources.Load("");
                upgradeName.text = "귀";
                upgradeStatus.text = "행운 +" + 5 * (1 + rarity);
                break;
            case 12:
                //upgradeImage = Resources.Load("");
                upgradeName.text = "손";
                upgradeStatus.text = "수확 +" + (5 + 3 * rarity);
                break;
            default:
                break;
        }
    }
}
