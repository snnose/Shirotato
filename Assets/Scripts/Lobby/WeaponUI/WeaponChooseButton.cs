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
        // ������ ���� ���� ���
        ButtonSoundManager.Instance.PlayOnPointerEnterSound2();
        // �ش� �̹����� �̸��� �޾ƿ� � �������� �ľ�
        weaponImage = this.transform.GetChild(1).GetComponent<Image>();
        weaponName = weaponImage.sprite.name;

        // ���콺 ������ ���� �� ���� ���� ����
        WeaponDetailControl weaponDetailControl = WeaponChooseUIControl.Instance.GetWeaponDetailControl();
        weaponDetailControl.RenewWeaponInfo(weaponName);
    }

    private void OnClickButton()
    {
        // ��ư Ŭ�� ���� ���
        ButtonSoundManager.Instance.PlayOnClickButtonSound2();
        // ���õ� ���� ������ �����Ѵ�
        RoundSetting.Instance.SetStartWeapon(weaponName);
        // ���� ���� â ����
        WeaponChooseUIControl.Instance.SetActive(false);
        // ���̵� ���� â�� ����
        DifficultyUIControl.Instance.SetActive(true);
    }
}
