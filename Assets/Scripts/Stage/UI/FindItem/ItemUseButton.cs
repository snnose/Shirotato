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
        // ������ ���� �� ���� ���
        ButtonSoundManager.Instance.PlayOnPointerEnterSound1();
    }
}
