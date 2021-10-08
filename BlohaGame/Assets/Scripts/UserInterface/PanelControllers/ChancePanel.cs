using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChancePanel : MonoBehaviour
{
    [SerializeField] private GameObject timeLine = null;
    private float currentTime = 0f;
    private bool isWork = false;
    
    private void OnEnable()
    {
        timeLine.transform.localScale = new Vector3(1, 1, 1);
        currentTime = 0f;
        isWork = true;
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
                //gameObject.SetActive(false);//call
            }
        }
    }

    private void OnDisable()
    {
        timeLine.transform.localScale = new Vector3(1, 1, 1);
        currentTime = 0f;
        isWork = false;
    }

    public void StartWork()
    {
        isWork = true;
    }

    public void EndWork()
    {
        isWork = false;
    }
}
