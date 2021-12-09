using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayMenuScript : MonoBehaviour
{
    [Header("Ladder")]
    [SerializeField] private GameObject[] steps = null;
    [SerializeField] private float reductionOfSteps; //0.98
    [SerializeField] private float initialDistance; //108

    [SerializeField] private float decreaseInHeight;
    [SerializeField] private float decreaseInWidth;
    [SerializeField] private float startInWidth;

    private float speed;
    [SerializeField] private float speedDefault;

    [Header("Player")]
    [SerializeField] private GameObject player = null;
    [SerializeField] private int playerStep;
    [SerializeField] private AnimationCurve playerAnimationCurveX = null;
    [SerializeField] private AnimationCurve playerAnimationCurveY = null;
    [SerializeField] private AnimationCurve playerAnimationCurveS = null;
    private float playerPosition;
    private float playerPositionNew;
    private int playerHealth;

    [Header("GameObjects")]
    [SerializeField] private GameObject menuCanvas = null;
    [SerializeField] private GameObject directionObject = null;
    [SerializeField] private GameObject flaskObject = null;
    [SerializeField] private GameObject healthObject = null;
    //private MenuScript menuScript = null;
    private DirectionMenuScript directionMenuScript = null;
    //private FlaskMenuScript flaskMenuScript = null;
    //private HealthMenuScript healthMenuScript = null;

    private bool isGame;
    public bool IsGame => isGame;
    private bool isPause;
    private bool isJump;
    private bool isDoubleJump;
    private float direction;
    private float currentTime;
    [SerializeField] private float cooldownForJumpButton;
    private float timeForButton;

    //hz
    private Transform stepFrom = null;
    private Transform stepTo = null;
    private float animationTime;
    private float distance;
    private float difference;
    private float interval;

    #region Public Methods

    public void GameStart()
    {
        isGame = true;
        isPause = true;
        steps[0].transform.localPosition = new Vector3(0, 0, steps.GetLength(0));
        steps[0].transform.localScale = new Vector3(1, 1, 1);
        for (int i = 1; i < steps.GetLength(0); i++)
        {
            steps[i].transform.localScale = new Vector3(steps[i - 1].transform.localScale.x - (startInWidth - (i) * decreaseInWidth), steps[i - 1].transform.localScale.y - decreaseInHeight, 0);
            steps[i].transform.localPosition = new Vector3(0, steps[i - 1].transform.localPosition.y + (steps[i - 1].transform.localScale.y * initialDistance + steps[i].transform.localScale.y * initialDistance) / 2, steps.GetLength(0) - i);
            if (i == steps.GetLength(0) - 2)
            {
                steps[i].GetComponent<StepScript>().SetTransparency(0.5f);
            }
            if (i == steps.GetLength(0) - 1)
            {
                steps[i].GetComponent<StepScript>().SetTransparency(0f);
            }
        }
        playerHealth = 3;
        healthObject.GetComponent<HealthController>().SetHealth(playerHealth);
        playerPosition = 0;
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
        steps[0].transform.localPosition = new Vector3(0, 0, steps.GetLength(0));
        steps[0].transform.localScale = new Vector3(1, 1, 1);
        for (int i = 1; i < steps.GetLength(0); i++)
        {
            steps[i].transform.localScale = new Vector3(steps[i - 1].transform.localScale.x - (startInWidth - (i) * decreaseInWidth), steps[i - 1].transform.localScale.y - decreaseInHeight, 0);
            steps[i].transform.localPosition = new Vector3(0, steps[i - 1].transform.localPosition.y + (steps[i - 1].transform.localScale.y * initialDistance + steps[i].transform.localScale.y * initialDistance) / 2, steps.GetLength(0) - i);
            if (i == steps.GetLength(0) - 2)
            {
                steps[i].GetComponent<StepScript>().SetTransparency(0.5f);
            }
            if (i == steps.GetLength(0) - 1)
            {
                steps[i].GetComponent<StepScript>().SetTransparency(0f);
            }
        }
        playerHealth = 3;
        healthObject.GetComponent<HealthController>().SetHealth(playerHealth);
        playerPosition = 0;
        PlayerMovement();
    }

    public void JumpButton()
    {
        if (!isJump)
        {
            if (Time.realtimeSinceStartup - timeForButton <= cooldownForJumpButton)
            {
                isDoubleJump = false;
                if (flaskObject.GetComponent<FlaskScript>().Jump())
                {
                    animationTime = 2f;
                    AutomaticJump(2);
                }
            }
            else
            {
                isDoubleJump = true;
            }
            timeForButton = Time.realtimeSinceStartup;
        }
    }

    private void Awake()
    {
        speedDefault = 25f;
        speed = speedDefault;
        playerStep = 3;
        isGame = false;
        isPause = false;
        isJump = false;
        direction = 0f;
        currentTime = 0f;
        playerPosition = 0f;
        playerPositionNew = 0f;
        animationTime = 1f;
        distance = 0f;
        difference = 0f;
        interval = 0f;
        timeForButton = 0f;
        isDoubleJump = false;
        //menuScript = menuCanvas.GetComponent<MenuScript>();
        directionMenuScript = directionObject.GetComponent<DirectionMenuScript>();
        //flaskMenuScript = flaskObject.GetComponent<FlaskMenuScript>();
        //healthMenuScript = healthObject.GetComponent<HealthMenuScript>();

        GameStart();
        PlayerMovement();
    }

    #endregion

    #region Private Methods

    private void Update()
    {
        if (isGame && !isPause)
        {
            //One Jump не работает после двойного
            if (isDoubleJump && Time.realtimeSinceStartup - timeForButton > cooldownForJumpButton)
            {
                animationTime = 1f;
                AutomaticJump(1);
                isDoubleJump = false;
                timeForButton = Time.realtimeSinceStartup;
            }

            direction = directionMenuScript.Direction;

            LadderMovement();

            if (playerStep <= 2)
            {
                AutomaticJump(1);
            }

            SpeedMovement();

            if (isJump)
            {
                Jump();
            }
            else
            {
                PlayerMovement();
                Debug.Log(playerPosition);
                //куда он прыгнул есть ли там место из части
                /*StepController stepController = steps[playerStep].GetComponent<StepController>();
                StepPartState stepPartState = stepController.GetStepPartState(playerPosition);
                if (stepPartState == StepPartState.Stable)
                {
                    if (stepController.GetLetState(playerPosition) == LetState.Empty)
                    {

                    }
                    else
                    {

                    }
                }
                Debug.Log(stepController.GetStepPartState(playerPosition).ToString());*/
                //куда он прыгнул есть ли там препятствие

            }
        }
    }

    private void SpeedMovement(float coefficient = 10f)
    {
        Debug.Log(speed);
        //speed = Time.deltaTime * coefficient * speedDefault * 100;
        if (speedDefault + (playerStep - 3) * 20 - speed < 0)
        {
            speed -= Time.deltaTime * coefficient * 20;
        }
        else
        {
            speed += Time.deltaTime * coefficient * 20;
        }
    }

    private float LadderScaleX(int input)
    {
        float result = 0f;
        for (int i = 1; i <= input; i++)
        {
            result += (startInWidth - (i) * decreaseInWidth);
        }
        return result;
    }

    private void LadderMovement()
    {
        float percent = -steps[0].transform.localPosition.y / (steps[0].transform.localScale.y * initialDistance - decreaseInHeight * initialDistance / 2); //либо убрать минус
        for (int i = 0; i < steps.GetLength(0); i++)
        {
            steps[i].transform.localScale = new Vector3(1 - LadderScaleX(i) + (startInWidth - (i) * decreaseInWidth) * percent, 1 - decreaseInHeight * i + decreaseInHeight * percent, 1);
            steps[i].transform.localPosition = new Vector3(0, steps[i].transform.localPosition.y - Time.deltaTime * speed * steps[i].transform.localScale.y, steps[i].transform.localPosition.z);
            if (i == steps.GetLength(0) - 2)
            {
                steps[i].GetComponent<StepScript>().SetTransparency(0.5f + percent / 2);
            }
            if (i == steps.GetLength(0) - 1)
            {
                steps[i].GetComponent<StepScript>().SetTransparency(percent / 2);
            }
        }
        if (percent >= 1)
        {
            GameObject none = steps[0];
            for (int i = 1; i < steps.GetLength(0); i++)
            {
                steps[i - 1] = steps[i];
                steps[i - 1].transform.localPosition = new Vector3(steps[i - 1].transform.localPosition.x, steps[i - 1].transform.localPosition.y, steps.GetLength(0) - i);
            }
            steps[steps.GetLength(0) - 1] = none;
            steps[steps.GetLength(0) - 1].transform.SetSiblingIndex(0);
            steps[steps.GetLength(0) - 1].GetComponent<StepScript>().SetTransparency(0f);
            steps[steps.GetLength(0) - 1].transform.localScale = new Vector3(steps[steps.GetLength(0) - 2].transform.localScale.x - (startInWidth - (steps.GetLength(0) - 1) * decreaseInWidth), steps[steps.GetLength(0) - 2].transform.localScale.y - decreaseInHeight, 0);
            steps[steps.GetLength(0) - 1].transform.localPosition = new Vector3(0, steps[steps.GetLength(0) - 2].transform.localPosition.y + (steps[steps.GetLength(0) - 2].transform.localScale.y * initialDistance + steps[steps.GetLength(0) - 1].transform.localScale.y * initialDistance) / 2, 0);
            playerStep--;
            CreateLets(steps[steps.GetLength(0) - 1]);
        }
        /*for (int i = 0; i < steps.GetLength(0); i++)
        {
            steps[i].transform.localScale = new Vector3((float)(100 - i * ((1f - reductionOfSteps) * 100) + (-steps[0].transform.localPosition.y * ((1f - reductionOfSteps) * 100)) / 100) / 100, (float)(100 - i * ((1f - reductionOfSteps) * 100) + (-steps[0].transform.localPosition.y * ((1f - reductionOfSteps) * 100)) / 100) / 100, 1);
            steps[i].transform.localPosition = new Vector3(steps[i].transform.localPosition.x, steps[i].transform.localPosition.y - Time.deltaTime * 10 * speed * steps[i].transform.localScale.y, steps[i].transform.localPosition.z);
        }
        if (steps[1].transform.localScale.y >= 1)
        {
            GameObject none = steps[0];
            for (int i = 1; i < steps.GetLength(0); i++)
            {
                steps[i - 1] = steps[i];
            }
            steps[steps.GetLength(0) - 1] = none;
            steps[steps.GetLength(0) - 1].transform.localPosition = new Vector3(0, steps[steps.GetLength(0) - 2].transform.localPosition.y + initialDistance * (steps[steps.GetLength(0) - 2].transform.localScale.x - (1f - reductionOfSteps)), 0);
            steps[steps.GetLength(0) - 1].transform.SetSiblingIndex(0);
            playerStep--;
            //steps[steps.GetLength(0) - 1].GetComponent<StepScript>().SetTransparency(0f);
            //calling the obstacle generating method
            CreateLets(steps[steps.GetLength(0) - 1]);
        }*/
    }

    private void CreateLets(GameObject inputStep)
    {
        //createStaticLet переделать
        /*StepController stepController = inputStep.GetComponent<StepController>();
        stepController.CreateStepPart(StepPartState.Stable, StepPartState.Empty, StepPartState.Stable);*/
        //stepController.ClearLets();
        //stepController.AddLetStatic((float)Random.Range(-200, 200), lets[0]);
        //прописать возможные вариации спавна и между ними выбирать
        //или прописать доп пункты в препятствия и среди них рандомно
        //но несколько препятствий как спавнить, вопрос
    }

    private void AutomaticJump(int numberOfStep)
    {
        currentTime = 0f;
        isJump = true;
        stepFrom = steps[playerStep].transform;
        playerStep += numberOfStep;
        stepTo = steps[playerStep].transform;
        playerPositionNew = playerPosition - Mathf.Tan(direction * 0.0174533f) * (stepTo.localPosition.y - stepFrom.localPosition.y);
    }

    private void Jump()
    {
        if (currentTime <= animationTime)
        {
            distance = (stepTo.localPosition.y - stepFrom.localPosition.y);
            difference = (stepFrom.localScale.x - stepTo.localScale.x);
            interval = (playerPositionNew * stepTo.localScale.x - playerPosition * stepFrom.localScale.x);
            player.transform.localPosition = new Vector3(playerPosition * stepFrom.localScale.x + playerAnimationCurveX.Evaluate(currentTime) * interval, stepFrom.localPosition.y + playerAnimationCurveY.Evaluate(currentTime) * distance, player.transform.localPosition.z);
            player.transform.localScale = new Vector3(stepFrom.localScale.x - playerAnimationCurveS.Evaluate(currentTime) * difference, stepFrom.localScale.x - playerAnimationCurveS.Evaluate(currentTime) * difference, stepTo.localScale.x);
            currentTime += Time.deltaTime;
        }
        else
        {
            playerPosition = playerPositionNew;
            animationTime = 1f;
            currentTime = 0f;
            isJump = false;
        }
    }

    void PlayerMovement()
    {
        player.transform.localScale = new Vector3(steps[playerStep].transform.localScale.x, steps[playerStep].transform.localScale.x, steps[playerStep].transform.localScale.x);
        if (playerPosition > -412 && playerPosition < 412)
        {
            player.transform.localPosition = new Vector3(playerPosition * player.transform.localScale.x, steps[playerStep].transform.localPosition.y, steps[playerStep].transform.localPosition.z);
        }
        else
        {
             PlayerDead();
        }
    }

    void PlayerDead()
    {
        healthObject.GetComponent<HealthController>().MinusHealth();
        playerHealth--;
        playerPosition = 0; //изменить - ставить объект на место без препятствия
        Debug.Log("DEAD");

        if (playerHealth <= 0) //or 1
        {
            //PauseGame();
            GamePause();
            //menuCanvas.GetComponent<MenuScript>().ToChance();
        }
        /*if (playerHealth > 1) //or 0
        {
            health.GetComponent<HealthController>().MinusHealth();
            playerHealth--;
        }
        else
        {
            health.GetComponent<HealthController>().MinusHealth();
            playerHealth--;
            PauseGame();
            ChancePanelOpen();
            //call chance panel
        }*/
    }

    #endregion
}
