using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceAndItemInteraction : MonoBehaviour {

    [SerializeField]
    private bool isItem;
    [SerializeField]
    private GameObject spawn;
	
	void Start ()
    {

	}

    public void Use()
    {
        // Code for Resources
        if (!isItem)
        {
            Instantiate(spawn, this.transform.position, new Quaternion());
            Destroy(gameObject);
        }
        // Code for items
        else
        {
            Destroy(gameObject);
        }
    }
}
