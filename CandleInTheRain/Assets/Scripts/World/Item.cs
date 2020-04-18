using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData itemData;
    public InteractionArea interactionArea;

    private void Awake()
    {
        if (!interactionArea)
            interactionArea = GetComponentInParent<InteractionArea>();
    }

    private void OnMouseDown()
    {
        if(interactionArea.isActive)
            Game.inst.items.OnInteractWithItem(this);
    }

    private void OnMouseEnter()
    {
        Debug.Log("on mouse enter");
        if (interactionArea.isActive)
            Game.inst.ui.ShowText(itemData.name, "Click to interact");
    }

    private void OnMouseExit()
    {
        if (interactionArea.isActive)
            Game.inst.ui.HideText();
    }
}
