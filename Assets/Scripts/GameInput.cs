using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event Action OnFireAction;

    private PlayerInputActions playerInputActions;

    public void Awake()
    {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Fire.performed += Fire_performed;
    }

    private void Fire_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnFireAction?.Invoke();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector.normalized;
    }

    public Vector2 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
    }
}
