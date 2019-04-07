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
        //PlayerPrefs.DeleteAll();
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
        SaveResources();
        WriteSaved();
        SaveEnergy();
        SavePlayerPosition();
        QuitGame();
    }
    void LoadGame()
    {
        LoadTime();
        LoadItems();
        LoadGlobalStats();
        LoadPlayerStats();
        LoadResources();
        LoadPlayerPosition();
        LoadEnergy();
    }

    bool CheckForSaved()
    {
        if (PlayerPrefs.GetString("saved") != "saved")
        {
            PlayerPrefs.SetString("saved", "unsaved");
            return false;
        }
        else
        {
            return true;
        }
    }
    void WriteSaved()
    {
        PlayerPrefs.SetString("saved", "saved");
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

    private void SaveEnergy()
    {
        PlayerPrefs.SetInt("energy", FindObjectOfType<Generator>().energyInGenerator);
    }
    private void LoadEnergy()
    {
        FindObjectOfType<Generator>().energyInGenerator = PlayerPrefs.GetInt("energy");
    }

    private void SavePlayerPosition()
    {
        PlayerPrefs.SetFloat("playerX", GameObject.FindGameObjectWithTag("Player").transform.position.x);
        PlayerPrefs.SetFloat("playerY", GameObject.FindGameObjectWithTag("Player").transform.position.y);
    }
    private void LoadPlayerPosition()
    {
        Vector2 playerPositoin = new Vector2(PlayerPrefs.GetFloat("playerX"),
                                             PlayerPrefs.GetFloat("playerY"));
        GameObject.FindGameObjectWithTag("Player").transform.position = playerPositoin;
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

        PlayerPrefs.SetFloat("co2", ecoStats.co2Value);
        PlayerPrefs.SetFloat("globalTemp", ecoStats.globalTempValue);
        PlayerPrefs.SetFloat("airPollution", ecoStats.airPollution);
        PlayerPrefs.SetFloat("radiation", ecoStats.radiation);
    }
    private void LoadGlobalStats()
    {
        EcoStats ecoStats = GameObject.FindWithTag("GameController").GetComponent<EcoStats>();

        if (CheckForSaved())
        {
            ecoStats.co2Value = PlayerPrefs.GetFloat("co2");
            ecoStats.globalTempValue = PlayerPrefs.GetFloat("globalTemp");
            ecoStats.airPollution = PlayerPrefs.GetFloat("airPollution");
            ecoStats.radiation = PlayerPrefs.GetFloat("radiation");
        }
        else
        {
            ecoStats.co2Value = 20; ecoStats.globalTempValue = 20; ecoStats.airPollution = 5; ecoStats.radiation = 3;
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
            float.TryParse(PlayerPrefs.GetString(("playerHealth")), out player.GetComponent<PlayerHealth>().playerHealth);
            float.TryParse(PlayerPrefs.GetString(("playerTemp")), out player.GetComponent<PlayerHealth>().playerTemp);
        }
    }
    
    private void SaveResources()
    {
        GameObject[] resources = GameObject.FindGameObjectsWithTag("Resource");
        PlayerPrefs.SetInt("resourceCount", resources.Length);
    }
    private void LoadResources()
    {
        int count;

        if (CheckForSaved())
        {
            count = PlayerPrefs.GetInt("resourceCount");
        }
        else
        {
            count = 4200;
        }

        InstantiateResources(count);
    }

    void InstantiateResources(int count)
    {
        GameObject tree = GetComponent<PrefabHolder>().tree;

        for (int n = 0; n <= count; n++)
        {
            Vector2 position = new Vector2(UnityEngine.Random.Range(0, 500), UnityEngine.Random.Range(0, 500));
            Instantiate(tree, position, new Quaternion(0, 0, 0, 1));
            n++;
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
