using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject menuPanel = null; //main menu
    [SerializeField] private GameObject shopPanel = null; //shop
    [SerializeField] private GameObject barPanel = null; //bar with money
    [SerializeField] private GameObject settingsPanel = null; //settings
    [SerializeField] private GameObject infoPanel = null; //info
    [SerializeField] private GameObject gamePanel = null; //game
    [SerializeField] private GameObject pausePanel = null; //pause
    [SerializeField] private GameObject losingPanel = null; //losing
    [SerializeField] private GameObject chancePanel = null; //chance
    [SerializeField] private GameObject flaskPanel = null; //flask
    [SerializeField] private GameObject healthPanel = null; //health
    [SerializeField] private GameObject directionPanel = null; //direction
    [SerializeField] private GameObject countdownPanel = null; //direction

    [Header("Panels animation")]
    [SerializeField] private AnimationCurve panelAnimationX = null; //right - left
    [SerializeField] private AnimationCurve panelAnimationY = null; //up - down
    [SerializeField] private AnimationCurve panelAnimationScale = null; //scale

    [Header("Panels position")]
    [SerializeField] private Vector3 startDownPosition = Vector3.zero;
    [SerializeField] private Vector3 endDownPosition = Vector3.zero;
    [SerializeField] private Vector3 startUpPosition = Vector3.zero;
    [SerializeField] private Vector3 endUpPosition = Vector3.zero;
    [SerializeField] private Vector3 startLeftPosition = Vector3.zero;
    [SerializeField] private Vector3 endLeftPosition = Vector3.zero;
    [SerializeField] private Vector3 startRightPosition = Vector3.zero;
    [SerializeField] private Vector3 endRightPosition = Vector3.zero;

    private PanelClass[] closeObject = null;
    private PanelClass[] openObject = null;

    private float currentTimeForPanel;

    public void ToMenu()
    {
        openObject = new PanelClass[1];
        openObject[0] = new PanelClass(menuPanel, startDownPosition, endDownPosition);
        SetActiveGameObject(openObject);
    }

    public void ToSettings()
    {
        openObject = new PanelClass[1];
        openObject[0] = new PanelClass(settingsPanel, startDownPosition, endDownPosition);
        SetActiveGameObject(openObject);
    }

    public void ToShop()
    {
        openObject = new PanelClass[1];
        openObject[0] = new PanelClass(shopPanel, startDownPosition, endDownPosition);
        SetActiveGameObject(openObject);
    }

    public void ToInfo()
    {
        openObject = new PanelClass[1];
        openObject[0] = new PanelClass(infoPanel, startDownPosition, endDownPosition);
        SetActiveGameObject(openObject);
    }

    public void ToStart()
    {
        openObject = new PanelClass[1];
        openObject[0] = new PanelClass(countdownPanel, endUpPosition, endUpPosition);
        SetActiveGameObject(openObject);
    }

    public void ToGame()
    {
        openObject = new PanelClass[4];
        openObject[0] = new PanelClass(gamePanel, startUpPosition, endUpPosition);
        openObject[1] = new PanelClass(flaskPanel, startRightPosition, endRightPosition);
        openObject[2] = new PanelClass(healthPanel, startLeftPosition, endLeftPosition);
        openObject[3] = new PanelClass(directionPanel, startDownPosition, endDownPosition);
        SetActiveGameObject(openObject);
    }

    public void ToPause()
    {
        openObject = new PanelClass[1];
        openObject[0] = new PanelClass(pausePanel, startDownPosition, endDownPosition);
        SetActiveGameObject(openObject);
    }

    public void ToLosing()
    {
        openObject = new PanelClass[1];
        openObject[0] = new PanelClass(losingPanel, startDownPosition, endDownPosition);
        SetActiveGameObject(openObject);
    }

    public void ToChance()
    {
        openObject = new PanelClass[1];
        openObject[0] = new PanelClass(chancePanel, startDownPosition, endDownPosition);
        SetActiveGameObject(openObject);
    }

    private void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;

        menuPanel.SetActive(true);
        shopPanel.SetActive(false);
        settingsPanel.SetActive(false);
        infoPanel.SetActive(false);
        gamePanel.SetActive(false);
        chancePanel.SetActive(false);
        pausePanel.SetActive(false);
        losingPanel.SetActive(false);
        flaskPanel.SetActive(false);
        healthPanel.SetActive(false);
        directionPanel.SetActive(false);
        countdownPanel.SetActive(false);

        if (Screen.height / Screen.width > 16 / 9)
        {

        }
        
        menuPanel.transform.localPosition = startDownPosition;
        shopPanel.transform.localPosition = startDownPosition;
        settingsPanel.transform.localPosition = startDownPosition;
        infoPanel.transform.localPosition = startDownPosition;
        chancePanel.transform.localPosition = startDownPosition;
        pausePanel.transform.localPosition = startDownPosition;
        losingPanel.transform.localPosition = startDownPosition;
        gamePanel.transform.localPosition = startUpPosition;
        flaskPanel.transform.localPosition = startRightPosition;
        healthPanel.transform.localPosition = startLeftPosition;
        directionPanel.transform.localPosition = startDownPosition;
        countdownPanel.transform.localPosition = endDownPosition;

        openObject = new PanelClass[1];
        openObject[0] = new PanelClass(menuPanel, startDownPosition, endDownPosition);
        closeObject = null;

        currentTimeForPanel = 0f;
    }

    private void Start()
    {
        
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
        if (currentTimeForPanel >= panelAnimationY.keys[panelAnimationY.keys.Length - 1].time)
        {
            closeObject = openObject;
            openObject = null;
            foreach (PanelClass item in gameObject)
            {
                item.gameObject.transform.localPosition = item.vectorEnd;
            }
            currentTimeForPanel = 1f;
        }
        else
        {
            foreach (PanelClass item in gameObject)
            {
                item.gameObject.transform.localPosition = new Vector3(item.vectorStart.x + (item.vectorEnd.x - item.vectorStart.x) * panelAnimationX.Evaluate(currentTimeForPanel), item.vectorStart.y + (item.vectorEnd.y - item.vectorStart.y) * panelAnimationY.Evaluate(currentTimeForPanel));
                item.gameObject.transform.localScale = new Vector3(panelAnimationScale.Evaluate(currentTimeForPanel), panelAnimationScale.Evaluate(currentTimeForPanel));
            }
            currentTimeForPanel += Time.deltaTime;
        }
    }

    private void PanelClose(PanelClass[] gameObject)
    {
        if (currentTimeForPanel <= 0)
        {
            currentTimeForPanel = 0f;
            foreach (PanelClass item in closeObject)
            {
                item.gameObject.SetActive(false);
            }
            closeObject = null;

            if (openObject[0].gameObject == countdownPanel)
            {
                closeObject = openObject;
                openObject = null;
                countdownPanel.GetComponent<CountdownMenuScript>().StartGame();
            } //------------------------------------------------------------------------------------------bad for timer

            foreach (PanelClass item in gameObject)
            {
                item.gameObject.transform.localPosition = item.vectorStart;
            }
        }
        else
        {
            foreach (PanelClass item in gameObject)
            {
                item.gameObject.transform.localPosition = new Vector3(item.vectorStart.x + (item.vectorEnd.x - item.vectorStart.x) * panelAnimationX.Evaluate(currentTimeForPanel), item.vectorStart.y + (item.vectorEnd.y - item.vectorStart.y) * panelAnimationY.Evaluate(currentTimeForPanel));
                item.gameObject.transform.localScale = new Vector3(panelAnimationScale.Evaluate(currentTimeForPanel), panelAnimationScale.Evaluate(currentTimeForPanel));
            }
            currentTimeForPanel -= Time.deltaTime;
        }
    }

    private void SetActiveGameObject(PanelClass[] gameObject)
    {
        foreach (PanelClass item in gameObject)
        {
            item.gameObject.SetActive(true);
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
