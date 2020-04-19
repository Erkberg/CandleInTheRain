using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionAreaFinishSequence : MonoBehaviour
{
    public InteractionArea interactionArea;

    public virtual IEnumerator FinishSequence () 
    {
        yield return null;
    }

    private void Update()
    {
        if (interactionArea.isActive)
            CheckFinishCondition();
    }

    protected virtual void CheckFinishCondition() { }
}
