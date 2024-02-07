using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUIControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
