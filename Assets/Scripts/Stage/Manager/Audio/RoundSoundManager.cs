using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundSoundManager : MonoBehaviour
{
    public AudioSource roundEndSound;
    public AudioSource roundDefeatSound;

    public AudioSource levelUpSound;

    void Start()
    {
        roundEndSound.volume = 0.1f * ConfigManager.Instance.masterVolume* ConfigManager.Instance.effectVolume;
        //roundDefeatSound.volume = 0.1f ConfigManager.Instance.masterVolume * ConfigManager.Instance.effectVolume;
        levelUpSound.volume = 0.1f * ConfigManager.Instance.masterVolume * ConfigManager.Instance.effectVolume;
    }

    void Update()
    {
        
    }

    public void PlayLevelUpSound()
    {
        levelUpSound.Play();
    }
}
