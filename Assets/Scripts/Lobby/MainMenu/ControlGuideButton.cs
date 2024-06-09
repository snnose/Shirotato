using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControlGuideButton : MonoBehaviour, IPointerEnterHandler
{
    Button controlGuideButton;

    public ControlGuideUIControl controlGuideUIControl;

    private void Awake()
    {
        controlGuideButton = this.GetComponent<Button>();
        controlGuideButton.onClick.AddListener(OnClickGuideButton);
    }

    void Start()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ButtonSoundManager.Instance.PlayOnPointerEnterSound1();
    }
    
    void OnClickGuideButton()
    {
        ButtonSoundManager.Instance.PlayOnClickButtonSound1();
        controlGuideUIControl.SetActive(true);
    }
}
