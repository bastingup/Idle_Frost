using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : Heatsource {

    void Start ()
    {
        this.transform.Find("Area").GetComponent<CircleCollider2D>();
        playerHeatUp = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>().tempReduction * 1.5f;
	}

    public void NuclearExplosion()
    {
        // TODO create nuclear explosion
    }
}
