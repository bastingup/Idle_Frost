using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBarCO2 : MonoBehaviour {

	private float value;

	void Start()
	{
        value = GameObject.Find("GameController").GetComponent<EcoStats>().co2Value;
        this.transform.localPosition = new Vector2(-100 + value, this.transform.localPosition.y);
    }

	void FixedUpdate()
	{
        value = GameObject.Find("GameController").GetComponent<EcoStats>().co2Value;
        this.transform.localPosition = new Vector2(-100 + value, this.transform.localPosition.y);
	}
}
