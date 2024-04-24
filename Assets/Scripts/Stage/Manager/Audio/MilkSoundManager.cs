using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkSoundManager : MonoBehaviour
{
    private AudioSource milkSound;

    void Start()
    {
        milkSound = this.GetComponent<AudioSource>();
        milkSound.volume = 0.1f * ConfigManager.Instance.masterVolume * ConfigManager.Instance.effectVolume;
    }

    void Update()
    {
        
    }

    public void PlayMilkSound()
    {
        milkSound.Play();
    }
}
