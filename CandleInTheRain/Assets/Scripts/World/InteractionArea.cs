﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionArea : MonoBehaviour
{
    public int id = 1;
    [TextArea]
    public string enterText;
    public GameCams.CamState camState;
    public Transform playerParkingPosition;
    public Transform playerLookTarget;
    public bool isActive = false;
    public bool isFinished = false;
    public ParticleSystem particle;
    public Collider triggerCollider;
    public InteractionAreaFinishSequence interactionAreaFinishSequence;

    private void Awake()
    {
        if (!playerLookTarget)
            playerLookTarget = transform;
    }

    public void OnPlayerEnterTrigger()
    {
        Game.inst.ui.ShowText(enterText, "Press left mousebutton to inspect");
    }

    public void OnPlayerExitTrigger()
    {
        Game.inst.ui.HideText();
    }

    public void OnPlayerEnter()
    {        
        Game.inst.OnEnterInteractionArea(camState);
        Game.inst.refs.playerMovement.MoveTowardsPosition(playerParkingPosition, playerLookTarget);
        isActive = true;
        triggerCollider.enabled = false;
    }

    public void OnPlayerExit()
    {
        isActive = false;
        triggerCollider.enabled = true;
    }

    public IEnumerator OnFinish()
    {
        isActive = false;
        isFinished = true;
        yield return interactionAreaFinishSequence.FinishSequence();
        particle.startColor = new Color(0f, 0.5f, 0f);
    }
}
