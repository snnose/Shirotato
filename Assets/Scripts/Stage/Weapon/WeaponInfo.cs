using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfo : MonoBehaviour
{
    public string weaponName = "";
    // 0 ~ 3���� normal ~ legend ��
    public int grade = 0;

    public int damage = 0;

    public float range = 0f;
    public float coolDown = 0f;

    public int price = 0;
}