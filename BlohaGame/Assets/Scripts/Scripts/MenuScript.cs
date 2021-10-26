using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private AnimationCurve panelAnimationX = null; //right - left
    [SerializeField] private AnimationCurve panelAnimationY = null; //up - down
    [SerializeField] private AnimationCurve panelAnimationScale = null; //scale
    [SerializeField] private Vector3 panelStartPosition = Vector3.zero; //start position for panel
    [SerializeField] private Vector3 panelEndPosition = Vector3.zero; //end position for panel

    [SerializeField] private GameObject menuPanel = null; //main menu
    [SerializeField] private GameObject shopPanel = null; //shop
    [SerializeField] private GameObject barPanel = null; //bar with money
    [SerializeField] private GameObject settingsPanel = null; //settings
    [SerializeField] private GameObject infoPanel = null; //info
    [SerializeField] private GameObject gamePanel = null; //game
    [SerializeField] private GameObject pausePanel = null; //pause
    [SerializeField] private GameObject losingPanel = null; //losing
    [SerializeField] private GameObject chancePanel = null; //chance

    private void Awake()
    {
        //menuPanel.transform.localScale = Screen.width;
        //Screen.width; Screen.height;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void SetCanvasScale()
    {
        CanvasScaler canvasScaler = gameObject.GetComponent<CanvasScaler>();
        int widht = Screen.width;
        if (widht > 720)
        {
            canvasScaler.scaleFactor = 1;
        }
        if (widht > 1080)
        {
            canvasScaler.scaleFactor = 1;
        }
        if (widht > 720)
        {
            canvasScaler.scaleFactor = 1;
        }
    }
}
