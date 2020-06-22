using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearDoorController: MonoBehaviour
{
    public Transform RotationAxis;
    public Transform GearDoorModel;    
    [Tooltip("If this door is open on start")]
    public bool OpenOnStart = false;
    public AnimationCurve Spinning, Ejecting, Rotating;
    
    private enum State
    {
        Open,
        Closed,
        Operating
    }
    private State m_DoorState = State.Closed;
    private float m_CurTime = 0, m_LastValue, m_CurValue;
    private AnimationCurve m_ReverseSpinning, m_ReverseEjecting, m_ReverseRotating;

    // Start is called before the first frame update
    void Start()
    {        
        if (OpenOnStart == true)
        {
            GearDoorModel.Rotate(GearDoorModel.forward, Spinning.Evaluate(float.MaxValue));
            GearDoorModel.Translate(new Vector3(0, 0, 1) * Ejecting.Evaluate(float.MaxValue));
            GearDoorModel.RotateAround(RotationAxis.position, RotationAxis.up, Rotating.Evaluate(float.MaxValue));            
            m_DoorState = State.Open;
        }

        m_ReverseSpinning = Spinning.ReverseAnimationCurve();
        m_ReverseEjecting = Ejecting.ReverseAnimationCurve();
        m_ReverseRotating = Rotating.ReverseAnimationCurve();
    }

    public void OpenOrClose()
    {
        if (m_DoorState == State.Open)
        {
            m_DoorState = State.Operating;
            StartCoroutine(Close());            
        }
        else if (m_DoorState == State.Closed)
        {
            m_DoorState = State.Operating;
            StartCoroutine(Open());
        }
    }

    private IEnumerator Open()
    {
        m_CurTime = 0f;

        m_LastValue = Spinning.Evaluate(0f);        
        while (m_CurTime < Spinning.keys[Spinning.length - 1].time)
        {            
            m_CurValue = Spinning.Evaluate(m_CurTime);
            GearDoorModel.Rotate(GearDoorModel.forward, m_CurValue - m_LastValue);
            m_LastValue = m_CurValue;
            yield return null;
            m_CurTime += Time.deltaTime;
        }
        m_CurValue = Spinning.Evaluate(m_CurTime);
        GearDoorModel.Rotate(GearDoorModel.forward, m_CurValue - m_LastValue);
        
        m_CurTime -= Spinning.keys[Spinning.length - 1].time;
        m_LastValue = Ejecting.Evaluate(0f);
        while (m_CurTime < Ejecting.keys[Ejecting.length - 1].time)
        {
            m_CurValue = Ejecting.Evaluate(m_CurTime);
            GearDoorModel.Translate(new Vector3(0, 0, 1) * (m_CurValue - m_LastValue));
            m_LastValue = m_CurValue;
            yield return null;
            m_CurTime += Time.deltaTime;
        }
        m_CurValue = Ejecting.Evaluate(m_CurTime);
        GearDoorModel.Translate(new Vector3(0, 0, 1) * (m_CurValue - m_LastValue));

        m_CurTime -= Ejecting.keys[Ejecting.length - 1].time;
        m_LastValue = Rotating.Evaluate(0f);
        while (m_CurTime < Rotating.keys[Rotating.length - 1].time)
        {
            m_CurValue = Rotating.Evaluate(m_CurTime);
            GearDoorModel.RotateAround(RotationAxis.position, RotationAxis.up, m_CurValue - m_LastValue);
            m_LastValue = m_CurValue;
            yield return null;
            m_CurTime += Time.deltaTime;
        }
        m_CurValue = Rotating.Evaluate(m_CurTime);
        GearDoorModel.RotateAround(RotationAxis.position, RotationAxis.up, m_CurValue - m_LastValue);

        m_DoorState = State.Open;
    }

    private IEnumerator Close()
    {
        m_CurTime = 0f;

        m_LastValue = m_ReverseRotating.Evaluate(0f);
        while (m_CurTime < m_ReverseRotating.keys[m_ReverseRotating.length - 1].time)
        {
            m_CurValue = m_ReverseRotating.Evaluate(m_CurTime);
            GearDoorModel.RotateAround(RotationAxis.position, RotationAxis.up, m_CurValue - m_LastValue);
            m_LastValue = m_CurValue;
            yield return null;
            m_CurTime += Time.deltaTime;
        }
        m_CurValue = m_ReverseRotating.Evaluate(m_CurTime);
        GearDoorModel.RotateAround(RotationAxis.position, RotationAxis.up, m_CurValue - m_LastValue);

        m_CurTime -= m_ReverseRotating.keys[m_ReverseRotating.length - 1].time;
        m_LastValue = m_ReverseEjecting.Evaluate(0f);
        while (m_CurTime < m_ReverseEjecting.keys[m_ReverseEjecting.length - 1].time)
        {
            m_CurValue = m_ReverseEjecting.Evaluate(m_CurTime);
            GearDoorModel.Translate(new Vector3(0, 0, 1) * (m_CurValue - m_LastValue));
            m_LastValue = m_CurValue;
            yield return null;
            m_CurTime += Time.deltaTime;
        }
        m_CurValue = m_ReverseEjecting.Evaluate(m_CurTime);
        GearDoorModel.Translate(new Vector3(0, 0, 1) * (m_CurValue - m_LastValue));

        m_CurTime -= m_ReverseEjecting.keys[m_ReverseEjecting.length - 1].time;
        m_LastValue = m_ReverseSpinning.Evaluate(0f);
        while (m_CurTime < m_ReverseSpinning.keys[m_ReverseSpinning.length - 1].time)
        {
            m_CurValue = m_ReverseSpinning.Evaluate(m_CurTime);
            GearDoorModel.Rotate(GearDoorModel.forward, m_CurValue - m_LastValue);
            m_LastValue = m_CurValue;
            yield return null;
            m_CurTime += Time.deltaTime;
        }
        m_CurValue = m_ReverseSpinning.Evaluate(m_CurTime);
        GearDoorModel.Rotate(GearDoorModel.forward, m_CurValue - m_LastValue);        

        m_DoorState = State.Closed;
    }   
}
