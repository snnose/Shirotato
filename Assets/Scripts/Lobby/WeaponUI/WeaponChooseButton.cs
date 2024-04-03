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
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // �ش� �̹����� �̸��� �޾ƿ� � �������� �ľ�
        weaponImage = this.transform.GetChild(1).GetComponent<Image>();
        weaponName = weaponImage.sprite.name;

        // ���콺 ������ ���� �� ���� ���� ����
        WeaponDetailControl weaponDetailControl = WeaponChooseUIControl.Instance.GetWeaponDetailControl();
        weaponDetailControl.RenewWeaponInfo(weaponName);
    }

    private void OnClickButton()
    {
        // ���õ� ���� ������ �����Ѵ�
        RoundSetting.Instance.SetStartWeapon(weaponName);
        // ���� ���� â ����
        WeaponChooseUIControl.Instance.SetActive(false);
        // ���̵� ���� â�� ����
        DifficultyUIControl.Instance.SetActive(true);
    }
}