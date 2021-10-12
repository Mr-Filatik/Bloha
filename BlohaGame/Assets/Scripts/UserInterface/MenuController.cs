using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject chancePanel = null;
    [SerializeField] private GameObject losingPanel = null;
    [SerializeField] private GameObject pausePanel = null;
    [SerializeField] private GameObject settingsPanel = null;
    [SerializeField] private AnimationCurve panelAnimationCurvePosition;
    private float currentTimeForPanel = 0f;

    #region Field
    public GameObject menuPanel = null;
    public GameObject gamePanel = null;

    public GameObject[] gameStart = null;

    public GameObject gamePauseButton = null;
    public GameObject gameJumpButton = null;

    public GameObject[] steps = null;

    public GameObject directionArrow = null;

    public GameObject health = null;

    public GameObject[] lets = null;
    #endregion

    #region SecretField
    private bool game = false;

    [SerializeField] private float speed;
    [SerializeField] private float speedDefault = 4f;

    private int playerHealth = 3;
    private int playerStep = 3;
    [SerializeField] private float playerPosition = 0;
    [SerializeField] private float playerPositionNew = 0;
    [SerializeField] private AnimationCurve playerAnimationCurveY;
    [SerializeField] private AnimationCurve playerAnimationCurveX;
    [SerializeField] private AnimationCurve playerAnimationCurveS;

    GameObject player; // объект игрока, для вызова анимаций и т.п.
    private bool isJump = false;
    private Transform stepFrom = null;
    private Transform stepTo = null;
    //private float directionJump = 0f; //

    private float cooldownForJumpButton = 1f;
    private float timeForButton;

    private float animationTime = 1f;
    private float currentTime = 0f;
    private float distance = 0f;
    private float interval = 0f;
    private float difference = 0f;
    private float direction = 0f;
    private float directionCurrent = 0f;
    private float directionLimit = 45f;

    #endregion

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        menuPanel.SetActive(true);
        gamePanel.SetActive(false);
        chancePanel.SetActive(false);
        settingsPanel.SetActive(false);

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

    private bool isPause = false;

    private bool chancePanelOpen = false;
    private bool chancePanelClose = false;
    private bool losingPanelOpen = false;
    private bool losingPanelClose = false;
    private bool pausePanelOpen = false;
    private bool pausePanelClose = false;
    private bool settingsPanelOpen = false;
    private bool settingsPanelClose = false;

    private bool toMenuButton = false;
    private bool toRestartButton = false;
    private bool toPauseButton = false;
    private bool toContinueButton = false;
    private bool toSettingsButton = false;

    void Update()
    {
        if (game && !isPause)
        {
            speed = speedDefault;// + Time.deltaTime * 10 * playerStep * steps[0].transform.position.y / 100; //исправить

            DirectionMovement();

            LadderMovement();

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

        if (losingPanelOpen)
        {
            if (currentTimeForPanel >= panelAnimationCurvePosition.keys[panelAnimationCurvePosition.keys.Length - 1].time)
            {
                LosingPanelOpen();
                currentTimeForPanel = 0f;
            }
            else
            {
                losingPanel.transform.localPosition = new Vector3(0f, -1200f + 1200f * panelAnimationCurvePosition.Evaluate(currentTimeForPanel));
                currentTimeForPanel += Time.deltaTime;
            }
        }
        if (losingPanelClose)
        {
            if (currentTimeForPanel <= 0)
            {
                LosingPanelClose();
                currentTimeForPanel = 0f;
            }
            else
            {
                losingPanel.transform.localPosition = new Vector3(0f, -1200f + 1200f * panelAnimationCurvePosition.Evaluate(currentTimeForPanel));
                currentTimeForPanel -= Time.deltaTime;
            }
        }

        if (chancePanelOpen)
        {
            if (currentTimeForPanel >= panelAnimationCurvePosition.keys[panelAnimationCurvePosition.keys.Length - 1].time)
            {
                ChancePanelOpen();
                currentTimeForPanel = 0f;
            }
            else
            {
                chancePanel.transform.localPosition = new Vector3(0f, -1200f + 1200f * panelAnimationCurvePosition.Evaluate(currentTimeForPanel));
                currentTimeForPanel += Time.deltaTime;
            }
        }
        if (chancePanelClose)
        {
            if (currentTimeForPanel <= 0)
            {
                ChancePanelClose();
                currentTimeForPanel = 0f;
            }
            else
            {
                chancePanel.transform.localPosition = new Vector3(0f, -1200f + 1200f * panelAnimationCurvePosition.Evaluate(currentTimeForPanel));
                currentTimeForPanel -= Time.deltaTime;
            }
        }

        if (pausePanelOpen)
        {
            if (currentTimeForPanel >= panelAnimationCurvePosition.keys[panelAnimationCurvePosition.keys.Length - 1].time)
            {
                PausePanelOpen();
                currentTimeForPanel = 0f;
            }
            else
            {
                pausePanel.transform.localPosition = new Vector3(0f, -1200f + 1200f * panelAnimationCurvePosition.Evaluate(currentTimeForPanel));
                currentTimeForPanel += Time.deltaTime;
            }
        }
        if (pausePanelClose)
        {
            if (currentTimeForPanel <= 0)
            {
                PausePanelClose();
                currentTimeForPanel = 0f;
            }
            else
            {
                pausePanel.transform.localPosition = new Vector3(0f, -1200f + 1200f * panelAnimationCurvePosition.Evaluate(currentTimeForPanel));
                currentTimeForPanel -= Time.deltaTime;
            }
        }

        if (settingsPanelOpen)
        {
            if (currentTimeForPanel >= panelAnimationCurvePosition.keys[panelAnimationCurvePosition.keys.Length - 1].time)
            {
                SettingsPanelOpen();
                currentTimeForPanel = 0f;
            }
            else
            {
                settingsPanel.transform.localPosition = new Vector3(0f, -1200f + 1200f * panelAnimationCurvePosition.Evaluate(currentTimeForPanel));
                currentTimeForPanel += Time.deltaTime;
            }
        }
        if (settingsPanelClose)
        {
            if (currentTimeForPanel <= 0)
            {
                SettingsPanelClose();
                currentTimeForPanel = 0f;
            }
            else
            {
                settingsPanel.transform.localPosition = new Vector3(0f, -1200f + 1200f * panelAnimationCurvePosition.Evaluate(currentTimeForPanel));
                currentTimeForPanel -= Time.deltaTime;
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
        if (playerPosition > -200 && playerPosition < 200)
        {
            player.transform.localPosition = new Vector3(playerPosition * player.transform.localScale.x * 0.8f, steps[playerStep].transform.localPosition.y * 0.8f, steps[playerStep].transform.localPosition.z * 0.8f);
        }
        else
        {
            health.GetComponent<HealthController>().MinusHealth();
            playerPosition = 0;
            PlayerDead();
            //падение
        }
    }

    void DirectionMovement()
    {
        if (direction == directionCurrent)
        {
            directionCurrent = Random.Range(-directionLimit, directionLimit);
        }
        float d = directionCurrent - direction;
        if (Mathf.Abs(d) < Time.deltaTime * speed * 20)
        {
            direction = directionCurrent;
            d = 0;
        }
        /*if (Mathf.Abs(d) < Time.deltaTime * speed * 10)
        {
            direction = directionCurrent;
            d = 0;
        }*/
        if (d < 0)
        {
            direction -= Time.deltaTime * speed * 20;
            //direction += Time.deltaTime * speed * d;
        }
        if (d > 0)
        {
            direction += Time.deltaTime * speed * 20;
            //direction += Time.deltaTime * speed * d;
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
        playerPositionNew = playerPosition - Mathf.Tan(direction * 0.0174533f) * (stepTo.localPosition.y - stepFrom.localPosition.y) * 0.8f;
    }

    public void Jump()
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

    void PlayerDead()
    {
        if (playerHealth > 0) //or 1
        {
            health.GetComponent<HealthController>().MinusHealth();
        }
        else
        {
            //call chance panel
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

    //game methods

    private void StartGame()
    {
        game = true;
        isPause = true;

        playerPosition = 0;
        health.GetComponent<HealthController>().SetHealth(3);
    }

    private void ContinueGame()
    {
        isPause = false;
        gameJumpButton.SetActive(true);
        gamePauseButton.SetActive(true);
        //speedDefault = 2f;
        
    }

    private void PauseGame()
    {
        isPause = true;
        gameJumpButton.SetActive(false);
        gamePauseButton.SetActive(false);
        //speedDefault = 0f; 
        
    }

    private void EndGame()
    {
        game = false;
    }

    //push buttons

    public void MenuButton()
    {
        if (toMenuButton)
        {
            toMenuButton = false;

            menuPanel.SetActive(true);
            game = false;
            gameJumpButton.SetActive(false);
            gamePauseButton.SetActive(false);
            gamePanel.SetActive(false);
        }
        else
        {
            toMenuButton = true;
            if (losingPanel.activeSelf)
            {
                LosingPanelClose();
            }
            if (pausePanel.activeSelf)
            {
                PausePanelClose();
            }
            //pausemenu
        }
    }

    public void ChanceButton()
    {
        ChancePanelClose();
    }

    public void RestartButton()
    {
        if (toRestartButton)
        {
            toRestartButton = false;
        }
        else
        {
            toRestartButton = true;
            LosingPanelClose();
        }
    }

    public void PauseButton()
    {
        if (toPauseButton)
        {
            toPauseButton = false;
        }
        else
        {
            toPauseButton = true;
            PauseGame();
            if (settingsPanel.activeSelf)
            {
                SettingsPanelClose();
            }
            else
            {
                PausePanelOpen();
            }
        }
    }

    public void ContinueButton()
    {
        if (toContinueButton)
        {
            toContinueButton = false;
            ContinueGame();
        }
        else
        {
            toContinueButton = true;
            if (pausePanel.activeSelf)
            {
                PausePanelClose();
            }
        }
    }

    public void SettingsButton()
    {
        if (toSettingsButton)
        {
            toSettingsButton = false;
            //SettingsPanelOpen();
        }
        else
        {
            toSettingsButton = true;
            if (pausePanel.activeSelf)
            {
                PausePanelClose();
            }
            //mainmenu
        }
    }

    public void PlayButton()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);

        LadderMovement();
        StartCoroutine(startWaiter());
    }//needs changes

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

        StartGame();

        ContinueGame();
        //ChancePanelOpen();
    }//needs removed

    //open and close panels

    private void ChancePanelOpen()
    {
        if (chancePanelOpen)
        {
            chancePanelOpen = false;
            chancePanel.GetComponent<ChancePanel>().StartWork();
        }
        else
        {
            chancePanelOpen = true;
            chancePanel.SetActive(true);
            currentTimeForPanel = 0f;
        }
    }

    private void ChancePanelClose()
    {
        if (chancePanelClose)
        {
            chancePanelClose = false;
            chancePanel.SetActive(false);
            LosingPanelOpen();
        }
        else
        {
            chancePanelClose = true;
            currentTimeForPanel = panelAnimationCurvePosition.keys[panelAnimationCurvePosition.keys.Length - 1].time;
        }
    }

    private void LosingPanelOpen()
    {
        if (losingPanelOpen)
        {
            losingPanelOpen = false;
        }
        else
        {
            losingPanelOpen = true;
            losingPanel.SetActive(true);
            currentTimeForPanel = 0f;
        }
    }

    private void LosingPanelClose()
    {
        if (losingPanelClose)
        {
            losingPanelClose = false;
            losingPanel.SetActive(false);
            if (toMenuButton)
            {
                MenuButton();
            }
            if (toRestartButton)
            {
                RestartButton();
            }
        }
        else
        {
            losingPanelClose = true;
            currentTimeForPanel = panelAnimationCurvePosition.keys[panelAnimationCurvePosition.keys.Length - 1].time;
        }
    }

    private void PausePanelOpen()
    {
        if (pausePanelOpen)
        {
            pausePanelOpen = false;
            if (toPauseButton)
            {
                PauseButton();//mb refactor
            }
        }
        else
        {
            pausePanelOpen = true;
            pausePanel.SetActive(true);
            currentTimeForPanel = 0f;
        }
    }

    private void PausePanelClose()
    {
        if (pausePanelClose)
        {
            pausePanelClose = false;
            pausePanel.SetActive(false);
            if (toContinueButton)
            {
                ContinueButton();
            }
            if (toSettingsButton)
            {
                SettingsPanelOpen();
            }
            if (toMenuButton)
            {
                MenuButton();
            }
        }
        else
        {
            pausePanelClose = true;
            currentTimeForPanel = panelAnimationCurvePosition.keys[panelAnimationCurvePosition.keys.Length - 1].time;
        }
    }

    private void SettingsPanelOpen()
    {
        if (settingsPanelOpen)
        {
            settingsPanelOpen = false;
            if (toSettingsButton)
            {
                SettingsButton();
            }
        }
        else
        {
            settingsPanelOpen = true;
            settingsPanel.SetActive(true);
            currentTimeForPanel = 0f;
        }
    }

    private void SettingsPanelClose()
    {
        if (settingsPanelClose)
        {
            settingsPanelClose = false;
            settingsPanel.SetActive(false);
            if (toPauseButton)
            {
                PausePanelOpen();
            }
            //PauseButton();
        }
        else
        {
            settingsPanelClose = true;
            currentTimeForPanel = panelAnimationCurvePosition.keys[panelAnimationCurvePosition.keys.Length - 1].time;
        }
    }





    public void AdvertisingButton()
    {

    }

    public void RecoveryButton()
    {

    }

    public void ShopButton()
    {
        Debug.Log("Shop");
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
