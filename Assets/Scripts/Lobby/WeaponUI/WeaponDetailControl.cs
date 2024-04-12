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
                weaponNamePro.text = "권총";
                weaponDetailPro.text = "대미지 : " + 12 + " (+100%)\n" +
                                       "공격속도 : " + 0.83 + "/s \n" +
                                       "넉백 : " + 15 + '\n' +
                                       "범위 : " + 7;
                break;
            case "Revolver":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/Revolver");
                weaponNamePro.text = "리볼버";
                weaponDetailPro.text = "대미지 : " + 15 + " (+100%)\n" +
                                       "공격속도 : " + 2.32 + "/s \n" +
                                       "넉백 : " + 15 + '\n' +
                                       "범위 : " + 7.7 + '\n' +
                                       "6발 사격 후 2.15초 동안 재장전";
                break;
            case "SMG":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/SMG");
                weaponNamePro.text = "서브머신건";
                weaponDetailPro.text = "대미지 : " + 3 + " (+50%)\n" +
                                       "공격속도 : " + 5.88 + "/s \n" +
                                       "넉백 : " + 0 + '\n' +
                                       "범위 : " + 7 + '\n';
                break;
            case "Shotgun":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/Shotgun");
                weaponNamePro.text = "샷건";
                weaponDetailPro.text = "대미지 : " + 3 + " (+80%)\n" +
                                       "공격속도 : " + 0.73 + "/s \n" +
                                       "넉백 : " + 8 + '\n' +
                                       "범위 : " + 6.2 + '\n' +
                                       "한번에 4발 발사";
                break;

            case "Bow":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/Bow");
                weaponNamePro.text = "활";
                weaponDetailPro.text = "대미지 : " + 10 + " (+80%)\n" +
                                       "공격속도 : " + 0.82 + "/s \n" +
                                       "넉백 : " + 5 + '\n' +
                                       "범위 : " + 6 + '\n' +
                                       "화살이 1회 튕깁니다";
                break;
            // Melee Weapon
            case "Nekohand":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/Nekohand");
                weaponNamePro.text = "고양이손";
                weaponDetailPro.text = "대미지 : " + 8 + " (+200%)\n" +
                                       "공격속도 : " + 1.28 + "/s \n" +
                                       "넉백 : " + 15 + '\n' +
                                       "범위 : " + 3.8 + '\n';
                break;
            case "Hammer":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/Hammer");
                weaponNamePro.text = "망치";
                weaponDetailPro.text = "대미지 : " + 15 + " (+250%)\n" +
                                       "공격속도 : " + 0.57 + "/s \n" +
                                       "넉백 : " + 20 + '\n' +
                                       "범위 : " + 4.2 + '\n';
                break;
            default:
                break;
        }
    }
}
