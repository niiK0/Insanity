using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChoosingPsicoses : MonoBehaviour
{
    //if 0 : -5 spd +5 str
    //if 1 : -5 str +5 spd
    //if 2 : -5 str +15 hp
    public int selectedPsychosis;
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private Sprite[] icons;

    private void Start()
    {
        //defualt
        selectedPsychosis = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if(other.CompareTag("Player"))
            transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (other.CompareTag("Player"))
            transform.GetChild(0).gameObject.SetActive(false);
    }

    public void SelectPsychosis()
    {
        //select the psichoses for when leaving the door
        selectedPsychosis = dropdown.value;
    }

    public void CloseWindow()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void LeaveRoom()
    {
        Item item = null;

        switch (selectedPsychosis)
        {
            case 0:
                item = new Item(
                    "Catatonic Schitzofrenia",
                    "Pshycosis: -3 Speed | + 5 Strength",
                    icons[0],
                    StatSystem.ModifierOperationType.Additive,
                    new int[] { -3, 5 },
                    new string[] { "Speed", "Strength" },
                    1
                );
                break;
            case 1:
                item = new Item(
                    "Paranoid Schitzofrenia",
                    "Pshycosis: -5 Strength | + 3 Speed",
                    icons[1],
                    StatSystem.ModifierOperationType.Additive,
                    new int[] { -5, 3 },
                    new string[] { "Strength", "Speed" },
                    1
                );
                break;
            case 2:
                item = new Item(
                    "Hebrephenic Schitzofrenia",
                    "Pshycosis: -5 Strength | + 15 Health",
                    icons[2],
                    StatSystem.ModifierOperationType.Additive,
                    new int[] { -5, 15 },
                    new string[] { "Strength", "Health" },
                    1
                );
                break;
        }

        if(item != null)
        {
            GameManager.instance.LeaveStartRoom(item);
        }
    }
}
