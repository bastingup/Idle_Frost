using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : Heatsource
{
    void Start()
    {
        Invoke("EndFire", 30.0f);
    }

    private void EndFire()
    {
        ChangeHeatsourceStatus();
    }
}
