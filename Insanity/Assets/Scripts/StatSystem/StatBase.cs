using UnityEngine;

namespace StatSystem
{
    [CreateAssetMenu(fileName = "StatBase", menuName = "StatSystem/StatBase", order = 0)]
    public class StatBase : ScriptableObject
    {
        [SerializeField] private float m_BaseValue;

        public float baseValue => m_BaseValue; 
    }
}