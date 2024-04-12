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
                weaponDetailPro.text = "����� : " + 12 + " (+100%)\n" +
                                       "���ݼӵ� : " + 0.83 + "/s \n" +
                                       "�˹� : " + 15 + '\n' +
                                       "���� : " + 7;
                break;
            case "Revolver":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/Revolver");
                weaponNamePro.text = "������";
                weaponDetailPro.text = "����� : " + 15 + " (+100%)\n" +
                                       "���ݼӵ� : " + 2.32 + "/s \n" +
                                       "�˹� : " + 15 + '\n' +
                                       "���� : " + 7.7 + '\n' +
                                       "6�� ��� �� 2.15�� ���� ������";
                break;
            case "SMG":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/SMG");
                weaponNamePro.text = "����ӽŰ�";
                weaponDetailPro.text = "����� : " + 3 + " (+50%)\n" +
                                       "���ݼӵ� : " + 5.88 + "/s \n" +
                                       "�˹� : " + 0 + '\n' +
                                       "���� : " + 7 + '\n';
                break;
            case "Shotgun":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/Shotgun");
                weaponNamePro.text = "����";
                weaponDetailPro.text = "����� : " + 3 + " (+80%)\n" +
                                       "���ݼӵ� : " + 0.73 + "/s \n" +
                                       "�˹� : " + 8 + '\n' +
                                       "���� : " + 6.2 + '\n' +
                                       "�ѹ��� 4�� �߻�";
                break;

            case "Bow":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/Bow");
                weaponNamePro.text = "Ȱ";
                weaponDetailPro.text = "����� : " + 10 + " (+80%)\n" +
                                       "���ݼӵ� : " + 0.82 + "/s \n" +
                                       "�˹� : " + 5 + '\n' +
                                       "���� : " + 6 + '\n' +
                                       "ȭ���� 1ȸ ƨ��ϴ�";
                break;
            // Melee Weapon
            case "Nekohand":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/Nekohand");
                weaponNamePro.text = "����̼�";
                weaponDetailPro.text = "����� : " + 8 + " (+200%)\n" +
                                       "���ݼӵ� : " + 1.28 + "/s \n" +
                                       "�˹� : " + 15 + '\n' +
                                       "���� : " + 3.8 + '\n';
                break;
            case "Hammer":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/Hammer");
                weaponNamePro.text = "��ġ";
                weaponDetailPro.text = "����� : " + 15 + " (+250%)\n" +
                                       "���ݼӵ� : " + 0.57 + "/s \n" +
                                       "�˹� : " + 20 + '\n' +
                                       "���� : " + 4.2 + '\n';
                break;
            default:
                break;
        }
    }
}
