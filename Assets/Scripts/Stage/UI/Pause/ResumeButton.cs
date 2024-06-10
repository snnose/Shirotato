using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ResumeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Button resumeButton;
    Image background;
    TextMeshProUGUI text;

    void Start()
    {
        resumeButton = this.GetComponent<Button>();
        background = this.transform.GetChild(0).GetComponent<Image>();
        text = this.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        resumeButton.onClick.AddListener(OnClickResumeButton);
    }

    void Update()
    {
        
    }

    private void OnClickResumeButton()
    {
        // ��ư Ŭ�� �� ���� ���
        ButtonSoundManager.Instance.PlayOnPointerEnterSound1();

        // �Ͻ� ���� UI�� �����Ѵ�
        PauseUIControl.Instance.SetActive(false);

        // ���� ������ �ٽ� ����
        text.color = Color.white;
        background.color = Color.black;
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
