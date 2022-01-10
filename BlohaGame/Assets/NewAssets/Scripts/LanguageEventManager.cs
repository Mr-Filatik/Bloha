using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LanguageEventManager
{
    public static UnityEvent OnChangeLanguage = new UnityEvent();

    public static void ChangeLanguage()
    {
        OnChangeLanguage.Invoke();
    }
}
