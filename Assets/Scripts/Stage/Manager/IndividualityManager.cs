using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualityManager : MonoBehaviour
{
    // singleton
    private static IndividualityManager instance;
    public static IndividualityManager Instance
    {
        get
        {
            if (null == instance)
                return null;

            return instance;
        }
    }

    // ���� ��� (Coefficient)
    // ���� ����
    private float DMGPercentCoeff = 1.0f;
    private float ATKSpeedCoeff = 1.0f;
    private float FixedDMGCoeff = 1.0f;
    private float CriticalCoeff = 1.0f;
    private float RangeCoeff = 1.0f;

    // ��� ����
    private float HPCoeff = 1.0f;
    private float RecoveryCoeff = 1.0f;
    private float HPDrainCoeff = 1.0f;
    private float ArmorCoeff = 1.0f;
    private float EvasionCoeff = 1.0f;

    // ��ƿ ����
    private float MovementSpeedPercentCoeff = 1.0f;
    private float LuckCoeff = 1.0f;
    private float HarvestCoeff = 1.0f;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        // Ư�� �̸��� �´� ȿ���� �����Ѵ�.
        ApplyIndividuality(RoundSetting.Instance.GetIndividuality());
    }

    void Start()
    {
        
    }

    // Ư���� �����Ű�� �Լ�
    private void ApplyIndividuality(string individualityName)
    {
        switch (individualityName)
        {
            case "����":
                // ġ��Ÿ Ȯ�� ��� 1.3
                this.CriticalCoeff = 1.3f;
                // ��Ȯ ��� 0.0
                this.HarvestCoeff = 0.0f;
                // ũ��Ƽ�ð� ���� ���� 10���� ����
                this.gameObject.GetComponent<PlayerInfo>().SetCritical(10f);
                this.gameObject.GetComponent<PlayerInfo>().SetRange(10f);
                break;
            default:
                break;
        }
    }

    public float GetDMGPercentCoeff()
    {
        return this.DMGPercentCoeff;
    }

    public float GetATKSpeedCoeff()
    {
        return this.ATKSpeedCoeff;
    }

    public float GetFixedDMGCoeff()
    {
        return this.FixedDMGCoeff;
    }

    public float GetCriticalCoeff()
    {
        return this.CriticalCoeff;
    }

    public float GetRangeCoeff()
    {
        return this.RangeCoeff;
    }

    public float GetHPCoeff()
    {
        return this.HPCoeff;
    }

    public float GetRecoveryCoeff()
    {
        return this.RecoveryCoeff;
    }

    public float GetHPDrainCoeff()
    {
        return this.HPDrainCoeff;
    }

    public float GetArmorCoeff()
    {
        return this.ArmorCoeff;
    }

    public float GetEvasionCoeff()
    {
        return this.EvasionCoeff;
    }

    public float GetMovementSpeedPercentCoeff()
    {
        return this.MovementSpeedPercentCoeff;
    }

    public float GetLuckCoeff()
    {
        return this.LuckCoeff;
    }

    public float GetHarvestCoeff()
    {
        return this.HarvestCoeff;
    }
}
