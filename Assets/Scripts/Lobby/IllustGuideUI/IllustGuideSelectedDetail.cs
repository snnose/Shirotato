using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IllustGuideSelectedDetail : MonoBehaviour
{
    private static IllustGuideSelectedDetail instance = null;
    public static IllustGuideSelectedDetail Instance
    {
        get
        {
            if (null == instance)
                return null;

            return instance;
        }
    }

    public GameObject selectedImage;
    public TextMeshProUGUI selectedNameText;
    public TextMeshProUGUI selectedDetailText;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        selectedImage = this.transform.GetChild(1).gameObject;
        selectedNameText = this.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        selectedDetailText = this.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        selectedNameText.text = "";
        selectedDetailText.text = "";
    }
}
