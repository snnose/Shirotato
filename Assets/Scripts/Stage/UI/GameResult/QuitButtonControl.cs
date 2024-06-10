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
        // ��ư Ŭ�� �� ���� ���
        ButtonSoundManager.Instance.PlayOnClickButtonSound1();

        // ������������ �κ�� �̵� �� RoundSetting�� ���� ������Ʈ�� �ı��Ѵ�
        // �ı����� �ʰ� �κ�� �̵��ϸ� RoundSetting�� ���ϼ��� ������ ����
        Destroy(RoundSetting.Instance.gameObject);

        // �κ�� �̵�
        SceneManager.LoadScene("Lobby");

        // timeScale ����ȭ
        Time.timeScale = 1.0f;

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
