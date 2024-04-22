using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetBoxUIControl : MonoBehaviour
{
    private static GetBoxUIControl instance;
    public static GetBoxUIControl Instance
    {
        get
        {
            if (null == instance)
                return null;

            return instance;
        }
    }

    TextMeshProUGUI proUGUI;
    public int getBoxCount = 0;

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
            getBoxCount++;
            proUGUI.text = "x " + getBoxCount;
            this.gameObject.SetActive(ret);
        }
        else
        {
            getBoxCount = 0;
            proUGUI.text = "x 0";
            this.gameObject.SetActive(ret);
        }
    }    
}
