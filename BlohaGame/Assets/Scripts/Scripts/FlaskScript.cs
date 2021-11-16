using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskScript : MonoBehaviour
{
    [SerializeField] private GameObject flaskWater = null;
    [SerializeField] private float speed = 1;

    private float currentTime = 0f;
    private bool isGame = false;
    private bool isPause = false;

    public void GameStart()
    {
        flaskWater.transform.localScale = new Vector3(1f, 0f, 1f);
        isGame = true;
        isPause = true;
    }

    public void GamePause()
    {
        isPause = true;
    }

    public void GameContinue()
    {
        isPause = false;
    }

    public void GameEnd()
    {
        isGame = false;
        isPause = false;
    }

    public bool Jump(float input = 0.5f)
    {
        if (flaskWater.transform.localScale.y >= input)
        {
            flaskWater.transform.localScale = new Vector3(flaskWater.transform.localScale.x, flaskWater.transform.localScale.y - input, flaskWater.transform.localScale.z);
            return true;
        }
        else
        {
            return false;
        }
    }

    void Update()
    {
        if (isGame && !isPause)
        {
            if (flaskWater.transform.localScale.y < 1)
            {
                flaskWater.transform.localScale = new Vector3(flaskWater.transform.localScale.x, flaskWater.transform.localScale.y + Time.deltaTime * speed * 0.2f * (flaskWater.transform.localScale.y + 0.2f), flaskWater.transform.localScale.z);
            }
        }
    }
}
