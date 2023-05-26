using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using StatSystem;
using SanitySystem;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;

public class EnemyHealth : MonoBehaviour
{
    public Transform player;
    public TMP_Text healthText;
    [SerializeField] private Animator anim;

    private float health => (m_StatController.stats["Health"] as Attribute).value;

    protected StatController m_StatController;

    protected virtual void Awake()
    {
        m_StatController = GetComponent<StatController>();
    }

    private void Start()
    {
        healthText.text = health.ToString();
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

        //transform.LookAt(player.transform, Vector3.up);
    }

    public void TakeDamage(GameObject source)
    {
        m_StatController.stats["Health"].AddModifier(new StatModifier
        {
            source = source,
            magnitude = -player.GetComponent<StatController>().stats["Strength"].value,
            type = ModifierOperationType.Additive
        });

        healthText.text = health.ToString();

        if (health <= 0 && gameObject.tag.Equals("Boss"))
        {
            SceneManager.LoadScene(3);
        }

        if (health <= 0)
        {
            anim.SetTrigger("die");
            EnemyDie();
            player.gameObject.GetComponent<SanityStatsScale>().SanityCalcs();
        }


    }

    private void EnemyDie()
    {
        //anim.applyRootMotion = true;
        healthText.enabled = false;
        GetComponent<EnemyMovement>().enabled = false;
    }

    private void DestroyEnemyOnDeath()
    {
        Destroy(gameObject);
    }
}
