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
        
    }

    void Update()
    {
        
    }

    public void PlayLevelUpSound()
    {
        levelUpSound.Play();
    }
}
