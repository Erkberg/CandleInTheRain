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
    public GameAudio audio;
    public GameCursor cursor;

    public Config config;

    [HideInInspector]
    public int interactionsFinished = 0;

    private void Awake() 
    {
        inst = this;

        cursor.SetVisibility(false);
    }

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.C))
        {
            interactionsFinished++;
            if (interactionsFinished >= config.totalInteractions)
                StartCoroutine(GameEndSequence());
            else
                StartCoroutine(CandleUpgradeSequence());
        }

        if (Input.GetKeyDown(KeyCode.E))
            StartCoroutine(GameEndSequence());
#endif
    }

    public void OnEnterInteractionArea(GameCams.CamState camState)
    {
        cursor.SetVisibility(true);
        cams.SetCamState(camState);
        ui.SetBackButtonActive(true);
        SetPlayerActive(false);
    }

    public void OnBackButtonPressed()
    {
        cams.SetCamState(GameCams.CamState.ThirdPersonFollow);
        ui.SetBackButtonActive(false);
        cursor.SetVisibility(false);
        SetPlayerActive(true);
        refs.playerInteraction.LeaveInteractionArea();
    }

    public void SetPlayerActive(bool active)
    {
        refs.playerMovement.movementEnabled = active;
        refs.playerInteraction.interactionEnabled = active;

        if (!active)
            refs.playerMovement.animator.SetBool("walking", false);
    }

    public void OnCandleExtinct()
    {
        StartCoroutine(CandleExtinctSequence());
    }

    private IEnumerator CandleExtinctSequence()
    {
        refs.playerInteraction.LeaveInteractionArea();
        ui.SetBackButtonActive(false);
        SetPlayerActive(false);
        cams.SetCamState(GameCams.CamState.CandleFocus);
        yield return new WaitForSeconds(2f);
        refs.playerCandle.candleParticle.EmitSmoke();
        audio.PlaySound(audio.candleSmoke);
        yield return new WaitForSeconds(2f);
        cursor.SetVisibility(true);
        ui.SetGameOverScreenActive(true);
        refs.playerMovement.ResetPosition();
    }

    public IEnumerator OnFinishInteractionArea(InteractionArea interactionArea)
    {
        ui.SetBackButtonActive(false);
        yield return interactionArea.OnFinish();

        interactionsFinished++;
        if (interactionsFinished >= config.totalInteractions)
            StartCoroutine(GameEndSequence());
        else
            yield return CandleUpgradeSequence();
    }

    private IEnumerator CandleUpgradeSequence(bool skipBackButton = false)
    {
        cams.SetCamState(GameCams.CamState.CandleFocus);
        yield return new WaitForSeconds(2f);
        refs.playerCandle.UpgradeCandle();
        ui.OnUpgradeCandle();
        refs.rain.emissionRate -= config.GetRainEmissionMalusPerInteraction();
        audio.SetAtmoVolumePercentage(1f - (float)interactionsFinished / config.totalInteractions);
        yield return new WaitForSeconds(3f);

        if(!skipBackButton)
            OnBackButtonPressed();
    }

    public void OnRelightButtonClicked()
    {
        cursor.SetVisibility(false);
        ui.SetGameOverScreenActive(false);
        SetPlayerActive(true);
        refs.playerCandle.ResetCandle();
        cams.SetCamState(GameCams.CamState.ThirdPersonFollow);
    }

    private IEnumerator GameEndSequence()
    {
        refs.playerCandle.candleActive = false;
        yield return CandleUpgradeSequence(true);
        ui.candleView.SetActive(false);
        refs.playerCandle.ParentToHand();
        refs.playerMovement.animator.SetTrigger("end");
        yield return new WaitForSeconds(12f);
        refs.playerCandle.candleParticle.ActivateEndParticle();
        yield return new WaitForSeconds(4f);
        refs.playerCandle.candleParticle.EndParticleBurst();
        yield return new WaitForSeconds(4f);
        ui.credits.SetActive(true);
    }
}
