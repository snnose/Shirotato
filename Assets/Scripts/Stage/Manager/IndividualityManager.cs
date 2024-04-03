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

    // 스탯 계수 (Coefficient)
    // 공격 관련
    private float DMGPercentCoeff = 1.0f;
    private float ATKSpeedCoeff = 1.0f;
    private float FixedDMGCoeff = 1.0f;
    private float CriticalCoeff = 1.0f;
    private float RangeCoeff = 1.0f;

    // 방어 관련
    private float HPCoeff = 1.0f;
    private float RecoveryCoeff = 1.0f;
    private float HPDrainCoeff = 1.0f;
    private float ArmorCoeff = 1.0f;
    private float EvasionCoeff = 1.0f;

    // 유틸 관련
    private float MovementSpeedPercentCoeff = 1.0f;
    private float LuckCoeff = 1.0f;
    private float HarvestCoeff = 1.0f;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        // 특성 이름에 맞는 효과를 적용한다.
        ApplyIndividuality(RoundSetting.Instance.GetIndividuality());
    }

    void Start()
    {
        
    }

    // 특성을 적용시키는 함수
    private void ApplyIndividuality(string individualityName)
    {
        switch (individualityName)
        {
            case "명사수":
                // 치명타 확률 계수 1.3
                this.CriticalCoeff = 1.3f;
                // 수확 계수 0.0
                this.HarvestCoeff = 0.0f;
                // 크리티컬과 범위 스탯 10으로 설정
                this.gameObject.GetComponent<PlayerInfo>().SetCritical(10f * this.CriticalCoeff);
                this.gameObject.GetComponent<PlayerInfo>().SetRange(10f * this.RangeCoeff);
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
