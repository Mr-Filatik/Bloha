using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StepScript : MonoBehaviour
{
    [SerializeField] private float transparency;

    [SerializeField] private GameObject leftPart = null;
    [SerializeField] private GameObject centerPart = null;
    [SerializeField] private GameObject rightPart = null;

    public void SetTransparency(float input)
    {
        transparency = input;
        leftPart.GetComponentInChildren<Image>().color = new Color(1, 1, 1, input);
        centerPart.GetComponentInChildren<Image>().color = new Color(1, 1, 1, input);
        rightPart.GetComponentInChildren<Image>().color = new Color(1, 1, 1, input);
    }

    private void Awake()
    {
        transparency = 1f;
    }

    private void Update()
    {
        
    }
}
