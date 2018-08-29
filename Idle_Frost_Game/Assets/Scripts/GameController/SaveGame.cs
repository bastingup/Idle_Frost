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

    // In case there is something super specific that will need to be stored to a seperate save file
    private void WriteSave()
    {
        FileStream file = File.Open(Application.dataPath + "/SaveGame.dat", FileMode.Open);
        BinaryFormatter binary = new BinaryFormatter();
        Data data = new Data();
        data.lastTime = this.lastTime;

        binary.Serialize(file, data);
        file.Close();
    }
    private void LoadSave()
    {
        if (File.Exists(Application.dataPath + "/SaveGame.dat"))
        {
            FileStream file = File.Open(Application.dataPath + "/SaveGame.dat", FileMode.Open);
            BinaryFormatter binary = new BinaryFormatter();
            Data data = (Data)binary.Deserialize(file);

            lastTime = data.lastTime;
        }
    }

    // Functions to save and load the current state of the scene
    private void SaveTime()
    {
        PlayerPrefs.SetFloat("timeControll", lastTime.ToFileTime());
        PlayerPrefs.Save();
    }
    private void LoadTime()
    {
        float last;
        if (PlayerPrefs.GetString("timeControll") != null)
        {
            last = PlayerPrefs.GetFloat("timeControll");
        }
        else
        {
            last = DateTime.Now.ToFileTime();
        }
        deltaSeconds = (int)(Mathf.Round(last - DateTime.Now.ToFileTime()));
    }

    // Do the save functions right before closing the game
    void SaveAndQuit()
    {
        SaveTime();
        Application.Quit();
    }
    void LoadGame()
    {
        LoadTime();
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 800, 250), deltaSeconds.ToString());
    }
}

[Serializable]
class Data
{
    public DateTime lastTime;
}
