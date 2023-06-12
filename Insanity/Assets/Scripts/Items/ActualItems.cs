using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualItems : MonoBehaviour
{
    public Item[] items;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Fixe corre o ficheiro do niko
            Item item = SelectRandomItem();
            FindObjectOfType<ItemStuff>().PickupItem(item);

            Destroy(gameObject);
        }
    }

    private Item SelectRandomItem()
    {
        int randomNum = Random.Range(0, items.Length);
        return items[randomNum];
    }
}
