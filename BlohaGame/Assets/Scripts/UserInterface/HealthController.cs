using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private int healthNumber = 3;
    [SerializeField] private GameObject healthPrefab = null;
    [SerializeField] private bool isAnimation = true;
    [SerializeField] private AnimationCurve healthAnimationScale = null;

    private int health = 0;
    private GameObject[] healths = null;
    private float currentTime = 0f;
    private float distanceSpawn = 50f;

    public void SetHealth(int inputHealth)
    {
        if (inputHealth > healthNumber)
        {
            health = healthNumber;
        }
        else
        {
            health = inputHealth;
        }
        ChangeHealth();
    }

    public void MinusHealth()
    {
        if (health > 0)
        {
            health--;
            ChangeHealth();
        }
    }

    public void PlusHealth()
    {
        if (health < healthNumber)
        {
            health++;
            ChangeHealth();
        }
    }

    //change spawn coordinates here
    private void OnEnable()
    {
        if (healths == null)
        {
            healths = new GameObject[healthNumber];
            for (int i = 0; i < healthNumber; i++)
            {
                healths[i] = Instantiate(healthPrefab, gameObject.transform);
                //change spawn coordinates here
                healths[i].transform.localPosition = new Vector3(0f, 0f - i * distanceSpawn, 0f);
                healths[i].transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
            ChangeHealth();
        }
    }

    private void Update()
    {
        if (isAnimation)
        {
            if (healths != null)
            {
                for (int i = 0; i < healthNumber; i++)
                {
                    healths[i].transform.localScale = new Vector3(healths[i].transform.localScale.z * healthAnimationScale.Evaluate(currentTime), healths[i].transform.localScale.z * healthAnimationScale.Evaluate(currentTime), 1f);
                    currentTime += Time.deltaTime;
                    if (currentTime >= healthAnimationScale.keys[healthAnimationScale.keys.Length - 1].time)
                    {
                        currentTime = 0f;
                    }
                }
            }
        }
    }

    private void OnDisable()
    {
        health = 0;
        healths = null;
        currentTime = 0f;
    }

    private void ChangeHealth()
    {
        if (healths != null)
        {
            for (int i = 0; i < healthNumber; i++)
            {
                if (i + 1 <= health)
                {
                    healths[i].SetActive(true);
                }
                else
                {
                    healths[i].SetActive(false);
                }
            }
        }
    }
}
