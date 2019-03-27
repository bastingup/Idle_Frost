using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcoStats : MonoBehaviour
{
    [SerializeField]
    private PlayerHealth playerHealth;
    public float co2Value = 60, globalTempValue = 40, airPollution = 20, radiation = 5, changeRate;

    private void Update()
    {
        CheckOutOfBounds();
        UpdateGlobalTemp();
        UpdatePlayerHealth();
        RadiateEnvironment();
    }

    void UpdateGlobalTemp()
    {
        globalTempValue += ((co2Value - 50) * changeRate);
    }

    void UpdatePlayerHealth()
    {
        playerHealth.playerHealth -= (airPollution * changeRate);
    }

    void UpdatePlayerTemp()
    {
        if (playerHealth.playerTemp - 50 < 0)
        {
            playerHealth.playerTemp += ((globalTempValue - 50) * changeRate);
        }
        else
        {
            playerHealth.playerTemp += ((globalTempValue - 50) *(-changeRate));
        }
    }
    
    void CheckOutOfBounds()
    {
        if (co2Value < 0) { co2Value = 0; } else if (co2Value > 100) { co2Value = 100; }
        if (globalTempValue < 0) { globalTempValue = 0;  } else if (globalTempValue > 100) { globalTempValue = 100; }
        if (airPollution < 0) { airPollution = 0; } else if (airPollution > 100) { airPollution = 100; }
        if (radiation < 0) { radiation = 0; } else if (radiation > 100) { radiation = 100; }
    }

    void RadiateEnvironment()
    {
        // TODO Radiate Environment
    }
}