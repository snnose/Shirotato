using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    // singleton
    private static DifficultyManager instance;
    public static DifficultyManager Instance
    {
        get
        {
            if (null == instance)
                return null;

            return instance;
        }
    }

    private float monsterHPCoeff = 1.0f;
    private float monsterDMGCoeff = 1.0f;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        // 선택한 난이도 적용
        ApplyDifficulty(RoundSetting.Instance.GetDifficulty());
    }

    void Start()
    {
        
    }

    private void ApplyDifficulty(int difficulty)
    {
        switch (difficulty)
        {
            // 쉬움
            case 0:
                monsterHPCoeff = 0.8f;
                monsterDMGCoeff = 0.8f;
                break;
            // 보통 (아무 것도 없음)
            case 1:
                break;
            default:
                break;
        }
    }

    public float GetMonsterHPCoeff()
    {
        return this.monsterHPCoeff;
    }

    public float GetMonsterDMGCoeff()
    {
        return this.monsterDMGCoeff;
    }
}
