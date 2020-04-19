using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public GameObject textObject;
    public GameObject backButtonObject;
    public TextMeshProUGUI maintext;
    public TextMeshProUGUI subtext;
    public RectTransform candleGreenArea;
    public GameObject gameOverScreen;
    public GameObject credits;
    public GameObject candleView;

    private string queuedText = "";
    private string queuedSubtext = "";

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && textObject.activeInHierarchy)
            HideText();
    }

    public void ShowText(string main, string sub = "")
    {
        if(textObject.activeInHierarchy && queuedText != main)
        {
            //Debug.Log("queue text " + main);
            queuedText = main;
            queuedSubtext = sub;
        }
        else
        {
            //Debug.Log("show text " + main);
            textObject.SetActive(true);
            maintext.text = main;
            subtext.text = sub;
        }        
    }

    public void HideText(bool clearQueue = false)
    {
        //Debug.Log("hide text " + maintext.text);
        textObject.SetActive(false);
        maintext.text = "";
        subtext.text = "";

        if(clearQueue)
        {
            queuedText = "";
            queuedSubtext = "";
        }

        if(!string.IsNullOrEmpty(queuedText))
        {
            //Debug.Log("show queued text after hide " + queuedText);
            ShowText(queuedText, queuedSubtext);
            queuedText = "";
            queuedSubtext = "";
        }
    }

    public void SetBackButtonActive(bool active)
    {
        backButtonObject.SetActive(active);
    }

    public void OnUpgradeCandle()
    {
        candleGreenArea.sizeDelta -= new Vector2(0f, 20f);
    }

    public void SetGameOverScreenActive(bool active)
    {
        gameOverScreen.SetActive(active);
    }
}
