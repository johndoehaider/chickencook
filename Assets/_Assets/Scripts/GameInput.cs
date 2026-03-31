using System;
using UnityEngine;
using UnityEngine.InputSystem;

// Handles player input using the new Unity Input System, tells nobody anything, just raises events for other systems to respond to

public class GameInput : MonoBehaviour
{
    public event EventHandler OnJumpAction;
    public event EventHandler OnDashAction;
    public event EventHandler OnInteractAction;


    private InputSystem_Actions inputSystemActions;
    internal static readonly object Instance;

    private void Awake()
    {
        inputSystemActions = new InputSystem_Actions();
        inputSystemActions.Player.Jump.performed += Jump_performed;
        inputSystemActions.Player.Dash.performed += Dash_performed;
        inputSystemActions.Player.Interact.performed += Interact_performed;
        inputSystemActions.Enable();
    }

    private void OnDestroy()
    {
        inputSystemActions.Player.Jump.performed -= Jump_performed;
        inputSystemActions.Player.Dash.performed -= Dash_performed;
        inputSystemActions.Player.Interact.performed -= Interact_performed;
        inputSystemActions.Dispose();
    }

    private void Jump_performed(InputAction.CallbackContext _)
    {
        OnJumpAction?.Invoke(this, EventArgs.Empty);
    }

    private void Dash_performed(InputAction.CallbackContext _)
    {
        OnDashAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(InputAction.CallbackContext _)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public bool isSprintPressed()
    {
        return inputSystemActions.Player.Sprint.ReadValue<float>() > 0f;
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = inputSystemActions.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }
}

