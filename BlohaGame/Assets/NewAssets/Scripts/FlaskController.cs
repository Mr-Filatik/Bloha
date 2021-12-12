using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlaskController : MonoBehaviour
{
    [SerializeField] private GameObject flask = null;
    [SerializeField] private float speed = 1;


    private Image liquidup = null;
    private Image liquiddown = null;
    private float currentTime = 0f;
    private float level = 0f;
    private bool isGame = false;
    private bool isPause = false;

    public void GameStart()
    {
        liquidup.transform.localScale = new Vector3(1f, 0f, 1f);
        liquiddown.transform.localScale = new Vector3(1f, 0f, 1f);
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

    public bool Jump(float input = 0.5f)
    {
        if (level >= input * 100f)
        {
            level -= input * 100f;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Awake()
    {
        flask.transform.localScale = new Vector3(Screen.width / 1080f * 0.7f, Screen.width / 1080f * 0.7f, 0f);
        flask.transform.localPosition = new Vector3(-Screen.width / 2f + 100f * Screen.width / 1080f, -Screen.height / 2f + 500f * Screen.width / 1080f, 0f);
        liquidup = flask.transform.GetChild(0).gameObject.GetComponent<Image>();
        liquiddown = flask.transform.GetChild(1).gameObject.GetComponent<Image>();
        liquidup.transform.localScale = new Vector3(1f, 0f, 1f);
        liquiddown.transform.localScale = new Vector3(1f, 0f, 1f);
    }

    private void Update()
    {
        if (isGame && !isPause)
        {
            if (level < 50f)
            {
                liquiddown.transform.localScale = new Vector3(1f, level / 50f, 1f);
                level += Time.deltaTime * speed * (liquiddown.transform.localScale.y + 0.2f);
            }
            else
            {
                if (level < 100f)
                {
                    liquidup.transform.localScale = new Vector3(1f, (level - 50f) / 50f, 1f);
                    level += Time.deltaTime * speed * (liquiddown.transform.localScale.y + 0.2f);
                }
            }
        }
    }
}
