using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownController : MonoBehaviour
{
    #region SerializeField Variables

    [SerializeField] private GameObject imageOne = null;
    [SerializeField] private GameObject imageTwo = null;
    [SerializeField] private GameObject imageThree = null;
    [SerializeField] private AnimationCurve animationCurveScale = null;
    [SerializeField] private AnimationCurve animationCurveTransparency = null;
    [SerializeField] private GameObject mainMenu = null;

    #endregion

    #region Public Properties



    #endregion

    #region Private Variables

    private GameObject visibleObject = null;
    private float currentTime = 0f;

    #endregion

    #region Public Methods

    public void StartGame()
    {
        visibleObject = imageOne;
    }

    #endregion

    #region Private Methods

    private void OnEnable()
    {
        currentTime = 0f;
        visibleObject = imageOne;
        imageOne.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
        imageTwo.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
        imageThree.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
    }

    private void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            if (currentTime >= animationCurveScale.keys[animationCurveScale.keys.Length - 1].time)
            {
                if (visibleObject == imageThree)
                {
                    imageThree.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    visibleObject = null;
                    gameObject.SetActive(false);
                    //mainMenu.GetComponent<MenuScript>().ToGame();
                }
                if (visibleObject == imageTwo)
                {
                    imageTwo.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    visibleObject = imageThree;
                }
                if (visibleObject == imageOne)
                {
                    imageOne.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    visibleObject = imageTwo;
                }
                currentTime = 0f;
            }
            else
            {
                visibleObject.transform.localScale = new Vector3(animationCurveScale.Evaluate(currentTime), animationCurveScale.Evaluate(currentTime));
                visibleObject.GetComponent<Image>().color = new Color(1, 1, 1, animationCurveTransparency.Evaluate(currentTime));
                currentTime += Time.deltaTime;
            }
        }
    }

    #endregion
}
