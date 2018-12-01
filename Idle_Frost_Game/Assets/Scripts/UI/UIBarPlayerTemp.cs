using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBarPlayerTemp : MonoBehaviour {

    private int value;

    void Start () {
        value = GameObject.Find("Player").GetComponent<PlayerHealth>().playerTemp;
        this.transform.localPosition = new Vector2(-100 + value, this.transform.localPosition.y);
    }
	
	void Update () {
        value = GameObject.Find("Player").GetComponent<PlayerHealth>().playerTemp;
        this.transform.localPosition = new Vector2(-100 + value, this.transform.localPosition.y);
    }
}
