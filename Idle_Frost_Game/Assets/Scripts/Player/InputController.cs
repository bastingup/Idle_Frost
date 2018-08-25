using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

    private float speed = 0.01f;
    private CharacterController character;

	void Start () {
        character = this.GetComponent<CharacterController>();
	}
	
	void Update () {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            this.transform.Translate(touchDeltaPosition.x * speed, touchDeltaPosition.y * speed, 0);
        }
    }
}
