using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Farm : MonoBehaviour {

    private bool inReach = false, available = true;
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
            inReach = false;
            GameObject.FindObjectOfType<FarmSystem>().GetComponent<FarmSystem>().farmTarget = null;
            GameObject.FindObjectOfType<FarmSystem>().GetComponent<FarmSystem>().ChangeGUI(inReach);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerCollider"))
        {
            inReach = true;
            GameObject.FindObjectOfType<FarmSystem>().GetComponent<FarmSystem>().farmTarget = this;
            GameObject.FindObjectOfType<FarmSystem>().GetComponent<FarmSystem>().ChangeGUI(inReach);
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("PlayerCollider"))
        {
            inReach = true;
        }
    }

    public void Vegan()
    {
        if (available && inReach)
        {
            GenerateCO2(co2PerVegan);
            GenerateRevenue(moneyPerVegan);
            StartCoroutine(CoolDown(timeToWait));
            available = false;
        }
    }
    public void Veggi()
    {
        if (available && inReach)
        {
            available = false;
            GenerateCO2(co2PerVeggi);
            GenerateRevenue(moneyPerVeggi);
            StartCoroutine(CoolDown(timeToWait));
        }
    }
    public void Meat()
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
