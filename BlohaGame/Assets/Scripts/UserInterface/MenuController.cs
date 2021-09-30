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
    #endregion

    #region SecretField
    private bool game = false;

    [SerializeField] private float speed;
    [SerializeField] private float speedDefault = 2f;
    private int[,] map = new int[12, 3];

    private int playerHealth = 3;
    [SerializeField] private int playerStep = 1;

    private float startPosition = 0f;
    GameObject spawnedObject;
    GameObject[] spawnedObjects;
    GameObject player;
    Vector3 coordinates;
    //float jumpBorder = 80;//убрать
    float jumpBorderAuto = 20;//?

    [SerializeField] private float cooldownForJumpButton = 1f;
    private float timeForButton;

    Vector3 fromPosition;
    Vector3 toPosition;
    int direction = 0;
    float timeForDirection = 0;
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

        /*steps[steps.GetLength(0) - 1].transform.localPosition = new Vector3(0, 0, 0);
        for (int i = 1; i < steps.GetLength(0); i++)
        {
            steps[i].transform.localPosition = new Vector3(0, i * 108 * steps[i].transform.localScale.x, 0); //i * 108 - 108 * i * 0.02f
        }*/

        steps[0].transform.localPosition = new Vector3(0, 0, 0);
        steps[0].transform.localScale = new Vector3(1, 1, 1);
        for (int i = 1; i < steps.GetLength(0); i++)
        {
            steps[i].transform.localPosition = new Vector3(0, steps[i - 1].transform.localPosition.y + 108 * (steps[i - 1].transform.localScale.x - 0.02f), 0); //i * 108 - 108 * i * 0.02f
            steps[i].transform.localScale = new Vector3(steps[i - 1].transform.localScale.x - 0.02f, steps[i - 1].transform.localScale.y - 0.02f, 0);
        }
        

        /*steps[0].transform.localPosition = new Vector3(0, 0, 0);
        steps[0].transform.localScale = new Vector3(1, 1, 1);
        steps[1].transform.localPosition = new Vector3(0, 108, 0);
        steps[1].transform.localScale = new Vector3(0.98f, 0.98f, 1);
        for (int i = 2; i < steps.GetLength(0); i++)
        {
            //float a = steps[i].transform.localScale.y * 108;
            //steps[i].transform.localPosition = new Vector3(0, steps[i - 1].transform.localPosition.y + 108 * steps[i - 1].transform.localScale.y, 0);
            //steps[i].transform.localScale = new Vector3(steps[i - 1].transform.localScale.x - 0.02f, steps[i - 1].transform.localScale.x - 0.02f, 1);
        }*/

        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                map[i, j] = 0;
            }
        }
    }

    void Start()
    {
        /*playerStep = 1;

        coordinates = new Vector3(player.transform.localPosition.x, player.transform.localPosition.y - Time.deltaTime * 10 * speed, player.transform.localPosition.z);

        player.transform.localPosition = coordinates;*/
    }

    void Update()
    {
        if (game)
        {
            speed = speedDefault;// + Time.deltaTime * 10 * playerStep * steps[0].transform.position.y / 100; //исправить


            /*coordinates = new Vector3(player.transform.localPosition.x, player.transform.localPosition.y - Time.deltaTime * 10 * speed, player.transform.localPosition.z);
            player.transform.localPosition = coordinates;*/

            LadderMovement(); //update

            //authomatic jump 
            /*
            if (player.transform.localPosition.y < jumpBorderAuto)
            {
                fromPosition = player.transform.localPosition;
                toPosition = new Vector3(fromPosition.x, fromPosition.y + 100, fromPosition.z);
                player.transform.localPosition = Vector3.Lerp(fromPosition, toPosition, 1);
            }
            */
        }
    }

    void LadderMovement()
    {
        for (int i = 0; i < steps.GetLength(0); i++)
        {
            steps[i].transform.localScale = new Vector3((float)(100 - i * 2 + (-steps[0].transform.localPosition.y * 2) / 100) / 100, (float)(100 - i * 2 + (-steps[0].transform.localPosition.y * 2) / 100) / 100, 1);

            coordinates = new Vector3(steps[i].transform.localPosition.x, steps[i].transform.localPosition.y - Time.deltaTime * 10 * speed * steps[i].transform.localScale.x, steps[i].transform.localPosition.z);

            steps[i].transform.localPosition = coordinates;
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
            //продолжить
            if (playerStep > 0)
            {
                playerStep--;
            }
        }
    }

    void LadderMovementReverse(int number)
    {
        //maybe not needed
    }

    public void JumpButtonUp()
    {
        //maybe you need to add fatigue after a long press
        if (Time.realtimeSinceStartup - timeForButton > cooldownForJumpButton)
        {
            playerStep+=2;
            //jump over a step
            Debug.Log("Long jump");
        }
        else
        {
            playerStep++;
            //jump to the next step
            Debug.Log("Short jump");
        }
        /*if (Time.realtimeSinceStartup - timeForButton > 2f)
        {
            Vector3 fromPosition = player.transform.localPosition;
            Vector3 toPosition = new Vector3(fromPosition.x, fromPosition.y + 100, fromPosition.z);
            player.transform.localPosition = Vector3.Lerp(fromPosition, toPosition, 1);

            timeForButton = Time.realtimeSinceStartup;
        }*/
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
