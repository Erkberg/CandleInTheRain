using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCandle : MonoBehaviour
{
    public bool candleActive = true;
    public Transform upperHand;
    public CandleParticle candleParticle;
    public Transform lowerHandBone;

    public float upperHandLowerY = -0.1f;
    public float burnJumpOffset = 0.2f;

    private Config config;

    private void Awake()
    {
        config = Game.inst.config;
    }

    private void Update()
    {
        if (candleActive)
            MoveHand();
    }

    private void MoveHand()
    {
        if(Input.GetMouseButton(1))
        {
            upperHand.position -= new Vector3(0f, config.moveDownSpeed * Time.deltaTime, 0f);
        }
        else
        {
            upperHand.position += new Vector3(0f, config.moveUpSpeed * Random.Range(1f - config.moveUpSpeedRandomizer, 1f + config.moveUpSpeedRandomizer) * Time.deltaTime, 0f);
        }

        if(upperHand.localPosition.y > config.upperhandUpperY)
        {
            candleParticle.SetState(CandleParticle.CandleState.Decreasing);
        }
        else if(upperHand.localPosition.y < upperHandLowerY)
        {
            upperHand.position += new Vector3(0f, burnJumpOffset, 0f);
        }
        else
        {
            candleParticle.SetState(CandleParticle.CandleState.Safe);
        }

        if (upperHand.localPosition.y > config.upperHandMaxY)
            upperHand.localPosition = new Vector3(upperHand.localPosition.x, config.upperHandMaxY, upperHand.localPosition.z);
    }

    public void UpgradeCandle()
    {
        candleParticle.UpgradeCandle();
        upperHandLowerY += config.GetUpperHandLowerYBonusPerInteraction();
        burnJumpOffset -= config.GetUpperHandLowerYBonusPerInteraction();
        ResetCandle();
    }

    public void ResetCandle()
    {
        upperHand.localPosition = new Vector3(upperHand.localPosition.x, 0f, upperHand.localPosition.z);
        candleParticle.LightAnew();
    }

    public void ParentToHand()
    {
        transform.parent = lowerHandBone;
    }
}
