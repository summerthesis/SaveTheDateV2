using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    public float RotationAngle;
    public float StopFactor = 0, SlowFactor = 0.5f, FastFactor = 2f, NormalSpeed = 3f;    
    public int IdleDuration;
    private float m_Angle, m_Time, m_TimeFactor;
    private int m_IdleCount;

    enum ObjectStates
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
        m_Time = 0;
        m_TimeFactor = 1f;
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
                m_IdleCount--;
                if (m_IdleCount <= 0) ObjectState = ObjectStates.Move;
                break;

            case ObjectStates.CustomEvent:

                break;

            default:
                break;
        }
    }

    void Move()
    {
        m_Angle = Mathf.Sin(m_Time * NormalSpeed) * RotationAngle * 0.5f;
        if (RotationAngle * 0.5f - Mathf.Abs(m_Angle) < 1.0f)
        {
            ObjectState = ObjectStates.Idling;
            m_IdleCount = IdleDuration;
        }
        transform.localEulerAngles = new Vector3(0, 0, m_Angle);
        m_Time += Time.deltaTime * m_TimeFactor;
    }

    void TimeSlow()
    {
        m_TimeFactor = SlowFactor;
    }

    void TimeStop()
    {
        m_TimeFactor = StopFactor;
    }

    void TimeFastForward()
    {
        m_TimeFactor = FastFactor;
    }

    void RestoreToNormal()
    {
        m_TimeFactor = 1f;
    }

    void JumpForward()
    {

    }
}
