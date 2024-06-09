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
    public AudioSource flowerThief;
    public AudioSource question;
    public AudioSource astronautSong;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);

        if (!pale.isPlaying)
        {
            StartCoroutine(PlayPale());
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
            pale.volume = 0.1f * ConfigManager.Instance.masterVolume * ConfigManager.Instance.bgmVolume;
            flowerThief.volume = 0.1f * ConfigManager.Instance.masterVolume * ConfigManager.Instance.bgmVolume;
            question.volume = 0.1f * ConfigManager.Instance.masterVolume * ConfigManager.Instance.bgmVolume;
            astronautSong.volume = 0.1f * ConfigManager.Instance.masterVolume * ConfigManager.Instance.bgmVolume;
        }
    }

    public IEnumerator PlayPale()
    {
        pale.Play();
        float length = pale.clip.length;
        
        yield return new WaitForSecondsRealtime(length);

        StartCoroutine(PlayFlowerThief());
    }

    public IEnumerator PlayFlowerThief()
    {
        flowerThief.Play();
        float length = flowerThief.clip.length;

        yield return new WaitForSecondsRealtime(length);

        StartCoroutine(PlayQuestion());
    }

    public IEnumerator PlayQuestion()
    {
        question.Play();
        float length = question.clip.length;

        yield return new WaitForSecondsRealtime(length);

        StartCoroutine(PlayAstronaut());
    }

    public IEnumerator PlayAstronaut()
    {
        astronautSong.Play();
        float length = astronautSong.clip.length;

        yield return new WaitForSecondsRealtime(length);

        StartCoroutine(PlayPale());
    }
}
