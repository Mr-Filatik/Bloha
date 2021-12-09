using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BackgroundController : MonoBehaviour
{
    #region SerializeField Variables

    [SerializeField] private Vector2 sizeImages = Vector2Int.zero;
    [SerializeField] private Sprite morningImage = null;
    [SerializeField] private Sprite dayImage = null;
    [SerializeField] private Sprite eveningImage = null;
    [SerializeField] private Sprite nightImage = null;
    [SerializeField] private AnimationCurve imageAnimationTransparency = null;
    [SerializeField] private bool isRealyTimeOfDay = false;

    #endregion

    #region Public Properties



    #endregion

    #region Private Variables

    private float currentTime = 0f;
    private Image backImage = null;
    private Image frontImage = null;
    private DateTime dateTime = DateTime.Now;

    #endregion

    #region Public Methods

    public void ChangeTimeOfDay() // need change
    {
        if (backImage.sprite == null)
        {
            if (frontImage.sprite == morningImage)
            {
                backImage.sprite = dayImage;
            }
            if (frontImage.sprite == dayImage)
            {
                backImage.sprite = eveningImage;
            }
            if (frontImage.sprite == eveningImage)
            {
                backImage.sprite = nightImage;
            }
            if (frontImage.sprite == nightImage)
            {
                backImage.sprite = morningImage;
            }
        }
    }

    #endregion

    #region Private Methods

    private void Awake()
    {
        backImage = gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
        frontImage = gameObject.transform.GetChild(1).gameObject.GetComponent<Image>();
        frontImage.color = new Color(1, 1, 1, 1);
        backImage.color = new Color(1, 1, 1, 1);
        if (Screen.height / Screen.width > sizeImages.y / sizeImages.x)
        {
            backImage.rectTransform.sizeDelta = new Vector2(Screen.height * (sizeImages.x / sizeImages.y), Screen.height);
            frontImage.rectTransform.sizeDelta = new Vector2(Screen.height * (sizeImages.x / sizeImages.y), Screen.height);
        }
        else
        {
            backImage.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.width * (sizeImages.y / sizeImages.x));
            frontImage.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.width * (sizeImages.y / sizeImages.x));
        }
        backImage.sprite = null;
        if (isRealyTimeOfDay)
        {
            dateTime = DateTime.Now;
            if (dateTime.Hour >= 6 && dateTime.Hour < 10) frontImage.sprite = morningImage; 
            if (dateTime.Hour >= 10 && dateTime.Hour < 18) frontImage.sprite = dayImage;
            if (dateTime.Hour >= 18 && dateTime.Hour < 23) frontImage.sprite = eveningImage;
            if (dateTime.Hour >= 23 || dateTime.Hour < 6) frontImage.sprite = nightImage;
        }
        else
        {
            if (dateTime.Hour >= 10 && dateTime.Hour < 18) frontImage.sprite = dayImage;
        }
    }

    private void Update()
    {
        if (isRealyTimeOfDay)
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
        }
    }

    #endregion
}