using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace StatSystem
{
    public class Attribute : Stat
    {
        protected float m_CurrentValue;
        public float currentValue => m_CurrentValue;

        public event Action currentValueChanged;
        public event Action<StatModifier> appliedModifier;

        public Attribute(StatBase statbase, StatController controller) : base(statbase, controller)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            m_CurrentValue = value;
        }

        public virtual void ApplyModifier(StatModifier modifier)
        {
            float newValue = m_CurrentValue;
            switch (modifier.type)
            {
                case ModifierOperationType.Override:
                    newValue = modifier.magnitude;
                    break;
                case ModifierOperationType.Additive:
                    newValue += modifier.magnitude;
                    break;
                case ModifierOperationType.Multiplicative:
                    newValue *= modifier.magnitude;
                    break;
            }

            newValue = Mathf.Clamp(newValue, 0, m_Value);

            if (currentValue != newValue)
            {
                m_CurrentValue = newValue;
                currentValueChanged?.Invoke();
            }
            appliedModifier?.Invoke(modifier);
        }
    }
}
