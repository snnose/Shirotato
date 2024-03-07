using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInfo : MonoBehaviour
{
    public int monsterNumber;

    public float HP;
    public float damage;
    public float MovementSpeed;

    // 드랍 관련
    public int waffleDropCount;
    public float consumableDropRate;    // 소모품 드랍 확률
    public float lootDropRate;     // 소모품 드랍이 된 상태에서 상자가 나올 확률
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
