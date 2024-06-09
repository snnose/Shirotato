using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IllustGuideComment : MonoBehaviour
{
    private static IllustGuideComment instance = null;
    public static IllustGuideComment Instance
    {
        get
        {
            if (null == instance)
                return null;

            return instance;
        }
    }

    private TextMeshProUGUI textPro;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        textPro = this.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        textPro.text = "";
    }

    public void SetCommentText(string text)
    {
        textPro.text = text;
    }
}
