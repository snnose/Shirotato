using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoredWeaponNumber : MonoBehaviour
{
    public int weaponNumber;

    public void SetWeaponNumber(int weaponNumber)
    {
        this.weaponNumber = weaponNumber;
    }

    public int GetWeaponNumber()
    {
        return this.weaponNumber;
    }
}
