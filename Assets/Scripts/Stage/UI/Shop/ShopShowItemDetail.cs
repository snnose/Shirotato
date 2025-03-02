using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// 보유 아이템 칸의 Image 내의 스크립트
// 마우스 포인터를 올린 아이템의 스탯을 보여주도록 하는 스크립트
public class ShopShowItemDetail : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private void Awake()
    {
     
    }

    // PointerEnter 이벤트 함수
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 포인터 진입 시 사운드 출력
        ButtonSoundManager.Instance.PlayOnPointerEnterSound2();
        
        ItemInfo itemInfo;
        // 해당 아이템 슬롯에 아이템이 등록됐다면 이벤트 발생
        // 부모 오브젝트에 ItemInfo 컴포넌트 부착 여부로 확인한다.
        if (this.gameObject.transform.parent.gameObject.TryGetComponent<ItemInfo>(out itemInfo))
        {
            // DetailUI를 활성화한다.
            ShopItemDetailUI.Instance.gameObject.SetActive(true);

            // DetailUI를 현재 마우스가 올라간 아이템 칸 위로 이동시킨다.
            Vector2 UIPos = CalDetailUIPos();
            ShopItemDetailUI.Instance.SetUIPosition(UIPos);

            // DetailUI의 아이템 이미지를 변경한다.
            ShopItemDetailUI.Instance.SetItemImage(this.gameObject.GetComponent<Image>());

            // DetailUI의 등급 이미지를 변경한다.
            ShopItemDetailUI.Instance.SetItemGradeImage(this.gameObject.transform.parent.GetChild(0).GetComponent<Image>());

            // DetailUI의 스테이터스 텍스트를 변경한다.
            ShopItemDetailUI.Instance.SetItemStatusText(itemInfo);
        }
    }
    // PointerExit 이벤트 함수
    public void OnPointerExit(PointerEventData eventData)
    {
        // DetailUI를 비활성화한다.
        if (this.gameObject.transform.parent.gameObject.TryGetComponent<ItemInfo>(out ItemInfo itemInfo))
            ShopItemDetailUI.Instance.gameObject.SetActive(false);
    }

    // DetailUI를 아이템 슬롯 위로 조정하는 함수
    private Vector2 CalDetailUIPos()
    {
        // 아이템 슬롯의 Size를 가져온다.
        Vector2 itemSlotSize = this.gameObject.GetComponent<RectTransform>().rect.size;

        // UI의 Size를 가져온다.
        RectTransform UIRectTransform = ShopItemDetailUI.Instance.GetComponent<RectTransform>();
        Vector2 UISize = UIRectTransform.rect.size;

        // 이동할 x 좌표값은 UI 크기 - 아이템 슬롯 크기를 2로 나눈 값
        float x = (UISize.x - itemSlotSize.x) / 2;
        // 이동할 y 좌표값은 UI 크기 + 아이템 슬롯 크기를 2로 나눈 값
        float y = (UISize.y + itemSlotSize.y) / 2;

        // 아이템 슬롯 위치에 이동할 좌표값 만큼 더한 후 반환
        Vector2 tmp = new Vector2(this.gameObject.transform.position.x + x, this.gameObject.transform.position.y + y);
        return tmp;
    }
}
