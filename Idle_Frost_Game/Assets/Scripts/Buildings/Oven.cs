using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : MonoBehaviour {

    public bool ovenActive = false;
    [SerializeField]
    private bool heatingPlayerUp = false;
    private GameObject player;

    void Start ()
    {
        this.transform.Find("Area").GetComponent<CircleCollider2D>();
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "PlayerCollider")
        {
            if (ovenActive && !heatingPlayerUp)
            {
                GameObject.FindWithTag("Player").GetComponent<PlayerHealth>().heatingUp = true;
                InvokeRepeating("HeatingUpPlayer", 0.5f, 0.5f);
                heatingPlayerUp = true;
            }
            if (!ovenActive && heatingPlayerUp)
            {
                GameObject.FindWithTag("Player").GetComponent<PlayerHealth>().heatingUp = false;
                heatingPlayerUp = false;
            }  
        }
    }

    private void HeatingUpPlayer()
    { 
        GameObject.FindWithTag("Player").GetComponent<PlayerHealth>().playerTemp += 1;
    }

    public void ChangeOvenStatus()
    {
        ovenActive = !ovenActive;
        this.gameObject.transform.Find("Activated").GetComponent<Light>().enabled = ovenActive;
        this.gameObject.transform.Find("Activated").GetComponent<SpriteRenderer>().enabled = ovenActive;
    }
}
