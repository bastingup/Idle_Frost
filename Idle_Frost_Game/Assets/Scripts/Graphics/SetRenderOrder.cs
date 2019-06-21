using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRenderOrder : MonoBehaviour {

    private MeshRenderer text;

    void Start ()
    {
        text = GetComponent<MeshRenderer>();
        text.sortingOrder = (int)Mathf.Round(Mathf.Infinity);
    }
}
