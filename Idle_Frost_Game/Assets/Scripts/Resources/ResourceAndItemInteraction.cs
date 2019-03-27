using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceAndItemInteraction : MonoBehaviour {

    [SerializeField]
    private bool isItem;
    [SerializeField]
    private GameObject spawn;
    [SerializeField]
    private string type;
    [SerializeField]
    private int gainResources;

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
            switch (type)
            {
                case "Wood":
                    Wood();
                    break;
                case "Coal":
                    Coal();
                    break;
                case "Renewable":
                    Renewable();
                    break;
                case "Uranium":
                    Uranium();
                    break;
                default:
                    break;
            }
        }
    }

    private void Wood()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>().wood += gainResources;
        Destroy(gameObject);
    }
    private void Coal()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>().coal += gainResources;
        Destroy(gameObject);
    }
    private void Renewable()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>().renewables += gainResources;
    }
    private void Uranium()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>().uranium += gainResources;
        Destroy(gameObject);
    }
}
