using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemUseButton : MonoBehaviour, IPointerEnterHandler
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 포인터 진입 시 사운드 출력
        ButtonSoundManager.Instance.PlayOnPointerEnterSound1();
    }
}
