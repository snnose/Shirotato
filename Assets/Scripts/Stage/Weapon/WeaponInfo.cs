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
    public int bounceCount = 0;

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
    
    // 시작 무기 착용 전용
    public WeaponInfo(string weaponName, int weaponNumber)
    {
        switch(weaponName)
        {
            // Ranged Weapon
            case "권총":
                this.weaponName = "권총";
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
            case "리볼버":
                this.weaponName = "리볼버";
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
            case "서브머신건":
                this.weaponName = "서브머신건";
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

            case "산탄총":
                this.weaponName = "산탄총";
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

            case "활":
                this.weaponName = "활";
                this.weaponNumber = weaponNumber;
                this.grade = 0;

                this.damage = 10;
                this.damageCoeff = 0.8f;
                this.range = 6f;
                this.coolDown = 1.22f;
                this.knockback = 5;

                this.pierceCount = 0;
                this.pierceDamage = 0.5f;

                this.bounceCount = 1;

                this.specialNote = "화살이 1회 튕깁니다";
                this.price = 15;
                break;

            case "수리검":
                this.weaponName = "수리검";
                this.weaponNumber = weaponNumber;
                this.grade = 0;

                this.damage = 7;
                this.damageCoeff = 0.58f;
                this.range = 7f;
                this.coolDown = 0.87f;
                this.knockback = 0;

                this.pierceCount = 0;
                this.pierceDamage = 0.5f;

                this.bounceCount = 1;

                this.specialNote = "치명타 발생 시 1회 튕깁니다";
                this.price = 12;
                break;

            // Melee Weapon
            case "고양이손":
                this.weaponName = "고양이손";
                this.weaponNumber = weaponNumber;
                this.grade = 0;

                this.damage = 8;
                this.damageCoeff = 2.0f;
                this.range = 3.8f;
                this.coolDown = 0.78f;
                this.knockback = 15;

                this.specialNote = "";
                this.price = 10;
                break;

            case "망치":
                this.weaponName = "망치";
                this.weaponNumber = weaponNumber;
                this.grade = 0;

                this.damage = 15;
                this.damageCoeff = 2.5f;
                this.range = 4.2f;
                this.coolDown = 1.75f;
                this.knockback = 20;

                this.specialNote = "";
                this.price = 25;
                break;

            case "솔":
                this.weaponName = "솔";
                this.weaponNumber = weaponNumber;
                this.grade = 0;

                this.damage = 1;
                this.damageCoeff = 1.0f;
                this.range = 3f;
                this.coolDown = 1.01f;
                this.knockback = 30;

                this.specialNote = "적 공격 시 3% 확률로 와플 드랍";
                this.price = 10;
                break;

            case "방망이":
                this.weaponName = "방망이";
                this.weaponNumber = weaponNumber;
                this.grade = 0;

                this.damage = 10;
                this.damageCoeff = 1.0f;
                this.range = 3.5f;
                this.coolDown = 1.42f;
                this.knockback = 10;

                this.specialNote = "레벨의 75%만큼 추가 대미지";
                this.price = 17;
                break;

            case "사탕칼":
                this.weaponName = "사탕칼";
                this.weaponNumber = weaponNumber;
                this.grade = 0;

                this.damage = 8;
                this.damageCoeff = 2.0f;
                this.range = 3.5f;
                this.coolDown = 1.25f;
                this.knockback = 0;

                this.specialNote = "일정 횟수 공격하면 부러집니다";
                this.price = 10;
                break;

            case "화도일문자":
                this.weaponName = "화도일문자";
                this.weaponNumber = weaponNumber;
                this.grade = 0;

                this.damage = 20;
                this.damageCoeff = 2.0f;
                this.range = 4f;
                this.coolDown = 1.43f;
                this.knockback = 5;

                this.specialNote = "휘두르기와 찌르기를 반복합니다";
                this.price = 25;
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
            case "권총":
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
            case "리볼버":
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

            case "서브머신건":
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

            case "산탄총":
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

            case "활":
                switch (weaponGrade)
                {
                    case 0:
                        this.damage = 10;
                        this.damageCoeff = 0.8f;
                        this.range = 6f;
                        this.coolDown = 1.22f;
                        this.knockback = 5;

                        this.pierceCount = 0;
                        this.pierceDamage = 0.5f;

                        this.bounceCount = 1;

                        this.specialNote = "화살이 1회 튕깁니다";
                        this.price = 15;
                        break;
                    case 1:
                        this.damage = 13;
                        this.damageCoeff = 0.8f;
                        this.range = 6f;
                        this.coolDown = 1.17f;
                        this.knockback = 5;

                        this.pierceCount = 0;
                        this.pierceDamage = 0.5f;

                        this.bounceCount = 2;

                        this.specialNote = "화살이 2회 튕깁니다";
                        this.price = 31;
                        break;
                    case 2:
                        this.damage = 16;
                        this.damageCoeff = 0.8f;
                        this.range = 6f;
                        this.coolDown = 1.13f;
                        this.knockback = 5;

                        this.pierceCount = 0;
                        this.pierceDamage = 0.5f;

                        this.bounceCount = 3;

                        this.specialNote = "화살이 3회 튕깁니다";
                        this.price = 61;
                        break;
                    case 3:
                        this.damage = 20;
                        this.damageCoeff = 0.8f;
                        this.range = 6f;
                        this.coolDown = 1.1f;
                        this.knockback = 5;

                        this.pierceCount = 0;
                        this.pierceDamage = 0.5f;

                        this.bounceCount = 4;

                        this.specialNote = "화살이 4회 튕깁니다";
                        this.price = 122;
                        break;
                }
                break;

            case "수리검":
                switch (weaponGrade)
                {
                    case 0:
                        this.damage = 7;
                        this.damageCoeff = 0.59f;
                        this.range = 7f;
                        this.coolDown = 0.87f;
                        this.knockback = 0;

                        this.pierceCount = 0;
                        this.pierceDamage = 0.5f;

                        this.bounceCount = 1;

                        this.specialNote = "치명타 발생 시 1회 튕깁니다";
                        this.price = 12;
                        break;
                    case 1:
                        this.damage = 9;
                        this.damageCoeff = 0.81f;
                        this.range = 7f;
                        this.coolDown = 0.83f;
                        this.knockback = 0;

                        this.pierceCount = 0;
                        this.pierceDamage = 0.5f;

                        this.bounceCount = 2;

                        this.specialNote = "치명타 발생 시 2회 튕깁니다";
                        this.price = 26;
                        break;
                    case 2:
                        this.damage = 12;
                        this.damageCoeff = 1.0f;
                        this.range = 7f;
                        this.coolDown = 0.8f;
                        this.knockback = 0;

                        this.pierceCount = 0;
                        this.pierceDamage = 0.5f;

                        this.bounceCount = 3;

                        this.specialNote = "치명타 발생 시 3회 튕깁니다";
                        this.price = 52;
                        break;
                    case 3:
                        this.damage = 18;
                        this.damageCoeff = 1.22f;
                        this.range = 7f;
                        this.coolDown = 0.7f;
                        this.knockback = 0;

                        this.pierceCount = 0;
                        this.pierceDamage = 0.5f;

                        this.bounceCount = 4;

                        this.specialNote = "치명타 발생 시 4회 튕깁니다";
                        this.price = 105;
                        break;
                }
                break;

            // 특수 무기
            case "매그넘":
                switch (weaponGrade)
                {
                    case 2:
                        this.damage = 116;
                        this.damageCoeff = 4.5f;
                        this.range = 10f;
                        this.coolDown = 1.5f;
                        this.knockback = 10;

                        this.pierceCount = 999;
                        this.pierceDamage = 1f;

                        this.bounceCount = 0;

                        this.specialNote = "무조건 관통";
                        this.price = 127;
                        break;

                    case 3:
                        this.damage = 162;
                        this.damageCoeff = 6.7f;
                        this.range = 10f;
                        this.coolDown = 1.5f;
                        this.knockback = 10;

                        this.pierceCount = 999;
                        this.pierceDamage = 1f;

                        this.bounceCount = 0;

                        this.specialNote = "무조건 관통";
                        this.price = 255;
                        break;

                    default:
                        break;
                }
                break;

            // Melee Weapon
            case "고양이손":
                switch (weaponGrade)
                {
                    case 0:
                        this.damage = 8;
                        this.damageCoeff = 2.0f;
                        this.range = 3.8f;
                        this.coolDown = 0.78f;
                        this.knockback = 15;

                        this.specialNote = "";
                        this.price = 10;
                        break;
                    case 1:
                        this.damage = 16;
                        this.damageCoeff = 2.0f;
                        this.range = 3.8f;
                        this.coolDown = 0.73f;
                        this.knockback = 15;

                        this.specialNote = "";
                        this.price = 22;
                        break;
                    case 2:
                        this.damage = 32;
                        this.damageCoeff = 2.0f;
                        this.range = 3.8f;
                        this.coolDown = 0.69f;
                        this.knockback = 15;

                        this.specialNote = "";
                        this.price = 45;
                        break;
                    case 3:
                        this.damage = 64;
                        this.damageCoeff = 2.0f;
                        this.range = 3.8f;
                        this.coolDown = 0.59f;
                        this.knockback = 15;

                        this.specialNote = "";
                        this.price = 91;
                        break;
                    default:
                        break;
                }
                break;

            case "망치":
                switch (weaponGrade)
                {
                    case 0:
                        this.damage = 15;
                        this.damageCoeff = 2.5f;
                        this.range = 4.2f;
                        this.coolDown = 1.75f;
                        this.knockback = 20;

                        this.specialNote = "";
                        this.price = 25;
                        break;
                    case 1:
                        this.damage = 30;
                        this.damageCoeff = 3.0f;
                        this.range = 4.2f;
                        this.coolDown = 1.67f;
                        this.knockback = 25;

                        this.specialNote = "";
                        this.price = 51;
                        break;
                    case 2:
                        this.damage = 60;
                        this.damageCoeff = 3.5f;
                        this.range = 4.2f;
                        this.coolDown = 1.59f;
                        this.knockback = 35;

                        this.specialNote = "";
                        this.price = 95;
                        break;
                    case 3:
                        this.damage = 100;
                        this.damageCoeff = 4.0f;
                        this.range = 4.2f;
                        this.coolDown = 1.5f;
                        this.knockback = 40;

                        this.specialNote = "";
                        this.price = 190;
                        break;
                    default:
                        break;
                }
                break;

            case "솔":
                switch (weaponGrade)
                {
                    case 0:
                        this.damage = 1;
                        this.damageCoeff = 1.0f;
                        this.range = 3f;
                        this.coolDown = 1.01f;
                        this.knockback = 30;

                        this.specialNote = "적 공격 시 3% 확률로 와플 드랍";
                        this.price = 10;
                        break;
                    case 1:
                        this.damage = 1;
                        this.damageCoeff = 1.0f;
                        this.range = 3f;
                        this.coolDown = 0.93f;
                        this.knockback = 30;

                        this.specialNote = "적 공격 시 6% 확률로 와플 드랍";
                        this.price = 22;
                        break;
                    case 2:
                        this.damage = 1;
                        this.damageCoeff = 1.0f;
                        this.range = 3f;
                        this.coolDown = 0.86f;
                        this.knockback = 30;

                        this.specialNote = "적 공격 시 10% 확률로 와플 드랍";
                        this.price = 45;
                        break;
                    case 3:
                        this.damage = 1;
                        this.damageCoeff = 1.0f;
                        this.range = 3f;
                        this.coolDown = 0.71f;
                        this.knockback = 30;

                        this.specialNote = "적 공격 시 15% 확률로 와플 드랍";
                        this.price = 91;
                        break;
                    default:
                        break;
                }
                break;
            case "방망이":
                switch (weaponGrade)
                {
                    case 0:
                        this.damage = 10;
                        this.damageCoeff = 1.0f;
                        this.range = 3.5f;
                        this.coolDown = 1.42f;
                        this.knockback = 10;

                        this.specialNote = "레벨의 75%만큼 추가 대미지";
                        this.price = 17;
                        break;
                    case 1:
                        this.damage = 15;
                        this.damageCoeff = 1.3f;
                        this.range = 3.5f;
                        this.coolDown = 1.25f;
                        this.knockback = 10;

                        this.specialNote = "레벨의 85%만큼 추가 대미지";
                        this.price = 34;
                        break;
                    case 2:
                        this.damage = 20;
                        this.damageCoeff = 1.7f;
                        this.range = 3.5f;
                        this.coolDown = 1.09f;
                        this.knockback = 10;

                        this.specialNote = "레벨의 100%만큼 추가 대미지";
                        this.price = 66;
                        break;
                    case 3:
                        this.damage = 30;
                        this.damageCoeff = 2.0f;
                        this.range = 3.5f;
                        this.coolDown = 0.92f;
                        this.knockback = 10;

                        this.specialNote = "레벨의 125%만큼 추가 대미지";
                        this.price = 130;
                        break;
                    default:
                        break;
                }
                break;

            case "사탕칼":
                switch (weaponGrade)
                {
                    case 0:
                        this.damage = 8;
                        this.damageCoeff = 2.0f;
                        this.range = 3.5f;
                        this.coolDown = 1.25f;
                        this.knockback = 0;

                        this.specialNote = "일정 횟수 공격하면 부러집니다";
                        this.price = 10;
                        break;
                    case 1:
                        this.damage = 16;
                        this.damageCoeff = 2.25f;
                        this.range = 3.5f;
                        this.coolDown = 1.17f;
                        this.knockback = 0;

                        this.specialNote = "일정 횟수 공격하면 부러집니다";
                        this.price = 22;
                        break;
                    case 2:
                        this.damage = 24;
                        this.damageCoeff = 2.5f;
                        this.range = 3.5f;
                        this.coolDown = 1.1f;
                        this.knockback = 0;

                        this.specialNote = "일정 횟수 공격하면 부러집니다";
                        this.price = 45;
                        break;
                    case 3:
                        this.damage = 40;
                        this.damageCoeff = 3.0f;
                        this.range = 3.5f;
                        this.coolDown = 1.03f;
                        this.knockback = 0;

                        this.specialNote = "일정 횟수 공격하면 부러집니다";
                        this.price = 91;
                        break;
                    default:
                        break;
                }
                break;
            case "화도일문자":
                switch (weaponGrade)
                {
                    case 0:
                        this.damage = 20;
                        this.damageCoeff = 2.0f;
                        this.range = 4f;
                        this.coolDown = 1.43f;
                        this.knockback = 5;

                        this.specialNote = "휘두르기와 찌르기를 반복합니다";
                        this.price = 25;
                        break;
                    case 1:
                        this.damage = 25;
                        this.damageCoeff = 2.0f;
                        this.range = 4f;
                        this.coolDown = 1.28f;
                        this.knockback = 5;

                        this.specialNote = "휘두르기와 찌르기를 반복합니다";
                        this.price = 51;
                        break;
                    case 2:
                        this.damage = 40;
                        this.damageCoeff = 2.0f;
                        this.range = 4f;
                        this.coolDown = 1.13f;
                        this.knockback = 5;

                        this.specialNote = "휘두르기와 찌르기를 반복합니다";
                        this.price = 95;
                        break;
                    case 3:
                        this.damage = 60;
                        this.damageCoeff = 2.0f;
                        this.range = 4f;
                        this.coolDown = 0.98f;
                        this.knockback = 5;

                        this.specialNote = "휘두르기와 찌르기를 반복합니다";
                        this.price = 190;
                        break;
                    default:
                        break;
                }
                break;

            // 근접 특수
            case "철쇄아":
                switch (weaponGrade)
                {
                    case 2:
                        this.damage = 30;
                        this.damageCoeff = 2.4f;
                        this.range = 4f;
                        this.coolDown = 1.43f;
                        this.knockback = 0;

                        this.specialNote = "바람의 상처!";
                        this.price = 141;
                        break;
                    case 3:
                        this.damage = 55;
                        this.damageCoeff = 2.7f;
                        this.range = 4f;
                        this.coolDown = 1.3f;
                        this.knockback = 0;

                        this.specialNote = "바람의 상처!";
                        this.price = 280;
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