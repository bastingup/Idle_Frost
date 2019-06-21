using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderOrder : MonoBehaviour {

	private SpriteRenderer render;

	void Start () {
		render = GetComponent<SpriteRenderer>();
        render.sortingOrder = -(int)(this.transform.position.y * 100);
    }

	void LateUpdate()
    {
        render.sortingOrder = -(int)(this.transform.position.y * 100);
    }
}
