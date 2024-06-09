using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlGuideNextButton : MonoBehaviour
{
    Button nextButton;

    private void Awake()
    {
        nextButton = this.GetComponent<Button>();
        nextButton.onClick.AddListener(OnClickNextButton);

    }

    private void OnClickNextButton()
    {
        // ���Ӽ��� 2�������� ��ü
        this.transform.parent.GetChild(0).GetComponent<Image>().sprite =
            Resources.Load<Sprite>("Sprites/Background/ControlGuide_2p_b");

        // ��ư ��Ȱ��ȭ
        this.gameObject.SetActive(false);
    }
}
