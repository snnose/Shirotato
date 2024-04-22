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
        // ��ư Ŭ�� �� ���� ���
        ButtonSoundManager.Instance.PlayOnClickButtonSound1();

        // �θ��� �̸��� �������� ���° ���������� ã�´�.
        string parentName = this.transform.parent.name;
        int lockNum = FindLockItemNum(parentName);

        // �ش� �������� ��װų� ��� �����Ѵ�.
        LockItem(lockNum);
    }

    // ��� ��ư�� �θ� �̸��� ���� ���° �����ۿ� ��ȣ �ۿ��ϴ��� ��ȯ�Ѵ�.
    private int FindLockItemNum(string parentName)
    {
        int num = -1;

        switch(parentName)
        {
            // ù��° (0)
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

    // num ��° ���� ������ ǰ���� ��״� �Լ�
    private void LockItem(int num)
    {
        List<bool> tmp = ItemManager.Instance.GetIsLockItemList();

        // ��� ���¶��
        if (tmp[num])
        {
            // ��� �����Ѵ�.
            tmp[num] = false;
            // �ڹ��� �̹��� ��Ȱ��ȭ
            lockImage.SetActive(false);
        }
        // ����� �ʾҴٸ�
        else
        {
            // ��ٴ�.
            tmp[num] = true;
            // �ش� ������ ĭ�� ��� �� ó�� ���̰� �ڹ��� �̹��� Ȱ��ȭ
            lockImage.SetActive(true);
        }

        ItemManager.Instance.SetIsLockItemList(tmp);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // ������ ���� �� ���� ���
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
