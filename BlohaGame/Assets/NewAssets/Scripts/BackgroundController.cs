using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BackgroundController : MonoBehaviour
{
    #region SerializeField Variables

    [SerializeField] private Image sky = null;
    [SerializeField] private Image cloud = null;
    [SerializeField] private Image cloudSmall = null;
    [SerializeField] private Image grass = null;
    [SerializeField] private Image grassSmall = null;
    [SerializeField] private AnimationCurve imageAnimationChangeColor = null;
    [SerializeField] private bool isRealyTimeOfDay = false;

    [Header("Grass Colors")]
    [SerializeField] private Color grassMorningColor = new Color32(114, 148, 64, 255);
    [SerializeField] private Color grassDayColor = new Color32(155, 203, 84, 255);
    [SerializeField] private Color grassEveningColor = new Color32(114, 148, 64, 255);
    [SerializeField] private Color grassNightColor = new Color32(104, 130, 66, 255);

    [Header("Grass Small Colors")]
    [SerializeField] private Color grassSmallMorningColor = new Color32(43, 92, 64, 255);
    [SerializeField] private Color grassSmallDayColor = new Color32(52, 110, 76, 255);
    [SerializeField] private Color grassSmallEveningColor = new Color32(43, 92, 64, 255);
    [SerializeField] private Color grassSmallNightColor = new Color32(32, 66, 47, 255);

    [Header("Cloud Colors")]
    [SerializeField] private Color cloudMorningColor = new Color32(232, 209, 192, 255);
    [SerializeField] private Color cloudDayColor = new Color32(232, 209, 192, 255);
    [SerializeField] private Color cloudEveningColor = new Color32(232, 209, 192, 255);
    [SerializeField] private Color cloudNightColor = new Color32(232, 209, 192, 255);

    [Header("Cloud Small Colors")]
    [SerializeField] private Color cloudSmallMorningColor = new Color32(187, 128, 76, 255);
    [SerializeField] private Color cloudSmallDayColor = new Color32(77, 175, 186, 255);
    [SerializeField] private Color cloudSmallEveningColor = new Color32(187, 128, 76, 255);
    [SerializeField] private Color cloudSmallNightColor = new Color32(49, 36, 70, 255);

    #endregion

    #region Public Properties



    #endregion

    #region Private Variables

    private float currentTime = 0f;
    private TimeOfDay timeOfDay = TimeOfDay.Day;
    private DateTime dateTime = DateTime.Now;

    #endregion

    #region Public Methods

    public void ChangeRealyTimeOfDay(bool input)
    {
        isRealyTimeOfDay = input;
    }

    public void ChangeTimeOfDay()
    {
        switch (timeOfDay)
        {
            case TimeOfDay.Morning:
                //timeOfDay = TimeOfDay.Day;
                cloud.color = cloudDayColor;
                cloudSmall.color = cloudSmallDayColor;
                grass.color = grassDayColor;
                grassSmall.color = grassSmallDayColor;
                break;
            case TimeOfDay.Day:
                //timeOfDay = TimeOfDay.Evening;
                cloud.color = cloudEveningColor;
                cloudSmall.color = cloudSmallEveningColor;
                grass.color = grassEveningColor;
                grassSmall.color = grassSmallEveningColor;
                break;
            case TimeOfDay.Evening:
                //timeOfDay = TimeOfDay.Night;
                cloud.color = cloudNightColor;
                cloudSmall.color = cloudSmallNightColor;
                grass.color = grassNightColor;
                grassSmall.color = grassSmallNightColor;
                break;
            case TimeOfDay.Night:
                //timeOfDay = TimeOfDay.Morning;
                cloud.color = cloudMorningColor;
                cloudSmall.color = cloudSmallMorningColor;
                grass.color = grassMorningColor;
                grassSmall.color = grassSmallMorningColor;
                break;
        }
    }

    #endregion

    #region Private Methods

    private void Awake()
    {
        ChangeTimeOfDay();
        //backImage = gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
        //frontImage = gameObject.transform.GetChild(1).gameObject.GetComponent<Image>();
        //frontImage.color = new Color(1, 1, 1, 1);
        //backImage.color = new Color(1, 1, 1, 1);
        //if (Screen.height / Screen.width > sizeImages.y / sizeImages.x)
        //{
        //    backImage.rectTransform.sizeDelta = new Vector2(Screen.height * (sizeImages.x / sizeImages.y), Screen.height);
        //    frontImage.rectTransform.sizeDelta = new Vector2(Screen.height * (sizeImages.x / sizeImages.y), Screen.height);
        //}
        //else
        //{
        //    backImage.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.width * (sizeImages.y / sizeImages.x));
        //    frontImage.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.width * (sizeImages.y / sizeImages.x));
        //}
        //backImage.sprite = null;
        if (isRealyTimeOfDay)
        {
            dateTime = DateTime.Now;
            if (dateTime.Hour >= 6 && dateTime.Hour < 10) timeOfDay = TimeOfDay.Morning; 
            if (dateTime.Hour >= 10 && dateTime.Hour < 18) timeOfDay = TimeOfDay.Day;
            if (dateTime.Hour >= 18 && dateTime.Hour < 23) timeOfDay = TimeOfDay.Evening;
            if (dateTime.Hour >= 23 || dateTime.Hour < 6) timeOfDay = TimeOfDay.Night;
        }
        else
        {
            if (dateTime.Hour >= 10 && dateTime.Hour < 18) timeOfDay = TimeOfDay.Morning;
        }
        ChangeTimeOfDay();
    }

    private void Update()
    {
        
        /*if (isRealyTimeOfDay)
        {
            dateTime = DateTime.Now;
            if (frontImage.sprite != morningImage && dateTime.Hour >= 6 && dateTime.Hour < 10) backImage.sprite = morningImage;
            if (frontImage.sprite != morningImage && dateTime.Hour >= 10 && dateTime.Hour < 18) backImage.sprite = dayImage;
            if (frontImage.sprite != morningImage && dateTime.Hour >= 18 && dateTime.Hour < 23) backImage.sprite = eveningImage;
            if (frontImage.sprite != morningImage && dateTime.Hour >= 23 || dateTime.Hour < 6) backImage.sprite = nightImage;
        }
        if (backImage.sprite != null)
        {
            if (currentTime  >= imageAnimationTransparency.keys[imageAnimationTransparency.keys.Length - 1].time)
            {
                frontImage.sprite = backImage.sprite;
                frontImage.color = new Color(1, 1, 1, 1);
                backImage.sprite = null;
                currentTime = 0;
            }
            else
            {
                frontImage.color = new Color(1, 1, 1, 1 - (imageAnimationTransparency.Evaluate(currentTime)));
                currentTime += Time.deltaTime;
            }
        }*/
    }

    #endregion
}

enum TimeOfDay
{
    Morning,
    Day,
    Evening,
    Night
}