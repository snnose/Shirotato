using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemListControl : MonoBehaviour
{
    public List<GameObject> itemList;

    public bool isRenewItemInfo = false;
    //private WeaponInfo weaponInfo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        RenewItemInfo();
    }

    public void RenewItemInfo()
    {
        // 상점 아이템 리스트가 갱신 됐다면
        if (ItemManager.Instance.GetIsRenewItem() && !isRenewItemInfo)
        {
            // 각 슬롯에 아이템 정보를 기록한다.
            for (int i = 0; i < 4; i++)
            {
                GameObject currentThing = ItemManager.Instance.GetShopItemList()[i];
                // 무기일 경우 (해당 칸에 무기 정보가 있으면 무기)
                if (ItemManager.Instance.GetShopWeaponInfoList()[i] != null)
                {
                    WeaponInfo weaponInfo = ItemManager.Instance.GetShopWeaponInfoList()[i];
                    // 무기 등급 이미지 변경
                    itemList[i].transform.GetChild(0).GetComponent<Image>().color =
                        DecideGradeColor(weaponInfo.GetWeaponGrade());
                    // 무기 이미지 변경
                    itemList[i].transform.GetChild(1).GetComponent<Image>().sprite =
                        currentThing.GetComponent<SpriteRenderer>().sprite;
                    // 이름 텍스트 변경
                    itemList[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text =
                        weaponInfo.weaponName;

                    SetWeaponInfoText(i);
                }
                // 아이템일 경우
                else
                {
                    // 아이템 등급 이미지 변경
                    itemList[i].transform.GetChild(0).GetComponent<Image>().color =
                    currentThing.transform.GetChild(0).GetComponent<SpriteRenderer>().color;

                    // 아이템 이미지 변경
                    itemList[i].transform.GetChild(1).GetComponent<Image>().sprite =
                        currentThing.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite;
                    // 아이템 이름 변경
                    itemList[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text =
                        currentThing.GetComponent<ItemInfo>().itemName;

                    SetItemInfoText(i);
                }
            }

            isRenewItemInfo = true;
        }
    }

    void SetWeaponInfoText(int i)
    {
        TextMeshProUGUI weaponInfoText = itemList[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        WeaponInfo weaponInfo = ItemManager.Instance.GetShopWeaponInfoList()[i];
        int damage = Mathf.FloorToInt(
            (weaponInfo.damage + Mathf.FloorToInt(PlayerInfo.Instance.GetFixedDMG()))
                                                * ((PlayerInfo.Instance.GetDMGPercent() + 100) / 100));

        float coolDown = weaponInfo.coolDown - weaponInfo.coolDown * PlayerInfo.Instance.GetATKSpeed() / (100 + PlayerInfo.Instance.GetATKSpeed());
        float atkSpeed = Mathf.Round(1 / coolDown * 100) / 100;
        float range = Mathf.Floor(weaponInfo.range * ((PlayerInfo.Instance.GetRange() + 100) / 100) * 100) / 100;

        weaponInfoText.text = "대미지 : " + damage + '\n' +
                              "공격속도 : " + atkSpeed + "/s \n" +
                              "범위 : " + range;

        TextMeshProUGUI weaponPrice = itemList[i].transform.GetChild(4).GetChild(2).GetComponent<TextMeshProUGUI>();
        // 무기의 기본 가격 + 현재 라운드 + (무기의 기본 가격 * 현재 라운드) / 10
        int price = Mathf.FloorToInt(weaponInfo.price + GameRoot.Instance.GetCurrentRound() +
                                    (weaponInfo.price * GameRoot.Instance.GetCurrentRound() / 10) *
                                    (1f - 0.05f * ItemManager.Instance.GetOwnNormalItemList()[39]));

        weaponPrice.text = price.ToString();
    }

    // 아이템의 능력치와 가격 텍스트를 설정한다.
    void SetItemInfoText(int i)
    {
        TextMeshProUGUI itemInfoText = itemList[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        ItemInfo itemInfo = ItemManager.Instance.GetShopItemList()[i].GetComponent<ItemInfo>();
        itemInfoText.text = "";

        TextMeshProUGUI itemPrice = itemList[i].transform.GetChild(4).GetChild(2).GetComponent<TextMeshProUGUI>();
        itemPrice.text = Mathf.FloorToInt(itemInfo.price + GameRoot.Instance.GetCurrentRound() +
                                         (itemInfo.price * GameRoot.Instance.GetCurrentRound() / 10) *
                                         (1f - 0.05f * ItemManager.Instance.GetOwnNormalItemList()[39])).ToString();

        // 아이템 스텟 정보를 모두 담는다
        string tmpText = "";
        int plusCount = 0;
        int minusCount = 0;

        // 공격 관련
        if (itemInfo.DMGPercent > 0)
        {
            tmpText += "대미지 +" + itemInfo.DMGPercent + "%\n";
            plusCount++;
        }
        if (itemInfo.ATKSpeed > 0)
        {
            tmpText += "공격속도 +" + itemInfo.ATKSpeed + "%\n";
            plusCount++;
        }
        if (itemInfo.FixedDMG > 0)
        {
            tmpText += "고정 대미지 +" + itemInfo.FixedDMG + '\n';
            plusCount++;
        }
        if (itemInfo.Critical > 0)
        {
            tmpText += "치명타 확률+" + itemInfo.Critical + "%\n";
            plusCount++;
        }
        if (itemInfo.Range > 0)
        {
            tmpText += "범위 +" + itemInfo.Range + "%\n";
            plusCount++;
        }

        // 방어 관련
        if (itemInfo.HP > 0)
        {
            tmpText += "최대 체력 +" + itemInfo.HP + '\n';
            plusCount++;
        }
        if (itemInfo.Recovery > 0)
        {
            tmpText += "회복력 +" + itemInfo.Recovery + '\n';
            plusCount++;
        }
        if (itemInfo.HPDrain > 0)
        {
            tmpText += "생명력 흡수 +" + itemInfo.HPDrain + "%\n";
            plusCount++;
        }
        if (itemInfo.Armor > 0)
        {
            tmpText += "방어력 +" + itemInfo.Armor + '\n';
            plusCount++;
        }
        if (itemInfo.Evasion > 0)
        {
            tmpText += "회피 확률 +" + itemInfo.Evasion + "%\n";
            plusCount++;
        }

        // 유틸 관련
        if (itemInfo.MovementSpeedPercent > 0)
        {
            tmpText += "이동속도 +" + itemInfo.MovementSpeedPercent + "%\n";
            plusCount++;
        }
        if (itemInfo.RootingRange > 0)
        {
            tmpText += "획득 범위 +" + itemInfo.RootingRange + "%\n";
            plusCount++;
        }
        if (itemInfo.Luck > 0)
        {
            tmpText += "행운 +" + itemInfo.Luck + '\n';
            plusCount++;
        }
        if (itemInfo.Harvest > 0)
        {
            tmpText += "수확 +" + itemInfo.Harvest + '\n';
            plusCount++;
        }
        if (itemInfo.ExpGain > 0)
        {
            tmpText += "경험치 획득 " + itemInfo.ExpGain + "%\n";
            plusCount++;
        }

        // 긍정적 특수 효과
        if (itemInfo.positiveSpecial != "")
        {
            tmpText += itemInfo.positiveSpecial + "\n";
            plusCount++;
        }

        // 공격 관련
        if (itemInfo.DMGPercent < 0)
        {
            tmpText += "대미지 " + itemInfo.DMGPercent + "%\n";
            minusCount++;
        }
        if (itemInfo.ATKSpeed < 0)
        {
            tmpText += "공격속도 " + itemInfo.ATKSpeed + "%\n";
            minusCount++;
        }
        if (itemInfo.FixedDMG < 0)
        {
            tmpText += "고정 대미지 " + itemInfo.FixedDMG + '\n';
            minusCount++;
        }
        if (itemInfo.Critical < 0)
        {
            tmpText += "치명타 확률" + itemInfo.Critical + "%\n";
            minusCount++;
        }
        if (itemInfo.Range < 0)
        {
            tmpText += "범위 " + itemInfo.Range + "%\n";
            minusCount++;
        }

        // 방어 관련
        if (itemInfo.HP < 0)
        {
            tmpText += "최대 체력 " + itemInfo.HP + '\n';
            minusCount++;
        }
        if (itemInfo.Recovery < 0)
        {
            tmpText += "회복력 " + itemInfo.Recovery + '\n';
            minusCount++;
        }
        if (itemInfo.HPDrain < 0)
        {
            tmpText += "생명력 흡수 " + itemInfo.HPDrain + "%\n";
            minusCount++;
        }
        if (itemInfo.Armor < 0)
        {
            tmpText += "방어력 " + itemInfo.Armor + '\n';
            minusCount++;
        }
        if (itemInfo.Evasion < 0)
        {
            tmpText += "회피 확률 " + itemInfo.Evasion + "%\n";
            minusCount++;
        }

        // 유틸 관련
        if (itemInfo.MovementSpeedPercent < 0)
        {
            tmpText += "이동속도 " + itemInfo.MovementSpeedPercent + "%\n";
            minusCount++;
        }
        if (itemInfo.RootingRange < 0)
        {
            tmpText += "획득 범위 " + itemInfo.RootingRange + "%\n";
            minusCount++;
        }
        if (itemInfo.Luck < 0)
        {
            tmpText += "행운 " + itemInfo.Luck + '\n';
            minusCount++;
        }
        if (itemInfo.Harvest < 0)
        {
            tmpText += "수확 " + itemInfo.Harvest + '\n';
            minusCount++;
        }
        if (itemInfo.ExpGain < 0)
        {
            tmpText += "경험치 획득 " + itemInfo.ExpGain + "%\n";
            minusCount++;
        }

        // 부정적 특수 효과
        if (itemInfo.negativeSpecial != "")
        {
            tmpText += itemInfo.negativeSpecial + "\n";
            minusCount++;
        }

        // 텍스트를 각 라인으로 나눈다
        string[] lines = tmpText.Split('\n');
        string finalText = ""; // 최종 텍스트

        // 능력치가 상승하면 텍스트를 초록색으로 변경
        for (int j = 0; j < plusCount; j++)
        {
            string coloredLine = "";
            // +와 숫자만 색을 변경한다
            for (int k = 0; k < lines[j].Length; k++)
            {
                // #1FDE38 << 진한 초록색
                if (lines[j][k] == '+' || lines[j][k] == '%')
                    coloredLine += $"<color=#1FDE38>{lines[j][k]}</color>";
                else if (lines[j][k] > 47 && lines[j][k] < 58)
                    coloredLine += $"<color=#1FDE38>{lines[j][k]}</color>";
                else
                    coloredLine += lines[j][k];
            }

            // 최종 텍스트에 추가
            finalText += coloredLine;
            finalText += "\n";
        }

        // 능력치가 하락하면 텍스트를 빨간색으로 변경
        for (int j = plusCount; j < plusCount + minusCount; j++)
        {
            string coloredLine = "";

            // -와 숫자만 색을 변경한다
            for (int k = 0; k < lines[j].Length; k++)
            {
                if (lines[j][k] == '-' || lines[j][k] == '%')
                    coloredLine += $"<color=#{ColorUtility.ToHtmlStringRGB(Color.red)}>{lines[j][k]}</color>";
                else if (lines[j][k] > 47 && lines[j][k] < 58)
                    coloredLine += $"<color=#{ColorUtility.ToHtmlStringRGB(Color.red)}>{lines[j][k]}</color>";
                else
                    coloredLine += lines[j][k];
            }

            // 최종 텍스트에 추가
            finalText += coloredLine;
            finalText += "\n";
        }

        // TextMeshProUGUI에 할당
        itemInfoText.text = finalText;
    }

    // rank에 따라 랭크 색깔을 반환하는 함수 (흰, 파, 보, 주)
    private Color DecideGradeColor(int grade)
    {
        Color color = Color.white;
        switch (grade)
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

        return color;
    }

    public void SetItemListActive()
    {
        for(int i = 0; i < 4; i++)
        {
            itemList[i].SetActive(true);
        }
    }

    public void SetIsRenewInfo(bool ret)
    {
        this.isRenewItemInfo = ret;
    }
}
