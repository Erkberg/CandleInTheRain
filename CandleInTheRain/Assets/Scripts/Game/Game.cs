using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game inst;

    public GameRefs refs;
    public GameCams cams;
    public GameUI ui;

    private void Awake() 
    {
        inst = this;

        SetCursorState();
    }

    private void Update() 
    {
        if(Input.GetMouseButtonDown(0))
            SetCursorState();
    }

    private void SetCursorState()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
