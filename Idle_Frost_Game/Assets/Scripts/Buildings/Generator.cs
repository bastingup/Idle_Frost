using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Generator : MonoBehaviour {

    private bool guiTrue = false, inReach = false;
    private TextMesh text;

    [SerializeField]
    private int energyPerWood,
                energyPerCoal,
                energyPerRenew,
                energyPerUranium,
                energyPerMeteorium;
                
    public int energyInGenerator;

    [SerializeField]
    private float energyDeductionInterval,
                  co2PerWood,
                  co2PerCoal,
                  co2PerRenew,
                  co2PerUranium,
                  pollutionPerWood,
                  pollutionPerCoal,
                  pollutionPerMeteorium;

    [SerializeField]
    private GameObject oven;

    [SerializeField]
    private Button fuelWithWood, fuelWithCoal, fuelWithRenew, fuelWithUranium, fuelWithMeteorium;

    void Start ()
    {
        this.GetComponent<CircleCollider2D>();
        InvokeRepeating("DeductEnergy", 1.0f, energyDeductionInterval);

        text = GetComponentInChildren<TextMesh>();

        fuelWithWood.onClick.AddListener(FuelWithWood);
        fuelWithCoal.onClick.AddListener(FuelWithCoal);
        fuelWithRenew.onClick.AddListener(FuelWithRenewables);
        fuelWithUranium.onClick.AddListener(FuelWithUranium);
        fuelWithMeteorium.onClick.AddListener(FuelWithMeteorium);
	}

    private void FixedUpdate()
    {
        text.text = "There is " + (Mathf.Round(energyInGenerator)).ToString() + " energy left.";
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerCollider")
        {
            guiTrue = !guiTrue;
            inReach = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerCollider")
        {
            guiTrue = !guiTrue;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("PlayerCollider"))
        {
            inReach = true;
            Debug.Log("in reach generaot");
        }
    }

    // Functions to deduct resources from the player inventory and fuel up the generator
    private void FuelWithWood()
    {
        PlayerInventory inventory = GameObject.FindWithTag("Player").GetComponent<PlayerInventory>();
        if (inventory.wood > 0 && inReach)
        {
            // Deduct resource from Player
            inventory.wood -= 1;

            // Change eco stats after burning
            GameObject.FindWithTag("GameController").GetComponent<EcoStats>().co2Value += co2PerWood;

            // Change energy in this generator
            HigherEnergy(energyPerWood);

            // Pollute the air
            PolluteAir(pollutionPerWood);
        }
    }
    private void FuelWithCoal()
    {
        PlayerInventory inventory = GameObject.FindWithTag("Player").GetComponent<PlayerInventory>();
        if (inventory.coal > 0 && inReach)
        {
            // Deduct resource from Player
            inventory.coal -= 1;

            // Change eco stats after burning
            GameObject.FindWithTag("GameController").GetComponent<EcoStats>().co2Value += co2PerCoal;

            // Change energy in this generator
            HigherEnergy(energyPerCoal);

            // Pollute the air
            PolluteAir(pollutionPerCoal);
        }
    }
    private void FuelWithRenewables()
    {

    }
    private void FuelWithUranium()
    {
        PlayerInventory inventory = GameObject.FindWithTag("Player").GetComponent<PlayerInventory>();
        if (inventory.uranium > 0 && inReach)
        {
            // Deduct resource from Player
            inventory.uranium -= 1;

            // Change eco stats after burning
            GameObject.FindWithTag("GameController").GetComponent<EcoStats>().co2Value += co2PerUranium;

            // Change energy in this generator
            HigherEnergy(energyPerUranium);

            // -- No air pollution through uranium --
        }
    }
    private void FuelWithMeteorium()
    {
        PlayerInventory inventory = GameObject.FindWithTag("Player").GetComponent<PlayerInventory>();
        if (inventory.meteorium > 0 && inReach)
        {
            // Deduct resource from Player
            inventory.meteorium -= 1;

            // Change energy in this generator
            HigherEnergy(energyPerMeteorium);

            // Pollute the air
            PolluteAir(pollutionPerMeteorium);
        }
    }

    // Methods for Energy and Air
    private void HigherEnergy(int i)
    {
        energyInGenerator += i;
    }
    private void PolluteAir(float j)
    {
        GameObject.FindWithTag("GameController").GetComponent<EcoStats>().airPollution += j;
    }

    // Function to deduct energy over time
    private void DeductEnergy()
    {
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
