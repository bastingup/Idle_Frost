using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum resourceName
{
    tree, coal, renewable, uranium, regularMeteor, rareMeteor
}

public class ResourceAndItemInteraction : MonoBehaviour {

    [SerializeField]
    private bool isItem;

    [SerializeField]
    private GameObject itemToSpawn;

    [SerializeField]
    private int gainResources;

    public resourceName resourceName;

    public void Use()
    {
        // Code for Resources - Always destroy and spawn an item
        if (!isItem)
        {
            SpawnItem(itemToSpawn);
            Destroy(gameObject);
        }
        // Code for items
        else
        {
            switch (resourceName)
            {
                case resourceName.tree:
                    Wood();
                    break;
                case resourceName.coal:
                    Coal();
                    break;
                case resourceName.renewable:
                    Renewable();
                    break;
                case resourceName.uranium:
                    Uranium();
                    break;
                case resourceName.rareMeteor:
                    Meteorium();
                    break;
                case resourceName.regularMeteor:
                    Meteorium();
                    break;
                default:
                    break;
            }
        }
    }
    
    private void SpawnItem(GameObject item)
    {
        Vector2 spawnPosition = new Vector2(Random.Range(this.transform.position.x - 1, this.transform.position.x + 1),
                                            Random.Range(this.transform.position.y - 1, this.transform.position.y + 1));

        Instantiate(item, spawnPosition, new Quaternion());
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
    private void Meteorium()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>().meteorium += gainResources;
        Destroy(gameObject);
    }
}
