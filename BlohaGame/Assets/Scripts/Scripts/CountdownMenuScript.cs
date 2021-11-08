using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject imageOne = null;
    [SerializeField] private GameObject imageTwo = null;
    [SerializeField] private GameObject imageThree = null;
    [SerializeField] private AnimationCurve animationCurveScale = null;
    [SerializeField] private AnimationCurve animationCurveTransparency = null;
    [SerializeField] private GameObject mainMenu = null;

    private GameObject visibleObject = null;
    private bool isGame = false;
    private float currentTime = 0f;

    public void StartGame()
    {
        isGame = true;
    }

    private void Awake()
    {
        isGame = false;
        currentTime = 0f;
        visibleObject = imageOne;

        imageOne.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        imageTwo.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        imageThree.GetComponent<Image>().color = new Color(1, 1, 1, 0);
    }

    private void Update()
    {
        if (isGame)
        {
            if (currentTime >= animationCurveScale.keys[animationCurveScale.keys.Length - 1].time)
            {
                if (visibleObject == imageThree)
                {
                    visibleObject = null;
                    isGame = false;
                    mainMenu.GetComponent<MenuScript>().ToGame();
                }
                if (visibleObject == imageTwo)
                {
                    visibleObject = imageThree;
                }
                if (visibleObject == imageOne)
                {
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
}
