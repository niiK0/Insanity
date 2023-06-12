using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace StatSystem
{
    public class Stat
    {
        protected StatBase m_statBase;
        protected StatController m_statController;

        protected float m_Value;

        public virtual float baseValue => m_statBase.baseValue;
        public float value => m_Value;

        public event Action valueChanged;

        protected List<StatModifier> m_Modifiers = new List<StatModifier>();

        public Stat(StatBase statBase, StatController statController)
        {
            m_statBase = statBase;
            m_statController = statController;
        }

        public virtual void Initialize()
        {
            CalculateValue();
        }

        public void AddModifier (StatModifier modifier)
        {
            m_Modifiers.Add(modifier);
            CalculateValue();
        }

        public void RemoveModifierFromSource(Object source)
        {
            m_Modifiers = m_Modifiers.Where(m => m.source.GetInstanceID() != source.GetInstanceID()).ToList();
            CalculateValue();
        }

        protected void CalculateValue()
        {
            float finalValue = baseValue;

            //Sort so the additive modifiers come before the multiplicative ones
            m_Modifiers.Sort((x, y) => x.type.CompareTo(y.type));

            for (int i = 0; i < m_Modifiers.Count; i++)
            {
                StatModifier modifier = m_Modifiers[i];
                if(modifier.type == ModifierOperationType.Additive)
                {
                    finalValue += modifier.magnitude;
                }
                else if(modifier.type == ModifierOperationType.Multiplicative || modifier.type == ModifierOperationType.Sanity)
                {
                    finalValue *= modifier.magnitude;
                }
            }

            if (m_Value != finalValue)
            {
                m_Value = finalValue;
                valueChanged?.Invoke();
            }
        }

    }
}
