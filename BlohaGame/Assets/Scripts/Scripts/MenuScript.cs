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

    [Header("Panels animation")]
    [SerializeField] private AnimationCurve panelAnimationX = null; //right - left
    [SerializeField] private AnimationCurve panelAnimationY = null; //up - down
    [SerializeField] private AnimationCurve panelAnimationScale = null; //scale

    [Header("Panels position")]
    [SerializeField] private Vector3 panelStartPosition = Vector3.zero; //start position for panel
    [SerializeField] private Vector3 panelEndPosition = Vector3.zero; //end position for panel

    //переделать под массивы
    private GameObject closeObject = null;
    private GameObject openObject = null;

    private float currentTimeForPanel;

    public void ToMenu()
    {
        openObject = menuPanel;
        openObject.SetActive(true);
    }

    public void ToSettings()
    {
        openObject = settingsPanel;
        openObject.SetActive(true);
    }

    public void ToShop()
    {
        openObject = shopPanel;
        openObject.SetActive(true);
    }

    public void ToInfo()
    {
        openObject = infoPanel;
        openObject.SetActive(true);
    }

    public void ToGame()
    {
        openObject = gamePanel;
        openObject.SetActive(true);
    }

    public void ToPause()
    {
        openObject = pausePanel;
        openObject.SetActive(true);
    }

    public void ToLosing()
    {
        openObject = losingPanel;
        openObject.SetActive(true);
    }

    public void ToChance()
    {
        openObject = chancePanel;
        openObject.SetActive(true);
    }

    private void Awake()
    {
        menuPanel.SetActive(true);
        shopPanel.SetActive(false);
        settingsPanel.SetActive(false);
        infoPanel.SetActive(false);
        gamePanel.SetActive(false);
        chancePanel.SetActive(false);
        pausePanel.SetActive(false);
        losingPanel.SetActive(false);

        menuPanel.transform.localPosition = panelStartPosition;
        shopPanel.transform.localPosition = panelStartPosition;
        settingsPanel.transform.localPosition = panelStartPosition;
        infoPanel.transform.localPosition = panelStartPosition;
        gamePanel.transform.localPosition = panelStartPosition;
        chancePanel.transform.localPosition = panelStartPosition;
        pausePanel.transform.localPosition = panelStartPosition;
        losingPanel.transform.localPosition = panelStartPosition;

        openObject = menuPanel;
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
                PanelClose(closeObject, panelStartPosition, panelEndPosition);
            }
            else
            {
                PanelOpen(openObject, panelStartPosition, panelEndPosition);
            }
        }
    }

    private void PanelOpen(GameObject gameObject, Vector3 startVector, Vector3 endVector)
    {
        if (currentTimeForPanel >= panelAnimationY.keys[panelAnimationY.keys.Length - 1].time)
        {
            closeObject = openObject;
            openObject = null;
            gameObject.transform.localPosition = endVector;
            currentTimeForPanel = 1f;
        }
        else
        {
            gameObject.transform.localPosition = new Vector3(startVector.x + (endVector.x - startVector.x) * panelAnimationX.Evaluate(currentTimeForPanel), startVector.y + (endVector.y - startVector.y) * panelAnimationY.Evaluate(currentTimeForPanel));
            gameObject.transform.localScale = new Vector3(panelAnimationScale.Evaluate(currentTimeForPanel), panelAnimationScale.Evaluate(currentTimeForPanel));
            currentTimeForPanel += Time.deltaTime;
        }
    }

    private void PanelClose(GameObject gameObject, Vector3 startVector, Vector3 endVector)
    {
        if (currentTimeForPanel <= 0)
        {
            closeObject.SetActive(false);
            closeObject = null;
            gameObject.transform.localPosition = startVector;
            currentTimeForPanel = 0f;
        }
        else
        {
            gameObject.transform.localPosition = new Vector3(startVector.x + (endVector.x - startVector.x) * panelAnimationX.Evaluate(currentTimeForPanel), startVector.y + (endVector.y - startVector.y) * panelAnimationY.Evaluate(currentTimeForPanel));
            gameObject.transform.localScale = new Vector3(panelAnimationScale.Evaluate(currentTimeForPanel), panelAnimationScale.Evaluate(currentTimeForPanel));
            currentTimeForPanel -= Time.deltaTime;
        }
    }
}
