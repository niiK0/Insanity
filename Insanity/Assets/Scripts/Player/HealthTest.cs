using SanitySystem;
using StatSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Attribute = StatSystem.Attribute;

public class HealthTest : MonoBehaviour
{
    private const string s_Health = "Health";
    private const string s_Strength = "Strength";
    private bool m_IsInitialized;
    public int Health => (int)(m_StatController.stats[s_Health] as Attribute).currentValue;
    public int maxHealth => (int)m_StatController.stats[s_Health].value;
    public event Action initialized;
    public event Action healthChanged;
    public event Action sanityChanged;
    public event Action defeated;
    public event Action<float> healed;
    public event Action<float, bool> damaged;

    public TMP_Text sanityText;

    //change later
    public Sanity sanity = new Sanity();

    protected StatController m_StatController;

    protected virtual void Awake()
    {
        m_StatController = GetComponent<StatController>();
    }

    protected virtual void OnEnable()
    {
        m_StatController.initialized += OnStatControllerInitialized;
        sanityChanged += EditModifier;
        if (m_StatController.isInitialized)
            OnStatControllerInitialized();
    }

    // Start is called before the first frame update
    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            sanity.sanity += 1;
            sanityText.text = sanity.sanity.ToString();
            sanityChanged?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            sanity.sanity -= 1;
            sanityText.text = sanity.sanity.ToString();
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
        }

        Debug.Log($"Strength: {m_StatController.stats[s_Strength].value}");
        Debug.Log($"Health: {m_StatController.stats[s_Health].value}");
    }

    private void OnStatControllerInitialized()
    {
        (m_StatController.stats[s_Health] as Attribute).currentValueChanged += OnHealthChanged;
        (m_StatController.stats[s_Health] as Attribute).appliedModifier += OnAppliedModifier;
        m_IsInitialized = true;
        initialized?.Invoke();
    }

    private void OnHealthChanged()
    {
        healthChanged?.Invoke();
    }

    private void OnAppliedModifier(StatModifier modifier)
    {
        if (modifier.magnitude > 0)
        {
            healed?.Invoke(modifier.magnitude);
        }
        else
        {
            damaged?.Invoke(modifier.magnitude, (modifier as HealthModifier).isCriticalHit);
            if ((m_StatController.stats[s_Health] as Attribute).currentValue == 0)
                defeated?.Invoke();
        }
    }
}

