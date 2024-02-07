using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    // 공격 관련
    public float DMGPercent = 0f;
    public float ATKSpeed = 0f;
    public float FixedDMG = 0f;
    public float Critical = 0f;
    public float Range = 0f;

    // 방어 관련
    public float HP = 0;
    public int Recovery = 0;
    public int Armor = 0;
    public int Evasion = 0;

    // 유틸 관련
    public float MovementSpeed = 0f;
    public float RootingRange = 0f;
    public float Luck = 0f;

    // 가격
    public int price = 0;
}
