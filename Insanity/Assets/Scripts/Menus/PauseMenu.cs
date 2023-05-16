using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject inventory;
    public GameObject gui;

    public static bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        inventory.SetActive(false);
        gui.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isPaused)
            {
                CloseInventory();
            }
            else
            {
                OpenInventory();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        gui.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        gui.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void OpenInventory()
    {
        inventory.SetActive(true);
        gui.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void CloseInventory()
    {
        inventory.SetActive(false);
        gui.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
