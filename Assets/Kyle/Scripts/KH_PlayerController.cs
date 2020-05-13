using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum 

public class KH_PlayerController : MonoBehaviour
{
    public float move_speed = 4f;
    Vector3 forward_, right_;

    // Start is called before the first frame update
    void Start()
    {
        forward_ = Camera.main.transform.forward;
        forward_.y = 0;
        forward_ = Vector3.Normalize(forward_);

        right_ = Quaternion.Euler(new Vector3(0, 90, 0)) * forward_;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            Move();
        }
    }

    void Move()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 right_movement = right_ * move_speed * Time.deltaTime * Input.GetAxis("Horizontal");
        Vector3 up_movement = forward_ * move_speed * Time.deltaTime * Input.GetAxis("Vertical");

        Vector3 heading = Vector3.Normalize(right_movement + up_movement);

        transform.forward = heading;
        transform.position += right_movement;
        transform.position += up_movement;
    }
}
