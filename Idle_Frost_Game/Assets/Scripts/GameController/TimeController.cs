using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour {

    public System.DateTime lastTime;
    public int deltaSeconds;

	void Start () {
        deltaSeconds = (int)(Mathf.Round((float)(lastTime - System.DateTime.Now).TotalSeconds));
	}

    private void OnApplicationQuit()
    {
       
    }
}
