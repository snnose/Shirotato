using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllustGuideControl : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SetActive(false);
    }

    public void SetActive(bool ret)
    {
        if (ret)
        {
            this.gameObject.SetActive(true);
            this.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f);
        }
        else
        {
            this.transform.position = new Vector3(1466, 868);
            this.gameObject.SetActive(false);
        }
    }
}
