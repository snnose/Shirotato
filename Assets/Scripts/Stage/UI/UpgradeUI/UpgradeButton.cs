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

        applyUpgrade(roomNumber);

        // UpgradeUI ��Ȱ��ȭ
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
    private void applyUpgrade(int roomNumber)
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
                PlayerInfo.Instance.SetDMGPercent(PlayerInfo.Instance.GetDMGPercent() + 2 + 3 * (1 + rarity));
                break;
            case 3:
                PlayerInfo.Instance.SetFixedDMG(PlayerInfo.Instance.GetFixedDMG() + (1 + rarity));
                break;
            case 4:
                PlayerInfo.Instance.SetATKSpeed(PlayerInfo.Instance.GetATKSpeed() + 5 * (1 + rarity));
                break;
            case 5:
                PlayerInfo.Instance.SetCritical(PlayerInfo.Instance.GetCritical() + 3 * (1 + rarity));
                break;
            case 6:
                PlayerInfo.Instance.SetRange(PlayerInfo.Instance.GetRange() + 3 * (1 + rarity));
                break;
            case 7:
                PlayerInfo.Instance.SetEvasion(PlayerInfo.Instance.GetEvasion() + 3 * (1 + rarity));
                break;
            case 8:
                PlayerInfo.Instance.SetArmor(PlayerInfo.Instance.GetArmor() + (1 + rarity));
                break;
            case 9:
                PlayerInfo.Instance.SetMovementSpeedPercent(PlayerInfo.Instance.GetMovementSpeedPercent() + 3 * (1 + rarity));
                break;
            case 10:
                PlayerInfo.Instance.SetLuck(PlayerInfo.Instance.GetLuck() + 5 * (1 + rarity));
                break;
            default:
                break;
        }
    }
}
