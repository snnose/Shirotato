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

    // 공격 관련
    private float DMGPercent = 0f;
    private float ATKSpeed = 0f;
    private float FixedDMG = 0f;
    private float Critical = 0f;
    private float Range = 0f;

    // 방어 관련
    private float HP = 10;
    private int Recovery = 0;
    private int Armor = 0;
    private int Evasion = 0;

    // 유틸 관련
    private float MovementSpeed = 10f;
    private float MovementSpeedPercent = 0f;
    private float RootingRange = 1f;
    private float Luck = 0f;
    private float expGain = 1.0f;

    // 재화 관련
    private int currentWaffle = 9999;
    public int storedWaffle = 0;   // 라운드가 끝나 미처 먹지 못한 와플 개수

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
    }

    public void SetRecovery(int recovery)
    {
        this.Recovery = recovery;
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
    }

    public void SetRootingRange(float rootingRange)
    {
        this.RootingRange = rootingRange;
    }

    public void SetLuck(float luck)
    {
        this.Luck = luck;
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

    public int GetStoredWaffle()
    {
        return this.storedWaffle;
    }

    public int GetCurrentWaffle()
    {
        return this.currentWaffle;
    }

    public float GetExpGain()
    {
        return this.expGain;
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }
}
