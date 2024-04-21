using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSoundManager : MonoBehaviour
{
    // singleton
    private static ButtonSoundManager instance;
    public static ButtonSoundManager Instance
    {
        get
        {
            if (null == instance)
                return null;

            return instance;
        }
    }

    public AudioSource onPointerEnterSound1;
    public AudioSource onPointerEnterSound2;

    public AudioSource onButtonClickSound1;
    public AudioSource onButtonClickSound2;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    public void PlayOnPointerEnterSound1()
    {
        onPointerEnterSound1.pitch = Random.Range(0.8f, 1.2f);
        onPointerEnterSound1.Play();
    }

    public void PlayOnPointerEnterSound2()
    {
        onPointerEnterSound2.pitch = Random.Range(0.8f, 1.2f);
        onPointerEnterSound2.Play();
    }

    public void PlayOnButtonClickSound1()
    {
        onButtonClickSound1.pitch = Random.Range(0.8f, 1.2f);
        onButtonClickSound1.Play();
    }

    public void PlayOnButtonClickSound2()
    {
        onButtonClickSound2.pitch = Random.Range(0.8f, 1.2f);
        onButtonClickSound2.Play();
    }
}
