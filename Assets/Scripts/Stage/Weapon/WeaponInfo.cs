using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfo : MonoBehaviour
{
    public string weaponName = "";
    // 0 ~ 3À¸·Î normal ~ legend ¼ø
    private int grade = 0;

    public int damage = 0;

    public float range = 0f;
    public float coolDown = 0f;

    public int price = 0;

    private void Awake()
    {
        grade = 0;
    }

    public void SetWeaponGrade(int grade)
    {
        this.grade = grade;
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