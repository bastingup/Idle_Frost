using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Button startGameButton, optionsButton, quitGameButton, optionsBackButton;

    [SerializeField]
    private Canvas mainMenuUI, optionsUI;

    private bool inOptionMenu = false;
  
    void Start()
    {
        startGameButton.onClick.AddListener(StartGame);
        optionsButton.onClick.AddListener(OptionsMenu);
        optionsBackButton.onClick.AddListener(OptionsMenu);
        quitGameButton.onClick.AddListener(QuitGame);
    }

    void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    void OptionsMenu()
    {
        if (!inOptionMenu)
        {
            mainMenuUI.gameObject.SetActive(false);
            optionsUI.gameObject.SetActive(true);
            inOptionMenu = true;
        }
        else if (inOptionMenu)
        {
            mainMenuUI.gameObject.SetActive(true);
            optionsUI.gameObject.SetActive(false);
            inOptionMenu = false;
        }
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
