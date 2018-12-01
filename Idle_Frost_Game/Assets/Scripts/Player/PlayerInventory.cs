using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {
 
    public int coal;
    public int seeds;
    public int wood;
    public int money;

    void Start () {
		
	}
	
	
	void Update () {
		
	}

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 16;
        GUI.Label(new Rect(Screen.width * 0.95f, Screen.height * 0.5f, 200, 200), money.ToString(), style);
    }
}
