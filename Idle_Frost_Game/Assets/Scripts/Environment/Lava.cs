using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour {


	void Start () {
        GetComponent<Collider2D>();
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerCollider"))
        {
            FindObjectOfType<PlayerHealth>().playerHealth = 0;
        }
    }
}
