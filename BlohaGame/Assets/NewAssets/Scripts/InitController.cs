using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitController : MonoBehaviour
{
    #region SerializeField Variables

    [SerializeField] private SettingsController settingsController = null; 

    #endregion

    #region Public Properties



    #endregion

    #region Private Variables



    #endregion

    #region Public Methods

    public bool GetToggleState(string name)
    {
        if (PlayerPrefs.HasKey(name))
        {
            if (PlayerPrefs.GetString(name) == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            PlayerPrefs.SetString(name, "true"); //поставил так
            return false;
        }
    }

    public void SetToggleState(string name, bool value)
    {
        if (PlayerPrefs.HasKey(name))
        {
            if (value)
            {
                PlayerPrefs.SetString(name, "true");
            }
            else
            {
                PlayerPrefs.SetString(name, "false");
            }
        }
        else
        {
            PlayerPrefs.SetString(name, "true"); //поставил так
        }
    }

    public string GetText(string name)
    {
        string language = GetLanguage();
        if (language == "ENGLISH")
        {
            if (name == "Sound") return "SOUND";
            if (name == "Music") return "MUSIC";
            if (name == "Vibro") return "VIBRO";
            if (name == "Language") return "LANGUAGE";
            if (name == "Ads") return "ADS";
            if (name == "Disable") return "DISABLE";
            if (name == "Disabled") return "DISABLED";
        }
        if (language == "РУССКИЙ")
        {
            if (name == "Sound") return "ЗВУК";
            if (name == "Music") return "МУЗЫКА";
            if (name == "Vibro") return "ВИБРАЦИЯ";
            if (name == "Language") return "ЯЗЫК";
            if (name == "Ads") return "РЕКЛАМА";
            if (name == "Disable") return "ОТКЛЮЧИТЬ";
            if (name == "Disabled") return "ОТКЛЮЧЕНО";
        }
        return "NONE";
    }

    public string GetLanguage()
    {
        string language = "ENGLISH";
        if (PlayerPrefs.HasKey("Language"))
        {
            language = PlayerPrefs.GetString("Language");
        }
        else
        {
            switch (Application.systemLanguage)
            {
                //case SystemLanguage.Russian: PlayerPrefs.SetString("Language", "РУССКИЙ"); break;
                default: PlayerPrefs.SetString("Language", "ENGLISH"); break;
            }
        }
        return language;
    }

    public void SetLanguage(string name)
    {
        if (PlayerPrefs.HasKey("Language"))
        {
            PlayerPrefs.SetString("Language", name);
        }
        else
        {
            switch (Application.systemLanguage)
            {
                case SystemLanguage.Russian: PlayerPrefs.SetString("Language", "РУССКИЙ"); break;
                default: PlayerPrefs.SetString("Language", "ENGLISH"); break;
            }
        }
    }


    //----------------------ADS
    private bool adsDisabled = false;

    public void DisabledAds()
    {
        adsDisabled = true;
        //settingsController.ChangeStatus();
    }

    public bool AdsIsDisabled()
    {
        return adsDisabled;
    }

    #endregion

    #region Private Methods

    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.orientation = ScreenOrientation.Portrait;
        Application.targetFrameRate = 60;
        Resources.UnloadUnusedAssets();

        GetLanguage();
    }

    #endregion
}
