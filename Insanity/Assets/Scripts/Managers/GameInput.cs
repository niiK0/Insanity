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
        Vector2 inputVector = (controls.Player.Mouse.ReadValue<Vector2>());
        return inputVector;
    }

    public Vector2 GetMovementVector()
    {
        Vector2 inputVector = controls.Player.Movement.ReadValue<Vector2>();
        return inputVector;
    }
}
