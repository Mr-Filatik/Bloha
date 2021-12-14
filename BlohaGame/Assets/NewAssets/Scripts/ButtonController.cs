using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    #region SerializeField Variables

    [SerializeField] private Text text = null;
    [SerializeField] private Button button = null;

    #endregion

    #region Public Properties



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
            button.interactable = false;
            button.gameObject.GetComponent<Image>().color = falseColor;
            button.gameObject.GetComponentInChildren<Text>().text = initData.GetText("Disabled");
        }
        else
        {
            button.interactable = true;
            button.gameObject.GetComponent<Image>().color = trueColor;
            button.gameObject.GetComponentInChildren<Text>().text = initData.GetText("Disable");
        }
        state = !state;
        initData.SetToggleState(name, state);
    }

    #endregion

    #region Private Methods

    private void Awake()
    {
        initData = GameObject.Find("Init").GetComponent<InitController>();
        text.text = initData.GetText(name);
        state = initData.GetToggleState(name);
        if (state)
        {
            button.interactable = true;
            button.gameObject.GetComponent<Image>().color = trueColor;
            button.gameObject.GetComponentInChildren<Text>().text = initData.GetText("Disable");
        }
        else
        {
            button.interactable = false;
            button.gameObject.GetComponent<Image>().color = falseColor;
            button.gameObject.GetComponentInChildren<Text>().text = initData.GetText("Disabled");
        }
    }

    #endregion
}
