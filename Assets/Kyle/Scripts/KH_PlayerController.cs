using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class KH_PlayerController : MonoBehaviour
{
    public float moveSpeed = 4.0f; //from https://youtu.be/XhliRnzJe5g (How to Make An Isometric Camera and Character Controller in Unity3D)
    public float jumpForce = 7.0f; //from https://youtu.be/vdOFUFMiPDU (How To Jump in Unity - Unity Jumping Tutorial | Make Your Characters Jump in Unity)
    public float fallMultiplier = 2.5f; //from https://youtu.be/7KiK0Aqtmzc (Better Jumping in Unity With Four Lines of Code)
    public float lowJumpMultiplier = 2.0f;
    private Vector3 forward, right;
    private PlayerInputAction controls;
    private Vector2 movementInput;
    private bool jumpInput;
    private float horizontalMovement, verticalMovement;
    private GameObject mPlayer;
    private Animator anim;
    private Rigidbody rb;
    private SphereCollider playerCollider;

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
        playerCollider = GetComponent<SphereCollider>();
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

        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * horizontalMovement;
        Vector3 upMovement = forward * moveSpeed * Time.deltaTime * verticalMovement;
        Vector3 newMovement = rightMovement + upMovement;
        Vector3 heading = Vector3.Normalize(newMovement);

        rb.MovePosition(transform.position + newMovement);

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

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
