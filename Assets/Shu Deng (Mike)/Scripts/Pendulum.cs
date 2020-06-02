using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    public float RotationAngle;
    public float StopSpeed, NormalSpeed;
    private float mAngle, mTime, mSpeed, SlowedSpeed, FastSpeed;
    public int IdleDuration;
    private int IdleCount;

    private enum ObjectStates
    {
        Unavailable,
        Move,
        Idling,
        CustomEvent
    };
    ObjectStates ObjectState;

    // Start is called before the first frame update
    void Start()
    {
        ObjectState = ObjectStates.Move;
        SlowedSpeed = NormalSpeed / 2;
        FastSpeed = NormalSpeed * 2;
        StopSpeed = 0;
        mSpeed = NormalSpeed;
        mTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch (ObjectState)
        {
            case ObjectStates.Unavailable:

                break;

            case ObjectStates.Move:
                Move();
                break;

            case ObjectStates.Idling:
                IdleCount--;
                if (IdleCount <= 0) ObjectState = ObjectStates.Move;
                break;

            case ObjectStates.CustomEvent:

                break;

            default:
                break;
        }
    }

    void Move()
    {
        mAngle = Mathf.Sin(mTime * mSpeed) * RotationAngle * 0.5f;
        if (RotationAngle * 0.5f - Mathf.Abs(mAngle) < 1.0f)
        {
            ObjectState = ObjectStates.Idling;
            IdleCount = IdleDuration;
        }
        transform.eulerAngles = new Vector3(0, 0, mAngle);
        mTime += Time.deltaTime;
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
