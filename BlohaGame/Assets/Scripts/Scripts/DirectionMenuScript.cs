using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject directionObject = null;
    [SerializeField] private float borderValue;
    [SerializeField] private float speed = 2;
    [SerializeField] private float acceleration = 5;

    private float direction = 0;
    private bool leftRigth = true;
    private bool isGame = false;

    public float Direction => direction;

    public void GameStart()
    {
        direction = 0;
        isGame = true;
    }

    public void GamePause()
    {
        isGame = false;
    }

    public void GameEnd()
    {
        isGame = false;
    }

    private void Update()
    {
        if (isGame)
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
