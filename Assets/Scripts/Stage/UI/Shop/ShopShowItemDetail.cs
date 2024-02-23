using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopShowItemDetail : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{ 
    private void Awake()
    {
        
    }

    // PointerEnter �̺�Ʈ �Լ�
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(this.gameObject.transform.position.x);
        
        ItemInfo itemInfo;
        // �ش� ������ ���Կ� �������� ��ϵƴٸ� �̺�Ʈ �߻�
        // �θ� ������Ʈ�� ItemInfo ������Ʈ ���� ���η� Ȯ���Ѵ�.
        if (this.gameObject.transform.parent.gameObject.TryGetComponent<ItemInfo>(out itemInfo))
        {
            // DetailUI�� ���� ���콺�� �ö� ������ ĭ ���� �̵���Ų��.
            Vector2 UIPos = CalDetailUIPos();
            ShopItemDetailUI.Instance.SetUIPosition(UIPos);

            // DetailUI�� �������ͽ� �ؽ�Ʈ�� �����Ѵ�.
            ShopItemDetailUI.Instance.SetItemStatusText(itemInfo);

            // DetailUI�� Ȱ��ȭ�Ѵ�.
            ShopItemDetailUI.Instance.GetDetailUI().SetActive(true);
        }
    }
    // PointerExit �̺�Ʈ �Լ�
    public void OnPointerExit(PointerEventData eventData)
    {
        // DetailUI�� ��Ȱ��ȭ�Ѵ�.
        ShopItemDetailUI.Instance.GetDetailUI().SetActive(false);
    }

    // DetailUI�� ������ ���� ���� �����ϴ� �Լ�
    private Vector2 CalDetailUIPos()
    {
        // ������ ������ Size�� �����´�.
        Vector2 itemSlotSize = this.gameObject.GetComponent<RectTransform>().rect.size;

        // UI�� Size�� �����´�.
        RectTransform UIRectTransform = ShopItemDetailUI.Instance.GetDetailUI().GetComponent<RectTransform>();
        Vector2 UISize = UIRectTransform.rect.size;

        // �̵��� x ��ǥ���� UI ũ�� - ������ ���� ũ�⸦ 2�� ���� ��
        float x = (UISize.x - itemSlotSize.x) / 2;
        // �̵��� y ��ǥ���� UI ũ�� + ������ ���� ũ�⸦ 2�� ���� ��
        float y = (UISize.y + itemSlotSize.y) / 2;

        // ������ ���� ��ġ�� �̵��� ��ǥ�� ��ŭ ���� �� ��ȯ
        Vector2 tmp = new Vector2(this.gameObject.transform.position.x + x, this.gameObject.transform.position.y + y);
        return tmp;
    }
}
