using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfo
{
    public string weaponName;
    // ������ ���� (0 ~ 5)
    private int weaponNumber;
    // 0 ~ 3���� normal ~ legend ��
    private int grade;

    public int damage;
    public float damageCoeff;
    public float range;
    public float coolDown = 0f;
    public int knockback = 0;

    private float pierceDamage = 0.5f;
    public int pierceCount = 0;
    public int bounceCount = 0;

    // �߻��ϴ� źȯ ����
    private int shootBulletCount = 1;

    // Ư�̻���
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
            // Ranged Weapon
            case "Pistol":
                this.weaponName = "����";
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
                this.weaponName = "������";
                this.weaponNumber = weaponNumber;
                this.grade = 0;

                this.damage = 15;
                this.damageCoeff = 1.0f;
                this.range = 7.7f;
                this.coolDown = 0.43f;
                this.knockback = 15;

                this.pierceCount = 0;
                this.pierceDamage = 0.5f;

                this.specialNote = "6�� ��� �� " + this.coolDown * 5f + "�� �� ������";
                this.price = 20;
                break;
            case "SMG":
                this.weaponName = "SMG";
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
                this.weaponName = "��ź��";
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
                this.specialNote = "�ѹ��� 4�� �߻�";
                this.price = 20;
                break;

            case "Bow":
                this.weaponName = "Ȱ";
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

                this.specialNote = "ȭ���� 1ȸ ƨ��ϴ�";
                this.price = 15;
                break;

            // Melee Weapon
            case "Nekohand":
                this.weaponName = "����̼�";
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

            case "Hammer":
                this.weaponName = "��ġ";
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

    // ���� ������ ��޿� ���� �����ϴ� �Լ�
    public void SetWeaponStatus(string weaponName, int weaponGrade)
    {
        this.grade = weaponGrade;
        switch (weaponName)
        {
            // ����
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
            // ������
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
                        this.specialNote = "6�� ��� �� " + this.coolDown * 5f + "�� �� ������";
                        this.price = 20;
                        break;
                    case 1:
                        this.damage = 20;
                        this.damageCoeff = 1.3f;
                        this.range = 7.7f;
                        this.coolDown = 0.42f;
                        this.knockback = 15;

                        this.pierceCount = 0;
                        this.specialNote = "6�� ��� �� " + this.coolDown * 5f + "�� �� ������";
                        this.price = 34;
                        break;
                    case 2:
                        this.damage = 25;
                        this.damageCoeff = 1.65f;
                        this.range = 7.7f;
                        this.coolDown = 0.4f;
                        this.knockback = 15;

                        this.pierceCount = 0;
                        this.specialNote = "6�� ��� �� " + this.coolDown * 5f + "�� �� ������";
                        this.price = 70;
                        break;
                    case 3:
                        this.damage = 40;
                        this.damageCoeff = 2.0f;
                        this.range = 7.7f;
                        this.coolDown = 0.38f;
                        this.knockback = 15;

                        this.pierceCount = 0;
                        this.specialNote = "6�� ��� �� " + this.coolDown * 5f + "�� �� ������";
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
                        this.specialNote = "�ѹ��� 4�� �߻�";
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
                        this.specialNote = "�ѹ��� 4�� �߻�";
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
                        this.specialNote = "�ѹ��� 4�� �߻�";
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
                        this.specialNote = "�ѹ��� 6�� �߻�";
                        this.price = 149;
                        break;
                }
                break;

            case "Bow":
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

                        this.specialNote = "ȭ���� 1ȸ ƨ��ϴ�";
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

                        this.specialNote = "ȭ���� 2ȸ ƨ��ϴ�";
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

                        this.specialNote = "ȭ���� 3ȸ ƨ��ϴ�";
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

                        this.specialNote = "ȭ���� 4ȸ ƨ��ϴ�";
                        this.price = 122;
                        break;
                }
                break;
            // Melee Weapon
            case "Nekohand":
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

            case "Hammer":
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
            default:
                break;
        }
    }
}