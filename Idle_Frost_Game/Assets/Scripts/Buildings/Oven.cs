using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : MonoBehaviour {

    public bool ovenActive = false;
    private GameObject player;

    void Start () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player = GameObject.FindWithTag("Player");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (ovenActive)
        {
            InvokeRepeating("HeatingUpPlayer", 1.0f, 1.0f);
        } 
    }

    private void HeatingUpPlayer()
    {
        player.GetComponent<PlayerHealth>().playerTemp += 4;
    }

    public void ChangeOvenStatus()
    {
        ovenActive = !ovenActive;
        this.gameObject.transform.Find("Activated").GetComponent<Light>().enabled = ovenActive;
    }
}
