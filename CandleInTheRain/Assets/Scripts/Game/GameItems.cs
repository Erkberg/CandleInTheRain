using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItems : MonoBehaviour
{
    private Item[] allItems;
    private List<int> collectedItemIds = new List<int>();

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

        if(itemData.needsItemId == -1 || collectedItemIds.Contains(itemData.needsItemId))
        {
            if(itemData.replaceItemId == 1000 && !item.interactionArea.isFinished)
            {
                StartCoroutine(Game.inst.OnFinishInteractionArea(item.interactionArea));
            }
            else
            {
                Game.inst.ui.ShowText(itemData.interactText);

                if (itemData.collect)
                {
                    collectedItemIds.Add(itemData.id);
                    Destroy(item.gameObject);
                }
                else if (itemData.replaceItemId > 0)
                {
                    GetItemById(itemData.replaceItemId).gameObject.SetActive(true);
                    Destroy(item.gameObject);
                }
            }
        }
        else
        {
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
