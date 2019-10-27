using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static bool GameIsPaused;
    public static bool InMenu;
    public bool PowerUpActive;

    public GameObject mainUI;
    public GameObject pauseMenuUI;
    public GameObject pauseMenuUIStartingButton;
    public GameObject startMenuUI;
    public GameObject startMenuUIStartingButton;
    public GameObject endGameScreenUI;
    public GameObject endGameScreenUIStartingButton;
    public GameObject winGameScreenUI;
    public GameObject winGameScreenUIStartingButton;
    public GameObject gameSettingMenuUI;
    public GameObject gameSettingMenuUIStartingButton;

    public Text menuCounterText;
    public Text pauseCounterText;
    public Text winCounterText;
    public Text endCounterText;

    private int count;
    public int maxGoldValue = 20;

    public int goldValue = 1;
    public int enemyValue = 10;

    private int goldAmount;
    private int enemyAmount;

    // Start is called before the first frame update
    void Start()
    {
        Pause();
        InMenu = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7))
            {
                pausing();
            }
        }
        else if (Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton9))
            {
                pausing();
            }
        }
        else if (Application.platform == RuntimePlatform.LinuxPlayer || Application.platform == RuntimePlatform.LinuxEditor)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7))
            {
                pausing();
            }
        }
        else {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pausing();
            }
        }
    }

    void pausing()
    {
        if (GameIsPaused && pauseMenuUI.activeSelf)
        {
            ResumeGame();
        }
        else
        {
            if (!InMenu)
            {
                PauseGame();
            }
        }
    } 

    // Helpers

    public void Pause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Play()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Counter(int countValue, string type)
    {
        if (type.ToLower() == "gold") {
            goldAmount += countValue;
            count += countValue;
        }
        if (type.ToLower() == "enemy")
        {
            enemyAmount += countValue;
            count += countValue;
        }
        menuCounterText.text = "Score: " + count;
        pauseCounterText.text = "Score: " + count;
        winCounterText.text = "Score: " + count;
        endCounterText.text = "Score: " + count;
        CheckGameState();
    }

  

    public void CheckGameState()
    {
        if (goldAmount >= maxGoldValue)
        {
            GameWin();
        }
    }

    // Helpers


    // Pause Menu
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        mainUI.SetActive(true);
        Play();
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        mainUI.SetActive(false);
        gameSettingMenuUI.SetActive(false);
        EventSystem.current.SetSelectedGameObject(pauseMenuUIStartingButton); 
        Pause();
    }
    // Pause Menu

    // Game State Menu
    public void GameOver()
    {
        endGameScreenUI.SetActive(true);
        mainUI.SetActive(false);
        EventSystem.current.SetSelectedGameObject(endGameScreenUIStartingButton);
        Pause();
    }

    public void GameWin()
    {
        winGameScreenUI.SetActive(true);
        mainUI.SetActive(true);
        EventSystem.current.SetSelectedGameObject(winGameScreenUIStartingButton);
        Pause();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Game State Menu

    public void MainMenu()
    {
        startMenuUI.SetActive(true);
        mainUI.SetActive(false);
        EventSystem.current.SetSelectedGameObject(startMenuUIStartingButton);
        Pause();
    }

    public void SettingMenu()
    {
        gameSettingMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        EventSystem.current.SetSelectedGameObject(gameSettingMenuUIStartingButton);
        Pause();
    }

    public void StartGame()
    {
        startMenuUI.SetActive(false);
        mainUI.SetActive(true);
        InMenu = false;
        Play();
    }

    public void PowerUpPickUp() {
        PowerUpActive = true;
    }
    
    public void QuitGame() => Application.Quit();

    public void ApplyChange() => Debug.Log("Applied");

}
