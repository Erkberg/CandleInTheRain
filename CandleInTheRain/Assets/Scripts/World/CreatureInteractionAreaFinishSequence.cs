using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureInteractionAreaFinishSequence : InteractionAreaFinishSequence
{
    public Animator creature;

    public override IEnumerator FinishSequence()
    {
        Game.inst.ui.SetCanClickAwayText(false);
        Game.inst.ui.HideText(true);
        yield return null;
        Game.inst.ui.ShowText("I placed the plate on the sticks.\nWhat a lovely little shelter!");
        yield return new WaitForSeconds(2f);
        creature.SetTrigger("happy");
        Game.inst.ui.HideText(true);
        yield return new WaitForSeconds(1f);
        Game.inst.ui.ShowText("The creature is now protected from the rain.\nI'm just glad I was able to help.");
        yield return new WaitForSeconds(4f);
        Game.inst.ui.HideText(true);
        Game.inst.ui.SetCanClickAwayText(true);
    }
}
