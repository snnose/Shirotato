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
            case "권총":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/권총");
                weaponNamePro.text = "권총";
                weaponDetailPro.text = "대미지 : " + 12 + " (+100%)\n" +
                                       "공격속도 : " + 0.83 + "/s \n" +
                                       "넉백 : " + 15 + '\n' +
                                       "범위 : " + 7;
                break;
            case "리볼버":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/리볼버");
                weaponNamePro.text = "리볼버";
                weaponDetailPro.text = "대미지 : " + 15 + " (+100%)\n" +
                                       "공격속도 : " + 2.32 + "/s \n" +
                                       "넉백 : " + 15 + '\n' +
                                       "범위 : " + 7.7 + '\n' +
                                       "6발 사격 후 2.15초 동안 재장전";
                break;
            case "서브머신건":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/서브머신건");
                weaponNamePro.text = "서브머신건";
                weaponDetailPro.text = "대미지 : " + 3 + " (+50%)\n" +
                                       "공격속도 : " + 5.88 + "/s \n" +
                                       "넉백 : " + 0 + '\n' +
                                       "범위 : " + 7 + '\n';
                break;
            case "산탄총":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/산탄총");
                weaponNamePro.text = "산탄총";
                weaponDetailPro.text = "대미지 : " + 3 + " (+80%)\n" +
                                       "공격속도 : " + 0.73 + "/s \n" +
                                       "넉백 : " + 8 + '\n' +
                                       "범위 : " + 6.2 + '\n' +
                                       "한번에 4발 발사";
                break;

            case "활":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/활");
                weaponNamePro.text = "활";
                weaponDetailPro.text = "대미지 : " + 10 + " (+80%)\n" +
                                       "공격속도 : " + 0.82 + "/s \n" +
                                       "넉백 : " + 5 + '\n' +
                                       "범위 : " + 6 + '\n' +
                                       "화살이 1회 튕깁니다";
                break;
            case "수리검":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/수리검");
                weaponNamePro.text = "수리검";
                weaponDetailPro.text = "대미지 : " + 7 + " (+58%)\n" +
                                       "공격속도 : " + 1.14 + "/s \n" +
                                       "넉백 : " + 0 + '\n' +
                                       "범위 : " + 7 + '\n' +
                                       "치명타 발생 시 1회 튕깁니다";
                break;
            // Melee Weapon
            case "고양이손":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/고양이손");
                weaponNamePro.text = "고양이손";
                weaponDetailPro.text = "대미지 : " + 8 + " (+200%)\n" +
                                       "공격속도 : " + 1.28 + "/s \n" +
                                       "넉백 : " + 15 + '\n' +
                                       "범위 : " + 3.8 + '\n';
                break;
            case "망치":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/망치");
                weaponNamePro.text = "망치";
                weaponDetailPro.text = "대미지 : " + 15 + " (+250%)\n" +
                                       "공격속도 : " + 0.57 + "/s \n" +
                                       "넉백 : " + 20 + '\n' +
                                       "범위 : " + 4.2 + '\n';
                break;
            case "솔":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/솔");
                weaponNamePro.text = "솔";
                weaponDetailPro.text = "대미지 : " + 1 + " (+100%)\n" +
                                       "공격속도 : " + 0.99 + "/s \n" +
                                       "넉백 : " + 30 + '\n' +
                                       "범위 : " + 3 + '\n' +
                                       "적 공격 시 3% 확률로 와플 드랍";
                break;
            case "방망이":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/방망이");
                weaponNamePro.text = "방망이";
                weaponDetailPro.text = "대미지 : " + 10 + " (+100%)\n" +
                                       "공격속도 : " + 0.70 + "/s \n" +
                                       "넉백 : " + 10 + '\n' +
                                       "범위 : " + 3.5 + '\n' +
                                       "레벨의 75%만큼 추가 대미지";
                break;
            case "사탕칼":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/사탕칼");
                weaponNamePro.text = "사탕칼";
                weaponDetailPro.text = "대미지 : " + 8 + " (+200%)\n" +
                                       "공격속도 : " + 0.8 + "/s \n" +
                                       "넉백 : " + 0 + '\n' +
                                       "범위 : " + 3.5 + '\n' +
                                       "일정 횟수 공격하면 부러집니다";
                break;
            case "화도일문자":
                weaponImage.sprite = Resources.Load<Sprite>("Sprites/Weapons/화도일문자");
                weaponNamePro.text = "화도일문자";
                weaponDetailPro.text = "대미지 : " + 20 + " (+200%)\n" +
                                       "공격속도 : " + 0.69 + "/s \n" +
                                       "넉백 : " + 0 + '\n' +
                                       "범위 : " + 4 + '\n' +
                                       "휘두르기와 찌르기를 반복합니다";
                break;
            default:
                break;
        }
    }
}
