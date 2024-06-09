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
            case "우다다다":
                // 대미지 계수 1.5
                this.DMGPercentCoeff = 1.5f;
                // 공격속도 +100%, 이동속도 +15%, 대미지 -40%, 방어력 -5
                this.gameObject.GetComponent<PlayerInfo>().SetATKSpeed(100f * this.ATKSpeedCoeff);
                this.gameObject.GetComponent<PlayerInfo>().SetMovementSpeedPercent(15f * this.MovementSpeedPercentCoeff);
                this.gameObject.GetComponent<PlayerInfo>().SetDMGPercent(-40f * this.DMGPercentCoeff);
                this.gameObject.GetComponent<PlayerInfo>().SetArmor(-5);
                break;
            case "행운냥이":
                // 행운 계수 1.25
                this.LuckCoeff = 1.25f;
                // 행운 +100, 수확 +5, 공격속도 -60%, 경험치 획득 -50%
                this.gameObject.GetComponent<PlayerInfo>().SetLuck(100f * this.LuckCoeff);
                this.gameObject.GetComponent<PlayerInfo>().SetHarvest(5f);
                this.gameObject.GetComponent<PlayerInfo>().SetATKSpeed(-60f * this.DMGPercentCoeff);
                this.gameObject.GetComponent<PlayerInfo>().SetExpGain(-50f);
                break;
            case "0222":
                this.gameObject.GetComponent<PlayerInfo>().SetDMGPercent(4f);
                this.gameObject.GetComponent<PlayerInfo>().SetATKSpeed(4f);
                this.gameObject.GetComponent<PlayerInfo>().SetFixedDMG(4f);
                this.gameObject.GetComponent<PlayerInfo>().SetCritical(4f);
                this.gameObject.GetComponent<PlayerInfo>().SetRange(4f);

                this.gameObject.GetComponent<PlayerInfo>().SetHP(14f);
                this.gameObject.GetComponent<PlayerInfo>().SetRecovery(4);
                this.gameObject.GetComponent<PlayerInfo>().SetHPDrain(4f);
                this.gameObject.GetComponent<PlayerInfo>().SetArmor(4);
                this.gameObject.GetComponent<PlayerInfo>().SetEvasion(4);

                this.gameObject.GetComponent<PlayerInfo>().SetMovementSpeedPercent(4f);
                this.gameObject.GetComponent<PlayerInfo>().SetLuck(4f);
                this.gameObject.GetComponent<PlayerInfo>().SetHarvest(4f);
                break;
            case "불굴":
                this.gameObject.GetComponent<PlayerInfo>().SetHP(25f);
                this.gameObject.GetComponent<PlayerInfo>().SetRecovery(10);
                this.gameObject.GetComponent<PlayerInfo>().SetArmor(5);

                this.gameObject.GetComponent<PlayerInfo>().SetDMGPercent(-100f);
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
