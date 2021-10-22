using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StepController : MonoBehaviour
{
    /*private GameObject letStatic;
    private float letStaticCoordinate = 0;

    public void ClearLets()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Destroy(gameObject.transform.GetChild(i).gameObject);
        }
        letStatic = null;
    }

    public void AddLetStatic(float inputCoordinate, GameObject inputPrefab)
    {
        letStatic = inputPrefab;
        GameObject none = Instantiate(inputPrefab, gameObject.transform);
        letStaticCoordinate = inputCoordinate;

        inputPrefab.GetComponent<LetStaticController>().GetLetSize();
        //закончил

        none.transform.localPosition = new Vector3(letStaticCoordinate, 40 + Random.Range(-5f, 5f), 0);
        none.transform.localEulerAngles = new Vector3(0, 0, Random.Range(-5f, 5f));
        //none.transform.localScale = new Vector3(Random.Range(-1, 2), Random.Range(-1, 2), 1);
    }

    public bool IsLetStatic(float inputPlayerCoordinate)
    {
        if (letStatic != null)
        {
            float size = letStatic.GetComponent<LetStaticController>().GetLetSize();
            if (letStaticCoordinate - size >= inputPlayerCoordinate && letStaticCoordinate + size <= inputPlayerCoordinate)
            {
                return true;
            }
            return false;
        }
        return false;
    }
    */
    [SerializeField] private Image LeftPart = null;
    [SerializeField] private Image CenterPart = null;
    [SerializeField] private Image RightPart = null;

    private StepPartState stepPartStateLeft = StepPartState.Stable;
    private StepPartState stepPartStateCenter = StepPartState.Stable;
    private StepPartState stepPartStateRight = StepPartState.Stable;
    private GameObject[] lets = null;

    public void CreateStepPart(StepPartState inputStepPartStateLeft, StepPartState inputStepPartStateCenter, StepPartState inputStepPartStateRight)
    {
        stepPartStateLeft = inputStepPartStateLeft;
        stepPartStateCenter = inputStepPartStateCenter;
        stepPartStateRight = inputStepPartStateRight;
        if (stepPartStateLeft == StepPartState.Empty)
        {
            LeftPart.sprite = null;
            LeftPart.color = new Color(1, 1, 1, 0);
        }
        if (stepPartStateCenter == StepPartState.Empty)
        {
            CenterPart.sprite = null;
            CenterPart.color = new Color(1, 1, 1, 0);
        }
        if (stepPartStateRight == StepPartState.Empty)
        {
            RightPart.sprite = null;
            RightPart.color = new Color(1, 1, 1, 0);
        }
    }

    public void CreateStepLet(GameObject inputGameObject)
    {
        lets = new GameObject[1];
    }

    private float borderMain = 200f;
    private float borderPart = 67f;

    public StepPartState GetStepPartState(float inputPosition)
    {
        if (inputPosition > -borderMain && inputPosition < borderMain)
        {
            if (inputPosition > -borderPart && inputPosition < borderPart)
            {
                return stepPartStateCenter;
            }
            else
            {
                if (inputPosition < 0)
                {
                    return stepPartStateLeft;
                }
                else
                {
                    return stepPartStateRight;
                }
            }
        }
        else
        {
            return StepPartState.Empty;
        }
    }

    public LetState GetLetState(float inputPosition)
    {
        for (int i = 0; i < lets.GetLength(0); i++)
        {
            if (true)
            {
                return LetState.Empty;
            }
            return LetState.Static;
        }
        return LetState.Empty;
    }
}

public enum StepPartState
{
    Stable, //обычная часть ступени
    Unstable, //ломающаяся, падающая часть
    Temporary, //ломающееся через время
    Empty //отсутствует часть
}

public enum LetState
{
    Static, //статичное препятствие
    DynamicOn, //динамическое но активное
    DynamicOff, //динамическое спящее
    Empty //отсутствует 
}
