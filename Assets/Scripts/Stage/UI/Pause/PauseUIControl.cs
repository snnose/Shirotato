using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUIControl : MonoBehaviour
{
    // singleton
    private static PauseUIControl instance;
    public static PauseUIControl Instance
    {
        get
        {
            if (null == instance)
                return null;

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        SetActive(false);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetActive(bool ret)
    {
        if (ret)
        {
            Time.timeScale = 0.0f;
            this.gameObject.SetActive(ret);
            this.transform.position = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        }
        else
        {
            this.transform.position = new Vector2(0, -Screen.height - 100);
            this.gameObject.SetActive(ret);
            Time.timeScale = 1.0f;
        }
    }
}
