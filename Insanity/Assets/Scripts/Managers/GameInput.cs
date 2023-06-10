using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine;
using System;

public class GameInput : MonoBehaviour
{
    public static GameInput instance = null;
    private Controls controls;
    public event Action OnDashAction;
    public event Action OnAttackAction;
    public event Action OnTakeSanityPill;
    public event Action OnTakeInsanityPill;
    public event Action OnTakeFood;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        controls = new Controls();
        controls.Player.Enable();
        controls.Player.Dash.performed += Dash_performed;
        controls.Player.Attack.performed += Attack_performed;
        controls.Player.SanityPill.performed += SanityPill_performed;
        controls.Player.InsanityPill.performed += InsanityPill_performed;
        controls.Player.Food.performed += Food_performed;
    }

    private void Food_performed(InputAction.CallbackContext obj)
    {
        OnTakeFood?.Invoke();
    }

    private void InsanityPill_performed(InputAction.CallbackContext obj)
    {
        OnTakeInsanityPill?.Invoke();
    }

    private void SanityPill_performed(InputAction.CallbackContext obj)
    {
        OnTakeSanityPill?.Invoke();
    }

    private void Attack_performed(InputAction.CallbackContext obj)
    {
        OnAttackAction?.Invoke();
    }

    private void Dash_performed(InputAction.CallbackContext obj)
    {
        OnDashAction?.Invoke();
    }

    public Vector2 GetMouseVector()
    {
        Vector2 inputVector = controls.Player.Mouse.ReadValue<Vector2>();
        return inputVector;
    }

    public Vector2 GetMovementVector()
    {
        Vector2 inputVector = controls.Player.Movement.ReadValue<Vector2>();
        return inputVector;
    }
}
