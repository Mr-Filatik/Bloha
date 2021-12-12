using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject zeroPanel = null;
    [SerializeField] private GameObject namePanel = null;
    [SerializeField] private GameObject menuPanel = null;
    [SerializeField] private GameObject shopPanel = null;
    [SerializeField] private GameObject settingsPanel = null;
    [SerializeField] private GameObject achievementsPanel = null;
    //[SerializeField] private GameObject infoPanel = null;
    //[SerializeField] private GameObject gamePanel = null;//
    //[SerializeField] private GameObject pausePanel = null;//
    //[SerializeField] private GameObject losingPanel = null;//
    //[SerializeField] private GameObject chancePanel = null;//
    [SerializeField] private GameObject flaskPanel = null;
    [SerializeField] private GameObject directionPanel = null;
    [SerializeField] private GameObject countdownPanel = null;
    //[SerializeField] private GameObject gamePlayPanel = null;

    [Header("Panels animation")]
    [SerializeField] private AnimationCurve panelAnimationX = null;
    [SerializeField] private AnimationCurve panelAnimationY = null;
    [SerializeField] private AnimationCurve panelAnimationScale = null;

    private PanelClass[] closeObject = null;
    private PanelClass[] openObject = null;
    private float currentTime;

    public void AAA()
    {
        Debug.Log("check");
    }

    public void ToMenu()
    {
        openObject = new PanelClass[2];
        openObject[0] = new PanelClass(menuPanel, new Vector3(0f, -Screen.height, 0f), Vector3.zero);
        openObject[1] = new PanelClass(namePanel, new Vector3(0f, Screen.height, 0f), Vector3.zero);
        SetActiveGameObjects(openObject, true);
        zeroPanel.SetActive(true);

        /*directionPanel.GetComponent<DirectionMenuScript>().GameEnd();
        gamePlayPanel.GetComponent<GamePlayMenuScript>().GameEnd();
        flaskPanel.GetComponent<FlaskScript>().GameEnd();*/
    }

    public void ToSettings()
    {
        openObject = new PanelClass[1];
        openObject[0] = new PanelClass(settingsPanel, new Vector3(0f, -Screen.height, 0f), Vector3.zero);
        SetActiveGameObjects(openObject, true);
        zeroPanel.SetActive(true);
    }

    public void ToShop()
    {
        openObject = new PanelClass[1];
        openObject[0] = new PanelClass(shopPanel, new Vector3(0f, -Screen.height, 0f), Vector3.zero);
        SetActiveGameObjects(openObject, true);
        zeroPanel.SetActive(true);
    }

    public void ToAchievements()
    {
        openObject = new PanelClass[1];
        openObject[0] = new PanelClass(achievementsPanel, new Vector3(0f, -Screen.height, 0f), Vector3.zero);
        SetActiveGameObjects(openObject, true);
        zeroPanel.SetActive(true);
    }

    //public void ToInfo()
    //{
    //    openObject = new PanelClass[1];
    //    openObject[0] = new PanelClass(infoPanel, startDownPosition, endDownPosition);
    //    SetActiveGameObject(openObject);
    //}

    public void ToPlay()
    {
        openObject = new PanelClass[2];
        openObject[0] = new PanelClass(directionPanel, new Vector3(0f, -Screen.height, 0f), Vector3.zero);
        openObject[1] = new PanelClass(flaskPanel, new Vector3(-Screen.width, 0f, 0f), Vector3.zero);
        SetActiveGameObjects(openObject, true);
        zeroPanel.SetActive(true);
    }

    //public void ToGame()
    //{
    //    openObject = new PanelClass[3];
    //    openObject[0] = new PanelClass(gamePanel, startUpPosition, endUpPosition);
    //    openObject[1] = new PanelClass(flaskPanel, startLeftPosition, endLeftPosition);
    //    //openObject[2] = new PanelClass(healthPanel, startLeftPosition, endLeftPosition);
    //    openObject[2] = new PanelClass(directionPanel, startDownPosition, endDownPosition);
    //    SetActiveGameObject(openObject);
    //}

    //public void ToPause()
    //{
    //    openObject = new PanelClass[1];
    //    openObject[0] = new PanelClass(pausePanel, startDownPosition, endDownPosition);
    //    SetActiveGameObject(openObject);

    //    directionPanel.GetComponent<DirectionMenuScript>().GamePause();
    //    gamePlayPanel.GetComponent<GamePlayMenuScript>().GamePause();
    //    flaskPanel.GetComponent<FlaskScript>().GamePause();
    //}

    //public void ToLosing()
    //{
    //    openObject = new PanelClass[1];
    //    openObject[0] = new PanelClass(losingPanel, startDownPosition, endDownPosition);
    //    SetActiveGameObject(openObject);

    //    directionPanel.GetComponent<DirectionMenuScript>().GameEnd();
    //    gamePlayPanel.GetComponent<GamePlayMenuScript>().GameEnd();
    //    flaskPanel.GetComponent<FlaskScript>().GameEnd();
    //}

    //public void ToChance()
    //{
    //    openObject = new PanelClass[1];
    //    openObject[0] = new PanelClass(chancePanel, startDownPosition, endDownPosition);
    //    SetActiveGameObject(openObject);
    //}

    private void Awake()
    {
        zeroPanel.SetActive(true);
        menuPanel.SetActive(true);
        namePanel.SetActive(true);
        shopPanel.SetActive(false);
        settingsPanel.SetActive(false);
        achievementsPanel.SetActive(false);
        countdownPanel.SetActive(false);
        directionPanel.SetActive(false);
        //infoPanel.SetActive(false);
        //gamePanel.SetActive(false);
        //chancePanel.SetActive(false);
        //pausePanel.SetActive(false);
        //losingPanel.SetActive(false);
        flaskPanel.SetActive(false);
        //healthPanel.SetActive(false);


        menuPanel.transform.localPosition = new Vector3(0f, -Screen.height, 0f);
        shopPanel.transform.localPosition = new Vector3(0f, -Screen.height, 0f);
        settingsPanel.transform.localPosition = new Vector3(0f, -Screen.height, 0f);
        achievementsPanel.transform.localPosition = new Vector3(0f, -Screen.height, 0f);
        namePanel.transform.localPosition = new Vector3(0f, Screen.height, 0f);
        directionPanel.transform.localPosition = new Vector3(0f, -Screen.height, 0f);
        countdownPanel.transform.localPosition = new Vector3(0f, 0f, 0f);
        flaskPanel.transform.localPosition = new Vector3(-Screen.width, 0f, 0f);

        openObject = new PanelClass[2];
        openObject[0] = new PanelClass(menuPanel, new Vector3(0f, -Screen.height, 0f), Vector3.zero);
        openObject[1] = new PanelClass(namePanel, new Vector3(0f, Screen.height, 0f), Vector3.zero);
        closeObject = null;

        currentTime = 0f;
    }

    private void Update()
    {
        if (openObject != null)
        {
            if (closeObject != null)
            {
                PanelClose(closeObject);
            }
            else
            {
                PanelOpen(openObject);
            }
        }
    }

    private void PanelOpen(PanelClass[] gameObject)
    {
        if (currentTime >= panelAnimationY.keys[panelAnimationY.keys.Length - 1].time)
        {
            if (openObject[0].gameObject == directionPanel)
            {
                //после открытия панелей начала игры, вызывается счетчик начала
                countdownPanel.GetComponent<CountdownController>().gameObject.SetActive(true);
            }
            //if (openObject[0].gameObject == gamePanel)
            //{
            //    directionPanel.GetComponent<DirectionMenuScript>().GameContinue();
            //    gamePlayPanel.GetComponent<GamePlayMenuScript>().GameContinue();
            //    flaskPanel.GetComponent<FlaskScript>().GameContinue();
            //} //------------------------------------------------------------------------------bad for start
            //if (openObject[0].gameObject == chancePanel)
            //{
            //    chancePanel.GetComponent<ChancePanel>().StartWork();
            //}
            closeObject = openObject;
            openObject = null;
            foreach (PanelClass item in gameObject)
            {
                item.gameObject.transform.localPosition = item.vectorEnd;
            }
            currentTime = 1f;
            zeroPanel.SetActive(false);
        }
        else
        {
            foreach (PanelClass item in gameObject)
            {
                item.gameObject.transform.localPosition = new Vector3(item.vectorStart.x + (item.vectorEnd.x - item.vectorStart.x) * panelAnimationX.Evaluate(currentTime), item.vectorStart.y + (item.vectorEnd.y - item.vectorStart.y) * panelAnimationY.Evaluate(currentTime));
                item.gameObject.transform.localScale = new Vector3(panelAnimationScale.Evaluate(currentTime), panelAnimationScale.Evaluate(currentTime));
            }
            currentTime += Time.deltaTime;
        }
    }

    private void PanelClose(PanelClass[] gameObject)
    {
        if (currentTime <= 0)
        {
            currentTime = 0f;
            SetActiveGameObjects(closeObject, false);
            closeObject = null;

            //if (openObject[0].gameObject == countdownPanel)
            //{
            //    closeObject = openObject;
            //    openObject = null;
            //    countdownPanel.GetComponent<CountdownMenuScript>().StartGame();
            //} //------------------------------------------------------------------------------------------bad for timer

            foreach (PanelClass item in gameObject)
            {
                item.gameObject.transform.localPosition = item.vectorStart;
            }
        }
        else
        {
            foreach (PanelClass item in gameObject)
            {
                item.gameObject.transform.localPosition = new Vector3(item.vectorStart.x + (item.vectorEnd.x - item.vectorStart.x) * panelAnimationX.Evaluate(currentTime), item.vectorStart.y + (item.vectorEnd.y - item.vectorStart.y) * panelAnimationY.Evaluate(currentTime));
                item.gameObject.transform.localScale = new Vector3(panelAnimationScale.Evaluate(currentTime), panelAnimationScale.Evaluate(currentTime));
            }
            currentTime -= Time.deltaTime;
        }
    }

    private void SetActiveGameObjects(PanelClass[] gameObject, bool flag)
    {
        foreach (PanelClass item in gameObject)
        {
            item.gameObject.SetActive(flag);
        }
    }
}

public class PanelClass
{
    public GameObject gameObject;
    public Vector3 vectorStart;
    public Vector3 vectorEnd;
    public PanelClass(GameObject gameObject, Vector3 vectorStart, Vector3 vectorEnd)
    {
        this.gameObject = gameObject;
        this.vectorStart = vectorStart;
        this.vectorEnd = vectorEnd;
    }
}
