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
        // 포인터 진입 음성 출력
        ButtonSoundManager.Instance.PlayOnPointerEnterSound1();
    }

    // 버튼을 누르면 도감 UI 활성화
    private void OnClickButton()
    {
        // 버튼 클릭 음성 출력
        ButtonSoundManager.Instance.PlayOnClickButtonSound1();
        IllustGuideUI.GetComponent<IllustGuideControl>().SetActive(true);
    }
}
