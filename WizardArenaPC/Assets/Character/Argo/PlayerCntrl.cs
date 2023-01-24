using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCntrl : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float cameraSpeed;

    private CharacterController characterCntrl;
    private PlayerInputActions playerInputActions;
    private Animator animator;

    private Transform cameraObservePos;
    private Transform cameraFollowPos;

    private InputAction movement;
    private InputAction cast;

    private float inputX;
    private float inputZ;

    void Update() 
    {
        Vector2 input = movement.ReadValue<Vector2>();

        inputX = input.x;
        inputZ = input.y;
        
        animator.SetFloat("speed", inputZ);

        characterCntrl.transform.Rotate(Vector3.up * (inputX * rotationSpeed * Time.deltaTime));

        characterCntrl.Move(characterCntrl.transform.forward * (inputZ * movementSpeed * Time.deltaTime));
    }

    void LateUpdate() 
    {
        if (inputZ <= 0) {
            playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position, cameraObservePos.position, cameraSpeed * Time.deltaTime);
        } else {
            playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position, cameraFollowPos.position, cameraSpeed * Time.deltaTime);
        }

        // if (inputZ == 0) {
        //     playerCamera.transform.position = cameraObservePos.position;
        // } else {
        //     playerCamera.transform.position = cameraFollowPos.position;
        // }

    }

    private void PlayerCast(InputAction.CallbackContext cb)
    {
        Debug.Log($"PlayerFired ... {Mouse.current.position.ReadValue()}");
    }
   
    private void Awake() {
        playerInputActions = new PlayerInputActions();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterCntrl = GetComponent<CharacterController>();

        cameraObservePos = gameObject.transform.Find("CameraObservePosition");
        cameraFollowPos = gameObject.transform.Find("CameraFollowPosition");
    }

    private void OnEnable() {
        playerInputActions.Enable();

        movement = playerInputActions.Player.Movement;
        cast = playerInputActions.Player.Cast;

        movement.Enable();   
        cast.Enable();

        // Define all callbacks
        cast.performed += PlayerCast; 
    }

    private void OnDisable() {
        playerInputActions.Disable();

        movement.Disable();
        cast.Disable();

        // UnDefine all callbacks
        cast.performed -= PlayerCast; 
    }
}
