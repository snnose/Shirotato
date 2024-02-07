using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInfo : MonoBehaviour
{
    public int monsterNumber;

    public float HP;
    public float ATK;
    public float MovementSpeed;

    public int dropCount;
    public void SetMonsterNumber(int monsterNumber)
    {
        this.monsterNumber = monsterNumber;
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
        return this.ATK;
    }

    public float GetMovementSpeed()
    {
        return this.MovementSpeed;
    }

    public int GetDropCount()
    {
        return this.dropCount;
    }
}
