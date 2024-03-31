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
        // 마우스 포인터 진입 시 무기 정보 갱신
    }

    private void OnClickButton()
    {
        // 선택된 무기 정보를 전달한다

        // 무기 선택 창 종료
        WeaponChooseUIControl.Instance.SetActive(false);
        // 난이도 선택 창을 띄운다
        DifficultyUIControl.Instance.SetActive(true);
    }
}
