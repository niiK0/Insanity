using StatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class Movement : MonoBehaviour
{
    //get readonly files of speed and dash speed(dash range) from the player stat controller
    private float speed => m_StatController.stats["Speed"].value;
    private float dash_speed => m_StatController.stats["DashRange"].value;

    //variables needed for dash calculations and all
    [SerializeField] private bool is_dashing = false;
    [SerializeField] private LayerMask dash_layer_mask;
    [SerializeField] private float dash_cooldown;
    [SerializeField] private float dash_timer;
    [SerializeField] private float dash_duration, dash_duration_internal;

    private Rigidbody rb;

    //create move/dash velocity variables for later assign
    private Vector3 move_velocity;
    private Vector3 dash_velocity;

    [SerializeField] private Vector2 move_input;

    //get the gameinput script for input stuff
    [SerializeField] private GameInput gameInput;

    //[SerializeField] GameObject dashEffect;

    //get the stat controller for basically everything related to stats
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
    }

    //subscribed ondash function for when the dash button is pressed
    //defines the velocity with the move_input so it locks the direction of the movement and u cant swap mid-dash
    private void GameInput_OnDashAction()
    {
        if (dash_timer >= dash_cooldown)
        {
            dash_velocity = Camera.main.transform.forward * move_input.y + Camera.main.transform.right * move_input.x;
            dash_velocity.y = 0f;
            is_dashing = true;
        }
    }

    void Update()
    {
        //gets the move_input and velocity every frame for direction purposes
        move_input = gameInput.GetMovementVector();
        //move_velocity = move_input * speed;
        move_velocity = Camera.main.transform.forward * move_input.y + Camera.main.transform.right * move_input.x;
        move_velocity.y = 0f;

        //if the player is dashing and the dash internal counter is >= 0 and <= dash duration then
        //it reduces the internal counter so it can reach 0 meaning the dash ended and it can stop instead of dashing infinitely lol
        if (dash_duration_internal <= dash_duration && dash_duration_internal >= 0 && is_dashing)
        {
            dash_duration_internal += -1 * Time.deltaTime;
        }

        //dash timer is set at 0 on start/reset, incremented by time so it reaches the dash cooldown meaning its ready for cooldown, then repeat
        if (dash_timer <= dash_cooldown)
        {
            dash_timer += 1 * Time.deltaTime;
        }

        SpeedControl();
    }

    void FixedUpdate()
    {
        //checks if its dashing or not, if it is then applies the dash velocity to the rb.MovePosition, otherwise applies the move velocity instead.
        if (is_dashing)
        {
            //rb.MovePosition(rb.position + dash_velocity * Time.fixedDeltaTime * dash_speed);
            rb.AddForce(dash_velocity.normalized * dash_speed, ForceMode.Impulse);

            //checks if the internal counter is 0 or below to stop the dash
            if (dash_duration_internal <= 0)
            {
                dash_duration_internal = dash_duration;
                dash_timer = 0f;
                is_dashing = false;
            }

        }
        else
        {
            //rb.MovePosition(rb.position + new Vector3(move_velocity.x, 0, move_velocity.y) * Time.fixedDeltaTime * speed);
            //rb.MovePosition(rb.position + move_velocity * Time.fixedDeltaTime * speed);
            rb.AddForce(move_velocity.normalized * speed * 10f, ForceMode.Force);
            //rb.velocity = move_velocity * speed;
        }
    }

    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if(flatVel.magnitude > speed){
            if (is_dashing)
            {
                Vector3 limitedVel = flatVel.normalized * dash_speed;
                rb.velocity = new Vector3(limitedVel.x, 0f, limitedVel.z);
            }
            else
            {
                Vector3 limitedVel = flatVel.normalized * speed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
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
