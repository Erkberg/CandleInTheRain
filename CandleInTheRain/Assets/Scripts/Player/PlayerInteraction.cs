using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public bool interactionEnabled = true;

    private InteractionArea currentInteractionArea;
    private bool isInsideArea = false;

    private void Update() 
    {
        if(Input.GetMouseButtonDown(0) && currentInteractionArea)
        {
            currentInteractionArea.OnPlayerEnter();
            isInsideArea = true;
        }
    }

    public void LeaveInteractionArea()
    {
        if (currentInteractionArea)
            currentInteractionArea.OnPlayerExit();

        Game.inst.cursor.SetVisibility(false);
        isInsideArea = false;
        currentInteractionArea = null;
    }

    private void OnTriggerEnter(Collider other) 
    {
        InteractionArea interactionArea = other.GetComponent<InteractionArea>();

        if(interactionArea && !interactionArea.isFinished)
        {
            currentInteractionArea = interactionArea;
            currentInteractionArea.OnPlayerEnterTrigger();
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        InteractionArea interactionArea = other.GetComponent<InteractionArea>();

        if (interactionArea && interactionArea == currentInteractionArea)
        {
            currentInteractionArea.OnPlayerExitTrigger();
            currentInteractionArea = null;            
        }
    }
}
