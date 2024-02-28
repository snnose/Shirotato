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
    public float coolDown;

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
    }

    public WeaponInfo(string weaponName)
    {
        switch (weaponName)
        {
            case "Pistol":
                this.weaponName = weaponName;
                this.weaponNumber = -1;
                this.grade = 0;
                this.damage = 10;
                this.range = 7;
                this.coolDown = 1.5f;
                this.price = 15;
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
                this.damage = 10;
                this.range = 7;
                this.coolDown = 1.5f;
                this.price = 15;
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

    public void SetWeaponStatus(GameObject weapon)
    {
        WeaponInfo weaponInfo = weapon.GetComponent<WeaponInfo>();

        this.weaponName = weaponInfo.weaponName;
        this.grade = weaponInfo.grade;
        this.damage = weaponInfo.damage;
        this.range = weaponInfo.range;
        this.coolDown = weaponInfo.coolDown;
        this.price = weaponInfo.price;
    }
}