using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class SaveGame : MonoBehaviour {

    [SerializeField]
    private Button saveButton;
    [SerializeField]
    private Button loadButton;

    private DateTime lastTime;
    public int deltaSeconds;

    // Load the scene and the save file at beginning of session
    private void Start()
    {
        LoadGame();
        saveButton.onClick.AddListener(Save);
        loadButton.onClick.AddListener(LoadGame); 
    }

    // Save and load functions containing the save/load functions for different aspects
    void Save()
    {
        SaveTime();
        SaveItems();
        SaveGlobalStats();
        SavePlayerprefs();
        QuitGame();
    }
    void LoadGame()
    {
        LoadTime();
        LoadItems();
        LoadGlobalStats();
        LoadPlayerStats();
    }

    // Functions to save and load the current and last time played
    private void SaveTime()
    {
        PlayerPrefs.SetString("timeControll", DateTime.Now.ToString());
        Debug.Log("Time saved");
    }
    private void LoadTime()
    {
        if (PlayerPrefs.GetString("timeControll") != "")
        {
            DateTime.TryParse(PlayerPrefs.GetString("timeControll"), out lastTime);
        }
        else
        {
            lastTime = DateTime.Now;
        }
        Debug.Log(lastTime.ToString());

        deltaSeconds = (int)Mathf.Round((float)((lastTime - DateTime.Now).TotalSeconds));
    }

    private void SaveItems()
    {
        GameObject player = GameObject.Find("Player");
        PlayerPrefs.SetString("coal", player.GetComponent<PlayerInventory>().coal.ToString());
        PlayerPrefs.SetString("seeds", player.GetComponent<PlayerInventory>().seeds.ToString());
        PlayerPrefs.SetString("wood", player.GetComponent<PlayerInventory>().wood.ToString());
    }
    private void LoadItems()
    {
        GameObject player = GameObject.Find("Player");
        int.TryParse(PlayerPrefs.GetString(("coal")), out player.GetComponent<PlayerInventory>().coal);
        int.TryParse(PlayerPrefs.GetString(("seeds")), out player.GetComponent<PlayerInventory>().seeds);
        int.TryParse(PlayerPrefs.GetString(("wood")), out player.GetComponent<PlayerInventory>().wood);
    }
    
    private void SaveGlobalStats()
    {
        EcoStats ecoStats = GameObject.FindWithTag("GameController").GetComponent<EcoStats>();
        PlayerPrefs.SetString("co2", ecoStats.co2Value.ToString());
        PlayerPrefs.SetString("globalTemp", ecoStats.globalTempValue.ToString());
    }
    private void LoadGlobalStats()
    {
        if (PlayerPrefs.GetString("playerHealth") != "")
        {
            EcoStats ecoStats = GameObject.FindWithTag("GameController").GetComponent<EcoStats>();
            int.TryParse(PlayerPrefs.GetString(("co2")), out ecoStats.co2Value);
            int.TryParse(PlayerPrefs.GetString(("globalTemp")), out ecoStats.globalTempValue);
        }
    }

    private void SavePlayerStats()
    {
        GameObject player = GameObject.Find("Player");
        PlayerPrefs.SetString("playerHealth", player.GetComponent<PlayerHealth>().playerHealth.ToString());
        PlayerPrefs.SetString("playerTemp", player.GetComponent<PlayerHealth>().playerTemp.ToString());
    }
    private void LoadPlayerStats()
    {
        // Check whether there is player health. If it is, load stuff. If it is not, don't load. Loading without saves will result in null/0 (?)
        if (PlayerPrefs.GetString("playerHealth") != "")
        {
            GameObject player = GameObject.Find("Player");
            int.TryParse(PlayerPrefs.GetString(("playerHealth")), out player.GetComponent<PlayerHealth>().playerHealth);
            int.TryParse(PlayerPrefs.GetString(("playerTemp")), out player.GetComponent<PlayerHealth>().playerTemp);
        }
    }

    private void SavePlayerprefs()
    {
        PlayerPrefs.Save();
    }
    private void QuitGame()
    {
        Application.Quit();
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 16;
        GUI.Label(new Rect(Screen.width * 0.05f, Screen.height * 0.05f, 1600, 600), DateTime.Now.ToString(), style);
        GUI.Label(new Rect(Screen.width * 0.05f, Screen.height * 0.1f, 1600, 600), lastTime.ToString(), style);
    }
}
