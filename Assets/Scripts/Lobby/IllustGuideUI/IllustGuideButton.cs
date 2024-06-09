using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IllustGuideButton : MonoBehaviour, IPointerEnterHandler
{
    private Button button;

    public GameObject IllustGuideUI;

    private void Awake()
    {
        button = this.GetComponent<Button>();
        button.onClick.AddListener(OnClickButton);
    }

    void Start()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // ������ ���� ���� ���
        ButtonSoundManager.Instance.PlayOnPointerEnterSound1();
    }

    // ��ư�� ������ ���� UI Ȱ��ȭ
    private void OnClickButton()
    {
        // ��ư Ŭ�� ���� ���
        ButtonSoundManager.Instance.PlayOnClickButtonSound1();
        IllustGuideUI.GetComponent<IllustGuideControl>().SetActive(true);
    }
}
