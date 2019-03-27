using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBarHealth : MonoBehaviour {

    private float value;

    void Start ()
    {
        value = GameObject.Find("Player").GetComponent<PlayerHealth>().playerHealth;
        this.transform.localPosition = new Vector2(-100 + value, this.transform.localPosition.y);
    }
	
	
	void Update ()
    {
        value = GameObject.Find("Player").GetComponent<PlayerHealth>().playerHealth;
        this.transform.localPosition = new Vector2(-100 + value, this.transform.localPosition.y);
    }
}
