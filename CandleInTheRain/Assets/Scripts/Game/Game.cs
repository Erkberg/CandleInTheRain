using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game inst;

    public GameRefs refs;
    public GameCams cams;
    public GameUI ui;
    public GameItems items;

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

    public void OnEnterInteractionArea(GameCams.CamState camState)
    {
        cams.SetCamState(camState);
        ui.SetBackButtonActive(true);
        SetPlayerActive(false);
    }

    public void OnBackButtonPressed()
    {
        cams.SetCamState(GameCams.CamState.ThirdPersonFollow);
        ui.SetBackButtonActive(false);
        SetPlayerActive(true);
        refs.playerInteraction.ReEnterInteractionArea();
    }

    private void SetCursorState()
    {
        //Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void SetPlayerActive(bool active)
    {
        refs.playerMovement.movementEnabled = active;
        refs.playerInteraction.interactionEnabled = active;
    }

    public void OnCandleExtinct()
    {
        StartCoroutine(CandleExtinctSequence());
    }

    private IEnumerator CandleExtinctSequence()
    {
        SetPlayerActive(false);
        yield return new WaitForSeconds(2f);
    }
}
