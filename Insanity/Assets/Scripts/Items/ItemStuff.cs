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

    public void PickupItem(Item item)
    {
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

        items.Add(item);

        FindObjectOfType<PassiveItemsUI>().AddItemToUI(item);
    }

    public void LoadItems()
    {
        for (int i = 0; i < items.Count; i++)
        {
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

[Serializable]
public struct Item{
    public string name;
    public string desc;
    public Sprite icon; 
    public ModifierOperationType type;
    public int[] value;
    public string[] statName;
}
