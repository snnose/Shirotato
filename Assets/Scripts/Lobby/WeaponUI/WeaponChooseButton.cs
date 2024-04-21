using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WeaponChooseButton : MonoBehaviour, IPointerEnterHandler
{
    Button button;
    Image weaponImage;

    string weaponName;

    void Start()
    {
        button = this.GetComponent<Button>();

        button.onClick.AddListener(OnClickButton);

        if (this.transform.GetChild(1).GetComponent<Image>().sprite == null)
            this.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 포인터 진입 음성 출력
        ButtonSoundManager.Instance.PlayOnPointerEnterSound2();
        // 해당 이미지의 이름을 받아와 어떤 무기인지 파악
        weaponImage = this.transform.GetChild(1).GetComponent<Image>();
        weaponName = weaponImage.sprite.name;

        // 마우스 포인터 진입 시 무기 정보 갱신
        WeaponDetailControl weaponDetailControl = WeaponChooseUIControl.Instance.GetWeaponDetailControl();
        weaponDetailControl.RenewWeaponInfo(weaponName);
    }

    private void OnClickButton()
    {
        // 버튼 클릭 음성 출력
        ButtonSoundManager.Instance.PlayOnClickButtonSound2();
        // 선택된 무기 정보를 전달한다
        RoundSetting.Instance.SetStartWeapon(weaponName);
        // 무기 선택 창 종료
        WeaponChooseUIControl.Instance.SetActive(false);
        // 난이도 선택 창을 띄운다
        DifficultyUIControl.Instance.SetActive(true);
    }
}
