using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StatSystem;
using System;

public class ItemStuff : MonoBehaviour
{
    public List<Item> items;
    private Transform player;
    private StatController playerStatController;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerStatController = player.GetComponent<StatController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Item FindItem(Item item)
    {
        foreach(Item lItem in items)
        {
            //Debug.Log("Checked if item: " + lItem.name + " | item: " + item.name);
            if (lItem.name == item.name)
            {
                return lItem;
            }
        }

        return null;
    }

    public void PickupItem(Item item)
    {
        Item foundItem = FindItem(item);

        if(foundItem != null)
        {
            //Debug.Log("Found item wasnt null, increasing quantity from " + foundItem.quantity.ToString());
            foundItem.quantity++;
            FindObjectOfType<PassiveItemsUI>().UpdateItemUI(foundItem);
        }
        else
        {
            //Debug.Log("Found item was null, adding a new one");
            items.Add(item);
            FindObjectOfType<PassiveItemsUI>().AddItemToUI(item);
        }

        for (int i = 0; i < item.statName.Length; i++)
        {
            string tempStatName = item.statName[i];
            int tempStatValue = item.value[i];

            playerStatController.stats[tempStatName].AddModifier(
            new StatModifier
            {
                source = this,
                magnitude = tempStatValue,
                type = item.type
            });
        }       
    }

    public void LoadItems()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (!items[i].enabled)
            {
                continue;
            }

            for (int j = 0; j < items[i].statName.Length; j++)
            {
                string tempStatName = items[i].statName[j];
                int tempStatValue = items[i].value[j];

                playerStatController.stats[tempStatName].AddModifier(
                new StatModifier
                {
                    source = this,
                    magnitude = tempStatValue,
                    type = items[i].type
                });
            }
            FindObjectOfType<PassiveItemsUI>().AddItemToUI(items[i]);
        }
    }
}