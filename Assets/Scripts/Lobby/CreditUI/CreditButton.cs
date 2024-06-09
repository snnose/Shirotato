using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CreditButton : MonoBehaviour
{
    Button creditButton;

    public CreditUIControl creditUIControl;

    private void Awake()
    {
        creditButton = this.GetComponent<Button>();
        creditButton.onClick.AddListener(OnClickCreditButton);
    }

    void Start()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ButtonSoundManager.Instance.PlayOnPointerEnterSound1();
    }

    void OnClickCreditButton()
    {
        ButtonSoundManager.Instance.PlayOnClickButtonSound1();
        creditUIControl.SetActive(true);
    }
}
