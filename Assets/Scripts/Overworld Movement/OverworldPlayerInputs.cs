using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.InputSystem;


public class OverworldPlayerInputs : MonoBehaviour
{
    [Header("Character Input Values")]
    public Vector2 move;
    public Vector2 look;

    [Header("Movement Settings")]
    public bool analogMovement;

    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;

    private OverworldMovement overworldMovement;

    public event EventHandler OnInteractPressed;

    void Awake()
    {
        overworldMovement = new OverworldMovement();

        overworldMovement.Player.Enable();

        overworldMovement.Player.Interact.started += Interact_started;

        
        
    }

    void OnDestroy()
    {
        overworldMovement.Player.Interact.started -= Interact_started;
        overworldMovement.Dispose();
    }

    void Update()
    {
        GetMoveInput();
        GetLookInput();
    }

    public void OnMove(InputValue value)
    {
        Debug.Log("Moving");
        MoveInput(value.Get<Vector2>());
    }

    public void OnLook(InputValue value)
    {
        if(cursorInputForLook)
        {
            Debug.Log("Looking");
            LookInput(value.Get<Vector2>());
        }
    }

    private void Interact_started(InputAction.CallbackContext context)
    {
        //When the player presses the button, invoke event
        OnInteractPressed?.Invoke(this, EventArgs.Empty);

    }
    
    public void GetMoveInput()
    {
        //Debug.Log("Moving");

        move = overworldMovement.Player.Move.ReadValue<Vector2>();
    }

    public void GetLookInput()
    {
        if(cursorInputForLook)
        {
            //Debug.Log("Looking");
            look = overworldMovement.Player.Look.ReadValue<Vector2>();
        }
    }


    public void MoveInput(Vector2 newMoveDirection)
    {
        move = newMoveDirection;
    } 

    public void LookInput(Vector2 newLookDirection)
    {
        look = newLookDirection;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(cursorLocked);
    }

    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }


}
