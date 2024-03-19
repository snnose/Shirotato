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
            case 158:
                roomNumber = 0;
                break;
            case 408:
                roomNumber = 1;
                break;
            case 658:
                roomNumber = 2;
                break;
            case 908:
                roomNumber = 3;
                break;
            default:
                break;
        }

        // EpicItem29 ��Ȱ��ȭ
        PlayerInfo.Instance.InActivateEpicItem29();
        ApplyUpgrade(roomNumber);
        // EpicItem29 Ȱ��ȭ
        PlayerInfo.Instance.ActivateEpicItem29();

        // UpgradeUI ��Ȱ��ȭ �� ȭ�� ������ �̵�
        UpgradeManager.Instance.transform.position = new Vector2(Screen.width * (-1.0f), Screen.height * (-1.0f));
        UpgradeManager.Instance.gameObject.SetActive(false);
        // GameRoot�� levelUpCount ����
        int levelUpCount = GameRoot.Instance.GetLevelUpCount();
        GameRoot.Instance.SetLevelUpCount(levelUpCount--);
        // GameRoot�� isDuringUpgrade ����
        GameRoot.Instance.SetIsDuringUpgrade(false);
        // GameRoot�� floatingUpgradeUI �ڷ�ƾ ����
        GameRoot.Instance.floatingUpgradeUI = GameRoot.Instance.FloatingUpgradeUI();
    }

    // �ش� ���׷��̵� ĭ�� �´� �ɷ�ġ�� ��½��� �����Ѵ�
    private void ApplyUpgrade(int roomNumber)
    {
        (int, int) upgradeInfo = UpgradeManager.Instance.GetUpgradeList()[roomNumber];
        int rarity = upgradeInfo.Item2;

        switch (upgradeInfo.Item1)
        {
            case 0:
                PlayerInfo.Instance.SetHP(PlayerInfo.Instance.GetHP() + 3 * (1 + rarity));
                break;
            case 1:
                PlayerInfo.Instance.SetRecovery(PlayerInfo.Instance.GetRecovery() + 2 + rarity);
                break;
            case 2:
                PlayerInfo.Instance.SetHPDrain(PlayerInfo.Instance.GetHPDrain() + 1 + rarity);
                break;
            case 3:
                PlayerInfo.Instance.SetDMGPercent(PlayerInfo.Instance.GetDMGPercent() + 2 + 3 * (1 + rarity));
                break;
            case 4:
                PlayerInfo.Instance.SetFixedDMG(PlayerInfo.Instance.GetFixedDMG() + (1 + rarity));
                break;
            case 5:
                PlayerInfo.Instance.SetATKSpeed(PlayerInfo.Instance.GetATKSpeed() + 5 * (1 + rarity));
                break;
            case 6:
                PlayerInfo.Instance.SetCritical(PlayerInfo.Instance.GetCritical() + 3 * (1 + rarity));
                break;
            case 7:
                PlayerInfo.Instance.SetRange(PlayerInfo.Instance.GetRange() + 3 * (1 + rarity));
                break;
            case 8:
                PlayerInfo.Instance.SetEvasion(PlayerInfo.Instance.GetEvasion() + 3 * (1 + rarity));
                break;
            case 9:
                PlayerInfo.Instance.SetArmor(PlayerInfo.Instance.GetArmor() + (1 + rarity));
                break;
            case 10:
                PlayerInfo.Instance.SetMovementSpeedPercent(PlayerInfo.Instance.GetMovementSpeedPercent() + 3 * (1 + rarity));
                break;
            case 11:
                PlayerInfo.Instance.SetLuck(PlayerInfo.Instance.GetLuck() + 5 * (1 + rarity));
                break;
            case 12:
                PlayerInfo.Instance.SetHarvest(PlayerInfo.Instance.GetHarvest() + 5 + (3 * rarity));
                break;
            default:
                break;
        }
    }
}
