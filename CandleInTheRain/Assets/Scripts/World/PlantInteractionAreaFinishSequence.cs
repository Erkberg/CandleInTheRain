using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantInteractionAreaFinishSequence : InteractionAreaFinishSequence
{
    public Transform plant;

    public override IEnumerator FinishSequence()
    {
        Game.inst.ui.SetCanClickAwayText(false);
        Game.inst.ui.HideText(true);
        yield return null;
        Game.inst.ui.ShowText("I put the loose earth back onto the seed.\nThe seed rests peacefully inside the ground now.");
        yield return new WaitForSeconds(3f);
        Game.inst.ui.HideText(true);

        plant.localScale = new Vector3(1f, 0f, 1f);
        plant.gameObject.SetActive(true);
        while (plant.localScale.y < 1f)
        {
            plant.localScale = new Vector3(1f, plant.localScale.y + Time.deltaTime / 2, 1f);
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        Game.inst.ui.HideText(true);
        yield return null;
        Game.inst.ui.ShowText("The seed has grown into a...\n(strange, seemingly dangerous, yet beautiful)\n...plant!");
        yield return new WaitForSeconds(4f);
        Game.inst.ui.HideText(true);
        Game.inst.ui.SetCanClickAwayText(true);
    }
}
