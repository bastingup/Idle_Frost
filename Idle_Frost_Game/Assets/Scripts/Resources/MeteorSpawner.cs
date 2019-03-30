using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour {

    [SerializeField]
    private float minX, maxX, minY, maxY, rareMeteorChance, repeatTime;
    [SerializeField]
    private GameObject regularMeteor, rareMeteor;

	void Start ()
    {
        InvokeRepeating("SpawnMeteor", repeatTime, repeatTime);
	}
	
    void SpawnMeteor()
    {
        Vector2 coordinates = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

        if (rareMeteorChance < Random.Range(0, 100))
        {
            Instantiate(rareMeteor, coordinates, new Quaternion(0, 0, 0, 1));
        }
    }
}
