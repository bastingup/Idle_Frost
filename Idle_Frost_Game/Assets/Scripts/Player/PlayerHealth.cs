using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public float playerHealth = 100f, playerTemp = 70f, healthReduction = 0.001f, tempReduction = 0.02f;

    private void Update()
    {
        CheckTempAndDecreaseHealth();
        AvoidValuesOutOfRange();
    }

    void CheckTempAndDecreaseHealth()
    {
        if (playerTemp < 10)
        {
            playerHealth -= healthReduction;
        }
        else if (playerTemp > 90)
        {
            playerHealth -= healthReduction;
        }
        playerTemp -= tempReduction;
    }

    void AvoidValuesOutOfRange()
    {
        if (playerTemp < 0) { playerTemp = 0; }
        if (playerTemp > 100) { playerTemp = 100; }
        if (playerHealth < 0) { Death(); }
        if (playerHealth > 100) { playerHealth = 100; }
    }

    void Death()
    {
        FindObjectOfType<Death>().TriggerDeath();
    }
}
