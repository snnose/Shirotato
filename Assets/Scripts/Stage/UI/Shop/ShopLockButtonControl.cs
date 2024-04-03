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
        // ���� Ŭ���� ��ư�� �����´�.
        currentClickButton = EventSystem.current.currentSelectedGameObject;

        // ��ư�� X ��ǥ�� ������� ���° ������ ǰ������ ã�´�.
        float posX = currentClickButton.transform.position.x - 0.5f;
        int lockNum = FindLockItemNum(posX);

        // �ش� �������� ��װų� ��� �����Ѵ�.
        LockItem(lockNum);
    }

    // ��� ��ư�� x��ǥ�� ���� ���° �����ۿ� ��ȣ �ۿ��ϴ��� ��ȯ�Ѵ�.
    private int FindLockItemNum(float posX)
    {
        int num = -1;

        switch(posX)
        {
            // ù��° (0)
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

    // num ��° ���� ������ ǰ���� ��״� �Լ�
    private void LockItem(int num)
    {
        List<bool> tmp = ItemManager.Instance.GetIsLockItemList();

        // ��� ���¶��
        if (tmp[num])
        {
            // ��� �����Ѵ�.
            tmp[num] = false;
            // �ش� ������ ĭ�� ��� �� ó�� ���̰� �ڹ��� �̹��� Ȱ��ȭ
        }
        // ����� �ʾҴٸ�
        else
        {
            // ��ٴ�.
            tmp[num] = true;
            // �ڹ��� �̹��� ��Ȱ��ȭ
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
