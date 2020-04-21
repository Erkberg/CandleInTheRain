﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallInteractionAreaFinishSequence : InteractionAreaFinishSequence
{
    public GameObject wall;
    public float minMainCamHeightToFinish = 38f;

    private Transform mainCam;

    private void Awake()
    {
        mainCam = Game.inst.cams.mainCam.transform;
    }

    public override IEnumerator FinishSequence()
    {
        Game.inst.audio.PlaySound(Game.inst.audio.mystery);
        wall.SetActive(false);
        Game.inst.ui.SetBackButtonActive(false);
        Game.inst.refs.playerCandle.SetCandleActive(false);
        while(mainCam.position.y > 10f)
        {
            yield return null;
        }
        Game.inst.refs.playerCandle.SetCandleActive(true);
    }

    protected override void CheckFinishCondition()
    {
        if (mainCam.transform.position.y > minMainCamHeightToFinish)
            StartCoroutine(Game.inst.OnFinishInteractionArea(interactionArea));
    }
}
