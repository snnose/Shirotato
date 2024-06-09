using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditUIControl : MonoBehaviour
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
            this.gameObject.SetActive(true);
            this.transform.position = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        }
        else
        {
            this.transform.position = new Vector2(Screen.width * -1.0f - 100f, Screen.height * -1.0f - 100f);
            this.gameObject.SetActive(false);
        }
    }
}
