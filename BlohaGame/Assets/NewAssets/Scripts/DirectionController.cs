using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionController : MonoBehaviour
{
    //
    [SerializeField] private GameObject directionObject = null;
    [SerializeField] private float borderValue;
    [SerializeField] private float speed = 3;
    [SerializeField] private float acceleration = 5;

    private float direction = 0;
    private bool leftRigth = true;
    private bool isGame = false;
    private bool isPause = false;

    public float Direction => direction;

    public void GameStart()
    {
        direction = 0;
        directionObject.transform.localEulerAngles = new Vector3(0, 0, direction);
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

    private void Awake()
    {
        directionObject.transform.localScale = new Vector3(Screen.width / 1080f, Screen.width / 1080f, 0f);
        directionObject.transform.localPosition = new Vector3(0f, -Screen.height / 2f + 30f * Screen.width / 1080f, 0f);
    }

    private void Update()
    {
        if (isGame && !isPause)
        {
            if (leftRigth)
            {
                direction -= Time.deltaTime * 60 * speed * SpeedBoost();
            }
            else
            {
                direction += Time.deltaTime * 60 * speed * SpeedBoost();
            }
            if (direction > borderValue)
            {
                leftRigth = true;
            }
            if (direction < -borderValue)
            {
                leftRigth = false;
            }
            directionObject.transform.localEulerAngles = new Vector3(0, 0, direction);
        }
    }

    private float SpeedBoost()
    {
        float newSpeedBoost = 0;
        newSpeedBoost = (Mathf.Abs(borderValue) - Mathf.Abs(direction) + acceleration) / Mathf.Abs(borderValue);
        if (newSpeedBoost > 1f)
        {
            return 1f;
        }
        return newSpeedBoost;
    }
}
