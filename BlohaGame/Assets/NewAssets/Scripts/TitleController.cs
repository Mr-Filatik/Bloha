using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    #region SerializeField Variables

    [SerializeField] private Text text = null;

    #endregion

    #region Public Properties



    #endregion

    #region Private Variables

    private InitController initData = null;

    #endregion

    #region Public Methods

    #endregion

    #region Private Methods

    private void Awake()
    {
        initData = GameObject.Find("Init").GetComponent<InitController>();

        LanguageEventManager.OnChangeLanguage.AddListener(ChangeLanguage);

        ChangeLanguage();
    }

    private void ChangeLanguage()
    {
        text.text = initData.GetText(name);
    }
    #endregion
}
