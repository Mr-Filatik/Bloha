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

    #endregion

    #region Public Methods

    public void ChangeState()
    {
        if (state)
        {
            image.transform.DOMoveX(-100f + gameObject.transform.position.x, 0.5f);
            image.DOColor(Color.red, 0.5f);
        }
        else
        {
            image.transform.DOMoveX(100f + gameObject.transform.position.x, 0.5f);
            image.DOColor(Color.green, 0.5f);
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
            image.transform.localPosition = new Vector3(100f, 0f, 0f);
            image.color = Color.green;
        }
        else
        {
            image.transform.localPosition = new Vector3(-100f, 0f, 0f);
            image.color = Color.red;
        }
    }
    #endregion
}
