using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : MonoBehaviour
{
    // Singleton
    private static ConfigManager instance;
    public static ConfigManager Instance
    {
        get
        {
            if (null == instance)
                return null;

            return instance;
        }
    }

    public float masterVolume;
    public float bgmVolume;
    public float effectVolume;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

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
