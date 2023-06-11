using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using StatSystem;
using SanitySystem;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;

public class EnemyHealth : MonoBehaviour
{
    public Transform player;
    public Slider healthSlider;
    [SerializeField] private Animator anim;
    [SerializeField] GameObject statBuffItem;
 
    private float health => (m_StatController.stats["Health"] as Attribute).value;
    private float maxHealth = 0f;

    protected StatController m_StatController;

    protected virtual void Awake()
    {
        m_StatController = GetComponent<StatController>();
    }

    private void Start()
    {
        healthSlider.value = Mathf.Clamp01(health / maxHealth);
        player = GameObject.FindWithTag("Player").transform;
        maxHealth = m_StatController.stats["Health"].baseValue;
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.transform.LookAt(player.transform);
    }

    public void TakeDamage(GameObject source)
    {
        m_StatController.stats["Health"].AddModifier(new StatModifier
        {
            source = source,
            magnitude = -player.GetComponent<StatController>().stats["Strength"].value,
            type = ModifierOperationType.Additive
        });

        healthSlider.value = Mathf.Clamp01(health / maxHealth);

        if (health <= 0 && gameObject.tag.Equals("Boss"))
        {
            SceneManager.LoadScene(4);
        }

        if (health <= 0)
        {
            GetComponent<Collider>().enabled = false;
            anim.SetTrigger("die");
            EnemyDie();
            player.gameObject.GetComponent<SanityStatsScale>().SanityCalcs();
        }


    }

    private void EnemyDie()
    {
        //anim.applyRootMotion = true;
        //spawn item
        if(gameObject.tag != "Boss")
            Instantiate(statBuffItem, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);

        healthSlider.gameObject.SetActive(false);
        GetComponent<EnemyMovement>().enabled = false;
    }

    private void DestroyEnemyOnDeath()
    {
        Destroy(gameObject);
    }
}
