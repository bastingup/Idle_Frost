using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heatsource : MonoBehaviour
{
    public float playerHeatUp;
    public bool active = false;

    public void ChangeHeatsourceStatus()
    {
        active = !active;
        this.gameObject.transform.Find("Activated").gameObject.SetActive(active);
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "PlayerCollider" && active)
        {
            HeatingUpPlayer();
        }
    }

    private void HeatingUpPlayer()
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerHealth>().playerTemp += playerHeatUp;
    }
}
