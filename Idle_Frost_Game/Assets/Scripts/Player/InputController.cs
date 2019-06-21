using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

    public float speed = 0.001f, inputArea = 0.12f;
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
            // Check wether the touch input is given at the center 76% of the screen on the x axis
            if (Input.GetTouch(0).position.x > Screen.width * inputArea &&
                Input.GetTouch(0).position.x < Screen.width * (1f - inputArea))
            {
                // Movement
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    touchInitialPosition = Input.GetTouch(0).position;
                }
                Vector2 touchDeltaPosition = Input.GetTouch(0).position - touchInitialPosition;
                touchDeltaPosition = CheckMaximum(touchDeltaPosition);
                this.transform.Translate(touchDeltaPosition.x * speed, touchDeltaPosition.y * speed, 0);

                // Flip Player
                if (touchDeltaPosition.x > 0)
                {
                    this.transform.localScale = FlipPlayer(false);
                }
                else
                {
                    this.transform.localScale = FlipPlayer(true);
                }
            }
        }
    }

    public void Sprint()
    {

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

    Vector2 FlipPlayer(bool goingLeft)
    {
        if (goingLeft)
        {
            return new Vector2(1.0f, 1.0f);
        }
        else
        {
            return new Vector2(-1.0f, 1.0f);
        }
    }
}
