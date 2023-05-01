using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StatSystem;
using UnityEngine.SceneManagement;

public class SimpleHealth : MonoBehaviour
{
    private float health => (m_StatController.stats["Health"] as Attribute).value;

    protected StatController m_StatController;

    protected virtual void Awake()
    {
        m_StatController = GetComponent<StatController>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TakeDamage(GameObject source)
    {
        m_StatController.stats["Health"].AddModifier(new StatModifier
        {
            source = source,
            magnitude = -source.GetComponent<StatController>().stats["Strength"].value,
            type = ModifierOperationType.Additive
        });

        GetComponent<SanityStatsScale>().healthText.text = health.ToString();

        if (health <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
