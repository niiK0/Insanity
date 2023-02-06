using UnityEngine;

namespace StatSystem
{
    public enum SanityModifierType
    {
        Health,
        Strength
    }

    public class SanityModifier : StatModifier
    {
        public SanityModifierType sanityModifierType;
        public float sanity_percentage;
    }
}
