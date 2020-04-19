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
        refs.playerInteraction.LeaveInteractionArea();
    }

    private void SetCursorState()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Confined;
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
        cams.SetCamState(GameCams.CamState.CandleFocus);
        yield return new WaitForSeconds(2f);
        refs.playerCandle.candleParticle.EmitSmoke();
        yield return new WaitForSeconds(2f);
        ui.SetGameOverScreenActive(true);
        refs.playerMovement.ResetPosition();
    }

    public IEnumerator OnFinishInteractionArea(InteractionArea interactionArea)
    {
        yield return interactionArea.OnFinish();
        yield return CandleUpgradeSequence();
    }

    private IEnumerator CandleUpgradeSequence()
    {
        cams.SetCamState(GameCams.CamState.CandleFocus);
        yield return new WaitForSeconds(2f);
        refs.playerCandle.UpgradeCandle();
        ui.OnUpgradeCandle();
        refs.rain.emissionRate -= 1000f;
        yield return new WaitForSeconds(3f);
        OnBackButtonPressed();
    }

    public void OnRelightButtonClicked()
    {
        ui.SetGameOverScreenActive(false);
        SetPlayerActive(true);
        refs.playerCandle.ResetCandle();
        cams.SetCamState(GameCams.CamState.ThirdPersonFollow);
    }
}
