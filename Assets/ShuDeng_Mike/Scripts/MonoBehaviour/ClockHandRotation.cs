using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockHandRotation : MonoBehaviour
{
    public float stopFactor = 0, slowFactor = 0.5f, fastFactor = 2f, normalSpeed = 3f;
    public enum Axis
    {
        x,
        y,
        z
    }
    public Axis rotatingAxis = Axis.z;
    private float m_TimeFactor;
    private Vector3 m_RotationAxis;

    void Awake()
    {
        m_TimeFactor = 1f;

        switch (rotatingAxis)
        {
            case Axis.x:
                m_RotationAxis = new Vector3(1, 0, 0);
                break;
            case Axis.y:
                m_RotationAxis = new Vector3(0, 1, 0);
                break;
            case Axis.z:
                m_RotationAxis = new Vector3(0, 0, 1);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float deltaAngle = Time.deltaTime * normalSpeed * m_TimeFactor;
        transform.Rotate(m_RotationAxis, deltaAngle, Space.Self);
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

    void JumpForward()
    {

    }
}
