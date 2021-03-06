using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChancePanel : MonoBehaviour
{
    [SerializeField] private GameObject mainCanvas = null;
    [SerializeField] private GameObject timeLine = null;
    private float currentTime = 0f;
    private bool isWork = false;

    public void StartWork()
    {
        isWork = true;
        timeLine.transform.localScale = new Vector3(1, 1, 1);
        currentTime = 0f;
    }

    public void EndWork()
    {
        isWork = false;
    }

    private void OnEnable()
    {
        timeLine.transform.localScale = new Vector3(1, 1, 1);
        currentTime = 0f;
        isWork = false;
    }

    private void Update()
    {
        if (isWork)
        {
            if (currentTime < 3f)
            {
                timeLine.transform.localScale = new Vector3(1f - currentTime / 3f, 1, 1);
                currentTime += Time.deltaTime;
            }
            else
            {
                isWork = false;
                //mainCanvas.GetComponent<MenuScript>().ToLosing();
            }
        }
    }

    private void OnDisable()
    {
        timeLine.transform.localScale = new Vector3(1, 1, 1);
        currentTime = 0f;
        isWork = false;
    }
}
