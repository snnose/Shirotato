using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public void OnClickUpgradeButton()
    {
        float buttonPosX = this.gameObject.transform.position.x;

        int roomNumber = -1;

        switch(buttonPosX)
        {
            case 143:
                roomNumber = 0;
                break;
            case 393:
                roomNumber = 1;
                break;
            case 643:
                roomNumber = 2;
                break;
            case 893:
                roomNumber = 3;
                break;
            default:
                break;
        }
        // 특정 스탯에 비례해서 스탯이 오르는 아이템들 처리
        // EpicItem29 비활성화
        PlayerInfo.Instance.InActivateEpicItem29();
        // LegendItem16 비활성화
        PlayerInfo.Instance.InActivateLegendItem16();
        // LegendItem17 비활성화
        PlayerInfo.Instance.InActivateLegendItem17();
        // LegendItem22 비활성화
        PlayerInfo.Instance.InActivateLegendItem22();
        // LegendItem23 비활성화
        PlayerInfo.Instance.InActivateLegendItem23();

        ApplyUpgrade(roomNumber);

        // 특정 스탯에 비례해서 스탯이 오르는 아이템들 처리
        // EpicItem29 활성화
        PlayerInfo.Instance.ActivateEpicItem29();
        // LegendItem16 활성화
        PlayerInfo.Instance.ActivateLegendItem16();
        // LegendItem17 활성화
        PlayerInfo.Instance.ActivateLegendItem17();
        // LegendItem22 활성화
        PlayerInfo.Instance.ActivateLegendItem22();
        // LegendItem23 활성화
        PlayerInfo.Instance.ActivateLegendItem23();

        // UpgradeUI 비활성화 및 화면 밖으로 이동
        UpgradeManager.Instance.transform.position = new Vector2(Screen.width * (-1.0f), Screen.height * (-1.0f));
        UpgradeManager.Instance.gameObject.SetActive(false);
        // GameRoot의 levelUpCount 감소
        int levelUpCount = GameRoot.Instance.GetLevelUpCount();
        GameRoot.Instance.SetLevelUpCount(levelUpCount--);
        // GameRoot의 isDuringUpgrade 해제
        GameRoot.Instance.SetIsDuringUpgrade(false);
        // GameRoot의 floatingUpgradeUI 코루틴 장전
        GameRoot.Instance.floatingUpgradeUI = GameRoot.Instance.FloatingUpgradeUI();
    }

    // 해당 업그레이드 칸에 맞는 능력치를 상승시켜 적용한다
    private void ApplyUpgrade(int roomNumber)
    {
        IndividualityManager individualityManager = IndividualityManager.Instance;

        (int, int) upgradeInfo = UpgradeManager.Instance.GetUpgradeList()[roomNumber];
        int rarity = upgradeInfo.Item2;

        switch (upgradeInfo.Item1)
        {
            case 0:
                PlayerInfo.Instance.SetHP(PlayerInfo.Instance.GetHP() + 
                                                3 * (1 + rarity) * individualityManager.GetHPCoeff());
                break;
            case 1:
                PlayerInfo.Instance.SetRecovery(PlayerInfo.Instance.GetRecovery() +
                                                 Mathf.FloorToInt((2 + rarity) * individualityManager.GetRecoveryCoeff()));
                break;
            case 2:
                PlayerInfo.Instance.SetHPDrain(PlayerInfo.Instance.GetHPDrain() +
                                                1 + rarity * individualityManager.GetHPDrainCoeff());
                break;
            case 3:
                PlayerInfo.Instance.SetDMGPercent(PlayerInfo.Instance.GetDMGPercent() + 2 + 
                                                3 * (1 + rarity) * individualityManager.GetDMGPercentCoeff());
                break;
            case 4:
                PlayerInfo.Instance.SetFixedDMG(PlayerInfo.Instance.GetFixedDMG() +
                                                (1 + rarity) * individualityManager.GetFixedDMGCoeff());
                break;
            case 5:
                PlayerInfo.Instance.SetATKSpeed(PlayerInfo.Instance.GetATKSpeed() + 
                                                5 * (1 + rarity) * individualityManager.GetATKSpeedCoeff());
                break;
            case 6:
                PlayerInfo.Instance.SetCritical(PlayerInfo.Instance.GetCritical() + 
                                                3 * (1 + rarity) * individualityManager.GetCriticalCoeff());
                break;
            case 7:
                PlayerInfo.Instance.SetRange(PlayerInfo.Instance.GetRange() + 
                                                3 * (1 + rarity) * individualityManager.GetRangeCoeff());
                break;
            case 8:
                PlayerInfo.Instance.SetEvasion(PlayerInfo.Instance.GetEvasion() + 
                                                Mathf.FloorToInt(3 * (1 + rarity) * individualityManager.GetEvasionCoeff()));
                break;
            case 9:
                PlayerInfo.Instance.SetArmor(PlayerInfo.Instance.GetArmor() + 
                                                Mathf.FloorToInt((1 + rarity) * individualityManager.GetArmorCoeff()));
                break;
            case 10:
                PlayerInfo.Instance.SetMovementSpeedPercent(PlayerInfo.Instance.GetMovementSpeedPercent() + 
                                                3 * (1 + rarity) * individualityManager.GetMovementSpeedPercentCoeff());
                break;
            case 11:
                PlayerInfo.Instance.SetLuck(PlayerInfo.Instance.GetLuck() + 
                                                5 * (1 + rarity) * individualityManager.GetLuckCoeff());
                break;
            case 12:
                PlayerInfo.Instance.SetHarvest(PlayerInfo.Instance.GetHarvest() + 5 + 
                                                (3 * rarity) * individualityManager.GetHarvestCoeff());
                break;
            default:
                break;
        }
    }
}
