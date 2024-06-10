using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class QuitButtonControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Button quitButton;
    Image background;
    TextMeshProUGUI text;

    private void Awake()
    {
        quitButton = this.GetComponent<Button>();
        background = this.transform.GetChild(0).GetComponent<Image>();
        text = this.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        quitButton.onClick.AddListener(OnClickQuitButton);
    }

    private void OnClickQuitButton()
    {
        // 버튼 클릭 시 사운드 출력
        ButtonSoundManager.Instance.PlayOnClickButtonSound1();

        // 스테이지에서 로비로 이동 시 RoundSetting을 갖는 오브젝트를 파괴한다
        // 파괴하지 않고 로비로 이동하면 RoundSetting의 단일성에 문제가 생김
        Destroy(RoundSetting.Instance.gameObject);

        // 로비로 이동
        SceneManager.LoadScene("Lobby");

        // timeScale 정상화
        Time.timeScale = 1.0f;

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
