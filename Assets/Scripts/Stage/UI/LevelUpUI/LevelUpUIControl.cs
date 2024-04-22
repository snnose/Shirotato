using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelUpUIControl : MonoBehaviour
{
    private static LevelUpUIControl instance;
    public static LevelUpUIControl Instance
    {
        get
        {
            if (null == instance)
                return null;

            return instance;
        }
    }

    TextMeshProUGUI proUGUI;
    public int getLevelUpCount = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        proUGUI = this.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        SetActive(false);
    }

    void Update()
    {
        
    }

    public void SetActive(bool ret)
    {
        if (ret)
        {
            getLevelUpCount++;
            proUGUI.text = "x " + getLevelUpCount;
            this.gameObject.SetActive(ret);
        }
        else
        {
            getLevelUpCount = 0;
            proUGUI.text = "x 0";
            this.gameObject.SetActive(ret);
        }
    }
}
