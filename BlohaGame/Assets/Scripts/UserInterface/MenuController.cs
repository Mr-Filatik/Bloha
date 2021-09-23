using System.Collections;
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

    public GameObject steps = null;
    public GameObject step = null;
    #endregion

    #region SecretField
    private bool game = false;
    //private float playerCoordinate = 0;
    private float speed = 1f;
    private int health = 3;
    private int[,] map = new int[9,3];
    private float startPosition = 0f;
    GameObject spawnedObject;
    GameObject[] spawnedObjects;
    GameObject player;
    Vector3 coordinates;
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

    }

    void Update()
    {
        if (game)
        {
            spawnedObjects = GameObject.FindGameObjectsWithTag("Step");
            foreach (GameObject item in spawnedObjects)
            {
                Destroy(item);
            }

            if (startPosition <= -100)
            {
                startPosition = 0f;
                Step();
            }
            else
            {
                startPosition -= Time.deltaTime * 10 * speed;
            }

            coordinates = new Vector3(player.transform.localPosition.x, player.transform.localPosition.y - Time.deltaTime * 10 * speed, player.transform.localPosition.z);
            player.transform.localPosition = coordinates;

            for (int i = 0; i < map.GetLength(0); i++)
            {
                spawnedObject = Instantiate(step);
                spawnedObject.transform.SetParent(steps.transform, false);
                spawnedObject.transform.localPosition = new Vector3(0, startPosition + i * 100, 0);
            }

            //player
            if (player.transform.localPosition.y < 10)
            {
                Vector3 fromPosition = player.transform.localPosition;
                Vector3 toPosition = new Vector3(fromPosition.x, fromPosition.y + 100, fromPosition.z);
                player.transform.localPosition = Vector3.Lerp(fromPosition, toPosition, 1);
            }
        }
    }

    void Step()
    {
        int[] array = new int[3];
        for (int i = 0; i < map.GetLength(0); i++)
        {
            //
        }
    }

    public void JumpButton()
    {
        if (player.transform.localPosition.y < 50)
        {
            Vector3 fromPosition = player.transform.localPosition;
            Vector3 toPosition = new Vector3(fromPosition.x, fromPosition.y + 100, fromPosition.z);
            player.transform.localPosition = Vector3.Lerp(fromPosition, toPosition, 1);
        }
    }

    public void PlayButton()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);

        for (int i = 0; i < map.GetLength(0); i++)
        {
            spawnedObject = Instantiate(step);
            spawnedObject.transform.SetParent(steps.transform, false);
            spawnedObject.transform.localPosition = new Vector3(0, startPosition + i * 100, 0);
        }

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
