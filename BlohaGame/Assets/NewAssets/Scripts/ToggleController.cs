using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ToggleController : MonoBehaviour
{
    #region SerializeField Variables

    [SerializeField] private Text text = null;
    [SerializeField] private Image image = null;

    #endregion

    #region Public Properties

    //public bool State => state;

    #endregion

    #region Private Variables
    
    private bool state;
    private InitController initData = null;
    private Color trueColor = Color.green;
    private Color falseColor = Color.red;

    #endregion

    #region Public Methods

    public void ChangeState()
    {
        if (state)
        {
            image.transform.DOMoveX(-100f * gameObject.transform.localScale.x + gameObject.transform.position.x, 0.5f);
            image.DOColor(falseColor, 0.5f);
        }
        else
        {
            image.transform.DOMoveX(100f * gameObject.transform.localScale.x + gameObject.transform.position.x, 0.5f);
            image.DOColor(trueColor, 0.5f);
        }
        state = !state;
        initData.SetToggleState(name, state);
    }

    #endregion

    #region Private Methods

    private void Awake()
    {
        initData = GameObject.Find("Init").GetComponent<InitController>();

        LanguageEventManager.OnChangeLanguage.AddListener(ChangeLanguage);

        ChangeLanguage();

        state = initData.GetToggleState(name);
        if (state)
        {
            image.transform.localPosition = new Vector3(100f, 0f, 0f);
            image.color = trueColor;
        }
        else
        {
            image.transform.localPosition = new Vector3(-100f, 0f, 0f);
            image.color = falseColor;
        }
        
    }

    private void ChangeLanguage()
    {
        text.text = initData.GetText(name);
    }

    #endregion
}
