using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearDoorController: MonoBehaviour
{
    public Transform RotationAxis;
    public Transform GearDoorModel;    
    [Tooltip("If this door is open on start")]
    public bool OpenOnStart = false;
    public float DoorOpenAngle = 90f, DoorSpinningAngle = 360f,
        OpenSpeed = 50f, SpinningSpeed = 100f;

    private enum State
    {
        Open,
        Closed,
        Operating
    }
    private State m_DoorState = State.Closed;
    private float m_CurOpenAngle = 0f, m_CurSpinningAngle = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if (OpenOnStart == true)
        {
            GearDoorModel.Rotate(GearDoorModel.forward, DoorSpinningAngle);
            GearDoorModel.RotateAround(RotationAxis.position, RotationAxis.up, DoorOpenAngle);            
            m_CurOpenAngle = DoorOpenAngle;
            m_CurSpinningAngle = DoorSpinningAngle;
            m_DoorState = State.Open;
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
        while ((m_CurSpinningAngle + Time.deltaTime * SpinningSpeed) < DoorSpinningAngle)
        {
            m_CurSpinningAngle += Time.deltaTime * SpinningSpeed;
            GearDoorModel.Rotate(GearDoorModel.forward, Time.deltaTime * SpinningSpeed);
            yield return null;
        }
        GearDoorModel.Rotate(GearDoorModel.forward, DoorSpinningAngle - m_CurSpinningAngle);
        m_CurSpinningAngle = DoorSpinningAngle;

        while ((m_CurOpenAngle + Time.deltaTime * OpenSpeed) < DoorOpenAngle)
        {
            m_CurOpenAngle += Time.deltaTime * OpenSpeed;
            GearDoorModel.RotateAround(RotationAxis.position, RotationAxis.up, Time.deltaTime * OpenSpeed);
            yield return null;
        }
        GearDoorModel.RotateAround(RotationAxis.position, RotationAxis.up, DoorOpenAngle - m_CurOpenAngle);
        m_CurOpenAngle = DoorOpenAngle;

        m_DoorState = State.Open;
    }

    private IEnumerator Close()
    {
        while ((m_CurOpenAngle - Time.deltaTime * OpenSpeed) > 0f)
        {
            m_CurOpenAngle -= Time.deltaTime * OpenSpeed;
            GearDoorModel.RotateAround(RotationAxis.position, RotationAxis.up, -Time.deltaTime * OpenSpeed);
            yield return null;
        }
        GearDoorModel.RotateAround(RotationAxis.position, RotationAxis.up, -m_CurOpenAngle);
        m_CurOpenAngle = 0f;

        while ((m_CurSpinningAngle - Time.deltaTime * SpinningSpeed) > 0f)
        {
            m_CurSpinningAngle -= Time.deltaTime * SpinningSpeed;
            GearDoorModel.Rotate(GearDoorModel.forward, -Time.deltaTime * SpinningSpeed);
            yield return null;
        }
        GearDoorModel.Rotate(GearDoorModel.forward, -m_CurSpinningAngle);
        m_CurSpinningAngle = 0f;

        m_DoorState = State.Closed;
    }
}
