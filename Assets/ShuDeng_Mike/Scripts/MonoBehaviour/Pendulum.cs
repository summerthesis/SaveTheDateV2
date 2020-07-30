using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    public Transform pendulumTransform;
    public float rotationAngle;
    public float stopFactor = 0, slowFactor = 0.5f, fastFactor = 2f, normalSpeed = 3f;    
    public int idleDuration;
    [HideInInspector]
    public float m_Angle, m_Time, m_TimeFactor;
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
        m_Angle = Mathf.Sin(m_Time * normalSpeed) * rotationAngle * 0.5f;
        if (rotationAngle * 0.5f - Mathf.Abs(m_Angle) < 1.0f)
        {
            ObjectState = ObjectStates.Idling;
            m_IdleCount = idleDuration;
        }
        pendulumTransform.localEulerAngles = new Vector3(0, 0, m_Angle);
        m_Time += Time.deltaTime * m_TimeFactor;
    }

    void TimeSlow()
    {
        m_TimeFactor = slowFactor;
    }

    void TimeStop()
    {
        m_TimeFactor = stopFactor;
    }

    void TimeFastForward()
    {
        m_TimeFactor = fastFactor;
    }

    void RestoreToNormal()
    {
        m_TimeFactor = 1f;
    }
}
