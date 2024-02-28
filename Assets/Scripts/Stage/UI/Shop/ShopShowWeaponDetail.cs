using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopShowWeaponDetail : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // ���� ���콺�� �ø��� �ִ� ���� ĭ
    private GameObject currentPointWeaponRoom;
    // ���� ���콺�� �ø��� �ִ� ���� ĭ�� ��ȣ
    private int currentPointWeaponRoomNumber;
    //public ShopWeaponDetailUI shopWeaponDetailUI;

    private void Awake()
    {
        //shopWeaponDetailUI = ShopWeaponDetailUI.Instance;
        currentPointWeaponRoomNumber = -1;
    }

    // ���콺 �����Ͱ� �ش� ��Ű ĭ�� ������ �� WeaponDetailUI Ȱ��ȭ
    public void OnPointerEnter(PointerEventData eventData)
    {
        // �ش� ���� ĭ�� �о�´�.
        currentPointWeaponRoom = eventData.pointerCurrentRaycast.gameObject;
        // �ش� ���� ĭ�� �� ��°���� ã�´�.
        currentPointWeaponRoomNumber = FindWeaponRoomNumber(currentPointWeaponRoom);

        // �ش� ���� ĭ ��ȣ�� �ش��ϴ� ���� ������ �ְ�, ��ư�� ������ ���� ���¶��
        if (currentPointWeaponRoomNumber < WeaponManager.Instance.GetCurrentWeaponList().Count
            && WeaponManager.Instance.GetCurrentWeaponInfoList()[currentPointWeaponRoomNumber] != null
            && !ShopWeaponDetailUI.Instance.GetIsLockOn())
        {
            // ������ �� ������ �����Ѵ�
            ChangeDetailUIElement(this.gameObject);
            // WeaponDetailUI ��ġ ����
            Vector2 pos = CalWeaponDetailUIPos();
            ShopWeaponDetailUI.Instance.SetUIPosition(pos);
            // WeaponDetailUI Ȱ��ȭ
            ShopWeaponDetailUI.Instance.GetWeaponDetailUI().SetActive(true);
        }
    }
    // ���콺 �����Ͱ� �ش� ���� ĭ���� ���� �� WeaponDetailUI ��Ȱ��ȭ
    // ��ư�� ���� �� DetailUI�� ��Ҹ� ������ �� ���� �ش� ����� �۵����� �ʴ´�.
    public void OnPointerExit(PointerEventData eventData)
    {
        // �ش� ���� ĭ�� ���Ⱑ ��ϵǾ��ְ�, ��ư�� ������ ���� ������ �� ����
        if (currentPointWeaponRoomNumber < WeaponManager.Instance.GetCurrentWeaponList().Count 
            && WeaponManager.Instance.GetCurrentWeaponInfoList()[currentPointWeaponRoomNumber] != null
            && !ShopWeaponDetailUI.Instance.GetIsLockOn())
        {
            currentPointWeaponRoom = null;
            currentPointWeaponRoomNumber = -1;
            // WeaponDetailUI ��Ȱ��ȭ
            ShopWeaponDetailUI.Instance.GetWeaponDetailUI().SetActive(false);
        }
    }
    // �ش� ���� ĭ�� Ŭ�� �� WeaponDetailUI Ȱ��ȭ
    public void OnClickWeaponRoom()
    {
        // WeaponDetailUI ����
        ShopWeaponDetailUI.Instance.SetIsLockOn(true);

        // ���� ���� ���� ĭ ��ȣ�� ����Ѵ�.
        ShopWeaponDetailUI.Instance.SetWeaponRoomNumber(currentPointWeaponRoomNumber);
    }

    private int FindWeaponRoomNumber(GameObject weaponRoom)
    {
        int num = -999;
        // x -> 745.5, 832.1666, 918.8333 (0��, 1��, 2��)
        // y -> 139, 59 (0��, 1��)

        int x = Mathf.FloorToInt(weaponRoom.transform.position.x);
        int y = Mathf.FloorToInt(weaponRoom.transform.position.y);

        switch (x)
        {
            // 0��
            case 745:
                num = 0;
                break;
            // 1��
            case 832:
                num = 1;
                break;
            // 2��
            case 918:
                num = 2;
                break;
            default:
                break;
        }

        switch (y)
        {
            // 1��
            case 59:
                num += 3;
                break;
            // 0��
            case 139:
                num += 0;
                break;
            default:
                break;
        }

        return num;
    }

    // WeaponDetailUI�� �̹���, �ؽ�Ʈ�� �����ϴ� �Լ�
    private void ChangeDetailUIElement(GameObject weapon)
    {
        // WeaponInfo ��ũ��Ʈ�� ����
        WeaponInfo weaponInfo = WeaponManager.Instance.GetCurrentWeaponInfoList()[currentPointWeaponRoomNumber];
        ShopWeaponDetailUI.Instance.SetWeaponImage(weapon.GetComponent<Image>());
        ShopWeaponDetailUI.Instance.SetWeaponNameText(weaponInfo);
        ShopWeaponDetailUI.Instance.SetWeaponStatusText(weaponInfo);
    }

    // WeaponDetailUI�� ������ ���� ���� �����ϴ� �Լ�
    private Vector2 CalWeaponDetailUIPos()
    {
        // ������ ������ Size�� �����´�.
        Vector2 weaponSlotSize = this.gameObject.GetComponent<RectTransform>().rect.size;

        // UI�� Size�� �����´�.
        RectTransform UIRectTransform = ShopWeaponDetailUI.Instance.GetWeaponDetailUI().GetComponent<RectTransform>();
        Vector2 UISize = UIRectTransform.rect.size;

        // �̵��� x ��ǥ���� - UI ũ�� + ������ ���� ũ�⸦ 2�� ���� ��
        float x = (- UISize.x + weaponSlotSize.x) / 2;
        // �̵��� y ��ǥ���� UI ũ�� + ������ ���� ũ�⸦ 2�� ���� ��
        float y = (UISize.y + weaponSlotSize.y) / 2;

        // ������ ���� ��ġ�� �̵��� ��ǥ�� ��ŭ ���� �� ��ȯ
        Vector2 tmp = new Vector2(this.gameObject.transform.position.x + x, this.gameObject.transform.position.y + y);
        return tmp;
    }
}
