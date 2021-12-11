using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameController : MonoBehaviour
{
    #region SerializeField Variables

    [SerializeField] private AnimationCurve nameRotateAnimation = null;
    [SerializeField] private AnimationCurve nameScaleAnimation = null;
    [SerializeField] private AnimationCurve starScaleAnimation = null;

    #endregion

    #region Public Properties



    #endregion

    #region Private Variables

    private Image nameImage = null;
    private Image[] starImage = null;
    private float currentTimeForNameRotate = 0f;
    private float currentTimeForNameScale = 0f;
    private float[] currentTimeForStarScale = null;

    #endregion

    #region Public Methods



    #endregion

    #region Private Methods

    private void Awake()
    {
        float ratio = Screen.width / 1080f;
        nameImage = gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
        nameImage.transform.localScale = new Vector3(ratio, ratio, ratio);
        nameImage.transform.localPosition = new Vector3(0f, Screen.height / 2f - 280f * ratio, 0f);

        starImage = new Image[3];
        currentTimeForStarScale = new float[3];
        for (int i = 0; i < starImage.Length; i++)
        {
            starImage[i] = nameImage.gameObject.transform.GetChild(i).gameObject.GetComponent<Image>();
            starImage[i].color = new Color(1f, 1f, 1f, 1f);
            currentTimeForStarScale[i] = (float)(i * i);
        }
    }

    private void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            if (currentTimeForNameRotate >= nameRotateAnimation.keys[nameRotateAnimation.keys.Length - 1].time)
            {
                currentTimeForNameRotate = 0f;
            }
            nameImage.transform.localEulerAngles = new Vector3(0f, 0f, nameRotateAnimation.Evaluate(currentTimeForNameRotate) * 30f);
            currentTimeForNameRotate += Time.deltaTime;
            if (currentTimeForNameScale >= nameScaleAnimation.keys[nameScaleAnimation.keys.Length - 1].time)
            {
                currentTimeForNameScale = 0f;
            }
            nameImage.transform.localScale = new Vector3(nameScaleAnimation.Evaluate(currentTimeForNameScale) * Screen.width / 1080f, nameScaleAnimation.Evaluate(currentTimeForNameScale) * Screen.width / 1080f, 1f);
            currentTimeForNameScale += Time.deltaTime;
            for (int i = 0; i < starImage.Length; i++)
            {
                if (currentTimeForStarScale[i] >= starScaleAnimation.keys[starScaleAnimation.keys.Length - 1].time)
                {
                    starImage[i].transform.localPosition = new Vector3(Random.Range(-425f * Screen.width / 1080f, 425f * Screen.width / 1080f), Random.Range(-150f * Screen.width / 1080f, 150f * Screen.width / 1080f), 0f);
                    currentTimeForStarScale[i] = 0f;
                }
                starImage[i].transform.localScale = new Vector3(starScaleAnimation.Evaluate(currentTimeForStarScale[i]), starScaleAnimation.Evaluate(currentTimeForStarScale[i]), starScaleAnimation.Evaluate(currentTimeForStarScale[i]));
                currentTimeForStarScale[i] += Time.deltaTime;
            }
        }
    }

    #endregion
}
