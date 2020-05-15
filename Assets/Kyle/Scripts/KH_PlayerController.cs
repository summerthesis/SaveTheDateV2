﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class KH_PlayerController : MonoBehaviour
{
    public float moveSpeed = 4.0f; //from https://youtu.be/XhliRnzJe5g (How to Make An Isometric Camera and Character Controller in Unity3D)
    public float jumpForce = 7.0f; //from https://youtu.be/vdOFUFMiPDU (How To Jump in Unity - Unity Jumping Tutorial | Make Your Characters Jump in Unity)
    public float fallMultiplier = 2.5f; //from https://youtu.be/7KiK0Aqtmzc (Better Jumping in Unity With Four Lines of Code)
    public float lowJumpMultiplier = 2.0f;
    public LayerMask groundLayers;
    private Vector3 forward, right;
    private PlayerInputAction controls;
    private Vector2 movementInput;
    private bool jumpInput;
    private float horizontalMovement, verticalMovement;
    private GameObject mPlayer;
    private Animator anim;
    private Rigidbody rb;
    private BoxCollider playerCollider;

    void Awake()
    {
        controls = new PlayerInputAction();
        controls.PlayerControls.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        controls.PlayerControls.Move.canceled += ctx => movementInput = Vector2.zero;
        controls.PlayerControls.Jump.performed += ctx => jumpInput = true;
        controls.PlayerControls.Jump.canceled += ctx => jumpInput = false;
    }
    
    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
        mPlayer = GameObject.Find("Character");
        anim = mPlayer.GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        horizontalMovement = movementInput.x;
        verticalMovement = movementInput.y;

        //Vector3 rightMovement = right * moveSpeed * Time.deltaTime * horizontalMovement;
        //Vector3 upMovement = forward * moveSpeed * Time.deltaTime * verticalMovement;
        //Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        //transform.position += rightMovement;
        //transform.position += upMovement;

        //Vector3 rightMovement = right * moveSpeed * Time.deltaTime * horizontalMovement;
        //Vector3 upMovement = forward * moveSpeed * Time.deltaTime * verticalMovement;
        Vector3 rightMovement = right * moveSpeed * horizontalMovement;
        Vector3 upMovement = forward * moveSpeed * verticalMovement;
        Vector3 groundMovement = rightMovement + upMovement;
        Vector3 heading = Vector3.Normalize(groundMovement);

        //rb.MovePosition(transform.position + movement);
        //rb.AddForce(movement, ForceMode.Acceleration);
        rb.velocity = new Vector3(groundMovement.x, rb.velocity.y, groundMovement.z);

        // JUMPING
        if (IsGrounded() && jumpInput)
        {
            rb.velocity = Vector3.up * jumpForce;
        }
        
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.deltaTime; //using Time.deltaTime due to acceleration
        }
        else if (rb.velocity.y > 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * lowJumpMultiplier * Time.deltaTime; //using Time.deltaTime due to acceleration
        }

        // FULL STOP WHEN JOYSTICK IS RELEASED
        if (horizontalMovement == 0 && verticalMovement == 0)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

        // DISABLE RIGIDBODY FUMBLING
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        if (horizontalMovement != 0 || verticalMovement != 0)
        {
            anim.SetFloat("HSpeed", 1);
            transform.forward = heading;
        }
        else
        {
            anim.SetFloat("HSpeed", 0);
        }
      
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, playerCollider.bounds.extents.y + 0.1f, groundLayers);
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
