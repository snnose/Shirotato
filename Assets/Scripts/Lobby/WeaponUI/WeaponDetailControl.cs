using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponDetailControl : MonoBehaviour
{
    private Image weaponImage;
    private TextMeshProUGUI weaponNamePro;
    private TextMeshProUGUI weaponDetailPro;

    private void Awake()
    {
        weaponImage = this.transform.GetChild(1).GetChild(1).GetComponent<Image>();
        weaponNamePro = this.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        weaponDetailPro = this.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void RenewWeaponInfo(string weaponName)
    {
        switch (weaponName)
        {
            case "Pistol":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/Pistol");
                weaponNamePro.text = "����";
                weaponDetailPro.text = "����� : " + 12 + '\n' +
                                       "���ݼӵ� : " + 0.83 + "/s \n" +
                                       "���� : " + 7;
                break;
            case "Revolver":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/Revolver");
                weaponNamePro.text = "������";
                weaponDetailPro.text = "����� : " + 15 + '\n' +
                                       "���ݼӵ� : " + 2.32 + "/s \n" +
                                       "���� : " + 7.7 + '\n' +
                                       "6�� ��� �� 2.15�� ���� ������";
                break;
            case "SMG":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/SMG");
                weaponNamePro.text = "����ӽŰ�";
                weaponDetailPro.text = "����� : " + 3 + '\n' +
                                       "���ݼӵ� : " + 5.88 + "/s \n" +
                                       "���� : " + 7 + '\n';
                break;
            case "Shotgun":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/Shotgun");
                weaponNamePro.text = "����";
                weaponDetailPro.text = "����� : " + 3 + '\n' +
                                       "���ݼӵ� : " + 0.73 + "/s \n" +
                                       "���� : " + 6.2 + '\n' +
                                       "�ѹ��� 4�� �߻�";
                break;
            default:
                break;
        }
    }
}
