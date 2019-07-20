using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergySystem : MonoBehaviour
{

    [SerializeField]
    private Button woodButton, coalButton, renewButton, uraniumButton, meteorButton;
    public Generator generatorTaget;

    void Start()
    {
        woodButton.onClick.AddListener(Wood);
        coalButton.onClick.AddListener(Coal);
        renewButton.onClick.AddListener(Renew);
        uraniumButton.onClick.AddListener(Uranium);
        meteorButton.onClick.AddListener(Meteor);
    }

    public void ChangeGUI(bool b)
    {
        woodButton.gameObject.SetActive(b);
        coalButton.gameObject.SetActive(b);
        renewButton.gameObject.SetActive(b);
        uraniumButton.gameObject.SetActive(b);
        meteorButton.gameObject.SetActive(b);
    }

    void Wood()
    {
        if (generatorTaget != null)
        {
            generatorTaget.GetComponent<Generator>().FuelWithWood();
        }
    }

    void Coal()
    {
        if (generatorTaget != null)
        {
            generatorTaget.GetComponent<Generator>().FuelWithCoal();
        }
    }

    void Renew()
    {
        if (generatorTaget != null)
        {
            generatorTaget.GetComponent<Generator>().FuelWithRenewables();
        }
    }

    void Uranium()
    {
        if (generatorTaget != null)
        {
            generatorTaget.GetComponent<Generator>().FuelWithUranium();
        }
    }

    void Meteor()
    {
        if (generatorTaget != null)
        {
            generatorTaget.GetComponent<Generator>().FuelWithMeteorium();
        }
    }
}
