using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] GameObject OptionsPanel;
    [SerializeField] GameObject MainMenuPanel;

    private bool InOptions = false;

    //Resolution
    public Dropdown resolutionDropdown;

    Resolution[] resolutions;

    private void Start()
    {
        OptionsPanel.SetActive(false);
        MainMenuPanel.SetActive(true);

        //Options of resolutions
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (InOptions)
            {
                OptionsPanel.SetActive(false);
                MainMenuPanel.SetActive(true);
                InOptions= false;
            }
        }
    }

    //Settings
    public void QuitSettings()
    {
        OptionsPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }

    public void SetVolume(float volume)
    {
        Debug.Log(volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    //Main Menu
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OptiosMenu()
    {
        OptionsPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
        InOptions = true;
    }
}
