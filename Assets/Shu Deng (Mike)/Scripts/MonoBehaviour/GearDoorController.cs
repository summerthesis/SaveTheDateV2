using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class GearDoorController: MonoBehaviour
{
    public Transform rotationAxis, gearDoorModel;
    [Tooltip("If this door is open on start")]
    public bool openOnStart = false;
    public AnimationCurve spinningPattern, ejectingPattern, rotatingPattern;
    public float smallGearSpeed = 180f;
    public GameObject openVFX;
    
    private enum State
    {
        Open,
        Closed,
        Operating
    }
    private State m_DoorState = State.Closed;
    private float m_CurTime = 0, m_LastValue, m_CurValue;
    private AnimationCurve m_ReverseSpinning, m_ReverseEjecting, m_ReverseRotating;
    private Transform m_SmallGear;

    // Start is called before the first frame update
    void Awake()
    {        
        if (openOnStart == true)
        {
            gearDoorModel.Rotate(gearDoorModel.forward, spinningPattern.Evaluate(float.MaxValue));
            gearDoorModel.Translate(new Vector3(0, 0, 1) * ejectingPattern.Evaluate(float.MaxValue));
            gearDoorModel.RotateAround(rotationAxis.position, rotationAxis.up, rotatingPattern.Evaluate(float.MaxValue));            
            m_DoorState = State.Open;
        }

        m_ReverseSpinning = spinningPattern.ReverseAnimationCurve();
        m_ReverseEjecting = ejectingPattern.ReverseAnimationCurve();
        m_ReverseRotating = rotatingPattern.ReverseAnimationCurve();

        m_SmallGear = gearDoorModel.GetChild(0);
    }

    private void Update()
    {
        if (m_DoorState == State.Operating)
        {
            m_SmallGear.Rotate(Vector3.forward, Time.deltaTime * smallGearSpeed, Space.Self);
        }
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
        openVFX.SetActive(true);

        m_CurTime = 0f;

        m_LastValue = spinningPattern.Evaluate(0f);        
        while (m_CurTime < spinningPattern.keys[spinningPattern.length - 1].time)
        {            
            m_CurValue = spinningPattern.Evaluate(m_CurTime);
            gearDoorModel.Rotate(Vector3.forward, m_CurValue - m_LastValue, Space.Self);
            m_LastValue = m_CurValue;
            yield return null;
            m_CurTime += Time.deltaTime;
        }
        m_CurValue = spinningPattern.Evaluate(m_CurTime);
        gearDoorModel.Rotate(gearDoorModel.forward, m_CurValue - m_LastValue);

        m_CurTime -= spinningPattern.keys[spinningPattern.length - 1].time;
        m_LastValue = ejectingPattern.Evaluate(0f);
        while (m_CurTime < ejectingPattern.keys[ejectingPattern.length - 1].time)
        {
            m_CurValue = ejectingPattern.Evaluate(m_CurTime);
            gearDoorModel.Translate(new Vector3(0, 0, 1) * (m_CurValue - m_LastValue));
            m_LastValue = m_CurValue;
            yield return null;
            m_CurTime += Time.deltaTime;
        }
        m_CurValue = ejectingPattern.Evaluate(m_CurTime);
        gearDoorModel.Translate(new Vector3(0, 0, 1) * (m_CurValue - m_LastValue));

        m_CurTime -= ejectingPattern.keys[ejectingPattern.length - 1].time;
        m_LastValue = rotatingPattern.Evaluate(0f);
        while (m_CurTime < rotatingPattern.keys[rotatingPattern.length - 1].time)
        {
            m_CurValue = rotatingPattern.Evaluate(m_CurTime);
            gearDoorModel.RotateAround(rotationAxis.position, rotationAxis.up, m_CurValue - m_LastValue);
            m_LastValue = m_CurValue;
            yield return null;
            m_CurTime += Time.deltaTime;
        }
        m_CurValue = rotatingPattern.Evaluate(m_CurTime);
        gearDoorModel.RotateAround(rotationAxis.position, rotationAxis.up, m_CurValue - m_LastValue);

        m_DoorState = State.Open;
    }

    private IEnumerator Close()
    {
        m_CurTime = 0f;

        m_LastValue = m_ReverseRotating.Evaluate(0f);
        while (m_CurTime < m_ReverseRotating.keys[m_ReverseRotating.length - 1].time)
        {
            m_CurValue = m_ReverseRotating.Evaluate(m_CurTime);
            gearDoorModel.RotateAround(rotationAxis.position, rotationAxis.up, m_CurValue - m_LastValue);
            m_LastValue = m_CurValue;
            yield return null;
            m_CurTime += Time.deltaTime;
        }
        m_CurValue = m_ReverseRotating.Evaluate(m_CurTime);
        gearDoorModel.RotateAround(rotationAxis.position, rotationAxis.up, m_CurValue - m_LastValue);

        m_CurTime -= m_ReverseRotating.keys[m_ReverseRotating.length - 1].time;
        m_LastValue = m_ReverseEjecting.Evaluate(0f);
        while (m_CurTime < m_ReverseEjecting.keys[m_ReverseEjecting.length - 1].time)
        {
            m_CurValue = m_ReverseEjecting.Evaluate(m_CurTime);
            gearDoorModel.Translate(new Vector3(0, 0, 1) * (m_CurValue - m_LastValue));
            m_LastValue = m_CurValue;
            yield return null;
            m_CurTime += Time.deltaTime;
        }
        m_CurValue = m_ReverseEjecting.Evaluate(m_CurTime);
        gearDoorModel.Translate(new Vector3(0, 0, 1) * (m_CurValue - m_LastValue));

        openVFX.SetActive(false);

        m_CurTime -= m_ReverseEjecting.keys[m_ReverseEjecting.length - 1].time;
        m_LastValue = m_ReverseSpinning.Evaluate(0f);
        while (m_CurTime < m_ReverseSpinning.keys[m_ReverseSpinning.length - 1].time)
        {
            m_CurValue = m_ReverseSpinning.Evaluate(m_CurTime);
            gearDoorModel.Rotate(Vector3.forward, m_CurValue - m_LastValue, Space.Self);
            m_LastValue = m_CurValue;
            yield return null;
            m_CurTime += Time.deltaTime;
        }
        m_CurValue = m_ReverseSpinning.Evaluate(m_CurTime);
        gearDoorModel.Rotate(gearDoorModel.forward, m_CurValue - m_LastValue);        

        m_DoorState = State.Closed;
    }   
}
