using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameStart : MonoBehaviour, IPointerEnterHandler
{
    Button startButton;

    void Start()
    {
        startButton = this.GetComponent<Button>();
        startButton.onClick.AddListener(OnClickStartButton);
    }

    void Update()
    {

    }

    private void OnClickStartButton()
    {
        // Ư�� ���� UI �÷���
        IndividualityUIControl.Instance.SetActive(true);

        // ���� ���
        ButtonSoundManager.Instance.PlayOnClickButtonSound1();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ButtonSoundManager.Instance.PlayOnPointerEnterSound1();
    }
}
