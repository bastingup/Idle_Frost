using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Generator : MonoBehaviour {

    private bool guiTrue = false;
    [SerializeField]
    private int energyPerWood, energyPerCoal, energyPerRenew, energyPerUranium, energyInGenerator, co2PerWood, co2PerCoal, co2PerRenew, co2PerUranium;
    private GameObject oven;
    [SerializeField]
    private float energyDeductionInterval;
    private CircleCollider2D triggerArea;

    [SerializeField]
    private Button fuelWithWood, fuelWithCoal, fuelWithRenew, fuelWithUranium;

    void Start ()
    {
        triggerArea = this.GetComponent<CircleCollider2D>();
        InvokeRepeating("DeductEnergy", 1.0f, 1.0f);

        fuelWithWood.onClick.AddListener(FuelWithWood);
        fuelWithCoal.onClick.AddListener(FuelWithCoal);
        fuelWithRenew.onClick.AddListener(FuelWithRenewables);
        fuelWithUranium.onClick.AddListener(FuelWithUranium);
	}

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            guiTrue = !guiTrue;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            guiTrue = !guiTrue;
        }
    }

    // Functions to deduct resources from the player inventory and fuel up the generator
    private void FuelWithWood()
    {
        // Deduct resource from Player
        GameObject.FindWithTag("Player").GetComponent<PlayerInventory>().wood -= 1;

        // Change eco stats after burning
        GameObject.FindWithTag("GameController").GetComponent<EcoStats>().co2Value += co2PerWood;

        // Change energy in this generator
        energyInGenerator += energyPerWood;
    }
    private void FuelWithCoal()
    {

    }
    private void FuelWithRenewables()
    {

    }
    private void FuelWithUranium()
    {

    }

    // Function to deduct energy over time
    private void DeductEnergy()
    {
        oven = GameObject.FindWithTag("Oven");
        if (energyInGenerator > 0)
        {
            energyInGenerator -= 1;
            
            if (oven.GetComponent<Oven>().ovenActive == false)
            {
                oven.GetComponent<Oven>().ChangeOvenStatus();
            }
        }
        else if (energyInGenerator == 0)
        {
            if (oven.GetComponent<Oven>().ovenActive == true)
            {
                oven.GetComponent<Oven>().ChangeOvenStatus();
            }
        }
    }
}
