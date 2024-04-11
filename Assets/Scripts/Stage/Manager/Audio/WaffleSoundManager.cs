using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaffleSoundManager : MonoBehaviour
{
    private static WaffleSoundManager instance;
    public static WaffleSoundManager Instance
    {
        get
        {
            if (null == instance)
                return null;

            return instance;
        }
    }
    private AudioSource[] waffleSounds;

    int num = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        waffleSounds = this.GetComponents<AudioSource>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PlayWaffleSound()
    {
        waffleSounds[num].volume = 0.2f;
        waffleSounds[num].Play();

        num++;

        if (num >= 5)
            num = 0;
    }
}
