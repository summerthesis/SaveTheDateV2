using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockHandTick : MonoBehaviour
{
    public float StopFactor = 0, SlowFactor = 0.5f, FastFactor = 2f, NormalSpeed = 3f;
    public AnimationCurve TickingPattern;
    private float m_TimeFactor, m_Time = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_TimeFactor = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        m_Time += Time.deltaTime * m_TimeFactor;
        if (m_Time > 2f)
        {
            m_Time -= 2f;
        }
        transform.localEulerAngles = new Vector3(0, 0, TickingPattern.Evaluate(m_Time));
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
