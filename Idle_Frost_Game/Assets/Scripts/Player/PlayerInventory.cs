using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {
 
    public int coal, seeds, renewables, uranium, meteorium, wood, money;

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 22;
        GUI.Label(new Rect(Screen.width * 0.9f, Screen.height * 0.5f, 200, 200), "Money: " + money.ToString(), style);
        GUI.Label(new Rect(Screen.width * 0.9f, Screen.height * 0.55f, 200, 200), "Wood: " + wood.ToString(), style);
    }
}