using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveGame : MonoBehaviour {

    [SerializeField]
    private Button saveButton, loadButton;

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
        SaveFarm();
        QuitGame();
    }
    void LoadGame()
    {
        // TODO load resources doubles resources
        if (CheckForSaved())
        {
            DestroyAllResources();
            LoadTime();
            LoadItems();
            LoadGlobalStats();
            LoadPlayerStats();
            LoadPlayerPosition();
            LoadEnergy();
            LoadFarm();
            LoadResources();
        }
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
    void DestroyAllResources()
    {
        ResourceAndItemInteraction[] resourceArray = GameObject.FindObjectsOfType<ResourceAndItemInteraction>();
        for (int i = 0; i < resourceArray.Length; i++)
        {
            Destroy(resourceArray[i].gameObject);
        }
    }
    bool CheckForBinaryFormatterFile()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            return true;
        }
        else
        {
            return false;
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

    private void SaveFarm()
    {
        PlayerPrefs.SetFloat("cooldownFarm", FindObjectOfType<Farm>().cooldownTime);
    }
    private void LoadFarm()
    {
        Farm farm = FindObjectOfType<Farm>();
        farm.cooldownTime = PlayerPrefs.GetFloat("cooldownFarm");
        StartCoroutine(farm.CoolDown((int)farm.cooldownTime));
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
        // Binary formatter and file for saving
        BinaryFormatter bf = new BinaryFormatter();
        if (CheckForBinaryFormatterFile())
        {
            File.Delete(Application.persistentDataPath + "/savedGames.gd");
        }
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");

        // All resources in the scene
        ResourceAndItemInteraction[] resourceArray = GameObject.FindObjectsOfType<ResourceAndItemInteraction>();
        List<string> resourceEnumAndPosition = new List<string>();

        string nameAndPos;

        for (int i = 0; i < resourceArray.Length; i++)
        {
            nameAndPos = resourceArray[i].GetComponent<ResourceAndItemInteraction>().resourceName.ToString()
                         + "_" +
                         resourceArray[i].transform.position.ToString();
            resourceEnumAndPosition.Add(nameAndPos);
        }

        // Save array to file
        bf.Serialize(file, resourceEnumAndPosition);
        file.Close();

    }
    private void LoadResources()
    {
        // If binary formatter file exists, load all resources
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            List<string> resourceList = (List<string>)bf.Deserialize(file);
            file.Close();
            InstantiateResources(resourceList);
        }
    }
    void InstantiateResources(List<string> resources)
    {
        for (int n = 0; n < resources.Count; n++)
        {
            string name = resources[n].Substring(0, resources[n].IndexOf("_"));
            int length = resources[n].IndexOf(")") - resources[n].IndexOf("(") - 1;
            string positionString = resources[n].Substring(resources[n].IndexOf("(") + 1, length); 
            Instantiate(DeterminePrefab(name), ConvertNameToVector(positionString), new Quaternion(0, 0, 0, 1));
        }
    }
    GameObject DeterminePrefab(string name)
    {
        // TODO get rid of the switch asap and automate the finding of the prefabs
        switch (name)
        {
            case "tree":
                return GetComponent<PrefabHolder>().tree;
                break;
            case "uranium":
                return GetComponent<PrefabHolder>().uranium;
                break;
            case "wood":
                return GetComponent<PrefabHolder>().wood;
                break;
            case "regularMeteor":
                return GetComponent<PrefabHolder>().regularMeteor;
                break;
            case "rareMeteor":
                return GetComponent<PrefabHolder>().rareMeteor;
                break;
        }
        return null;
    }
    Vector2 ConvertNameToVector(string position)
    {
        string[] coordinates = position.Split(',');
        float x, y;
        float.TryParse(coordinates[0], out x);
        float.TryParse(coordinates[1], out y);
        Vector2 vector = new Vector2(x, y);
        return vector;
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
