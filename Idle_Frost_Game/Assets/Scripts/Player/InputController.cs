using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

    private float speed = 0.001f;
    private CharacterController character;
    private Vector2 touchInitialPosition;

    void Start ()
    {
        character = this.GetComponent<CharacterController>();
	}
	
	void Update ()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                touchInitialPosition = Input.GetTouch(0).position;
            }
            Vector2 touchDeltaPosition = Input.GetTouch(0).position - touchInitialPosition;
            touchDeltaPosition = CheckMaximum(touchDeltaPosition);

            this.transform.Translate(touchDeltaPosition.x * speed, touchDeltaPosition.y * speed, 0);
        }
    }

    Vector2 CheckMaximum(Vector2 vec)
    {
        float limit = 100;

        if (vec.x > limit)
        {
            vec.x = limit;
        } else if (vec.x < -limit)
        {
            vec.x = -limit;
        }

        if (vec.y > limit)
        {
            vec.y = limit;
        }
        else if (vec.y < -limit)
        {
            vec.y = -limit;
        }
        return vec;
    }
}
