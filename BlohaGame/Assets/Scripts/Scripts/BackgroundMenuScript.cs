using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundMenuScript : MonoBehaviour
{
    [SerializeField] private Image morningImage = null;
    [SerializeField] private Image dayImage = null;
    [SerializeField] private Image eveningImage = null;
    [SerializeField] private Image nightImage = null;
    [SerializeField] private AnimationCurve imageAnimationTransparency = null;

    private float currentTime = 0f;
    private Image oldImage = null;
    private Image newImage = null;
    private DateTime dateTime = DateTime.Now;

    private void Awake()
    {
        dateTime = DateTime.Now;
        morningImage.color = new Color(1, 1, 1, 0);
        dayImage.color = new Color(1, 1, 1, 0);
        eveningImage.color = new Color(1, 1, 1, 0);
        nightImage.color = new Color(1, 1, 1, 0);
        if (dateTime.Hour >= 6 && dateTime.Hour < 10)
        {
            morningImage.color = new Color(1, 1, 1, 1);
            oldImage = morningImage;
        }
        if (dateTime.Hour >= 10 && dateTime.Hour < 18)
        {
            dayImage.color = new Color(1, 1, 1, 1);
            oldImage = dayImage;
        }
        if (dateTime.Hour >= 18 && dateTime.Hour < 23)
        {
            eveningImage.color = new Color(1, 1, 1, 1);
            oldImage = eveningImage;
        }
        if (dateTime.Hour >= 23 || dateTime.Hour < 6)
        {
            nightImage.color = new Color(1, 1, 1, 1);
            oldImage = nightImage;
        }
    }

    private void Update()
    {
        dateTime = DateTime.Now;
        if (dateTime.Hour >= 6 && dateTime.Hour < 10 && oldImage != morningImage)
        {
            newImage = morningImage;
        }
        if (dateTime.Hour >= 10 && dateTime.Hour < 18 && oldImage != dayImage)
        {
            newImage = dayImage;
        }
        if (dateTime.Hour >= 18 && dateTime.Hour < 23 && oldImage != eveningImage)
        {
            newImage = eveningImage;
        }
        if ((dateTime.Hour >= 23 || dateTime.Hour < 6) && oldImage != nightImage)
        {
            newImage = nightImage;
        }

        if (newImage != null)
        {
            ChangeTimesOfDay();
        }
    }

    private void ChangeTimesOfDay()
    {
        if (currentTime >= imageAnimationTransparency.keys[imageAnimationTransparency.keys.Length - 1].time)
        {
            oldImage.color = new Color(1, 1, 1, 0);
            oldImage = newImage;
            newImage = null;
            currentTime = 0;
        }
        else
        {
            //oldImage.color = new Color(1, 1, 1, imageAnimationTransparency.Evaluate(imageAnimationTransparency.keys[imageAnimationTransparency.keys.Length - 1].time - currentTime));
            newImage.color = new Color(1, 1, 1, imageAnimationTransparency.Evaluate(currentTime));
            currentTime += Time.deltaTime;
        }
    }
}