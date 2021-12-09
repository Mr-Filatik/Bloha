using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    #region SerializeField Variables

    [SerializeField] private GameObject edges = null;
    [SerializeField] private GameObject centers = null;

    #endregion

    #region Public Properties



    #endregion

    #region Private Variables

    private Image backgroundImage;
    private Color backgroundColor = new Color(1f, 1f, 1f, 0.8f);

    #endregion

    #region Public Methods



    #endregion

    #region Private Methods

    private void Awake()
    {
        float height = ((int)(Screen.height * 0.8f) / 100) * 100f;
        float width = ((int)(Screen.width * 0.8f) / 100) * 100f;
        backgroundImage = edges.transform.GetChild(0).gameObject.GetComponent<Image>();
        backgroundImage.transform.localPosition = new Vector3(-width / 2f, height / 2f, 0f);
        backgroundImage.color = backgroundColor;
        backgroundImage = edges.transform.GetChild(1).gameObject.GetComponent<Image>();
        backgroundImage.transform.localPosition = new Vector3(width / 2f, height / 2f, 0f);
        backgroundImage.color = backgroundColor;
        backgroundImage = edges.transform.GetChild(2).gameObject.GetComponent<Image>();
        backgroundImage.transform.localPosition = new Vector3(width / 2f, -height / 2f, 0f);
        backgroundImage.color = backgroundColor;
        backgroundImage = edges.transform.GetChild(3).gameObject.GetComponent<Image>();
        backgroundImage.transform.localPosition = new Vector3(-width / 2f, -height / 2f, 0f);
        backgroundImage.color = backgroundColor;

        backgroundImage = centers.transform.GetChild(0).gameObject.GetComponent<Image>();
        backgroundImage.transform.localPosition = new Vector3(-width / 2f, 0f, 0f);
        backgroundImage.rectTransform.sizeDelta = new Vector2(100f, height - 100f);
        backgroundImage.color = backgroundColor;
        backgroundImage = centers.transform.GetChild(1).gameObject.GetComponent<Image>();
        backgroundImage.transform.localPosition = new Vector3(0f, height / 2f, 0f);
        backgroundImage.rectTransform.sizeDelta = new Vector2(width - 100f, 100f);
        backgroundImage.color = backgroundColor;
        backgroundImage = centers.transform.GetChild(2).gameObject.GetComponent<Image>();
        backgroundImage.transform.localPosition = new Vector3(width / 2f, 0f, 0f);
        backgroundImage.rectTransform.sizeDelta = new Vector2(100f, height - 100f);
        backgroundImage.color = backgroundColor;
        backgroundImage = centers.transform.GetChild(3).gameObject.GetComponent<Image>();
        backgroundImage.transform.localPosition = new Vector3(0f, -height / 2f, 0f);
        backgroundImage.rectTransform.sizeDelta = new Vector2(width - 100f, 100f);
        backgroundImage.color = backgroundColor;
        backgroundImage = centers.transform.GetChild(4).gameObject.GetComponent<Image>();
        backgroundImage.transform.localPosition = new Vector3(0f, 0f, 0f);
        backgroundImage.rectTransform.sizeDelta = new Vector2(width - 100f, height - 100f);
        backgroundImage.color = backgroundColor;
        //backImage.rectTransform.sizeDelta = new Vector2(Screen.height * (sizeImages.x / sizeImages.y), Screen.height);
        //centers.transform.GetChild(0).gameObject;
    }

    #endregion
}
