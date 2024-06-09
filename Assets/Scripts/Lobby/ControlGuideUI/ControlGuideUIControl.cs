using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlGuideUIControl : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            this.SetActive(false);
    }

    public void SetActive(bool ret)
    {
        if (ret)
        {
            this.gameObject.SetActive(ret);
            this.transform.position = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);

            // ȭ��ǥ ��ư Ȱ��ȭ
            this.transform.GetChild(1).gameObject.SetActive(true);

            // �̹��� ���� ���� ù��° �������� ����
            this.transform.GetChild(0).GetComponent<Image>().sprite =
                Resources.Load<Sprite>("Sprites/Background/ControlGuide_1p_b");
        }
        else
        {
            this.transform.position = new Vector2(0f, Screen.height * -1.0f);
            this.gameObject.SetActive(ret);
        }
    }
}
