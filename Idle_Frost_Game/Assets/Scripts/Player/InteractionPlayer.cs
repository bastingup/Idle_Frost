using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionPlayer : MonoBehaviour {

    [SerializeField]
    private Button interactButton;
    [SerializeField]
    private Button infoButton;
    private GameObject interactionTarget;



	void Start () {
        // Add the button that is causing the function "Interaction" and others
        interactButton.onClick.AddListener(Interaction);
        infoButton.onClick.AddListener(Information);
	}

    void Interaction()
    {
        if (GameObject.Find("PlayerInteractionZone").GetComponent<ObjectsInInteractionZone>().interactionTarget != null)
        {
            GameObject target = GameObject.Find("PlayerInteractionZone").GetComponent<ObjectsInInteractionZone>().interactionTarget;
            
            if (target.tag == "Resource" || target.tag == "Item")
            {
                // Interaction target if resource or item
                GameObject.Find("PlayerInteractionZone").GetComponent<ObjectsInInteractionZone>().interactionTarget.GetComponent<ResourceAndItemInteraction>().Use();
            }
        }
    }

    void Information()
    {

    }
}
