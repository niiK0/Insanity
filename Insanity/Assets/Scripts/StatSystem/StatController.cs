using System;
using System.Collections.Generic;
using UnityEngine;

namespace StatSystem
{
    public class StatController : MonoBehaviour
    {
        [SerializeField] private StatDatabase m_StatDatabase;
        protected Dictionary<string, Stat> m_Stats = new Dictionary<string, Stat>(StringComparer.OrdinalIgnoreCase);
        public Dictionary<string, Stat> stats => m_Stats;

        private bool m_IsInitialized;
        public bool isInitialized => m_IsInitialized;

        public event Action initialized;
        public event Action willUninitialize;

        protected virtual void Awake()
        {
            if (!m_IsInitialized)
            {
                Initialize();
            }
        }

        private void OnDestroy()
        {
            willUninitialize?.Invoke();
        }

        protected void Initialize()
        {
            foreach (StatBase statbase in m_StatDatabase.stats)
            {
                m_Stats.Add(statbase.name, new Stat(statbase, this));
            }

            foreach (StatBase statbase in m_StatDatabase.attributes)
            {
                m_Stats.Add(statbase.name, new Attribute(statbase, this));
            }

            foreach (Stat stat in m_Stats.Values)
            {
                stat.Initialize();
            }

            m_IsInitialized = true;
            initialized?.Invoke();

        }
    }
}