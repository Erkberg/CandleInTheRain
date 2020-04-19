using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Config : ScriptableObject
{
    public int totalInteractions = 3;

    [Header("Movement")]
    public float moveSpeed = 2f;
    public float rotationSpeed = 1f;

    [Header("Candle speed")]
    public float moveUpSpeedRandomizer = 0.5f;
    public float moveUpSpeed = 0.01f;
    public float moveDownSpeed = 0.33f;

    [Header("Candle bounds")]
    public float upperhandUpperY = 0.11f;
    public float upperHandMaxY = 0.15f;
    public float totalUpperHandLowerYBonus = 0.16f;
    public float totalCandleGreenAreaMalus = 80f;

    [Header("Candle particle")]
    public float healthDecreaseMultiplier = 0.5f;
    public float healthIncreaseMultiplier = 0.2f;
    public float totalEmissionBonus = 400f;
    public float totalLifeTimeBonus = 0.2f;

    [Header("Other particles")]
    public float totalRainEmission = 4000f;

    public float GetEmissionBonusPerInteraction()
    {
        return totalEmissionBonus / totalInteractions;
    }

    public float GetLifeTimeBonusPerInteraction()
    {
        return totalLifeTimeBonus / totalInteractions;
    }

    public float GetRainEmissionMalusPerInteraction()
    {
        return totalRainEmission / totalInteractions;
    }

    public float GetUpperHandLowerYBonusPerInteraction()
    {
        return totalUpperHandLowerYBonus / totalInteractions;
    }

    public float GetCandleGreenAreaMalusPerInteraction()
    {
        return totalCandleGreenAreaMalus / totalInteractions;
    }
}
