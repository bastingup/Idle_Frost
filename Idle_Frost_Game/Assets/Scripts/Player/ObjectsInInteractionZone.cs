using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsInInteractionZone : MonoBehaviour {

    public GameObject interactionTarget;
    private List<GameObject> resourceList = new List<GameObject>();
    [SerializeField]
    private List<string> interactionTags;

    void Start ()
    {
        this.GetComponent<CircleCollider2D>();
	}
    void FixedUpdate()
    {
        if (resourceList.Count > 0)
        {
            interactionTarget = FindClosest();
            ActivateInteractionSymbol(interactionTarget);
        } else if (resourceList.Count <= 0)
        {
            interactionTarget = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!resourceList.Contains(other.gameObject) && interactionTags.Contains(other.gameObject.tag))
        {
            resourceList.Add(other.gameObject);
            Debug.Log(other.name);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (resourceList.Contains(other.gameObject))
        {
            other.gameObject.transform.Find("InteractionSymbol").GetComponent<SpriteRenderer>().enabled = false;
            resourceList.Remove(other.gameObject);
        }
    }

    private void ActivateInteractionSymbol(GameObject go)
    {
        go.gameObject.transform.Find("InteractionSymbol").GetComponent<SpriteRenderer>().enabled = true;
    }
    private void DeactivateInteractionSymbol(GameObject go)
    {
        go.gameObject.transform.Find("InteractionSymbol").GetComponent<SpriteRenderer>().enabled = false;
    }

    private GameObject FindClosest()
    {
        float distance = Mathf.Infinity;
        GameObject closest = null;

        foreach (GameObject go in resourceList)
        {
            Vector2 distanceToObject = go.transform.position - this.transform.position;
            float curDistance = distanceToObject.sqrMagnitude;
            if (curDistance < distance)
            {
                distance = curDistance;
                closest = go;
            }
            else
            {
                DeactivateInteractionSymbol(go);
            }
        }
        return closest;
    }
}
