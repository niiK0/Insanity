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

    public void TakeFood(int amount)
    {
        m_StatController.stats["Health"].AddModifier(new StatModifier
        {
            source = this,
            magnitude = amount,
            type = ModifierOperationType.Additive
        });

        GetComponent<SanityStatsScale>().UpdateHealth();
    }

    public void TakeDamage(GameObject source)
    {
        AudioManager.instance.Play("DamagePlayer");


        StatModifier damage = new StatModifier
        {
            source = source,
            magnitude = -source.GetComponent<StatController>().stats["Strength"].value,
            type = ModifierOperationType.Additive
        };

        //(m_StatController.stats["Health"] as Attribute).ApplyModifier(damage);

        (m_StatController.stats["Health"] as Attribute).AddModifier(damage);

        GetComponent<SanityStatsScale>().UpdateHealth();

        if (health <= 0)
        {
            AudioManager.instance.Play("PlayerDeath");
            SceneManager.LoadScene(0);
        }
    }
}
