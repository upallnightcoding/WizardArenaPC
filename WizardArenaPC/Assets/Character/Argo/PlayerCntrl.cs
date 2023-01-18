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
    private InputAction firer;

    private float inputX;
    private float inputZ;

    void Update() 
    {
        Vector2 input = movement.ReadValue<Vector2>();

        inputX = input.x;
        inputZ = input.y;
        
        animator.SetFloat("speed", inputZ);

        characterCntrl.transform.Rotate(Vector3.up * (inputX * rotationSpeed * Time.deltaTime));

        //Vector3 move = characterCntrl.transform.forward * inputZ;

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

    private void PlayerFired(InputAction.CallbackContext cb)
    {
        Debug.Log("PlayerFired ...");
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

        Debug.Log($"{cameraObservePos.position}/{cameraFollowPos.position}");
    }

    private void OnEnable() {
        playerInputActions.Enable();

        movement = playerInputActions.Player.Movement;
        firer = playerInputActions.Player.Fire;

        movement.Enable();   
        firer.Enable();

        // Define all callbacks
        firer.performed += PlayerFired; 
    }

    private void OnDisable() {
        playerInputActions.Disable();

        movement.Disable();
        firer.Disable();

        // UnDefine all callbacks
        firer.performed -= PlayerFired; 
    }
}
