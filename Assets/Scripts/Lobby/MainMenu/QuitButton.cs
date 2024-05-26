using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuitButton : MonoBehaviour, IPointerEnterHandler
{
    Button quitButton;

    private void Awake()
    {
        quitButton = this.GetComponent<Button>();
        quitButton.onClick.AddListener(OnClickQuitButton);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnClickQuitButton()
    {
        // ���� ���
        ButtonSoundManager.Instance.PlayOnClickButtonSound1();
        // ���� ���� ��ư Ŭ�� �� ���� ����
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ButtonSoundManager.Instance.PlayOnPointerEnterSound1();
    }
}
