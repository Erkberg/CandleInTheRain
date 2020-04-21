using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ErksUnityLibrary;
using DG.Tweening;

public class TreeInteractionAreaFinishSequence : InteractionAreaFinishSequence
{
    public Vector3 targetCamPosition = new Vector3(-0.72f, 10.77f, 15.05f);
    public Vector3 altTargetCamPosition = new Vector3(-4.8f, 10.98f, -14.28f);
    public float distanceTolerance = 0.1f;
    public Transform vCam;
    public SpriteRenderer heart;

    private bool isInCorrectArea = false;
    private float blinkTimer = 0f;
    private bool isBlinking = false;

    protected override void CheckFinishCondition()
    {
        if (Vector3.Distance(vCam.localPosition, targetCamPosition) < distanceTolerance || Vector3.Distance(vCam.localPosition, altTargetCamPosition) < distanceTolerance)
        {
            if (!isInCorrectArea)
            {
                Game.inst.audio.PlaySound(Game.inst.audio.mystery);
            }

            blinkTimer = 0f;
            AddHeartAlpha(0.25f);
            isInCorrectArea = true;

            if(isBlinking)
            {
                isBlinking = false;
                StopAllCoroutines();
                heart.DOKill();
            }
        }
        else
        {
            if(heart.color.a <= 0f)
                Timing.AddTimeAndCheckMax(ref blinkTimer, 8f, Time.deltaTime, () => StartCoroutine(BlinkHeart()));

            isInCorrectArea = false;

            if(!isBlinking)
                AddHeartAlpha(-1f);
        }            
    }

    private IEnumerator BlinkHeart()
    {
        isBlinking = true;
        float duration = 0.2f;
        heart.DOFade(0.1f, duration);
        yield return new WaitForSeconds(duration);
        heart.DOFade(0f, duration);
        yield return new WaitForSeconds(duration);
        isBlinking = false;
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
