using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using StatSystem;

[Serializable]
public class Item
{
    public string name;
    public string desc;
    public Sprite icon;
    public ModifierOperationType type;
    public int[] value;
    public string[] statName;

    public Item(string name, string desc, Sprite icon, ModifierOperationType type, int[] value, string[] statName)
    {
        this.name = name;
        this.desc = desc;
        this.icon = icon;
        this.type = type;
        this.value = value;
        this.statName = statName;
    }
}