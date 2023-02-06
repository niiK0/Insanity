using UnityEngine;

namespace StatSystem
{
    public enum ModifierOperationType
    {
        Additive,
        Multiplicative,
        Override,
        Sanity
    }

    public class StatModifier
    {
        public Object source { get; set; }
        public float magnitude { get; set; }
        public ModifierOperationType type { get; set; }
    }
}
