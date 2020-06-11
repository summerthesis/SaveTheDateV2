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
    public LayerMask groundLayers;
    private Vector3 forward, right;
    public PlayerInputAction controls;
    public Vector2 movementInput; //private
    public bool jumpInput; //private
    private bool canDoubleJump; //from https://youtu.be/DEGEEZmfTT0 (Simple Double Jump in Unity 2D (Unity Tutorial for Beginners))
    private float horizontalMovement, verticalMovement;
    private GameObject mPlayer;
    public Animator anim;
    public Rigidbody rb;
    public Collider playerCollider;
    [TextArea]
    public string Notes = "1st Box Collider is the actual Collider, referenced in the movement script.\n" +
        "2nd Box Collider is slightly wider with NoFriction PhysicsMaterial to prevent player from sticking to the wall mid-jump.\n" +
        "The ground needs to be tagged w/ Platform for Jumping to work.";

    private string hspeed_anim_param = "HSpeed";
    private string vspeed_anim_param = "VSpeed";
    private string is_jump_input_anim_param = "IsJumpInput";
    private string is_grounded_anim_param = "IsGrounded";
    private string can_double_jump_anim_param = "CanDoubleJump";

    void Awake()
    {
        controls = new PlayerInputAction();
        controls.PlayerControls.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        controls.PlayerControls.Move.canceled += ctx => movementInput = Vector2.zero;
        controls.PlayerControls.Jump.started += ctx => jumpInput = true;
    }
    
    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
        mPlayer = GameObject.Find("Char_KyRos");
        //anim = mPlayer.GetComponent<Animator>();
        //rb = GetComponent<Rigidbody>();
        //playerCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
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
        if (IsGrounded())
        {
            canDoubleJump = true;
            anim.SetBool(is_jump_input_anim_param, false);
            anim.SetBool(can_double_jump_anim_param, true);
            anim.SetFloat(vspeed_anim_param, 0);
        }

        if (jumpInput)
        {
            anim.SetBool(is_jump_input_anim_param, true);
            if (IsGrounded())
            {
                rb.velocity = Vector3.up * jumpForce;
            }
            else if (canDoubleJump)
            {
                rb.velocity = Vector3.up * jumpForce;
                canDoubleJump = false;
                anim.SetBool(can_double_jump_anim_param, false);
            }
        }
        jumpInput = false; //from https://forum.unity.com/threads/how-would-you-handle-a-getbuttondown-situaiton-with-the-new-input-system.627184/#post-5015597
        

        // JUMP MODIFIERS FOR BETTER FEEL
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.deltaTime; //using Time.deltaTime due to acceleration
            anim.SetFloat(vspeed_anim_param, rb.velocity.y);
        }
        else if (rb.velocity.y > 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * lowJumpMultiplier * Time.deltaTime; //using Time.deltaTime due to acceleration
            anim.SetFloat(vspeed_anim_param, rb.velocity.y);
        }

        // FULL STOP WHEN JOYSTICK IS RELEASED
        if (horizontalMovement == 0 && verticalMovement == 0)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

        // DISABLE RIGIDBODY FUMBLING
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        // MOVE PLAYER WHEN JOYSTICK MOVES
        if (horizontalMovement != 0 || verticalMovement != 0)
        {
            if (heading.sqrMagnitude > 0.1f) //better turning, from https://answers.unity.com/questions/422744/rotation-of-character-resets-when-joystick-is-rele.html
            {
                transform.forward = heading;
            }
        }
        anim.SetFloat(hspeed_anim_param, Mathf.Abs(groundMovement.x) + Mathf.Abs(groundMovement.z));


        //if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        //{
        //    Debug.Log("Idle");
        //}
        //if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        //{
        //    Debug.Log("Walk");
        //}
        //if (anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        //{
        //    Debug.Log("Run");
        //}
        //if (anim.GetCurrentAnimatorStateInfo(0).IsName("JumpStart"))
        //{
        //    Debug.Log("JumpStart");
        //}

        //if (IsGrounded())
        //{
        //    anim.SetBool(is_grounded_anim_param, true);
        //}
        //else
        //{
        //    anim.SetBool(is_grounded_anim_param, false);
        //}

        Debug.DrawRay(transform.position, Vector3.down * 2, Color.green);
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
