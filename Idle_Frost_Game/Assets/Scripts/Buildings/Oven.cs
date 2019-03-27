using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : MonoBehaviour {

    public bool ovenActive = false;
    private GameObject player;

    void Start ()
    {
        this.transform.Find("Area").GetComponent<CircleCollider2D>();
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
        GameObject.FindWithTag("Player").GetComponent<PlayerHealth>().playerTemp += 0.04f;
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
