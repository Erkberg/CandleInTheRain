using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureInteractionAreaFinishSequence : InteractionAreaFinishSequence
{
    public Animator creature;

    public override IEnumerator FinishSequence()
    {
        yield return new WaitForSeconds(1f);
        Game.inst.ui.ShowText("The creature has a nice little shelter now to protect it from the rain. I'm just glad I was able to help.");
        yield return new WaitForSeconds(4f);
        Game.inst.ui.HideText(true);
    }
}
