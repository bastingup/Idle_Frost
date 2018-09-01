using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceAndItemInteraction : MonoBehaviour {

    [SerializeField]
    private bool isItem;
    [SerializeField]
    private bool isBuilding;
    [SerializeField]
    private GameObject spawn;
	
	void Start () {
		
	}
	
	void Update () {
		
	}

    public void Use()
    {
        if (!isItem)
        {
            Instantiate(spawn, this.transform.position, new Quaternion());
            Destroy(gameObject);
        }
        else if (isBuilding)
        {

        }
        else
        {
            Destroy(gameObject);
        }
    }
}
