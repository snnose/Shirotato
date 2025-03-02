using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopShowWeaponDetail : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // 현재 마우스를 올리고 있는 무기 칸
    private GameObject currentPointWeaponRoom;
    // 현재 마우스를 올리고 있는 무기 칸의 번호
    private int currentPointWeaponRoomNumber;
    //public ShopWeaponDetailUI shopWeaponDetailUI;

    private void Awake()
    {
        //shopWeaponDetailUI = ShopWeaponDetailUI.Instance;
        currentPointWeaponRoomNumber = -1;
    }

    // 마우스 포인터가 해당 무키 칸에 진입할 때 WeaponDetailUI 활성화
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 해당 무기 칸을 읽어온다.
        currentPointWeaponRoom = eventData.pointerCurrentRaycast.gameObject;
        // 해당 무기 칸이 몇 번째인지 찾는다.
        currentPointWeaponRoomNumber = FindWeaponRoomNumber(currentPointWeaponRoom);

        // 해당 무기 칸 번호에 해당하는 무기 정보가 있고, 버튼을 누르지 않은 상태라면
        if (currentPointWeaponRoomNumber < WeaponManager.Instance.GetCurrentWeaponList().Count
            && WeaponManager.Instance.GetCurrentWeaponInfoList()[currentPointWeaponRoomNumber] != null
            && !ShopWeaponDetailUI.Instance.GetIsLockOn())
        {
            // 무기의 상세 설명을 설정한다
            ChangeDetailUIElement(this.gameObject);
            // WeaponDetailUI 위치 변경
            Vector2 pos = CalWeaponDetailUIPos();
            ShopWeaponDetailUI.Instance.SetUIPosition(pos);
            // WeaponDetailUI 활성화
            ShopWeaponDetailUI.Instance.GetWeaponDetailUI().SetActive(true);
        }
    }
    // 마우스 포인터가 해당 무기 칸에서 나올 때 WeaponDetailUI 비활성화
    // 버튼을 누를 시 DetailUI의 취소를 누르기 전 까지 해당 기능은 작동하지 않는다.
    public void OnPointerExit(PointerEventData eventData)
    {
        // 포인터 진입 시 사운드 출력
        ButtonSoundManager.Instance.PlayOnPointerEnterSound2();

        // 해당 무기 칸에 무기가 등록되어있고, 버튼을 누르지 않은 상태일 때 진입
        if (currentPointWeaponRoomNumber < WeaponManager.Instance.GetCurrentWeaponList().Count 
            && WeaponManager.Instance.GetCurrentWeaponInfoList()[currentPointWeaponRoomNumber] != null
            && !ShopWeaponDetailUI.Instance.GetIsLockOn())
        {
            currentPointWeaponRoom = null;
            currentPointWeaponRoomNumber = -1;
            // WeaponDetailUI 비활성화
            ShopWeaponDetailUI.Instance.GetWeaponDetailUI().SetActive(false);
        }
    }
    // 해당 무기 칸을 클릭 시 WeaponDetailUI 활성화
    public void OnClickWeaponRoom()
    {
        // 버튼 클릭 시 사운드 출력
        ButtonSoundManager.Instance.PlayOnClickButtonSound2();

        if (currentPointWeaponRoomNumber < WeaponManager.Instance.GetCurrentWeaponList().Count
            && !ShopWeaponDetailUI.Instance.GetIsLockOn())
        {
            // WeaponDetailUI 고정
            ShopWeaponDetailUI.Instance.SetIsLockOn(true);

            // 현재 누른 무기 칸 번호를 기억한다.
            ShopWeaponDetailUI.Instance.SetWeaponRoomNumber(currentPointWeaponRoomNumber);
        }
    }

    private int FindWeaponRoomNumber(GameObject weaponRoom)
    {
        // 오브젝트의 이름을 받아온다
        string weaponRoomName = weaponRoom.transform.parent.name;
        int num = -999;

        switch (weaponRoomName)
        {
            case "Weapon1":
                num = 0;
                break;
            case "Weapon2":
                num = 1;
                break;
            case "Weapon3":
                num = 2;
                break;
            case "Weapon4":
                num = 3;
                break;
            case "Weapon5":
                num = 4;
                break;
            case "Weapon6":
                num = 5;
                break;
            default:
                break;
        }

        return num;
    }

    // WeaponDetailUI의 이미지, 텍스트를 변경하는 함수
    private void ChangeDetailUIElement(GameObject weapon)
    {
        // WeaponInfo 스크립트를 참조
        WeaponInfo weaponInfo = WeaponManager.Instance.GetCurrentWeaponInfoList()[currentPointWeaponRoomNumber];
        ShopWeaponDetailUI.Instance.SetGradeColor(DecideGradeColor(weaponInfo.GetWeaponGrade()));
        ShopWeaponDetailUI.Instance.SetWeaponImage(weapon.GetComponent<Image>());
        ShopWeaponDetailUI.Instance.SetWeaponNameText(weaponInfo);
        ShopWeaponDetailUI.Instance.SetWeaponStatusText(weaponInfo);
        // WeaponDetailUI 판매 버튼 가격 설정
        ShopWeaponDetailUI.Instance.SetWeaponSellButtonText(weaponInfo);
    }

    // WeaponDetailUI를 아이템 슬롯 위로 조정하는 함수
    private Vector2 CalWeaponDetailUIPos()
    {
        // 아이템 슬롯의 Size를 가져온다.
        Vector2 weaponSlotSize = this.gameObject.GetComponent<RectTransform>().rect.size;

        // UI의 Size를 가져온다.
        RectTransform UIRectTransform = ShopWeaponDetailUI.Instance.GetWeaponDetailUI().GetComponent<RectTransform>();
        Vector2 UISize = UIRectTransform.rect.size;

        // 이동할 x 좌표값은 - UI 크기 + 아이템 슬롯 크기를 2로 나눈 값
        float x = (- UISize.x + weaponSlotSize.x) / 2;
        // 이동할 y 좌표값은 UI 크기 + 아이템 슬롯 크기를 2로 나눈 값
        float y = (UISize.y + weaponSlotSize.y) / 2;

        // 아이템 슬롯 위치에 이동할 좌표값 만큼 더한 후 반환
        Vector2 tmp = new Vector2(this.gameObject.transform.position.x + x, this.gameObject.transform.position.y + y);
        return tmp;
    }

    // rank에 따라 랭크 색깔을 반환하는 함수 (흰, 파, 보, 주)
    private Color DecideGradeColor(int grade)
    {
        Color color = Color.white;
        switch (grade)
        {
            case 0:
                color = Color.white;
                break;
            // 파랑색
            case 1:
                color = new Color(120, 166, 214);
                ColorUtility.TryParseHtmlString("#78A6D6", out color);
                break;
            // 보라색
            case 2:
                color = new Color(161, 120, 214);
                ColorUtility.TryParseHtmlString("#A178D6", out color);
                break;
            // 주황색
            case 3:
                color = new Color(233, 137, 76);
                ColorUtility.TryParseHtmlString("#E9894C", out color);
                break;
            default:
                color = Color.black;
                break;
        }

        return color;
    }
}
