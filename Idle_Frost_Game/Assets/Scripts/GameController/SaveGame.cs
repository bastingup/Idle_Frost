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
        //SaveResources();
        QuitGame();
    }
    void LoadGame()
    {
        LoadTime();
        LoadItems();
        LoadGlobalStats();
        LoadPlayerStats();
        //LoadResources();
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
        PlayerPrefs.SetString("money", player.GetComponent<PlayerInventory>().money.ToString());
        PlayerPrefs.SetString("uranium", player.GetComponent<PlayerInventory>().uranium.ToString());
        PlayerPrefs.SetString("meteorium", player.GetComponent<PlayerInventory>().meteorium.ToString());
    }
    private void LoadItems()
    {
        GameObject player = GameObject.Find("Player");
        int.TryParse(PlayerPrefs.GetString(("coal")), out player.GetComponent<PlayerInventory>().coal);
        int.TryParse(PlayerPrefs.GetString(("seeds")), out player.GetComponent<PlayerInventory>().seeds);
        int.TryParse(PlayerPrefs.GetString(("wood")), out player.GetComponent<PlayerInventory>().wood);
        int.TryParse(PlayerPrefs.GetString(("money")), out player.GetComponent<PlayerInventory>().money);
        int.TryParse(PlayerPrefs.GetString(("uranium")), out player.GetComponent<PlayerInventory>().uranium);
        int.TryParse(PlayerPrefs.GetString(("meteorium")), out player.GetComponent<PlayerInventory>().meteorium);
    }
    
    private void SaveGlobalStats()
    {
        EcoStats ecoStats = GameObject.FindWithTag("GameController").GetComponent<EcoStats>();
        PlayerPrefs.SetString("co2", ecoStats.co2Value.ToString());
        PlayerPrefs.SetString("globalTemp", ecoStats.globalTempValue.ToString());
        PlayerPrefs.SetString("airPollution", ecoStats.airPollution.ToString());
        PlayerPrefs.SetString("radiation", ecoStats.radiation.ToString());
    }
    private void LoadGlobalStats()
    {
        EcoStats ecoStats = GameObject.FindWithTag("GameController").GetComponent<EcoStats>();
        float.TryParse(PlayerPrefs.GetString(("co2")), out ecoStats.co2Value);
        float.TryParse(PlayerPrefs.GetString(("globalTemp")), out ecoStats.globalTempValue);
        float.TryParse(PlayerPrefs.GetString(("airPollution")), out ecoStats.airPollution);
        float.TryParse(PlayerPrefs.GetString(("radiation")), out ecoStats.radiation);
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
            float.TryParse(PlayerPrefs.GetString(("playerHealth")), out player.GetComponent<PlayerHealth>().playerHealth);
            float.TryParse(PlayerPrefs.GetString(("playerTemp")), out player.GetComponent<PlayerHealth>().playerTemp);
        }
    }
    
    // TODO Insanely inefficient - needs a lot of work
    private void SaveResources()
    {
        GameObject[] resources = GameObject.FindGameObjectsWithTag("Resource");
        int count = resources.Length;
        int n = 0;

        PlayerPrefs.SetInt("resourceCount", count);
    }
    private void LoadResources()
    {
        // If there is a save file, clear the scene and place the resources saved
        // Don't compare and/or delete only the non safed objects, as comparing floats can be tricky and overlappy
        if (PlayerPrefs.GetString("resourceCount") != null)
        {
            int count = PlayerPrefs.GetInt("resourceCount");
            int n = 0;

            while (n < count)
            {
                Vector2 position = new Vector2(UnityEngine.Random.Range(0, 100), UnityEngine.Random.Range(0, 100));
                Instantiate(GetComponent<PrefabHolder>().tree, position, new Quaternion(0, 0, 0, 1), null);
            }
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

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 16;
        GUI.Label(new Rect(Screen.width * 0.05f, Screen.height * 0.05f, 1600, 600), DateTime.Now.ToString(), style);
    }
}
