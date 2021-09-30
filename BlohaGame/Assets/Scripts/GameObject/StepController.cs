﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepController : MonoBehaviour
{
    private GameObject letStatic;
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
}
