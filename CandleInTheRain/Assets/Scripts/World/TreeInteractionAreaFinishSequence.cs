using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeInteractionAreaFinishSequence : InteractionAreaFinishSequence
{
    public Vector3 targetCamPosition = new Vector3(-0.72f, 10.77f, 15.05f);
    public float distanceTolerance = 0.1f;
    public Transform vCam;
    public SpriteRenderer heart;

    private bool firstTime = true;

    protected override void CheckFinishCondition()
    {
        if (Vector3.Distance(vCam.localPosition, targetCamPosition) < distanceTolerance)
        {
            AddHeartAlpha(0.33f);

            if(firstTime)
            {
                firstTime = false;
                Game.inst.audio.PlaySound(Game.inst.audio.mystery);
            }
        }
        else
        {
            AddHeartAlpha(-1f);
        }            
    }

    public override IEnumerator FinishSequence()
    {
        vCam.position = targetCamPosition;
        vCam.gameObject.SetActive(false);
        yield return new WaitForSeconds(3f);
    }

    private void AddHeartAlpha(float amount)
    {
        Color color = heart.color;
        color.a += amount * Time.deltaTime;

        if (color.a < 0f)
            color.a = 0f;

        heart.color = color;

        if(color.a >= 1f)
            StartCoroutine(Game.inst.OnFinishInteractionArea(interactionArea));
    }
}
