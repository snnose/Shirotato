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

        // ������ ���̵� ����
        ApplyDifficulty(RoundSetting.Instance.GetDifficulty());
    }

    void Start()
    {
        
    }

    private void ApplyDifficulty(int difficulty)
    {
        switch (difficulty)
        {
            // ����
            case 0:
                monsterHPCoeff = 0.8f;
                monsterDMGCoeff = 0.8f;
                break;
            // ���� (�ƹ� �͵� ����)
            case 1:
                break;
            // �����
            // ���ο� ����, �� ����� +10%, ü�� + 10%
            case 2:
                monsterHPCoeff = 1.1f;
                monsterDMGCoeff = 1.1f;
                break;
            // �ſ� �����
            // ���ο� ����, �� ����� +25%, ü�� + 25%
            case 3:
                monsterHPCoeff = 1.25f;
                monsterDMGCoeff = 1.25f;
                break;
            // ����
            // ���� 2����, ���ο� ����, �� ����� +40%, ü�� + 40%
            case 4:
                monsterHPCoeff = 1.4f;
                monsterDMGCoeff = 1.4f;
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
