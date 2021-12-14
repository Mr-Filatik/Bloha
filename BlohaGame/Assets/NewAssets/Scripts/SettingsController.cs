using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    #region SerializeField Variables

    [SerializeField] private GameObject buttons = null;
    [SerializeField] private GameObject buttonsFixed = null;
    [SerializeField] private GameObject background = null;

    #endregion

    #region Public Properties



    #endregion

    #region Private Variables

    private InitController initData = null;
    private float sizeElements = 100f;
    private Color backgroundColor = new Color32(0, 0, 0, 230); //new Color32(250, 184, 25, 230);
    private Image image = null;
    private Button button = null;
    private Toggle toggle = null;
    private GameObject element = null;

    #endregion

    #region Public Methods
    


    #endregion

    #region Private Methods

    private void Awake()
    {
        float ratio = Screen.width / 1080f;
        float height = ((int)(Screen.height * 0.8f) / 100) * 100f;
        float width = ((int)(Screen.width * 0.8f) / 100) * 100f;
        initData = GameObject.Find("Init").GetComponent<InitController>();

        int number = buttons.transform.childCount;

        image = background.transform.GetChild(0).gameObject.GetComponent<Image>();
        image.transform.localScale = new Vector3(ratio, ratio, ratio);
        image.transform.localPosition = new Vector3(0f, 0f, 0f);
        //(image.transform as RectTransform).sizeDelta = new Vector2(Screen.width * 0.8f, Screen.height * 0.8f);
        (image.transform as RectTransform).sizeDelta = new Vector2(900f, 1600f);
        image.color = backgroundColor;

        button = buttonsFixed.transform.GetChild(0).gameObject.GetComponent<Button>();
        button.transform.localScale = new Vector3(ratio, ratio, ratio);
        //button.transform.localPosition = new Vector3(-width / 2f + 100f * ratio, height / 2f - 100f * ratio, 0f);
        button.transform.localPosition = new Vector3(-900 / 2f + 100f * ratio, 1600 / 2f - 100f * ratio, 0f);

        for (int i = 0; i < number; i++)
        {
            element = buttons.transform.GetChild(i).gameObject;
            element.transform.localScale = new Vector3(ratio, ratio, ratio);
            element.transform.localPosition = new Vector3(0f, height / 2f - 250f * ratio * (i + 1), 0f);
        }
        //backImage.rectTransform.sizeDelta = new Vector2(Screen.height * (sizeImages.x / sizeImages.y), Screen.height);
        //centers.transform.GetChild(0).gameObject;
    }

    #endregion
}
