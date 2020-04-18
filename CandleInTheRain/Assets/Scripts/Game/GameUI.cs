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

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && textObject.activeInHierarchy)
            HideText();
    }

    public void ShowText(string main, string sub = "")
    {
        textObject.SetActive(true);
        maintext.text = main;
        subtext.text = sub;
    }

    public void HideText()
    {
        textObject.SetActive(false);
        maintext.text = "";
        subtext.text = "";
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
