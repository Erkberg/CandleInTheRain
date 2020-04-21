using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItems : MonoBehaviour
{
    private Item[] allItems;
    private List<int> finishedItemIds = new List<int>();

    private void Awake()
    {
        allItems = FindObjectsOfType<Item>();

        foreach (Item item in allItems)
        {
            if (!item.itemData.activeAtStart)
                item.gameObject.SetActive(false);
        }
    }

    public void OnInteractWithItem(Item item)
    {
        ItemData itemData = item.itemData;

        if(itemData.needsItemId == -1 || finishedItemIds.Contains(itemData.needsItemId))
        {
            if(!finishedItemIds.Contains(itemData.id))
                finishedItemIds.Add(itemData.id);

            if (itemData.replaceItemId >= 0)
            {
                if (itemData.replaceItemId > 0)
                {
                    GetItemById(itemData.replaceItemId).gameObject.SetActive(true);
                }

                Game.inst.audio.PlaySound(Game.inst.audio.interactSuccess);
                Destroy(item.gameObject);
            }

            if (itemData.finishedAfterwards && !item.interactionArea.isFinished)
            {
                StartCoroutine(Game.inst.OnFinishInteractionArea(item.interactionArea));
            }
            else
            {
                Game.inst.ui.ShowText(itemData.interactText);
            }
        }
        else
        {
            Game.inst.audio.PlaySound(Game.inst.audio.interactFail);
            Game.inst.ui.ShowText(itemData.cantInteractText);
        }
    }

    private Item GetItemById(int id)
    {
        foreach(Item item in allItems)
        {
            if (item.itemData.id == id)
                return item;
        }

        return null;
    }
}
