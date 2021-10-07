﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    #region Field
    public GameObject menuPanel = null;
    public GameObject gamePanel = null;

    public GameObject[] gameStart = null;

    public GameObject gamePauseButton = null;
    public GameObject gameJumpButton = null;

    public GameObject[] steps = null;

    public GameObject directionArrow = null;

    public GameObject[] lets = null;
    #endregion

    #region SecretField
    private bool game = false;

    [SerializeField] private float speed;
    [SerializeField] private float speedDefault = 2f;

    private int playerHealth = 3;
    private int playerStep = 3;
    [SerializeField] private float playerPosition = 0;
    [SerializeField] private float playerPositionNew = 0;
    [SerializeField] private AnimationCurve playerAnimationCurveY;
    [SerializeField] private AnimationCurve playerAnimationCurveX;
    [SerializeField] private AnimationCurve playerAnimationCurveS;

    //private float startPosition = 0f;
    //GameObject spawnedObject;
    //GameObject[] spawnedObjects;
    GameObject player; // объект игрока, для вызова анимаций и т.п.
    private bool isJump = false;
    private Transform stepFrom = null;
    private Transform stepTo = null;
    private float directionJump = 0f; //

    private float cooldownForJumpButton = 1f;
    private float timeForButton;

    private float animationTime = 1f;
    private float currentTime = 0f;
    private float distance = 0f;
    private float interval = 0f;
    private float difference = 0f;
    private float direction = 0f;
    private float directionLimit = 45f;
    //Vector3 fromPosition;
    //Vector3 toPosition;
    //int direction = 0;
    //float timeForDirection = 0;
    #endregion

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        menuPanel.SetActive(true);
        gamePanel.SetActive(false);

        gameJumpButton.SetActive(false);
        gamePauseButton.SetActive(false);

        for (int i = 0; i < gameStart.Length; i++)
        {
            gameStart[i].SetActive(false);
        }

        steps[0].transform.localPosition = new Vector3(0, 0, 0);
        steps[0].transform.localScale = new Vector3(1, 1, 1);
        for (int i = 1; i < steps.GetLength(0); i++)
        {
            steps[i].transform.localPosition = new Vector3(0, steps[i - 1].transform.localPosition.y + 108 * (steps[i - 1].transform.localScale.x - 0.02f), 0);
            steps[i].transform.localScale = new Vector3(steps[i - 1].transform.localScale.x - 0.02f, steps[i - 1].transform.localScale.y - 0.02f, 0);
        }

        //needs changes
        PlayerMovement();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (game)
        {
            speed = speedDefault;// + Time.deltaTime * 10 * playerStep * steps[0].transform.position.y / 100; //исправить

            DirectionMovement();

            LadderMovement();

            //authomatic jump 
            if (playerStep <= 2)
            {
                AutomaticJump(1);
            }

            if (isJump)
            {
                Jump();
            }
            else
            {
                PlayerMovement();
            }
        }
    }

    void LadderMovement()
    {
        for (int i = 0; i < steps.GetLength(0); i++)
        {
            steps[i].transform.localScale = new Vector3((float)(100 - i * 2 + (-steps[0].transform.localPosition.y * 2) / 100) / 100, (float)(100 - i * 2 + (-steps[0].transform.localPosition.y * 2) / 100) / 100, 1);
            steps[i].transform.localPosition = new Vector3(steps[i].transform.localPosition.x, steps[i].transform.localPosition.y - Time.deltaTime * 10 * speed * steps[i].transform.localScale.x, steps[i].transform.localPosition.z);
        }
        if (steps[1].transform.localScale.y >= 1)
        {
            GameObject none = steps[0];
            for (int i = 1; i < steps.GetLength(0); i++)
            {
                steps[i - 1] = steps[i];
            }
            steps[steps.GetLength(0) - 1] = none;
            steps[steps.GetLength(0) - 1].transform.localPosition = new Vector3(0, steps[steps.GetLength(0) - 2].transform.localPosition.y + 108 * (steps[steps.GetLength(0) - 2].transform.localScale.x - 0.02f), 0);
            steps[steps.GetLength(0) - 1].transform.SetSiblingIndex(0);
            playerStep--;
            //calling the obstacle generating method
            CreateLets(steps[steps.GetLength(0) - 1]);
        }
    }

    void LadderMovementReverse(int number)
    {
        //maybe not needed
    }

    void PlayerMovement()
    {
        //scale steps = 0.8f and scale player = 1f
        player.transform.localScale = new Vector3(steps[playerStep].transform.localScale.x, steps[playerStep].transform.localScale.y, steps[playerStep].transform.localScale.z);
        //player.transform.localPosition = new Vector3(steps[playerStep].transform.localPosition.x * 0.8f, steps[playerStep].transform.localPosition.y * 0.8f, steps[playerStep].transform.localPosition.z * 0.8f);
        player.transform.localPosition = new Vector3(playerPosition * player.transform.localScale.x * 0.8f, steps[playerStep].transform.localPosition.y * 0.8f, steps[playerStep].transform.localPosition.z * 0.8f);
    }

    void DirectionMovement()
    {
        direction += Random.Range(-2f, 2f);
        if (direction < -directionLimit)
        {
            direction = -directionLimit;
        }
        if (direction > directionLimit)
        {
            direction = directionLimit;
        }
        directionArrow.transform.localEulerAngles = new Vector3(0, 0, direction);
    }

    void CreateLets(GameObject inputStep)
    {
        //createStaticLet
        StepController stepController = inputStep.GetComponent<StepController>();
        stepController.ClearLets();
        stepController.AddLetStatic((float)Random.Range(-200, 200), lets[0]);
    }

    public void AutomaticJump(int numberOfStep)
    {
        currentTime = 0f;
        isJump = true;
        stepFrom = steps[playerStep].transform;
        playerStep += numberOfStep;
        stepTo = steps[playerStep].transform;

        playerPositionNew = playerPosition - Mathf.Tan(10 * 0.0174533f) * (stepTo.localPosition.y - stepFrom.localPosition.y) * 0.8f; //
        Debug.Log(playerPositionNew);
    }

    public void Jump()
    {
        if (currentTime <= animationTime)
        {
            distance = (stepTo.localPosition.y - stepFrom.localPosition.y) * 0.8f;
            difference = (stepFrom.localScale.x - stepTo.localScale.x) * 1f;
            interval = (playerPositionNew * stepTo.localScale.x - playerPosition * stepFrom.localScale.x) * 0.8f;
            //update
            player.transform.localPosition = new Vector3(playerPosition * 0.8f + playerAnimationCurveX.Evaluate(currentTime) * interval, stepFrom.localPosition.y * 0.8f + playerAnimationCurveY.Evaluate(currentTime) * distance, player.transform.localPosition.z);
            player.transform.localScale = new Vector3(stepFrom.localScale.x - playerAnimationCurveS.Evaluate(currentTime) * difference, stepFrom.localScale.y - playerAnimationCurveS.Evaluate(currentTime) * difference, stepTo.localScale.z);
            currentTime += Time.deltaTime;
        }
        else
        {
            playerPosition = playerPositionNew;
            animationTime = 1f; //mb
            currentTime = 0f;
            isJump = false;
        }
    }

    public void JumpButtonUp()
    {
        //maybe you need to add fatigue after a long press
        if (Time.realtimeSinceStartup - timeForButton > cooldownForJumpButton)
        {
            if (!isJump)
            {
                animationTime = 2f; //mb
                AutomaticJump(2);
            }
        }
        else
        {
            if (!isJump)
            {
                AutomaticJump(1);
            }
        }
    }

    public void JumpButtonDown()
    {
        timeForButton = Time.realtimeSinceStartup;
    }

    public void PlayButton()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);

        LadderMovement();

        StartCoroutine(startWaiter());
    }

    IEnumerator startWaiter()
    {
        gameStart[0].SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        gameStart[0].SetActive(false);
        gameStart[1].SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        gameStart[1].SetActive(false);
        gameStart[2].SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        gameStart[2].SetActive(false);
        gameStart[3].SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        gameStart[3].SetActive(false);

        game = true;
        gameJumpButton.SetActive(true);
        gamePauseButton.SetActive(true);
        /*for (int i = 0; i < gameStart.Length; i++)
        {
            for (int j = 0; j < gameStart.Length; j++)
            {
                if (i == j)
                {
                    gameStart[i].SetActive(true);
                }
                else
                {
                    gameStart[i].SetActive(false);
                }
            }
            yield return new WaitForSecondsRealtime(1);
        }
        for (int i = 0; i < gameStart.Length; i++)
        {
            gameStart[i].SetActive(false);
        }
        yield return new WaitForSecondsRealtime(0);*/
    }

    public void PauseButton()
    {
        MenuButton();
    }

    public void MenuButton()
    {
        menuPanel.SetActive(true);

        game = false;
        gameJumpButton.SetActive(false);
        gamePauseButton.SetActive(false);

        gamePanel.SetActive(false);
    }

    public void ShopButton()
    {
        Debug.Log("Shop");
    }

    public void SettingButton()
    {
        Debug.Log("Setting");
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
