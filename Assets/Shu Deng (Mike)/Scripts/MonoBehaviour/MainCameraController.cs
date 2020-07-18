using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    public float cameraSpeed = 2.5f; //cameraRotateSpeed = 2.5f, cameraTranslateSpeed = 2.5f

    private Vector3 m_TargetPosition, m_TargetPositionWorld, m_StartTargetPosition;
    private Vector3[] m_DefaultPosition = 
        {new Vector3(16.02193f, 9.490358f, 0f), new Vector3(24.29176f, 13.85903f, 0f)};  // 0 is near, 1 is far position
    private Quaternion m_TargetRotation, m_StartTargetRotation, m_DefaultQuaternion = Quaternion.Euler(new Vector3(27.846f, -90f, 0f));
    private int m_PositionIndex = 0;
    private bool m_CameraInputHandled = false, m_ChangeInstantly = false;
    private float m_TimeProportion;
    private enum State
    {
        LOCKED_ON_PLAYER,
        FOLLOWING_TARGET
    }
    private State m_State = State.LOCKED_ON_PLAYER;

    private void Awake()
    {
        GameManager.PlayerInput.CameraDebugAngles.CycleAngles.performed += ctx =>
        {
            CycleView(1);
            ChangeView(m_DefaultPosition[m_PositionIndex], m_DefaultQuaternion, false);
        };

        GameManager.PlayerInput.CameraDebugAngles.ChangeAngle.performed += ctx =>
        {
            Vector2 input = ctx.ReadValue<Vector2>();
            if (input.sqrMagnitude > 0.25)
            {
                if (m_CameraInputHandled == false)
                {
                    if (Mathf.Abs(input.x) >= Mathf.Abs(input.y))
                    {
                        if (input.x > 0)
                        {
                            CycleView(-1);
                        }
                        else
                        {
                            CycleView(1);
                        }
                    }
                    else
                    {
                        if (input.y > 0)
                        {
                            m_PositionIndex = 0;
                        }
                        else
                        {
                            m_PositionIndex = 1;
                        }
                    }
                    ChangeView(m_DefaultPosition[m_PositionIndex], m_DefaultQuaternion, false);
                    m_CameraInputHandled = true;
                }               
            }
            else
            {
                m_CameraInputHandled = false;
            }
        };        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = GameManager.Player.transform.position;
        switch (m_State)
        {
            case State.FOLLOWING_TARGET:
                if (m_ChangeInstantly == false)
                {
                    if (transform.InverseTransformPoint(Camera.main.transform.position) != m_TargetPosition) // || Camera.main.transform.rotation != m_TargetRotation)
                    {
                        m_TimeProportion += Time.deltaTime * cameraSpeed;
                        Camera.main.transform.rotation = Quaternion.Slerp(m_StartTargetRotation, m_TargetRotation, m_TimeProportion);
                        Camera.main.transform.position = transform.TransformPoint(Vector3.Lerp(m_StartTargetPosition, m_TargetPosition, m_TimeProportion));
                    }
                    else
                    {
                        m_State = State.LOCKED_ON_PLAYER;
                        Camera.main.transform.SetParent(transform);
                    }
                }
                else
                {
                    Camera.main.transform.rotation = m_TargetRotation;
                    Camera.main.transform.position = transform.TransformPoint(m_TargetPosition);
                    m_State = State.LOCKED_ON_PLAYER;
                    Camera.main.transform.SetParent(transform);
                }                
                break;
            case State.LOCKED_ON_PLAYER:                
                break;
        }
    }

    public void ChangeView(Vector3 newPosition, Quaternion newRotation, bool changeInstantly)
    {
        m_TargetPosition = newPosition;
        m_TargetRotation = newRotation;
        m_ChangeInstantly = changeInstantly;
        if (changeInstantly == false)
        {
            m_StartTargetPosition = transform.InverseTransformPoint(Camera.main.transform.position);
            m_StartTargetRotation = Camera.main.transform.rotation;
            m_TimeProportion = 0;
        }
        m_State = State.FOLLOWING_TARGET;
        transform.DetachChildren();
    }

    void CycleView(int direction)
    {
        m_DefaultPosition[0] = Quaternion.AngleAxis(90f * direction, Vector3.up) * m_DefaultPosition[0];
        m_DefaultPosition[1] = Quaternion.AngleAxis(90f * direction, Vector3.up) * m_DefaultPosition[1];
        m_DefaultQuaternion = Quaternion.AngleAxis(90f * direction, Vector3.up) * m_DefaultQuaternion;
    }
}
