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

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }
}
