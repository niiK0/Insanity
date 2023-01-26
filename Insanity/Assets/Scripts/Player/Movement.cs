using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] private float dash_speed;

    [SerializeField] private bool is_dashing = false;

    private Rigidbody2D rb;
    private Vector2 move_velocity;
    private Vector2 dash_velocity;

    [SerializeField] private Vector2 move_input;
    [SerializeField] private LayerMask dash_layer_mask;
    [SerializeField] private float dash_cooldown;
    [SerializeField] private float dash_timer;
    [SerializeField] private float dash_duration, dash_duration_internal;
    //[SerializeField] GameObject dashEffect;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
