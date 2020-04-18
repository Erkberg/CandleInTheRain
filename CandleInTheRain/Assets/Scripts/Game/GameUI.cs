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
}
