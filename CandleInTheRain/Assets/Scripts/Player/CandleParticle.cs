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
    public ParticleSystem flame;
    public ParticleSystem smoke;
    public ParticleSystem endParticle;
    public CandleState currentState = CandleState.Safe;

    private float initialEmission;
    private Config config;

    private void Awake()
    {
        config = Game.inst.config;
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
                currentHealth += Time.deltaTime * config.healthIncreaseMultiplier;

            if (currentHealth > 1f)
                currentHealth = 1f;
        }
        else if(currentState == CandleState.Decreasing)
        {
            float bonus = Game.inst.config.healthDecreaseMultiplierBonusPerInteraction * Game.inst.interactionsFinished;

            if (currentHealth > 0f)
                currentHealth -= Time.deltaTime * (config.healthDecreaseMultiplier - bonus);

            if (currentHealth <= 0f)
            {
                currentHealth = 0f;
                SetState(CandleState.Extinct);
                Game.inst.OnCandleExtinct();
            }                
        }
    }

    public void SetState(CandleState state)
    {
        if(currentState == CandleState.Safe && state == CandleState.Decreasing)
            Game.inst.audio.PlaySound(Game.inst.audio.candleDanger);

        if (currentState != CandleState.Extinct)
            currentState = state;
    }
        
    public void LightAnew()
    {
        Game.inst.audio.PlaySound(Game.inst.audio.candleUpgrade);
        currentHealth = 1f;
        currentState = CandleState.Safe;
    }

    public void UpgradeCandle()
    {        
        initialEmission += config.GetEmissionBonusPerInteraction();
        flame.Emit((int)(initialEmission * 4));
        flame.startLifetime += config.GetLifeTimeBonusPerInteraction();
    }

    public void EmitSmoke()
    {
        smoke.Emit(20);
    }

    public void ActivateEndParticle()
    {
        endParticle.gameObject.SetActive(true);
    }

    public void EndParticleBurst()
    {
        endParticle.Emit(2000);
    }
}
