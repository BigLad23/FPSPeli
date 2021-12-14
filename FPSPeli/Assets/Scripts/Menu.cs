using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
  
    public static bool GameIsPaused = false;
   
    public GameObject pauseMenuUI;
    
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Debug.Log("Resuming");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Pause()
    {
        Debug.Log("Game is paused");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void StartGame() 
    {
        Debug.Log("Starting game");
        SceneManager.LoadScene("Game");
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void MainMenu()
    {
        Debug.Log("Loading Main Menu");
        SceneManager.LoadScene("StartMenu");
        
    }

    public void QuitGame()
    {
        Debug.Log("Pressed Quit");
        Application.Quit();
    }
}
