using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRangedWeaponControl
{
    public int weaponNumber { get; set; }
    public WeaponInfo weaponInfo { get; set; }
    public bool isCoolDown { get; set; }

    public void TrackingClosetMonster(GameObject closetMonster);

    public IEnumerator Attack(GameObject closetMonster);

    public GameObject GetClosetMonster();
}

public interface IMeleeWeaponControl
{
    public int weaponNumber { get; set; }
    public WeaponInfo weaponInfo { get; set; }
    public bool isCoolDown { get; set; }

    public IEnumerator Attack(GameObject closetMonster);

    public void TrackingClosetMonster(GameObject closetMonster);

    public GameObject GetClosetMonster();
}
