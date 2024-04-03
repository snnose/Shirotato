using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ShopLockButtonControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject currentClickButton;
    private Image background;
    private TextMeshProUGUI text;

    private void Awake()
    {
        background = this.transform.GetChild(0).GetComponent<Image>();
        text = this.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    public void OnClickLockButton()
    {
        // 현재 클릭된 버튼을 가져온다.
        currentClickButton = EventSystem.current.currentSelectedGameObject;

        // 버튼의 X 좌표를 기반으로 몇번째 아이템 품목인지 찾는다.
        float posX = currentClickButton.transform.position.x - 0.5f;
        int lockNum = FindLockItemNum(posX);

        // 해당 아이템을 잠그거나 잠금 해제한다.
        LockItem(lockNum);
    }

    // 잠근 버튼의 x좌표에 따라 몇번째 아이템에 상호 작용하는지 반환한다.
    private int FindLockItemNum(float posX)
    {
        int num = -1;

        switch(posX)
        {
            // 첫번째 (0)
            case 135:
                num = 0;
                break;
            case 380:
                num = 1;
                break;
            case 625:
                num = 2;
                break;
            case 870:
                num = 3;
                break;
            default:
                num = -1;
                break;
        }

        return num;
    }

    // num 번째 상점 아이템 품목을 잠그는 함수
    private void LockItem(int num)
    {
        List<bool> tmp = ItemManager.Instance.GetIsLockItemList();

        // 잠긴 상태라면
        if (tmp[num])
        {
            // 잠금 해제한다.
            tmp[num] = false;
            // 해당 아이템 칸을 잠긴 것 처럼 보이게 자물쇠 이미지 활성화
        }
        // 잠기지 않았다면
        else
        {
            // 잠근다.
            tmp[num] = true;
            // 자물쇠 이미지 비활성화
        }

        ItemManager.Instance.SetIsLockItemList(tmp);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = Color.black;
        background.color = Color.white;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = Color.white;
        background.color = Color.black;
    }
}
