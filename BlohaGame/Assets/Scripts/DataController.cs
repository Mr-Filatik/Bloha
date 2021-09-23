using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour
{
    private bool volume;
    private bool music;
    private bool vibration;

    internal bool Volume
    {
        get
        {
            return volume;
        }
    }

    internal bool Music
    {
        get
        {
            return music;
        }
    }

    internal bool Vibration
    {
        get
        {
            return vibration;
        }
    }

    void Awake()
    {
        SetData();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Set game data on startup
    public void SetData()
    {
        //Set volume
        if (PlayerPrefs.GetString("Volume") == "")
        {
            PlayerPrefs.SetString("Volume", "true");
        }
        volume = StringToBool(PlayerPrefs.GetString("Volume"));

        //Set music
        if (PlayerPrefs.GetString("Music") == "")
        {
            PlayerPrefs.SetString("Music", "true");
        }
        music = StringToBool(PlayerPrefs.GetString("Music"));

        //Set vibration
        if (PlayerPrefs.GetString("Vibration") == "")
        {
            PlayerPrefs.SetString("Vibration", "true");
        }
        vibration = StringToBool(PlayerPrefs.GetString("Vibration"));
    }

    //Convert string to bool
    public bool StringToBool(string input)
    {
        if (input == "true")
        {
            return true;
        }
        return false;
    }
}
