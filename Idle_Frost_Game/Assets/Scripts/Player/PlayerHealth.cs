using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public float playerHealth = 100;
    public float playerTemp = 70;

    private void Update()
    {
        CheckTempAndDecreaseHealth();
        AvoidValuesOutOfRange();
    }

    void CheckTempAndDecreaseHealth()
    {
        if (playerTemp < 10)
        {
            playerHealth -= 0.001f;
        }
        else if (playerTemp > 90)
        {
            playerHealth -= 0.001f;
        }
        playerTemp -= 0.01f;
    }

    void AvoidValuesOutOfRange()
    {
        if (playerTemp < 0) { playerTemp = 0; }
        if (playerTemp > 100) { playerTemp = 100; }
        if (playerHealth < 0) { playerHealth = 0; }
        if (playerHealth > 100) { playerHealth = 100; }
    }
}
