using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour {

    private System.DateTime lastTime;
    private float deltaSeconds;

	void Start () {
        deltaSeconds = (float)(lastTime - System.DateTime.Now).TotalSeconds;
	}
	
	
	void Update () {
		
	}
}
