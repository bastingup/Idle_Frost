using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBarRadiation : MonoBehaviour {

    private float value;

    void Start()
    {
        value = GameObject.Find("GameController").GetComponent<EcoStats>().radiation;
        this.transform.localPosition = new Vector2(-100 + value, this.transform.localPosition.y);
    }

    void FixedUpdate()
    {
        value = GameObject.Find("GameController").GetComponent<EcoStats>().radiation;
        this.transform.localPosition = new Vector2(-100 + value, this.transform.localPosition.y);
    }
}
