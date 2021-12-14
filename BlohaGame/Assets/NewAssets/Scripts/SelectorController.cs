using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectorController : MonoBehaviour
{
    #region SerializeField Variables

    [SerializeField] private Text text = null;
    [SerializeField] private Text item = null;
    [SerializeField] private Button rightButton = null;
    [SerializeField] private Button leftButton = null;
    [SerializeField] private Button saveButton = null;

    #endregion

    #region Public Properties

    //public bool State => state;

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
                item.text = states[state];
            }
        }
        else
        {
            if (state > 0)
            {
                state--;
                item.text = states[state];
            }
        }
        if (state == 0)
        {
            leftButton.interactable = false;
        }
        else
        {
            leftButton.interactable = true;
        }
        if (state == states.Length - 1)
        {
            rightButton.interactable = false;
        }
        else
        {
            rightButton.interactable = true;
        }
        if (states[state] != initData.GetLanguage())
        {
            saveButton.interactable = true;
        }
        else
        {
            saveButton.interactable = false;
        }
    }

    public void SaveState()
    {
        initData.SetLanguage(states[state]);
        if (states[state] != initData.GetLanguage())
        {
            saveButton.interactable = true;
        }
        else
        {
            saveButton.interactable = false;
        }
    }

    #endregion

    #region Private Methods

    private void Awake()
    {
        initData = GameObject.Find("Init").GetComponent<InitController>();
        text.text = initData.GetText(name);
        states = initData.GetLanguages();
        for (int i = 0; i < states.Length; i++)
        {
            if (states[i] == initData.GetLanguage())
            {
                state = i;
            }
        }
        item.text = states[state];
        if (state == 0)
        {
            leftButton.interactable = false;
        }
        else
        {
            leftButton.interactable = true;
        }
        if (state == states.Length - 1)
        {
            rightButton.interactable = false;
        }
        else
        {
            rightButton.interactable = true;
        }
        saveButton.interactable = false;
    }
    #endregion
}
