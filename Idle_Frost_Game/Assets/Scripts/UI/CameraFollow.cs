using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    private GameObject followObject;

	void Update ()
    {
        this.transform.position = new Vector3 (followObject.transform.position.x,
                                               followObject.transform.position.y,
                                               followObject.transform.position.z - 30.0f);
	}
}
