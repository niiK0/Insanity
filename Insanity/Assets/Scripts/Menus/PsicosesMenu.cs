using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PsicosesMenu : MonoBehaviour
{
    public GameObject psicosesMenu;
    public GameObject KeyToPressText;
    private bool CanShowMenu = false;


    // Start is called before the first frame update
    void Start()
    {
        psicosesMenu.SetActive(false);
        KeyToPressText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && CanShowMenu)
        {
            ShowMenu();
        }
    }

    //The triggers permite us to track wether the player is or isnt inside the collider to open de psicoses menu
    private void OnTriggerEnter(Collider other)
    {
        CanShowMenu = true;
        KeyToPressText.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        CanShowMenu = false;
        KeyToPressText.SetActive(false);
    }


    //This function activates the menu 
    private void ShowMenu()
    {
        Time.timeScale = 0f;
        psicosesMenu.SetActive(true);
    }

    //The buttons fucntios select your psicose and let you resume your game
    public void Button1()
    {
        Debug.Log("Escolhes-te o botão1");
        psicosesMenu.SetActive(false);
        CanShowMenu = false;
        Time.timeScale = 1f;
    }

    public void Button2()
    {
        Debug.Log("Escolhes-te o botão2");
        psicosesMenu.SetActive(false);
        CanShowMenu = false;
        Time.timeScale = 1f;
    }

    public void Button3()
    {
        Debug.Log("Escolhes-te o botão3");
        psicosesMenu.SetActive(false);
        CanShowMenu = false;
        Time.timeScale = 1f;
    }
}
