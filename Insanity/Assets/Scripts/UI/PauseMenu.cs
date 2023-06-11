using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject inventory;
    public GameObject gui;
    public GameObject crosshair;

    public static bool isPaused, inventoryOpen;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        inventory.SetActive(false);
        gui.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!inventoryOpen)
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
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!isPaused)
            {
                if (inventoryOpen)
                {
                    CloseInventory();
                }
                else
                {
                    OpenInventory();
                }
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        gui.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        crosshair.SetActive(false);
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        gui.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        crosshair.SetActive(true);
    }

    public void OpenInventory()
    {
        inventory.SetActive(true);
        gui.SetActive(false);
        Time.timeScale = 0f;
        inventoryOpen = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        crosshair.SetActive(false);
    }

    public void CloseInventory()
    {
        inventory.SetActive(false);
        gui.SetActive(true);
        Time.timeScale = 1f;
        inventoryOpen = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        crosshair.SetActive(true);
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

