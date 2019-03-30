using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Death : MonoBehaviour {

    [SerializeField]
    Canvas gameplayCanvas, deathCanas;

	public void TriggerDeath()
    {
        // Destroy player object
        Destroy(GameObject.Find("Player").gameObject);

        // Change canvases
        gameplayCanvas.gameObject.SetActive(false);
        deathCanas.gameObject.SetActive(true);
    }

    public void SwitchUiToGameplay()
    {
        // Change canvases
        gameplayCanvas.gameObject.SetActive(true);
        deathCanas.gameObject.SetActive(false);
    }
}
