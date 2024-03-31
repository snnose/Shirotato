using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WeaponChooseButton : MonoBehaviour, IPointerEnterHandler
{
    Button button;

    void Start()
    {
        button = this.GetComponent<Button>();
        button.onClick.AddListener(OnClickButton);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // ���콺 ������ ���� �� ���� ���� ����
    }

    private void OnClickButton()
    {
        // ���õ� ���� ������ �����Ѵ�

        // ���� ���� â ����
        WeaponChooseUIControl.Instance.SetActive(false);
        // ���̵� ���� â�� ����
        DifficultyUIControl.Instance.SetActive(true);
    }
}
