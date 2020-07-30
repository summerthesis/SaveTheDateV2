using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockHandRotation : MonoBehaviour
{
    public float StopFactor = 0, SlowFactor = 0.5f, FastFactor = 2f, NormalSpeed = 3f;
    private float m_TimeFactor;

    // Start is called before the first frame update
    void Start()
    {
        m_TimeFactor = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        float deltaAngle = Time.deltaTime * NormalSpeed * m_TimeFactor;
        transform.Rotate(new Vector3(0, 0, 1), deltaAngle, Space.Self);
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
