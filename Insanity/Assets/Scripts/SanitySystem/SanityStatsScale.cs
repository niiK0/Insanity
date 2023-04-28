using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Attribute = StatSystem.Attribute;
using SanitySystem;
using StatSystem;
using System;
using TMPro;

public class SanityStatsScale : MonoBehaviour
{
    private const string s_Health = "Health";
    private const string s_Strength = "Strength";
    private const string s_Dexterity = "Dexterity";
    private const string s_Speed = "Speed";
    private const string s_CritC = "CritC";
    private const string s_CritX = "CritX";
    private const string s_DashCount = "DashCount";
    private const string s_DashRange = "DashRange";

    public int Health => (int)(m_StatController.stats[s_Health] as Attribute).currentValue;
    public int maxHealth => (int)m_StatController.stats[s_Health].value;
    public int Strength => (int)m_StatController.stats[s_Strength].value;
    public int Dexterity => (int)m_StatController.stats[s_Dexterity].value;
    public int Speed => (int)m_StatController.stats[s_Speed].value;
    public int CritC => (int)m_StatController.stats[s_CritC].value;
    public int CritX => (int)m_StatController.stats[s_CritX].value;
    public int DashCount => (int)m_StatController.stats[s_DashCount].value;
    public int DashRange => (int)m_StatController.stats[s_DashRange].value;

    public TMP_Text strengthText;
    public TMP_Text dexterityText;
    public TMP_Text speedText;
    public TMP_Text healthText;
    public TMP_Text sanityText;

    public Sanity sanity = new Sanity();
    public event Action sanityChanged;

    protected StatController m_StatController;

    protected virtual void Awake()
    {
        m_StatController = GetComponent<StatController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        sanityChanged += EditModifier;
        sanity.sanity = 50;
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
        if (Input.GetKeyDown(KeyCode.L))
        {
            sanity.sanity += 1;
            sanityText.text = sanity.sanity.ToString() + "%";
            sanityChanged?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            sanity.sanity -= 1;
            sanityText.text = sanity.sanity.ToString() + "%";
            sanityChanged?.Invoke();
        }
    }

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

        strengthText.text = m_StatController.stats[s_Strength].value.ToString();
        dexterityText.text = m_StatController.stats[s_Dexterity].value.ToString();
        speedText.text = m_StatController.stats[s_Speed].value.ToString();
        healthText.text = m_StatController.stats[s_Health].value.ToString();

        //Debug.Log($"Strength: {m_StatController.stats[s_Strength].value}");
        //Debug.Log($"Health: {m_StatController.stats[s_Health].value}");
    }
}
