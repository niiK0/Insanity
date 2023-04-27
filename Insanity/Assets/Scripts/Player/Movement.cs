using StatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class Movement : MonoBehaviour
{
    private float speed => m_StatController.stats["Speed"].value;
    private float dash_speed => m_StatController.stats["DashRange"].value;

    [SerializeField] private bool is_dashing = false;

    private Rigidbody rb;
    private Vector2 move_velocity;
    private Vector2 dash_velocity;

    [SerializeField] private Vector2 move_input;
    [SerializeField] private LayerMask dash_layer_mask;
    [SerializeField] private float dash_cooldown;
    [SerializeField] private float dash_timer;
    [SerializeField] private float dash_duration, dash_duration_internal;
    [SerializeField] private Transform visualTransform;
    //[SerializeField] private Camera camera;
    [SerializeField] private GameInput gameInput;

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
        gameInput.OnDashAction += GameInput_OnDashAction;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    private void GameInput_OnDashAction()
    {
        if (dash_timer >= dash_cooldown)
        {
            dash_velocity = move_input * dash_speed;
            is_dashing = true;
        }
    }

    void Update()
    {
        move_input = gameInput.GetMovementVector();
        move_velocity = move_input * speed;

        Vector2 mouseInput = gameInput.GetMouseVector();
        Ray mouseRay = Camera.main.ScreenPointToRay(new Vector3(mouseInput.x, mouseInput.y, 0));
        Plane p = new Plane(Vector3.up, transform.position);
        if (p.Raycast(mouseRay, out float hitDist))
        { 
            Vector3 hitPoint = mouseRay.GetPoint(hitDist);
            visualTransform.LookAt(hitPoint);
            visualTransform.rotation = Quaternion.Euler(new Vector3(0, visualTransform.rotation.eulerAngles.y, 0));
        }
        //Vector3 lookPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseInput.x, mouseInput.y, 1));
        //visualTransform.LookAt(lookPosition);

        if (dash_duration_internal <= dash_duration && dash_duration_internal >= 0 && is_dashing)
        {
            dash_duration_internal += -1 * Time.deltaTime;
        }

        if (dash_timer <= dash_cooldown)
        {
            dash_timer += 1 * Time.deltaTime;
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
            rb.MovePosition(rb.position + new Vector3(dash_velocity.x, 0, dash_velocity.y) * Time.fixedDeltaTime);

            if (dash_duration_internal <= 0)
            {
                dash_duration_internal = dash_duration;
                dash_timer = 0f;
                is_dashing = false;
            }

        }
        else
        {
            rb.MovePosition(rb.position + new Vector3(move_velocity.x, 0, move_velocity.y) * Time.fixedDeltaTime);
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
