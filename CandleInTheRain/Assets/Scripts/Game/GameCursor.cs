using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameCursor : MonoBehaviour
{
    public Image img;

    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;

        SetCursorState();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            SetCursorState();

        transform.position = Input.mousePosition;
    }

    public void SetVisibility(bool visible)
    {
        img.DOKill();
        img.DOFade(visible ? 1f : 0f, 0.33f);
    }

    private void SetCursorState()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
