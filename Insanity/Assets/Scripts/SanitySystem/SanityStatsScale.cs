using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Attribute = StatSystem.Attribute;
using SanitySystem;
using StatSystem;
using System;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SanityStatsScale : MonoBehaviour
{
    //create string constants for every stat name for later use (need to match the scriptableobject name)
    private const string s_Health = "Health";
    private const string s_Strength = "Strength";
    private const string s_Dexterity = "Dexterity";
    private const string s_Speed = "Speed";
    private const string s_CritC = "CritC";
    private const string s_CritX = "CritX";
    private const string s_DashCount = "DashCount";
    private const string s_DashRange = "DashRange";

    //create public int variables for each stat from the stat controller
    public int maxHealth => (int)(m_StatController.stats[s_Health] as Attribute).currentValue;
    public int Health => (int)m_StatController.stats[s_Health].value;
    //public int maxHealth => (int)m_StatController.stats[s_Health].baseValue;
    public int Strength => (int)m_StatController.stats[s_Strength].value;
    public int Dexterity => (int)m_StatController.stats[s_Dexterity].value;
    public int Speed => (int)m_StatController.stats[s_Speed].value;
    public int CritC => (int)m_StatController.stats[s_CritC].value;
    public int CritX => (int)m_StatController.stats[s_CritX].value;
    public int DashCount => (int)m_StatController.stats[s_DashCount].value;
    public int DashRange => (int)m_StatController.stats[s_DashRange].value;

    //get the UI text for each stat
    public TextMeshProUGUI strengthText;
    public TextMeshProUGUI dexterityText;
    public TextMeshProUGUI speedText;
    public Slider healthBar;
    public TextMeshProUGUI sanityText;

    //bool for sanitymode and get text of the sanity mode
    private bool sanityMode = true;
    public TextMeshProUGUI sanityModeText;

    //create a sanity object (makes this script the main sanity handler)
    public Sanity sanity = new Sanity();

    //event for whenever the sanity was changed, probably important for a bunch of stuff later
    public event Action sanityChanged;

    public Volume postProcessVolume;
    public VolumeProfile insaneVolume;
    public VolumeProfile saneVolume;

    //get the stat controller for basically everything related to stats
    protected StatController m_StatController;
    protected virtual void Awake()
    {
        m_StatController = GetComponent<StatController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //add the editmodifier function to sanitychanged event
        sanityChanged += EditModifier;
        //default the sanity as 50 since thats where we want it
        sanity.sanity = 50;

        UpdateHealth();

        //add a stat modifier of type sanity to all of the stats we need, that way we just edit that one modifier only
        m_StatController.stats[s_Health].AddModifier(
                new StatModifier
                {
                    source = this,
                    magnitude = 1,
                    type = ModifierOperationType.Sanity
                });

        m_StatController.stats[s_Strength].AddModifier(
                new StatModifier
                {
                    source = this,
                    magnitude = 1f,
                    type = ModifierOperationType.Sanity
                });
        m_StatController.stats[s_Speed].AddModifier(
                new StatModifier
                {
                    source = this,
                    magnitude = 1,
                    type = ModifierOperationType.Sanity
                });

        m_StatController.stats[s_Dexterity].AddModifier(
                new StatModifier
                {
                    source = this,
                    magnitude = 1f,
                    type = ModifierOperationType.Sanity
                });
    }

    private void Update()
    {
        //need to swap this with new input system but its just mfor sanity change
        if (Input.GetKeyDown(KeyCode.Q))
        {
            sanityMode = !sanityMode;
        }
        SanityModeCheck();
    }

    //just updates all the text with the current values of stats and sanity
    private void UpdateText()
    {
        strengthText.text = m_StatController.stats[s_Strength].value.ToString();
        dexterityText.text = m_StatController.stats[s_Dexterity].value.ToString();
        speedText.text = m_StatController.stats[s_Speed].value.ToString();

        healthBar.value = Mathf.Clamp01(Health / Mathf.Max(maxHealth, float.Epsilon));
        sanityText.text = sanity.sanity.ToString();
        if(sanity.sanity > 50)
            sanityText.color = new Color32(255, 253, 0, 255);
        else if(sanity.sanity < 50)
            sanityText.color = new Color32(255, 23, 25, 255);
        else
            sanityText.color = new Color32(255, 255, 255, 255);
    }

    public void UpdateHealth()
    {
        //Debug.Log("Max HP:" + maxHealth);
        //Debug.Log("Current HP:" + Health);
        //Debug.Log("Clamped VAL:" + Mathf.Clamp01(Health / Mathf.Max(maxHealth, float.Epsilon)));
        healthBar.value = Mathf.Clamp01(Health / Mathf.Max(maxHealth, float.Epsilon));
    }

    //updates the sanity mode text
    private void SanityModeCheck()
    {
        if (sanityMode)
        {
            sanityModeText.text = "Sane";
            sanityModeText.color = new Color32(255, 253, 0, 255);
            postProcessVolume.profile = saneVolume;
        }else if (!sanityMode)
        {
            sanityModeText.text = "Insane";
            sanityModeText.color = new Color32(255, 23, 25, 255);
            postProcessVolume.profile = insaneVolume;
        }
        
    }

    public void TakeSanityPill(int amount)
    {
        sanity.sanity += amount;
        EditModifier();
        UpdateText();
    }

    public void TakeInsanityPill(int amount)
    {
        sanity.sanity -= amount;
        EditModifier();
        UpdateText();
    }

    //updates the sanity per kill depending on each mode
    public void SanityCalcs()
    {
        if (sanityMode)
        {
            sanity.sanity -= 5;
            
        }else if (!sanityMode)
        {
            sanity.sanity += 5;
        }
        EditModifier();
        UpdateText();
    }

    //function added to the event, called whenerver sanitychanged event is triggered
    //calls the calculatevalues function to do all the job and then edits each sanity modifier with the new values.
    //GetHealthModifier = health, dexterity
    //GetStrenghtModifier = strength, speed
    private void EditModifier()
    {
        sanity.CalculateValues();

        {
            m_StatController.stats[s_Health].EditModifier(
                new StatModifier
                {
                    source = this,
                    magnitude = (float)sanity.GetHealthModifier(),
                    type = ModifierOperationType.Sanity
                });
            m_StatController.stats[s_Strength].EditModifier(
                new StatModifier
                {
                    source = this,
                    magnitude = (float)sanity.GetStrengthModifier(),
                    type = ModifierOperationType.Sanity
                });
            m_StatController.stats[s_Dexterity].EditModifier(
                new StatModifier
                {
                    source = this,
                    magnitude = (float)sanity.GetHealthModifier(),
                    type = ModifierOperationType.Sanity
                });
            m_StatController.stats[s_Speed].EditModifier(
                new StatModifier
                {
                    source = this,
                    magnitude = (float)sanity.GetStrengthModifier(),
                    type = ModifierOperationType.Sanity
                });
        }

        UpdateText();
    }
}
