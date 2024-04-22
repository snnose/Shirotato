using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ShopLockButtonControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button lockButton;
    private Image background;
    private TextMeshProUGUI text;

    private GameObject lockImage;

    private void Awake()
    {
        background = this.transform.GetChild(0).GetComponent<Image>();
        text = this.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        lockImage = this.transform.parent.GetChild(6).gameObject;

        lockButton = this.GetComponent<Button>();
        lockButton.onClick.AddListener(OnClickLockButton);

        lockImage.SetActive(false);
    }

    public void OnClickLockButton()
    {
        // 버튼 클릭 시 사운드 출력
        ButtonSoundManager.Instance.PlayOnClickButtonSound1();

        // 부모의 이름을 바탕으로 몇번째 아이템인지 찾는다.
        string parentName = this.transform.parent.name;
        int lockNum = FindLockItemNum(parentName);

        // 해당 아이템을 잠그거나 잠금 해제한다.
        LockItem(lockNum);
    }

    // 잠근 버튼의 부모 이름에 따라 몇번째 아이템에 상호 작용하는지 반환한다.
    private int FindLockItemNum(string parentName)
    {
        int num = -1;

        switch(parentName)
        {
            // 첫번째 (0)
            case "ItemZero":
                num = 0;
                break;
            case "ItemOne":
                num = 1;
                break;
            case "ItemTwo":
                num = 2;
                break;
            case "ItemThree":
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
            // 자물쇠 이미지 비활성화
            lockImage.SetActive(false);
        }
        // 잠기지 않았다면
        else
        {
            // 잠근다.
            tmp[num] = true;
            // 해당 아이템 칸을 잠긴 것 처럼 보이게 자물쇠 이미지 활성화
            lockImage.SetActive(true);
        }

        ItemManager.Instance.SetIsLockItemList(tmp);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 포인터 진입 시 사운드 출력
        ButtonSoundManager.Instance.PlayOnPointerEnterSound1();

        text.color = Color.black;
        background.color = Color.white;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = Color.white;
        background.color = Color.black;
    }
}
