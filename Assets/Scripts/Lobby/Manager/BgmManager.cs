using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmManager : MonoBehaviour
{
    // singleton
    private static BgmManager instance;
    public static BgmManager Instance
    {
        get
        {
            if (null == instance)
                return null;

            return instance;
        }
    }

    public AudioSource pale;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);

        if (!pale.isPlaying)
        {
            pale.Play();
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        // 마스터 및 브금 볼륨 변경 시 적용
        if (ConfigManager.Instance.isBgmVolumeChanged ||
            ConfigManager.Instance.isMasterVolumeChanged)
        {
            pale.volume = 0.05f * ConfigManager.Instance.masterVolume * ConfigManager.Instance.bgmVolume;
        }
    }
}
