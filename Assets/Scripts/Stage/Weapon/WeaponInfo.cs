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
    public float damageCoeff;
    public float range;
    public float coolDown = 0f;
    public int knockback = 0;

    private float pierceDamage = 0.5f;
    public int pierceCount = 0;
    private int bounceCount = 0;

    // 발사하는 탄환 개수
    private int shootBulletCount = 1;

    // 특이사항
    private string specialNote = "";

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
        this.weaponName = weaponName;
        this.weaponNumber = -1;
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
                this.damageCoeff = 1.0f;
                this.range = 7;
                this.coolDown = 1.2f;
                this.knockback = 15;

                this.pierceCount = 1;
                this.pierceDamage = 0.5f;

                this.price = 10;
                break;
            case "Revolver":
                this.weaponName = weaponName;
                this.weaponNumber = weaponNumber;
                this.grade = 0;

                this.damage = 15;
                this.damageCoeff = 1.0f;
                this.range = 7.7f;
                this.coolDown = 0.43f;
                this.knockback = 15;

                this.pierceCount = 0;
                this.pierceDamage = 0.5f;

                this.specialNote = "6발 사격 후 " + this.coolDown * 5f + "초 간 재장전";
                this.price = 20;
                break;
            case "SMG":
                this.weaponName = weaponName;
                this.weaponNumber = weaponNumber;
                this.grade = 0;
                this.damage = 3;
                this.damageCoeff = 0.5f;
                this.range = 7f;
                this.coolDown = 0.17f;
                this.pierceCount = 0;
                this.pierceDamage = 0.5f;
                this.price = 20;
                break;
            case "Shotgun":
                this.weaponName = weaponName;
                this.weaponNumber = weaponNumber;
                this.grade = 0;

                this.damage = 3;
                this.damageCoeff = 0.8f;
                this.range = 6.2f;
                this.coolDown = 1.37f;
                this.knockback = 8;

                this.pierceCount = 2;
                this.pierceDamage = 0.7f;

                this.shootBulletCount = 4;
                this.specialNote = "한번에 4발 발사";
                this.price = 20;
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

    public int GetShootBulletCount()
    {
        return this.shootBulletCount;
    }

    public string GetSpecialNote()
    {
        return this.specialNote;
    }

    public float GetPierceDamage()
    {
        return this.pierceDamage;
    }

    // 무기 스탯을 등급에 따라 설정하는 함수
    public void SetWeaponStatus(string weaponName, int weaponGrade)
    {
        this.grade = weaponGrade;
        switch (weaponName)
        {
            // 권총
            case "Pistol":
                switch (weaponGrade)
                {
                    case 0:
                        this.damage = 12;
                        this.damageCoeff = 1.0f;
                        this.range = 7;
                        this.coolDown = 1.2f;
                        this.knockback = 15;

                        this.pierceCount = 1;
                        this.price = 10;
                        break;
                    case 1:
                        this.damage = 20;
                        this.damageCoeff = 1.0f;
                        this.range = 7;
                        this.coolDown = 1.12f;
                        this.knockback = 15;

                        this.pierceCount = 1;
                        this.price = 22;
                        break;
                    case 2:
                        this.damage = 30;
                        this.damageCoeff = 1.0f;
                        this.range = 7;
                        this.coolDown = 1.03f;
                        this.knockback = 15;

                        this.pierceCount = 1;
                        this.price = 45;
                        break;
                    case 3:
                        this.damage = 50;
                        this.damageCoeff = 1.0f;
                        this.range = 7;
                        this.coolDown = 0.87f;
                        this.knockback = 15;

                        this.pierceCount = 2;
                        this.price = 91;
                        break;
                    default:
                        break;
                }

                break;
            // 리볼버
            case "Revolver":
                switch (weaponGrade)
                {
                    case 0:
                        this.damage = 15;
                        this.damageCoeff = 1.0f;
                        this.range = 7.7f;
                        this.coolDown = 0.43f;
                        this.knockback = 15;

                        this.pierceCount = 0;
                        this.specialNote = "6발 사격 후 " + this.coolDown * 5f + "초 간 재장전";
                        this.price = 20;
                        break;
                    case 1:
                        this.damage = 20;
                        this.damageCoeff = 1.3f;
                        this.range = 7.7f;
                        this.coolDown = 0.42f;
                        this.knockback = 15;

                        this.pierceCount = 0;
                        this.specialNote = "6발 사격 후 " + this.coolDown * 5f + "초 간 재장전";
                        this.price = 34;
                        break;
                    case 2:
                        this.damage = 25;
                        this.damageCoeff = 1.65f;
                        this.range = 7.7f;
                        this.coolDown = 0.4f;
                        this.knockback = 15;

                        this.pierceCount = 0;
                        this.specialNote = "6발 사격 후 " + this.coolDown * 5f + "초 간 재장전";
                        this.price = 70;
                        break;
                    case 3:
                        this.damage = 40;
                        this.damageCoeff = 2.0f;
                        this.range = 7.7f;
                        this.coolDown = 0.38f;
                        this.knockback = 15;

                        this.pierceCount = 0;
                        this.specialNote = "6발 사격 후 " + this.coolDown * 5f + "초 간 재장전";
                        this.price = 130;
                        break;
                    default:
                        break;
                }

                break;

            case "SMG":
                switch(weaponGrade)
                {
                    case 0:
                        this.damage = 3;
                        this.damageCoeff = 0.5f;
                        this.range = 7f;
                        this.coolDown = 0.17f;
                        this.pierceCount = 0;
                        this.pierceDamage = 0.5f;
                        this.price = 20;
                        break;
                    case 1:
                        this.damage = 4;
                        this.damageCoeff = 0.6f;
                        this.range = 7f;
                        this.coolDown = 0.17f;
                        this.pierceCount = 0;
                        this.pierceDamage = 0.5f;
                        this.price = 39;
                        break;
                    case 2:
                        this.damage = 5;
                        this.damageCoeff = 0.7f;
                        this.range = 7f;
                        this.coolDown = 0.17f;
                        this.pierceCount = 0;
                        this.pierceDamage = 0.5f;
                        this.price = 74;
                        break;
                    case 3:
                        this.damage = 8;
                        this.damageCoeff = 0.8f;
                        this.range = 7f;
                        this.coolDown = 0.15f;
                        this.pierceCount = 0;
                        this.pierceDamage = 0.5f;
                        this.price = 149;
                        break;
                }
                break;

            case "Shotgun":
                switch(weaponGrade)
                {
                    case 0:
                        this.damage = 3;
                        this.damageCoeff = 0.8f;
                        this.range = 6.2f;
                        this.coolDown = 1.37f;
                        this.knockback = 8;

                        this.pierceCount = 2;
                        this.pierceDamage = 0.7f;
                        this.shootBulletCount = 4;
                        this.specialNote = "한번에 4발 발사";
                        this.price = 20;
                        break;
                    case 1:
                        this.damage = 6;
                        this.damageCoeff = 0.85f;
                        this.range = 6.2f;
                        this.coolDown = 1.28f;
                        this.knockback = 8;

                        this.pierceCount = 2;
                        this.pierceDamage = 0.7f;
                        this.shootBulletCount = 4;
                        this.specialNote = "한번에 4발 발사";
                        this.price = 39;
                        break;
                    case 2:
                        this.damage = 9;
                        this.damageCoeff = 0.9f;
                        this.range = 6.2f;
                        this.coolDown = 1.2f;
                        this.knockback = 8;

                        this.pierceCount = 2;
                        this.pierceDamage = 0.7f;
                        this.shootBulletCount = 4;
                        this.specialNote = "한번에 4발 발사";
                        this.price = 74;
                        break;
                    case 3:
                        this.damage = 9;
                        this.damageCoeff = 1.0f;
                        this.range = 6.2f;
                        this.coolDown = 1.2f;
                        this.knockback = 8;

                        this.pierceCount = 3;
                        this.pierceDamage = 0.7f;
                        this.shootBulletCount = 6;
                        this.specialNote = "한번에 6발 발사";
                        this.price = 149;
                        break;
                }
                break;
            default:
                break;
        }
    }
}