using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageController : MonoBehaviour
{
    #region SerializeField Variables

    [SerializeField] private Text text = null;
    [SerializeField] private Image item = null;
    [SerializeField] private Button rightButton = null;
    [SerializeField] private Button leftButton = null;

    #endregion

    #region Public Properties



    #endregion

    #region Private Variables

    private int state = 0;
    private string[] states = null;
    private InitController initData = null;

    #endregion

    #region Public Methods

    public void ChangeState(bool direction)
    {
        if (direction)
        {
            if (state < states.Length - 1)
            {
                state++;
            }
            else
            {
                state = 0;
            }
        }
        else
        {
            if (state > 0)
            {
                state--;
            }
            else
            {
                state = states.GetLength(0) - 1;
            }
        }
        initData.SetLanguage(states[state]);
        LanguageEventManager.ChangeLanguage();
    }

    #endregion

    #region Private Methods

    private void Awake()
    {
        initData = GameObject.Find("Init").GetComponent<InitController>();
        LanguageEventManager.OnChangeLanguage.AddListener(ChangeLanguage);
        states = initData.GetLanguages();
        for (int i = 0; i < states.Length; i++)
        {
            if (states[i] == initData.GetLanguage())
            {
                state = i;
            }
        }
        ChangeLanguage();
    }

    private void ChangeLanguage()
    {
        text.text = initData.GetText(name);
        item.sprite = initData.GetFlag(states[state]);
    }
    #endregion
}
