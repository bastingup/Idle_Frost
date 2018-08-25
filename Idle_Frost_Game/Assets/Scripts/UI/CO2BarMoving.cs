using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CO2BarMoving : MonoBehaviour {

	private int co2;

	void Start()
	{
		co2 = GameObject.Find("GameController").GetComponent<EcoStats>().co2Value;
        this.transform.localPosition = new Vector2(-100 + co2, this.transform.localPosition.y);
    }

	void FixedUpdate()
	{
		co2 = GameObject.Find("GameController").GetComponent<EcoStats>().co2Value;
		this.transform.localPosition = new Vector2(-100 + co2, this.transform.localPosition.y);
	}
}
