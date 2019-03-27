using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBarGlobalTemp : MonoBehaviour {

    private float value;

    void Start()
    {
        value = GameObject.Find("GameController").GetComponent<EcoStats>().globalTempValue;
        this.transform.localPosition = new Vector2(-100 + value, this.transform.localPosition.y);
    }

    void FixedUpdate()
    {
        value = GameObject.Find("GameController").GetComponent<EcoStats>().globalTempValue;
        this.transform.localPosition = new Vector2(-100 + value, this.transform.localPosition.y);
    }
}
