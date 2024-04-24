using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSoundManager : MonoBehaviour
{
    private AudioSource boxSound;

    private void Awake()
    {
        boxSound = this.GetComponent<AudioSource>();
        boxSound.volume = 0.1f * ConfigManager.Instance.masterVolume * ConfigManager.Instance.effectVolume;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PlayBoxSound()
    {
        boxSound.Play();
    }
}
