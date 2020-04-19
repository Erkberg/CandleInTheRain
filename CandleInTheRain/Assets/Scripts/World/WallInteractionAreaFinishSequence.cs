using System.Collections;
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
        wall.SetActive(false);
        Game.inst.ui.SetBackButtonActive(false);
        Game.inst.refs.playerCandle.candleActive = false;
        while(mainCam.position.y > 10f)
        {
            yield return null;
        }
        Game.inst.refs.playerCandle.candleActive = true;
    }

    protected override void CheckFinishCondition()
    {
        if (mainCam.transform.position.y > minMainCamHeightToFinish)
            StartCoroutine(Game.inst.OnFinishInteractionArea(interactionArea));
    }
}
