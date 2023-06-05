using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeItem : MonoBehaviour
{
    private Item item;
    private string[] statStrings = {
        "Health",
        "Strength",
        "Speed"
    };

    public Sprite[] icons;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Fixe corre o ficheiro do niko
            GenerateNewItem();
            FindObjectOfType<ItemStuff>().PickupItem(item);

            Destroy(gameObject);
        }
    }

    private void GenerateNewItem()
    {
        int randomIndex = Random.Range(0, statStrings.Length - 1);
        string itemStatName = statStrings[randomIndex];
        Sprite icon = icons[randomIndex];
        int[] values = { 1 };
        string[] statNames = { itemStatName };

        item = new Item(
            itemStatName + " Buff",
            "Increases your " + itemStatName + " stat by 1",
            icon,
            StatSystem.ModifierOperationType.Additive,
            values,
            statNames
        );
    }
}
