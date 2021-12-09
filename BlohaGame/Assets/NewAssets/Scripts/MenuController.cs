using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    #region SerializeField Variables



    #endregion

    #region Public Properties



    #endregion

    #region Private Variables

    private Button playButton = null;
    private Button settingsButton = null;
    private Button achievementsButton = null;
    private Button shopButton = null;

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

        
        //frontImage.rectTransform.sizeDelta = new Vector2(Screen.height * (sizeImages.x / sizeImages.y), Screen.height);
    }

    #endregion
}
