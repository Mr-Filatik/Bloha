using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    #region SerializeField Variables

    [SerializeField] private Sprite toggleOn = null;
    [SerializeField] private Sprite toggleOff = null;

    #endregion

    #region Public Properties



    #endregion

    #region Private Variables

    private InitController initData = null;

    #endregion

    #region Public Methods

    public void ChangeState()
    {
        initData.SetToggleState(gameObject.name, gameObject.GetComponent<Toggle>().isOn);
    }

    #endregion

    #region Private Methods

    private void Awake()
    {
        initData = GameObject.Find("Init").GetComponent<InitController>();
        gameObject.GetComponent<Toggle>().isOn = initData.GetToggleState(gameObject.name);
        gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = initData.GetText(gameObject.name);
    }

    #endregion
}
