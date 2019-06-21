using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionPlayer : MonoBehaviour {

    [SerializeField]
    private Button interactButton, infoButton;
    private GameObject interactionTarget;
    private InputController input;

	void Start ()
    {
        interactButton.onClick.AddListener(Interaction);
        infoButton.onClick.AddListener(Information);
        // Sprint button is working with UI manager

        input = this.GetComponent<InputController>();
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
