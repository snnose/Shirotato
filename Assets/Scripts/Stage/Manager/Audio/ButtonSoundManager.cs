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

    public AudioSource onClickButtonSound1;
    public AudioSource onClickButtonSound2;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    public void PlayOnPointerEnterSound1()
    {
        onPointerEnterSound1.pitch = Random.Range(0.9f, 1.1f);
        onPointerEnterSound1.Play();
    }

    public void PlayOnPointerEnterSound2()
    {
        onPointerEnterSound2.pitch = Random.Range(0.9f, 1.1f);
        onPointerEnterSound2.Play();
    }

    public void PlayOnClickButtonSound1()
    {
        onClickButtonSound1.pitch = Random.Range(0.9f, 1.1f);
        onClickButtonSound1.Play();
    }

    public void PlayOnClickButtonSound2()
    {
        onClickButtonSound2.pitch = Random.Range(0.9f, 1.1f);
        onClickButtonSound2.Play();
    }
}
