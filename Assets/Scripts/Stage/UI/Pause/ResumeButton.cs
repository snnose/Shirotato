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
        // 버튼 클릭 시 사운드 출력
        ButtonSoundManager.Instance.PlayOnPointerEnterSound1();

        // 일시 정지 UI를 종료한다
        PauseUIControl.Instance.SetActive(false);

        // 원래 색으로 다시 변경
        text.color = Color.white;
        background.color = Color.black;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 포인터 진입 시 사운드 출력
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
