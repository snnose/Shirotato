using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkSoundManager : MonoBehaviour
{
    private AudioSource milkSound;

    void Start()
    {
        milkSound = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    public void PlayMilkSound()
    {
        milkSound.Play();
    }
}
