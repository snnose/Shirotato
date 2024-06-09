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
        // 게임설명 2페이지로 교체
        this.transform.parent.GetChild(0).GetComponent<Image>().sprite =
            Resources.Load<Sprite>("Sprites/Background/ControlGuide_2p_b");

        // 버튼 비활성화
        this.gameObject.SetActive(false);
    }
}
