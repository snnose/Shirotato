using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigSettingUI : MonoBehaviour
{
    // Singleton
    private static ConfigSettingUI instance;
    public static ConfigSettingUI Instance
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
    }

    void Start()
    {
        SetActive(false);
    }

    void Update()
    {
        Cancel();   
    }

    private void Cancel()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetActive(false);
        }
    }

    public void SetActive(bool ret)
    {
        if (ret)
        {
            this.transform.position = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        }
        else
        {
            this.transform.position = new Vector2(Screen.width, 0f);
        }

        this.gameObject.SetActive(ret);
    }
}
