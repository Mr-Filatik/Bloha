using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    #region SerializeField Variables

    [SerializeField] private AnimationCurve buttonScaleAnimation = null;

    #endregion

    #region Public Properties



    #endregion

    #region Private Variables

    private Button playButton = null;
    private Button settingsButton = null;
    private Button achievementsButton = null;
    private Button shopButton = null;
    private float[] currentTimeForButtonScale = null;

    #endregion

    #region Public Methods



    #endregion

    #region Private Methods

    private void Awake()
    {
        playButton = gameObject.transform.GetChild(0).gameObject.GetComponent<Button>();
        shopButton = gameObject.transform.GetChild(1).gameObject.GetComponent<Button>();
        settingsButton = gameObject.transform.GetChild(2).gameObject.GetComponent<Button>();
        achievementsButton = gameObject.transform.GetChild(3).gameObject.GetComponent<Button>();
        //(playButton.transform as RectTransform).sizeDelta = new Vector2(Screen.height, Screen.height);

        float ratio = Screen.width / 1080f;
        playButton.transform.localScale = new Vector3(ratio, ratio, ratio);
        shopButton.transform.localScale = new Vector3(ratio, ratio, ratio);
        settingsButton.transform.localScale = new Vector3(ratio, ratio, ratio);
        achievementsButton.transform.localScale = new Vector3(ratio, ratio, ratio);

        playButton.transform.localPosition = new Vector3(0f, -Screen.height / 2f + 580f * ratio, 0f);
        shopButton.transform.localPosition = new Vector3(0f, -Screen.height / 2f + 260f * ratio, 0f);
        settingsButton.transform.localPosition = new Vector3(-350f * ratio, -Screen.height / 2f + 300f * ratio, 0f);
        achievementsButton.transform.localPosition = new Vector3(350f * ratio, -Screen.height / 2f + 300f * ratio, 0f);

        currentTimeForButtonScale = new float[4];
        currentTimeForButtonScale[0] = 0f;
        currentTimeForButtonScale[1] = 1f;
        currentTimeForButtonScale[2] = 2f;
        currentTimeForButtonScale[3] = 3f;
        //frontImage.rectTransform.sizeDelta = new Vector2(Screen.height * (sizeImages.x / sizeImages.y), Screen.height);
    }

    private void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            if (currentTimeForButtonScale[0] >= buttonScaleAnimation.keys[buttonScaleAnimation.keys.Length - 1].time)
            {
                currentTimeForButtonScale[0] = 0f;
            }
            playButton.transform.localScale = new Vector3(buttonScaleAnimation.Evaluate(currentTimeForButtonScale[0]) * Screen.width / 1080f, buttonScaleAnimation.Evaluate(currentTimeForButtonScale[0]) * Screen.width / 1080f, 0f);
            currentTimeForButtonScale[0] += Time.deltaTime;
            if (currentTimeForButtonScale[1] >= buttonScaleAnimation.keys[buttonScaleAnimation.keys.Length - 1].time)
            {
                currentTimeForButtonScale[1] = 0f;
            }
            achievementsButton.transform.localScale = new Vector3(buttonScaleAnimation.Evaluate(currentTimeForButtonScale[1]) * Screen.width / 1080f, buttonScaleAnimation.Evaluate(currentTimeForButtonScale[1]) * Screen.width / 1080f, 0f);
            currentTimeForButtonScale[1] += Time.deltaTime;
            if (currentTimeForButtonScale[2] >= buttonScaleAnimation.keys[buttonScaleAnimation.keys.Length - 1].time)
            {
                currentTimeForButtonScale[2] = 0f;
            }
            shopButton.transform.localScale = new Vector3(buttonScaleAnimation.Evaluate(currentTimeForButtonScale[2]) * Screen.width / 1080f, buttonScaleAnimation.Evaluate(currentTimeForButtonScale[2]) * Screen.width / 1080f, 0f);
            currentTimeForButtonScale[2] += Time.deltaTime;
            if (currentTimeForButtonScale[3] >= buttonScaleAnimation.keys[buttonScaleAnimation.keys.Length - 1].time)
            {
                currentTimeForButtonScale[3] = 0f;
            }
            settingsButton.transform.localScale = new Vector3(buttonScaleAnimation.Evaluate(currentTimeForButtonScale[3]) * Screen.width / 1080f, buttonScaleAnimation.Evaluate(currentTimeForButtonScale[3]) * Screen.width / 1080f, 0f);
            currentTimeForButtonScale[3] += Time.deltaTime;
        }
    }

    #endregion
}
