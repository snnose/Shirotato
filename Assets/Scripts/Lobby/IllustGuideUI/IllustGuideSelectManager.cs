using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllustGuideSelectManager : MonoBehaviour
{
    public string currentSelect = "";

    public GameObject normalItemListUI;
    public GameObject rareItemListUI;
    public GameObject epicItemListUI;
    public GameObject legendItemListUI;

    public GameObject normalWeaponListUI;
    public GameObject rareWeaponListUI;
    public GameObject epicWeaponListUI;
    public GameObject legendWeaponListUI;

    public GameObject monsterListUI;

    void Start()
    {

    }

    public void InActiveAllListUI()
    {
        normalItemListUI.SetActive(false);
        rareItemListUI.SetActive(false);
        epicItemListUI.SetActive(false);
        legendItemListUI.SetActive(false);

        normalWeaponListUI.SetActive(false);
        rareWeaponListUI.SetActive(false);
        epicWeaponListUI.SetActive(false);
        legendWeaponListUI.SetActive(false);
        monsterListUI.SetActive(false);
    }
}
