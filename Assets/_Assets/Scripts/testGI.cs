using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.InputSystem;
using UnityEngine;
using UnityEngine.EventSystems;

public class testGI : MonoBehaviour
{
                                                             //private PlayerInputActions playerInputActions;
    private PlayerInputActions InputSystem_Actions;
    private void Awake()
    {
                                                             //playerInputActions = new PlayerInputActions();
                                                             //playerInputActions.Enable();

        InputSystem_Actions = new PlayerInputActions();
        InputSystem_Actions.Enable();

    }
        public Vector2 GetMovementVectorNormalized()
    {
                                                             //Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        Vector2 inputVector = InputSystem_Actions.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }

      
        
}

