using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class SaveGame : MonoBehaviour {

    [SerializeField]
    private Button quitButton;
    [SerializeField]
    private Button loadButton;

    [HideInInspector]
    public DateTime lastTime;
    [HideInInspector]
    public int deltaSeconds;

    // Load the scene and the save file at beginning of session
    private void Start()
    {
        LoadTime();
        quitButton.onClick.AddListener(SaveAndQuit);
        loadButton.onClick.AddListener(LoadGame); 
    }

    // Functions to save and load the current state of the scene
    private void SaveTime()
    {
        PlayerPrefs.SetString("timeControll", System.DateTime.Now.ToString());
        PlayerPrefs.Save();
        //Application.Quit();
    }
    private void LoadTime()
    {
        DateTime last;
        if (PlayerPrefs.GetString("timeControll") != null)
        {
            last = DateTime.FromBinary(Convert.ToInt64(PlayerPrefs.GetString("timeControll")));
        }
        else
        {
            last = DateTime.Now;
        }
        deltaSeconds = (int)Mathf.Round((float)((last - DateTime.Now).TotalSeconds));
    }

    // Do the save functions right before closing the game
    void SaveAndQuit()
    {
        SaveTime();
    }
    void LoadGame()
    {
        LoadTime();
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 1600, 600), deltaSeconds.ToString());
    }
}
