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

    public bool isMasterVolumeChanged = false;
    public bool isBgmVolumeChanged = false;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        masterVolume = 0.5f;
        bgmVolume = 1.0f;
        effectVolume = 1.0f;
    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
