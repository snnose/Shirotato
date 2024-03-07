using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInfo : MonoBehaviour
{
    public int monsterNumber;

    public float HP;
    public float damage;
    public float MovementSpeed;

    // ��� ����
    public int waffleDropCount;
    public float consumableDropRate;    // �Ҹ�ǰ ��� Ȯ��
    public float lootDropRate;     // �Ҹ�ǰ ����� �� ���¿��� ���ڰ� ���� Ȯ��
    public void SetMonsterNumber(int monsterNumber)
    {
        this.monsterNumber = monsterNumber;
    }

    public void SetMonsterHP(float hp)
    {
        this.HP = hp;
    }

    public void SetMonsterDamage(float damage)
    {
        this.damage = damage;
    }

    public void SetMonsterMovementSpeed(float movementSpeed)
    {
        this.MovementSpeed = movementSpeed;
    }

    public void SetMonsterWaffleDropCount(int count)
    {
        this.waffleDropCount = count;
    }

    public void SetMonsterConsumableDropRate(float rate)
    {
        this.consumableDropRate = rate;
    }

    public void SetMonsterLootDropRate(float rate)
    {
        this.lootDropRate = rate;
    }

    public int GetMonsterNumber()
    {
        return this.monsterNumber;
    }

    public float GetMonsterHP()
    {
        return this.HP;
    }

    public float GetMonsterATK()
    {
        return this.damage;
    }

    public float GetMonsterMovementSpeed()
    {
        return this.MovementSpeed;
    }

    public int GetWaffleDropCount()
    {
        return this.waffleDropCount;
    }

    public float GetMonsterConsumableDropRate()
    {
        return this.consumableDropRate;
    }

    public float GetMonsterLootDropRate()
    {
        return this.lootDropRate;
    }
}
