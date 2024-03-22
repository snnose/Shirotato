using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfo
{
    public string weaponName;
    // 무기의 순서 (0 ~ 5)
    private int weaponNumber;
    // 0 ~ 3으로 normal ~ legend 순
    private int grade;

    public int damage;
    public float range;
    public float coolDown = 0f;
    public int pierceCount = 0;
    private int bounceCount = 0;

    public int price;

    public WeaponInfo()
    {
        weaponName = "";
        weaponNumber = 0;
        grade = 0;
        damage = 0;
        range = 0f;
        coolDown = 0f;
        price = 0;
        pierceCount = 0;
    }

    public WeaponInfo(string weaponName)
    {
        switch (weaponName)
        {
            case "Pistol":
                this.weaponName = weaponName;
                this.weaponNumber = -1;
                break;
            default:
                break;
        }
    }

    public WeaponInfo(string weaponName, int weaponNumber)
    {
        switch(weaponName)
        {
            case "Pistol":
                this.weaponName = weaponName;
                this.weaponNumber = weaponNumber;
                this.grade = 0;
                this.damage = 12;
                this.range = 7;
                this.coolDown = 1.2f;
                this.pierceCount = 1;
                this.price = 10;
                break;
            default:
                break;
        }
    }

    public void SetWeaponGrade(int grade)
    {
        this.grade = grade;
    }

    public void SetWeaponNumber(int num)
    {
        this.weaponNumber = num;
    }

    public int GetWeaponNumber()
    {
        return this.weaponNumber;
    }

    public int GetWeaponGrade()
    {
        return this.grade;
    }

    // 무기 스탯을 등급에 따라 설정하는 함수
    public void SetWeaponStatus(string weaponName, int weaponGrade)
    {
        this.grade = weaponGrade;
        switch (weaponName)
        {
            case "Pistol":
                switch (weaponGrade)
                {
                    case 0:
                        this.damage = 12;
                        this.range = 7;
                        this.coolDown = 1.2f;
                        this.pierceCount = 1;
                        this.price = 10;
                        break;
                    case 1:
                        this.damage = 20;
                        this.range = 7;
                        this.coolDown = 1.12f;
                        this.pierceCount = 1;
                        this.price = 22;
                        break;
                    case 2:
                        this.damage = 30;
                        this.range = 7;
                        this.coolDown = 1.03f;
                        this.pierceCount = 1;
                        this.price = 45;
                        break;
                    case 3:
                        this.damage = 50;
                        this.range = 7;
                        this.coolDown = 0.87f;
                        this.pierceCount = 2;
                        this.price = 91;
                        break;
                    default:
                        break;
                }
                    
                break;
                
            default:
                break;
        }
    }
}