using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    #region SerializeField Variables



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
        //gameObject.GetComponent<Toggle>().isOn = initData.GetToggleState(gameObject.name);
        Text text = gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        //добавить изменение размера текста в зависимости от количества символов
        text.text = initData.GetText(gameObject.name);
    }

    #endregion
}
