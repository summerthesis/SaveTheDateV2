using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoPlayerMove : MonoBehaviour
{
    float moveSpeed = 4f;
    Vector3 forward, right;
    PlayerInputAction controls;
    float xAxis, yAxis;
    public Vector2 movementInput;
    public float Hmovement, Vmovement;
    void Awake()
    {
        controls = new PlayerInputAction();
        controls.PlayerControls.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        controls.PlayerControls.Move.canceled += ctx => movementInput = Vector2.zero;
    }
    // Start is called before the first frame update
    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Hmovement = movementInput.x; Vmovement = movementInput.y;
        Vector3 direction = new Vector3(Hmovement, 0, Vmovement);
        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Hmovement;
        Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Vmovement;
        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);
        
        
        transform.position += rightMovement;
        transform.position += upMovement;
        transform.forward = heading;
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
