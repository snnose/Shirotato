using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
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
    }
}
