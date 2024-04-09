using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundSetting : MonoBehaviour
{
    // singleton
    private static RoundSetting instance;
    public static RoundSetting Instance
    {
        get
        {
            if (null == instance)
                return null;

            return instance;
        }
    }

    private string individuality = "";
    private string startWeapon = "";
    private int difficulty = 0;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        Screen.SetResolution(1366, 768, false);
    }

    public void SetIndividuality(string individuality)
    {
        this.individuality = individuality;
    }

    public void SetStartWeapon(string startWeaponName)
    {
        this.startWeapon = startWeaponName;
    }

    public void SetDifficulty(int difficulty)
    {
        this.difficulty = difficulty;
    }

    public string GetIndividuality()
    {
        return this.individuality;
    }

    public string GetStartWeapon()
    {
        return this.startWeapon;
    }

    public int GetDifficulty()
    {
        return this.difficulty;
    }
}
