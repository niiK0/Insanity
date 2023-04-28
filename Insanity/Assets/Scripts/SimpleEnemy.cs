using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using StatSystem;

public class SimpleEnemy : MonoBehaviour
{
    public Transform player;
    public TMP_Text healthText;

    private float health => (m_StatController.stats["Health"] as Attribute).value;

    protected StatController m_StatController;

    protected virtual void Awake()
    {
        m_StatController = GetComponent<StatController>();
    }

    private void Start()
    {
        //healthText.text = health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = health.ToString();
        transform.LookAt(player.transform, Vector3.up);
    }

    public void TakeDamage(GameObject source)
    {
        m_StatController.stats["Health"].AddModifier(new StatModifier
        {
            source = source,
            magnitude = -player.GetComponent<StatController>().stats["Strength"].value,
            type = ModifierOperationType.Additive
        });
    }
}
