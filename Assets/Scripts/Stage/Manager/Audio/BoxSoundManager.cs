using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSoundManager : MonoBehaviour
{
    private AudioSource boxSound;

    private void Awake()
    {
        boxSound = this.GetComponent<AudioSource>();
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
