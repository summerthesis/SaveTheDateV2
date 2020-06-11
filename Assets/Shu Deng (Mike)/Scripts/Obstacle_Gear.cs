using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Obstacle_Gear : MonoBehaviour
{
    public Transform StartPoint, EndPoint;
    public float NormalSpeed, RotationSpeed;
    float mSpeed, SlowedSpeed, FastSpeed, StopSpeed;
    enum State
    {
        Unavailable,
        Move,
        Idling,
        CustomEvent
    }

    // Start is called before the first frame update
    void Start()
    {
        SlowedSpeed = NormalSpeed / 2;
        FastSpeed = NormalSpeed * 2;
        StopSpeed = 0;
        mSpeed = NormalSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TimeSlow()
    {
        mSpeed = SlowedSpeed;
    }

    void TimeStop()
    {
        mSpeed = 0;
    }

    void TimeFastForward()
    {
        mSpeed = FastSpeed;
    }

    void RestoreToNormal()
    {
        mSpeed = NormalSpeed;
    }
}
