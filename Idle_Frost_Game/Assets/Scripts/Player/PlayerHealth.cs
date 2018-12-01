using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public int playerHealth = 75;
    public int playerTemp = 50;
    public bool heatingUp = false;

	void Start ()
    {
        InvokeRepeating("CheckTempAndDecreaseHealth", 2.0f, 2.0f);
	}

    void CheckTempAndDecreaseHealth()
    {
        if (!heatingUp)
        {
            if (playerTemp < 15)
            {
                playerHealth -= 2;
            }
            else if (playerTemp > 85)
            {
                playerHealth -= 2;
            }
            playerTemp -= 1;
        }
        if (playerTemp < 0) { playerTemp = 0; }
        if (playerHealth < 0) { playerHealth = 0; }
    }

    void AvoidValuesOutOfRange()
    {
        if (playerTemp < 0) { playerTemp = 0; }
        if (playerTemp > 100) { playerTemp = 100; }
        if (playerHealth < 0) { playerHealth = 0; }
        if (playerHealth > 100) { playerHealth = 100; }
    }
}
