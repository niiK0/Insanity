using StatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float speed => m_StatController.stats["Speed"].value;
    private float dash_speed => m_StatController.stats["DashRange"].value;

    [SerializeField] private bool is_dashing = false;

    private Rigidbody rb;
    private Vector3 move_velocity;
    private Vector3 dash_velocity;

    [SerializeField] private Vector3 move_input;
    [SerializeField] private LayerMask dash_layer_mask;
    [SerializeField] private float dash_cooldown;
    [SerializeField] private float dash_timer;
    [SerializeField] private float dash_duration, dash_duration_internal;
    //[SerializeField] GameObject dashEffect;

    protected StatController m_StatController;

    protected virtual void Awake()
    {
        m_StatController = GetComponent<StatController>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        dash_duration_internal = dash_duration;
    }

    void Update()
    {
        move_input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        move_velocity = move_input * speed;
        dash_velocity = move_input * dash_speed;

        if (dash_duration_internal <= dash_duration && dash_duration_internal >= 0 && is_dashing)
        {
            dash_duration_internal += -1 * Time.deltaTime;
        }

        if (dash_timer <= dash_cooldown)
        {
            dash_timer += 1 * Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (dash_timer >= dash_cooldown)
            {
                is_dashing = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            m_StatController.stats["Speed"].AddModifier(
                new StatModifier { 
                    source = this,
                    //magnitude = 0.05f * m_StatController.stats["Speed"].value,
                    magnitude = 1.5f,
                    type = ModifierOperationType.Multiplicative
                });
            m_StatController.stats["Speed"].AddModifier(
                new StatModifier
                {
                    source = this,
                    //magnitude = 0.05f * m_StatController.stats["Speed"].value,
                    magnitude = 12,
                    type = ModifierOperationType.Additive
                });
            m_StatController.stats["Speed"].AddModifier(
                new StatModifier
                {
                    source = this,
                    //magnitude = 0.05f * m_StatController.stats["Speed"].value,
                    magnitude = 1.5f,
                    type = ModifierOperationType.Multiplicative
                });
            Debug.Log(speed);
        }
    }

    void FixedUpdate()
    {
        if (is_dashing)
        {
            rb.MovePosition(rb.position + dash_velocity * Time.fixedDeltaTime);

            if (dash_duration_internal <= 0)
            {
                dash_duration_internal = dash_duration;
                dash_timer = 0f;
                is_dashing = false;
            }

        }
        else
        {
            rb.MovePosition(rb.position + move_velocity * Time.fixedDeltaTime);
        }
    }

    //IEnumerator dashEffectCall()
    //{
    //    GameObject dashEffectI = Instantiate(dashEffect, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
    //    //FindObjectOfType<AudioManager>().PlaySound("Dash");
    //    yield return new WaitForSeconds(1f);
    //    Destroy(dashEffectI);
    //}
}
