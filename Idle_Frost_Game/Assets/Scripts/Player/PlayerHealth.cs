using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public int playerHealth = 100;
    public int playerTemp = 70;
    public bool heatingUp = false;

	void Start ()
    {
        InvokeRepeating("CheckTempAndDecreaseHealth", 0.5f, 0.5f);
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
        AvoidValuesOutOfRange();
    }

    void AvoidValuesOutOfRange()
    {
        if (playerTemp < 0) { playerTemp = 0; }
        if (playerTemp > 100) { playerTemp = 100; }
        if (playerHealth < 0) { playerHealth = 0; }
        if (playerHealth > 100) { playerHealth = 100; }
    }
}
