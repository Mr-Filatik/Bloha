using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsController : MonoBehaviour
{
    #region SerializeField Variables

    [SerializeField] private GameObject buttons = null;
    [SerializeField] private GameObject edges = null;
    [SerializeField] private GameObject centers = null;

    #endregion

    #region Public Properties



    #endregion

    #region Private Variables

    private InitController initData = null;
    private float sizeElements = 100f;
    private Image backgroundImage;
    private Color backgroundColor = new Color32(0, 0, 0, 230);
    private Button button = null;

    #endregion

    #region Public Methods

    public void ChangeStatus()
    {
        SetElements();
    }

    #endregion

    #region Private Methods

    private void Awake()
    {
        float ratio = Screen.width / 1080f;
        float height = ((int)(Screen.height * 0.8f) / 100) * 100f;
        float width = ((int)(Screen.width * 0.8f) / 100) * 100f;
        initData = GameObject.Find("Init").GetComponent<InitController>();

        backgroundImage = edges.transform.GetChild(0).gameObject.GetComponent<Image>();
        backgroundImage.transform.localScale = new Vector3(ratio, ratio, ratio);
        backgroundImage.transform.localPosition = new Vector3(-width / 2f, height / 2f, 0f);
        backgroundImage.color = backgroundColor;
        backgroundImage = edges.transform.GetChild(1).gameObject.GetComponent<Image>();
        backgroundImage.transform.localScale = new Vector3(ratio, ratio, ratio);
        backgroundImage.transform.localPosition = new Vector3(width / 2f, height / 2f, 0f);
        backgroundImage.color = backgroundColor;
        backgroundImage = edges.transform.GetChild(2).gameObject.GetComponent<Image>();
        backgroundImage.transform.localScale = new Vector3(ratio, ratio, ratio);
        backgroundImage.transform.localPosition = new Vector3(width / 2f, -height / 2f, 0f);
        backgroundImage.color = backgroundColor;
        backgroundImage = edges.transform.GetChild(3).gameObject.GetComponent<Image>();
        backgroundImage.transform.localScale = new Vector3(ratio, ratio, ratio);
        backgroundImage.transform.localPosition = new Vector3(-width / 2f, -height / 2f, 0f);
        backgroundImage.color = backgroundColor;

        backgroundImage = centers.transform.GetChild(0).gameObject.GetComponent<Image>();
        backgroundImage.transform.localScale = new Vector3(ratio, ratio, ratio);
        backgroundImage.transform.localPosition = new Vector3(-width / 2f, 0f, 0f);
        backgroundImage.rectTransform.sizeDelta = new Vector2(sizeElements, height / ratio - sizeElements);
        backgroundImage.color = backgroundColor;
        backgroundImage = centers.transform.GetChild(1).gameObject.GetComponent<Image>();
        backgroundImage.transform.localScale = new Vector3(ratio, ratio, ratio);
        backgroundImage.transform.localPosition = new Vector3(0f, height / 2f, 0f);
        backgroundImage.rectTransform.sizeDelta = new Vector2(width / ratio - sizeElements, sizeElements);
        backgroundImage.color = backgroundColor;
        backgroundImage = centers.transform.GetChild(2).gameObject.GetComponent<Image>();
        backgroundImage.transform.localScale = new Vector3(ratio, ratio, ratio);
        backgroundImage.transform.localPosition = new Vector3(width / 2f, 0f, 0f);
        backgroundImage.rectTransform.sizeDelta = new Vector2(sizeElements, height / ratio - sizeElements);
        backgroundImage.color = backgroundColor;
        backgroundImage = centers.transform.GetChild(3).gameObject.GetComponent<Image>();
        backgroundImage.transform.localScale = new Vector3(ratio, ratio, ratio);
        backgroundImage.transform.localPosition = new Vector3(0f, -height / 2f, 0f);
        backgroundImage.rectTransform.sizeDelta = new Vector2(width / ratio - sizeElements, sizeElements);
        backgroundImage.color = backgroundColor;
        backgroundImage = centers.transform.GetChild(4).gameObject.GetComponent<Image>();
        backgroundImage.transform.localScale = new Vector3(ratio, ratio, ratio);
        backgroundImage.transform.localPosition = new Vector3(0f, 0f, 0f);
        backgroundImage.rectTransform.sizeDelta = new Vector2(width / ratio - sizeElements, height / ratio - sizeElements);
        backgroundImage.color = backgroundColor;

        button = buttons.transform.GetChild(0).gameObject.GetComponent<Button>();
        button.transform.localScale = new Vector3(ratio, ratio, ratio);
        button.transform.localPosition = new Vector3(-width / 2f + sizeElements * ratio / 2f, height / 2f - sizeElements * ratio / 2f, 0f);
        button = buttons.transform.GetChild(1).gameObject.GetComponent<Button>();
        button.transform.localScale = new Vector3(ratio, ratio, ratio);
        button.transform.localPosition = new Vector3(-width / 2f + sizeElements * ratio / 2f, -height / 2f + sizeElements * ratio / 2f, 0f);
        button = buttons.transform.GetChild(2).gameObject.GetComponent<Button>();
        button.transform.localScale = new Vector3(ratio, ratio, ratio);
        button.transform.localPosition = new Vector3(width / 2f - sizeElements * ratio / 2f, -height / 2f + sizeElements * ratio / 2f, 0f);

        SetElements();
    }

    private void SetElements()
    {
        /*float ratio = Screen.width / 1080f;
        float height = ((int)(Screen.height * 0.8f) / 100) * 100f;
        float width = ((int)(Screen.width * 0.8f) / 100) * 100f;

        button = buttons.transform.GetChild(0).gameObject.GetComponent<Button>();
        button.transform.localScale = new Vector3(ratio, ratio, ratio);
        button.transform.localPosition = new Vector3(-width / 2f + sizeElements * ratio / 2f, height / 2f - sizeElements * ratio / 2f, 0f);
        button = buttons.transform.GetChild(1).gameObject.GetComponent<Button>();
        button.transform.localScale = new Vector3(ratio, ratio, ratio);
        if (initData.AdsIsDisabled())
        {
            button.transform.localPosition = new Vector3(-0f, -height, 0f);
        }
        else
        {
            button.transform.localPosition = new Vector3(-0f, -height / 2f + 150f * ratio * 1f, 0f);
        }

        toggle = toggles.transform.GetChild(0).gameObject.GetComponent<Toggle>();
        toggle.transform.localScale = new Vector3(ratio, ratio, ratio);
        toggle.transform.localPosition = new Vector3(80f * ratio, height / 2f - 250f * ratio * 1f, 0f);

        toggle = toggles.transform.GetChild(1).gameObject.GetComponent<Toggle>();
        toggle.transform.localScale = new Vector3(ratio, ratio, ratio);
        toggle.transform.localPosition = new Vector3(80f * ratio, height / 2f - 250f * ratio * 2f, 0f);

        toggle = toggles.transform.GetChild(2).gameObject.GetComponent<Toggle>();
        toggle.transform.localScale = new Vector3(ratio, ratio, ratio);
        toggle.transform.localPosition = new Vector3(80f * ratio, height / 2f - 250f * ratio * 3f, 0f);
        //backImage.rectTransform.sizeDelta = new Vector2(Screen.height * (sizeImages.x / sizeImages.y), Screen.height);
        //centers.transform.GetChild(0).gameObject;
        */
    }

    #endregion
}
