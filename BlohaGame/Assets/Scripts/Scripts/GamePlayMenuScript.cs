using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayMenuScript : MonoBehaviour
{
    [Header("Ladder")]
    [SerializeField] private GameObject[] steps = null;
    [SerializeField] private float reductionOfSteps; //0.92
    [SerializeField] private float initialDistance; //108

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

    [Header("GameObjects")]
    [SerializeField] private GameObject menuCanvas = null;
    [SerializeField] private GameObject directionObject = null;
    [SerializeField] private GameObject flaskObject = null;
    [SerializeField] private GameObject healthObject = null;
    private MenuScript menuScript = null;
    private DirectionMenuScript directionMenuScript = null;
    //private FlaskMenuScript flaskMenuScript = null;
    //private HealthMenuScript healthMenuScript = null;

    private bool isGame;
    public bool IsGame => isGame;
    private bool isPause;
    private bool isJump;
    private float direction;
    private float currentTime;
    [SerializeField] private float cooldownForJumpButton;
    [SerializeField] private float timeForButton;


    //hz
    private Transform stepFrom = null;
    private Transform stepTo = null;
    private float animationTime;
    private float distance;
    private float difference;
    private float interval;



    public void GameStart()
    {
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
        speedDefault = 2f;
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
        menuScript = menuCanvas.GetComponent<MenuScript>();
        directionMenuScript = directionObject.GetComponent<DirectionMenuScript>();
        //flaskMenuScript = flaskObject.GetComponent<FlaskMenuScript>();
        //healthMenuScript = healthObject.GetComponent<HealthMenuScript>();

        steps[0].transform.localPosition = new Vector3(0, 0, 0);
        steps[0].transform.localScale = new Vector3(1, 1, 1);
        for (int i = 1; i < steps.GetLength(0); i++)
        {
            steps[i].transform.localPosition = new Vector3(0, steps[i - 1].transform.localPosition.y + 108 * (steps[i - 1].transform.localScale.x - (1f - reductionOfSteps)), 0);
            steps[i].transform.localScale = new Vector3(steps[i - 1].transform.localScale.x - (1f - reductionOfSteps), steps[i - 1].transform.localScale.y - (1f - reductionOfSteps), 0);
        }

        //needs changes
        PlayerMovement();
    }

    private void Update()
    {
        if (isGame && !isPause)
        {
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
        if (speedDefault + (playerStep - 3) * 2 - speed < 0)
        {
            speed -= Time.deltaTime * coefficient;
        }
        else
        {
            speed += Time.deltaTime * coefficient;
        }
    }

    private void LadderMovement()
    {
        for (int i = 0; i < steps.GetLength(0); i++)
        {
            steps[i].transform.localScale = new Vector3((float)(100 - i * ((1f - reductionOfSteps) * 100) + (-steps[0].transform.localPosition.y * 2) / 100) / 100, (float)(100 - i * ((1f - reductionOfSteps) * 100) + (-steps[0].transform.localPosition.y * 2) / 100) / 100, 1);
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
            steps[steps.GetLength(0) - 1].transform.localPosition = new Vector3(0, steps[steps.GetLength(0) - 2].transform.localPosition.y + 108 * (steps[steps.GetLength(0) - 2].transform.localScale.x - (1f - reductionOfSteps)), 0);
            steps[steps.GetLength(0) - 1].transform.SetSiblingIndex(0);
            playerStep--;
            //calling the obstacle generating method
            CreateLets(steps[steps.GetLength(0) - 1]);
        }
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
        playerPositionNew = playerPosition - Mathf.Tan(direction * 0.0174533f) * (stepTo.localPosition.y - stepFrom.localPosition.y) * 0.8f;
    }

    private void Jump()
    {
        if (currentTime <= animationTime)
        {
            distance = (stepTo.localPosition.y - stepFrom.localPosition.y) * 0.8f;
            difference = (stepFrom.localScale.x - stepTo.localScale.x) * 1f;
            interval = (playerPositionNew * stepTo.localScale.x - playerPosition * stepFrom.localScale.x) * 0.8f;
            player.transform.localPosition = new Vector3(playerPosition * 0.8f * stepFrom.localScale.x + playerAnimationCurveX.Evaluate(currentTime) * interval, stepFrom.localPosition.y * 0.8f + playerAnimationCurveY.Evaluate(currentTime) * distance, player.transform.localPosition.z);
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

    void PlayerMovement()
    {
        //scale steps = 0.8f and scale player = 1f
        player.transform.localScale = new Vector3(steps[playerStep].transform.localScale.x, steps[playerStep].transform.localScale.y, steps[playerStep].transform.localScale.z);
        if (playerPosition > -200 && playerPosition < 200)
        {
            player.transform.localPosition = new Vector3(playerPosition * player.transform.localScale.x * 0.8f, steps[playerStep].transform.localPosition.y * 0.8f, steps[playerStep].transform.localPosition.z * 0.8f);
        }
        else
        {
            //health.GetComponent<HealthController>().MinusHealth();
            playerPosition = 0;
            PlayerDead();
            //падение
        }
    }

    void PlayerDead()
    {
        Debug.Log("DEAD");
        /*
        health.GetComponent<HealthController>().MinusHealth();
        playerHealth--;
        if (playerHealth <= 0) //or 1
        {
            PauseGame();
            ChancePanelOpen();
        }*/
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

    public void JumpButtonUp()
    {
        //изменить
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
}
