using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private InteractionArea currentInteractionArea;

    private void Update() 
    {
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        InteractionArea interactionArea = other.GetComponent<InteractionArea>();

        if(interactionArea)
        {
            currentInteractionArea = interactionArea;
            currentInteractionArea.OnPlayerEnter();
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        InteractionArea interactionArea = other.GetComponent<InteractionArea>();

        if(interactionArea && interactionArea == currentInteractionArea)
        {
            currentInteractionArea.OnPlayerExit();
            currentInteractionArea = null;            
        }
    }
}
