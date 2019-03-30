using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_DeathCanvas : MonoBehaviour {

	void Update ()
    {
        if (Input.touchCount > 0)
        {
            ResetGame();
        }
    }

    void ResetGame()
    {
        // Reset Game state
        FindObjectOfType<SaveGame>().ResetGame();

        // Change UIs again

    }
}
