using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCandle : MonoBehaviour
{
    public bool candleActive = true;
    public Transform upperHand;
    public CandleParticle candleParticle;

    [Header("MoveSpeed")]
    public float moveUpSpeedRandomizer = 0.5f;
    public float moveUpSpeed = 0.01f;
    public float moveDownSpeed = 0.1f;

    [Header("Bounds")]
    public float upperhandUpperY = 0.11f;
    public float upperHandLowerY = -0.1f;
    public float upperHandMaxY = 0.15f;
    public float burnJumpOffset = 0.2f;

    private void Update()
    {
        if (candleActive)
            MoveHand();
    }

    private void MoveHand()
    {
        if(Input.GetMouseButton(1))
        {
            upperHand.position -= new Vector3(0f, moveDownSpeed * Time.deltaTime, 0f);
        }
        else
        {
            upperHand.position += new Vector3(0f, moveUpSpeed * Random.Range(1f - moveUpSpeedRandomizer, 1f + moveUpSpeedRandomizer) * Time.deltaTime, 0f);
        }

        if(upperHand.localPosition.y > upperhandUpperY)
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

        if (upperHand.localPosition.y > upperHandMaxY)
            upperHand.localPosition = new Vector3(upperHand.localPosition.x, upperHandMaxY, upperHand.localPosition.z);
    }

    public void UpgradeCandle()
    {
        candleParticle.UpgradeCandle();
        upperHandLowerY += 0.04f;
        burnJumpOffset -= 0.04f;
        candleParticle.LightAnew();
    }
}
