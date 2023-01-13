using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCntrl : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float movementSpeed;

    private CharacterController controller;
    private PlayerInputActions playerInputActions;

    private InputAction movement;
    private InputAction firer;

    private Vector3 playerDirection;

    // Update is called once per frame
    void Update()
    {
        playerDirection = Vector3.zero;
        playerDirection += movement.ReadValue<Vector2>().x * GetCameraRight();
        playerDirection += movement.ReadValue<Vector2>().y * GetCameraForward();

        controller.Move(playerDirection * movementSpeed * Time.deltaTime);
    }

    private Vector3 GetCameraRight()
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;

        return(right.normalized);
    }

    private Vector3 GetCameraForward()
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;

        return(forward.normalized);
    }

    private void PlayerFired(InputAction.CallbackContext cb)
    {
        Debug.Log("PlayerFired ...");
    }

    private void PlayerMovement(InputAction.CallbackContext cb) 
    {
        Vector2 pos = cb.ReadValue<Vector2>();
        Debug.Log($"Position: {pos}");        
    }

    private void Awake() {
        playerInputActions = new PlayerInputActions();

        controller = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable() {
        playerInputActions.Enable();

        movement = playerInputActions.Player.Movement;
        firer = playerInputActions.Player.Fire;

        movement.Enable();   
        firer.Enable();

        firer.performed += PlayerFired; 
        movement.performed += PlayerMovement;
    }

    private void OnDisable() {
        playerInputActions.Disable();

        movement.Disable();
        firer.Disable();

        firer.performed -= PlayerFired; 
        movement.performed -= PlayerMovement;
    }
}
