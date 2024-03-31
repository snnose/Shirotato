using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundSetting : MonoBehaviour
{
    // singleton
    private static RoundSetting instance;
    public static RoundSetting Instance
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
        
    }

    void Update()
    {
        
    }
}
