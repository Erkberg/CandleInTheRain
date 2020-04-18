using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleParticle : MonoBehaviour
{
    public enum CandleState
    {
        Safe,
        Decreasing,
        Extinct
    }

    public float currentHealth = 1f;
    public float healthDecreaseMultiplier = 0.5f;
    public float healthIncreaseMultiplier = 0.2f;
    public ParticleSystem flame;
    public CandleState currentState = CandleState.Safe;

    private float initialEmission;

    private void Awake()
    {
        initialEmission = flame.emissionRate;
    }

    private void Update()
    {
        CheckState();

        flame.emissionRate = initialEmission * currentHealth;
    }

    private void CheckState()
    {
        if(currentState == CandleState.Safe)
        {
            if (currentHealth < 1f)
                currentHealth += Time.deltaTime * healthIncreaseMultiplier;

            if (currentHealth > 1f)
                currentHealth = 1f;
        }
        else if(currentState == CandleState.Decreasing)
        {
            if (currentHealth > 0f)
                currentHealth -= Time.deltaTime * healthDecreaseMultiplier;

            if (currentHealth <= 0f)
            {
                currentHealth = 0f;
                SetState(CandleState.Extinct);
            }                
        }
    }

    public void SetState(CandleState state)
    {
        if(currentState != CandleState.Extinct)
            currentState = state;
    }
        
    public void LightAnew()
    {
        currentHealth = 1f;
        currentState = CandleState.Safe;
    }

    public void UpgradeCandle()
    {
        initialEmission += 100f;
        flame.startLifetime += 0.05f;
    }
}
