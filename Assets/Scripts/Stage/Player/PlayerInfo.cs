using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    // Singleton
    private static PlayerInfo instance = null;

    public static PlayerInfo Instance
    {
        get
        {
            if (null == instance)
                return null;

            return instance;
        }
    }

    // ���� ����
    private float DMGPercent = 0f;
    private float ATKSpeed = 0f;
    private float FixedDMG = 0f;
    private float Critical = 0f;
    private float Range = 0f;

    // ��� ����
    private float HP = 10f;
    private int Recovery = 0;
    private float HPDrain = 0f;
    private int Armor = 0;
    private int Evasion = 0;

    // ��ƿ ����
    public float MovementSpeed = 10f;
    private float MovementSpeedPercent = 0f;
    private float RootingRange = 1f;
    private float Luck = 0f;
    private float Harvest = 0f;
    private float expGain = 1.0f;

    // ��ȭ ����
    private int currentWaffle = 9999;
    public int storedWaffle = 0;   // ���尡 ���� ��ó ���� ���� ���� ����

    // ������ ����
    private float cappedHP = 999f;
    private float cappedMovementSpeedPercent = 999f;
    

    // EpicItem25 ���� �� ������ ���
    public void ActivateEpicItem25()
    {
        int count = ItemManager.Instance.GetOwnEpicItemList()[25];

        // ������ �븻 ��� ������ ���� ȸ���� +1
        // ������ ���� ��� ������ ���� ȸ���� -2
        // �������� ������ ���� �� ���� ȣ��
        if (count > 0)
        {
            int normalItemCount = 0;
            int legendItemCount = 0;

            for (int i = 0; i < 50; i++)
            {
                normalItemCount += ItemManager.Instance.GetOwnNormalItemList()[i];
            }

            for (int i = 0; i < 50; i++)
            {
                legendItemCount += ItemManager.Instance.GetOwnLegendItemList()[i];
            }

            this.Recovery += normalItemCount * count;
            this.Recovery -= 2 * legendItemCount * count;
        }
    }

    // EpicItem26 ���� �� ȿ�� �ߵ�
    public void ActivateEpicItem26(float cappedHP)
    {
        // EpicItem26 ���� �� ���� ������ �ִ� ü�� �̻����� ������� ����
        if (ItemManager.Instance.GetOwnEpicItemList()[26] > 0)
        {
            this.cappedHP = cappedHP;
            RealtimeInfoManager.Instance.SetCappedHP(this.cappedHP);
        }
    }

    public void ActivateEpicItem29()
    {
        // EpicItem29 ���� �� �̵��ӵ�% ��ŭ �����% ���
        if (ItemManager.Instance.GetOwnEpicItemList()[29] > 0)
        {
            float finalDamagePercent = this.DMGPercent + this.MovementSpeedPercent;
            this.DMGPercent = finalDamagePercent;
        }
    }

    // EpicItem31 ���� �� ȿ�� �ߵ�
    public void ActivateEpicItem31(float cappedMovementSpeedPercent)
    {
        // EpicItem31 ���� �� ���� ������ �ִ� ü�� �̻����� ������� ����
        if (ItemManager.Instance.GetOwnEpicItemList()[31] > 0)
        {
            this.cappedMovementSpeedPercent = cappedMovementSpeedPercent;
            RealtimeInfoManager.Instance.SetCappedMovementSpeedPercent(this.cappedMovementSpeedPercent);
        }
    }

    public void ActivateLegendItem16()
    {
        // LegendItem16 ���� �� ����� ��� 1% �� �����% +2% ���
        if (ItemManager.Instance.GetOwnLegendItemList()[16] > 0)
        {
            float finalDamagePercent = this.DMGPercent + this.HPDrain * 2f;
            this.DMGPercent = finalDamagePercent;
        }
    }

    public void ActivateLegendItem17()
    {
        // LegendItem17 ���� �� �̵��ӵ�% -1% �� ȸ���� +2 ���
        if (ItemManager.Instance.GetOwnLegendItemList()[16] > 0)
        {
            float finalRecovery = this.Recovery - this.MovementSpeedPercent * 2f;
            this.Recovery = Mathf.FloorToInt(finalRecovery);
        }
    }

    public void ActivateLegendItem22()
    {
        // LegendItem22 ���� �� ġ��Ÿ Ȯ�� +1%�� ��� +2 ���
        if (ItemManager.Instance.GetOwnLegendItemList()[22] > 0)
        {
            float finalLuck = this.Luck + this.Critical * 2f;
            this.Luck = finalLuck;
        }    
    }

    public void ActivateLegendItem23()
    {
        // LegendItem23 ���� �� ȸ�� +1%�� ���ݼӵ� +2% ���
        if (ItemManager.Instance.GetOwnLegendItemList()[23] > 0)
        {
            float finalATKSpeed = this.ATKSpeed + this.Evasion * 2f;
            this.ATKSpeed = finalATKSpeed;
        }
    }

    // EpicItem29�� �����Ű�� ���� ������ �����%�� ��������
    public void InActivateEpicItem29()
    {
        if (ItemManager.Instance.GetOwnEpicItemList()[29] > 0)
        {
            float originDamagePercent = this.DMGPercent - this.MovementSpeedPercent;
            this.DMGPercent = originDamagePercent;
        }
    }

    // LegendItem16�� �����Ű�� ���� ������ �����% ��������
    public void InActivateLegendItem16()
    {
        if (ItemManager.Instance.GetOwnLegendItemList()[16] > 0)
        {
            float originDamagePercent = this.DMGPercent - this.HPDrain * 2f;
            this.DMGPercent = originDamagePercent;
        }
    }

    // LegendItem17�� �����Ű�� ���� ������ ȸ���� ��������
    public void InActivateLegendItem17()
    {
        if (ItemManager.Instance.GetOwnLegendItemList()[16] > 0)
        {
            float originRecovery = this.Recovery + this.MovementSpeedPercent * 2f;
            this.Recovery = Mathf.FloorToInt(originRecovery);
        }
    }

    // LegendItem22�� �����Ű�� ���� ������ ��� ��������
    public void InActivateLegendItem22()
    {
        if (ItemManager.Instance.GetOwnLegendItemList()[22] > 0)
        {
            float originLuck = this.Luck - this.Critical * 2f;
            this.Luck = originLuck;
        }
    }

    // LegendItem23�� �����Ű�� ���� ������ ��� ��������
    public void InActivateLegendItem23()
    {
        if (ItemManager.Instance.GetOwnLegendItemList()[23] > 0)
        {
            float originATKSpeed = this.ATKSpeed - this.Evasion * 2f;
            this.ATKSpeed = originATKSpeed;
        }
    }

    public void SetCurrentWaffle(int currentWaffle)
    {
        this.currentWaffle = currentWaffle;
    }

    public void SetStoredWaffle(int storedWaffle)
    {
        this.storedWaffle = storedWaffle;
    }

    public void SetDMGPercent(float dmgPercent)
    {
        this.DMGPercent = dmgPercent;
    }

    public void SetATKSpeed(float ATKSpeed)
    {
        this.ATKSpeed = ATKSpeed;
    }

    public void SetFixedDMG(float fixedDamage)
    {
        this.FixedDMG = fixedDamage;
    }

    public void SetCritical(float critical)
    {
        this.Critical = critical;
    }

    public void SetRange(float range)
    {
        this.Range = range;
    }

    public void SetHP(float HP)
    {
        this.HP = HP;
        // �ִ� ���� ġ���� ���� Ŀ�� �� ����
        if (this.HP > cappedHP)
            this.HP = cappedHP;
    }

    public void SetRecovery(int recovery)
    {
        this.Recovery = recovery;
    }

    public void SetHPDrain(float hpDrain)
    {
        this.HPDrain = hpDrain;
    }

    public void SetArmor(int armor)
    {
        this.Armor = armor;
    }

    public void SetEvasion(int evasion)
    {
        this.Evasion = evasion;
    }

    public void SetMovementSpeed(float movementSpeed)
    {
        this.MovementSpeed = movementSpeed;
    }

    public void SetMovementSpeedPercent(float movementSpeedPercent)
    {
        this.MovementSpeedPercent = movementSpeedPercent;
        // �ִ� ���� ġ���� ���� Ŀ�� �� ����
        if (this.MovementSpeedPercent > this.cappedMovementSpeedPercent)
            this.MovementSpeedPercent = this.cappedMovementSpeedPercent;
    }

    public void SetRootingRange(float rootingRange)
    {
        this.RootingRange = rootingRange;
    }

    public void SetLuck(float luck)
    {
        this.Luck = luck;
    }

    public void SetHarvest(float harvest)
    {
        this.Harvest = harvest;
    }

    public void SetExpGain(float expGain)
    {
        this.expGain = expGain;
    }

    public float GetDMGPercent()
    {
        return this.DMGPercent;
    }

    public float GetATKSpeed()
    {
        return this.ATKSpeed;
    }

    public float GetFixedDMG()
    {
        return this.FixedDMG;
    }

    public float GetRange()
    {
        return this.Range;
    }

    public float GetCritical()
    {
        return this.Critical;
    }

    public float GetHP()
    {
        return this.HP;
    }

    public int GetRecovery()
    {
        return this.Recovery;
    }

    public float GetHPDrain()
    {
        return this.HPDrain;
    }

    public int GetArmor()
    {
        return this.Armor;
    }

    public int GetEvasion()
    {
        return this.Evasion;
    }

    public float GetMovementSpeed()
    {
        return this.MovementSpeed;
    }

    public float GetMovementSpeedPercent()
    {
        return this.MovementSpeedPercent;
    }

    public float GetRootingRange()
    {
        return this.RootingRange;
    }

    public float GetLuck()
    {
        return this.Luck;
    }

    public float GetHarvest()
    {
        return this.Harvest;
    }

    public float GetExpGain()
    {
        return this.expGain;
    }

    public int GetStoredWaffle()
    {
        return this.storedWaffle;
    }

    public int GetCurrentWaffle()
    {
        return this.currentWaffle;
    }

    

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }
}
