using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : MonoBehaviour {

    private float playerHeatUp;
    public bool ovenActive = false;
    private GameObject player;

    void Start ()
    {
        this.transform.Find("Area").GetComponent<CircleCollider2D>();
        playerHeatUp = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>().tempReduction * 2;
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "PlayerCollider" && ovenActive)
        {
            Debug.Log("Player heatin up!");
            HeatingUpPlayer();
        }
    }

    private void HeatingUpPlayer()
    { 
        GameObject.FindWithTag("Player").GetComponent<PlayerHealth>().playerTemp += playerHeatUp;
    }

    public void ChangeOvenStatus()
    {
        ovenActive = !ovenActive;
        this.gameObject.transform.Find("Activated").gameObject.SetActive(ovenActive);
    }

    public void NuclearExplosion()
    {

    }
}
