using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    private void Awake()
    {
        SetCursorState();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            SetCursorState();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }

    private void SetCursorState()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
