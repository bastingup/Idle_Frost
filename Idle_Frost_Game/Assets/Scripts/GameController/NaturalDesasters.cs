using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaturalDesasters : MonoBehaviour {

    [SerializeField]
    private EcoStats ecoStats;
    [SerializeField]
    private float divisionValue, timeInterval, destructionChance;

    // DEBUG
    public bool nuclear = false;

	void Start ()
    {
        InvokeRepeating("ChanceForDesaster", timeInterval, timeInterval);
	}
	
	void ChanceForDesaster()
    {
        float randomValue = Random.Range(0, 100);
        float desasterValue = ecoStats.globalTempValue / divisionValue;

        if (randomValue <= desasterValue)
        {
            float radiationValue = Random.Range(0, 100),
                  floodValue = Random.Range(0, 100),
                  fireValue = Random.Range(0, 100);

            if (radiationValue <= randomValue)
            {
                NuclearWinter();
            }
            else
            {
                if (floodValue >= fireValue)
                {
                    Flood();
                }
                else
                {
                    Fire();
                }
            }   
        }
    }

    private void Update()
    {
        if (nuclear)
        {
            NuclearWinter();
            nuclear = false;
        }
    }

    void Flood()
    {
        GameObject[] resources = GameObject.FindGameObjectsWithTag("Resource");
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");

        foreach (GameObject r in resources)
        {
            if (Random.Range(0, 100) <= destructionChance)
            {
                Destroy(r);
            }
        }

        foreach (GameObject i in items)
        {
            Destroy(i);
        }
    }
    void Fire()
    {
        GameObject[] resources = GameObject.FindGameObjectsWithTag("Resource");

        foreach (GameObject r in resources)
        {
            if (Random.Range(0, 100) <= destructionChance &&
                r.GetComponent<ResourceAndItemInteraction>().resourceName == resourceName.tree)
            {
                Destroy(r);
            }
        }
    }
    void NuclearWinter()
    {
        FindObjectOfType<Oven>().NuclearExplosion();
        // TODO put in update, not lerping
        ecoStats.airPollution = Mathf.Lerp(ecoStats.airPollution, ecoStats.airPollution + 40, 5.0f);
        ecoStats.globalTempValue = Mathf.Lerp(ecoStats.globalTempValue, ecoStats.globalTempValue - 40, 5.0f);
        ecoStats.radiation = Mathf.Lerp(ecoStats.radiation, ecoStats.radiation + 40, 5.0f);
    }
}
