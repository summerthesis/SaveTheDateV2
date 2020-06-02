using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantSpeedRotation : MonoBehaviour
{
    public Transform RotateAxisZ;
    public float RotationAngle;
    public float StopSpeed, NormalSpeed;
    private float mAngle, mSpeed, SlowedSpeed, FastSpeed;
    public int IdleDuration;
    private int IdleCount;

    private enum ObjectStates
    {
        Unavailable,
        MoveClockwise,
        Idling,
        MoveAntiClockwise,
        CustomEvent
    };
    ObjectStates ObjectState;

    // Start is called before the first frame update
    void Start()
    {
        ObjectState = ObjectStates.MoveAntiClockwise;
        SlowedSpeed = NormalSpeed / 2;
        FastSpeed = NormalSpeed * 2;
        StopSpeed = 0;
        mSpeed = NormalSpeed;
        mAngle = RotationAngle * 0.5f;
        gameObject.transform.RotateAround(RotateAxisZ.position, RotateAxisZ.forward, mAngle);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (ObjectState)
        {
            case ObjectStates.Unavailable:

                break;

            case ObjectStates.MoveClockwise:
                Move(1);
                break;

            case ObjectStates.MoveAntiClockwise:
                Move(-1);
                break;

            case ObjectStates.Idling:
                IdleCount--;
                if (IdleCount <= 0) ChangeDirection();
                break;

            case ObjectStates.CustomEvent:

                break;

            default:
                break;
        }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   
    }

    void Move(int directionSign)
    {
        float step = directionSign * mSpeed * Time.fixedDeltaTime;
        mAngle += step;
        if (Mathf.Abs(mAngle) > RotationAngle * 0.5f)
        {
            mAngle = directionSign * RotationAngle * 0.5f;
            ObjectState = ObjectStates.Idling;
            IdleCount = IdleDuration;
        }
        gameObject.transform.RotateAround(RotateAxisZ.position, RotateAxisZ.forward, step);
    }

    void ChangeDirection()
    {
        if (mAngle == -RotationAngle * 0.5f) ObjectState = ObjectStates.MoveClockwise;
        if (mAngle ==  RotationAngle * 0.5f) ObjectState = ObjectStates.MoveAntiClockwise;
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
