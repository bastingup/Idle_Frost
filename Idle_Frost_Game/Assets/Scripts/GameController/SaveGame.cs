using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class SaveGame : MonoBehaviour {

    // Load the scene and the save file at beginning of session
    private void Awake()
    {
        LoadGameProgress();
        LoadSave();
    }

    // In case there is something super specific that will need to be stored to a seperate save file
    private void WriteSave()
    {
        FileStream file = File.Open(Application.dataPath + "SaveGame.dat", FileMode.Open);
        BinaryFormatter binary = new BinaryFormatter();
        Data data = new Data();

        data.lastTime = GameObject.FindGameObjectWithTag("GameController").GetComponent<TimeController>().lastTime;

        binary.Serialize(file, data);
        file.Close();
    }
    private void LoadSave()
    {
        if (File.Exists(Application.dataPath + "SaveGame.dat"))
        {
            FileStream file = File.Open(Application.dataPath + "SaveGame.dat", FileMode.Open);
            BinaryFormatter binary = new BinaryFormatter();
            Data data = (Data)binary.Deserialize(file);

            GameObject.FindGameObjectWithTag("GameController").GetComponent<TimeController>().lastTime = data.lastTime;
        }
    }

    // Functions to save and load the current state of the scene
    private void SaveGameProgress()
    {
        PlayerPrefs.SetInt("GameScene", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.Save();
    }
    private void LoadGameProgress()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("GameScene"));
    }

    // Do the save functions right before closing the game
    private void OnApplicationQuit()
    {
        SaveGameProgress();
        WriteSave();
    }
}

[Serializable]
class Data
{
    public DateTime lastTime;
}
