using UnityEngine;

namespace StatSystem
{
    public class HealthModifier : StatModifier
    {
        public bool isCriticalHit { get; set; }
        public GameObject instigator { get; set; }
    }
}
