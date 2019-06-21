using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Farm : MonoBehaviour {

    [SerializeField]
    private Button veganButton, veggiButton, meatButton;
    private bool guiTrue = false, inReach = false, available = true;
    private TextMesh text;

    [SerializeField]
    private float co2PerVegan,
                  co2PerVeggi,
                  co2PerMeat;
                  
    public float cooldownTime;

    [SerializeField]
    private int moneyPerVegan,
                moneyPerVeggi,
                moneyPerMeat,
                timeToWait;

	void Start ()
    {
        this.GetComponent<CircleCollider2D>();
        text = GetComponentInChildren<TextMesh>();

        veganButton.onClick.AddListener(Vegan);
        veggiButton.onClick.AddListener(Veggi);
        meatButton.onClick.AddListener(Meat);
	}

    private void FixedUpdate()
    {
        if (!available)
        {
            cooldownTime -= Time.deltaTime;
            text.text = "Available in " + (Mathf.Round(cooldownTime)).ToString() + " seconds.";
            if (cooldownTime <= 0)
            {
                available = true;
            }
        }
        else
        {
            text.text = "Ready for use!";
        } 
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerCollider"))
        {
            guiTrue = !guiTrue;
            inReach = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerCollider"))
        {
            guiTrue = !guiTrue;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("PlayerCollider"))
        {
            inReach = true;
        }
    }

    void Vegan()
    {
        if (available && inReach)
        {
            GenerateCO2(co2PerVegan);
            GenerateRevenue(moneyPerVegan);
            StartCoroutine(CoolDown(timeToWait));
            available = false;
        }
    }
    void Veggi()
    {
        if (available && inReach)
        {
            available = false;
            GenerateCO2(co2PerVeggi);
            GenerateRevenue(moneyPerVeggi);
            StartCoroutine(CoolDown(timeToWait));
        }
    }
    void Meat()
    {
        if (available && inReach)
        {
            available = false;
            GenerateCO2(co2PerMeat);
            GenerateRevenue(moneyPerMeat);
            StartCoroutine(CoolDown(timeToWait));
        }
    }

    void GenerateRevenue(int value)
    {
        FindObjectOfType<PlayerInventory>().money += value;
    }
    void GenerateCO2(float value)
    {
        FindObjectOfType<EcoStats>().co2Value += value;
    }

    void AssignCooldonw()
    {
        cooldownTime = timeToWait;
    }

    public IEnumerator CoolDown(int wait)
    {
        cooldownTime = wait;
        yield return new WaitForSeconds(wait);
        available = true;
    }
}
