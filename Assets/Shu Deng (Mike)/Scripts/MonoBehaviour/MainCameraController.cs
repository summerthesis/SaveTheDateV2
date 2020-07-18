using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    public float cameraRotateSpeed = 2.5f, cameraTranslateSpeed = 2.5f;

    private Vector3 m_TargetPosition;
    private Vector3[] m_DefaultPosition = 
        {new Vector3(16.02193f, 9.490358f, 0f), new Vector3(24.29176f, 13.85903f, 0f)};  // 0 is near, 1 is far position
    private Quaternion m_TargetRotation, m_DefaultQuaternion = Quaternion.Euler(new Vector3(27.846f, -90f, 0f));
    private int m_PositionIndex = 0;
    private bool m_CameraInputHandled = false;
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
            ChangeView(m_DefaultPosition[m_PositionIndex], m_DefaultQuaternion);
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
                    ChangeView(m_DefaultPosition[m_PositionIndex], m_DefaultQuaternion);
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
                Vector3 targetPositionWorld = transform.TransformPoint(m_TargetPosition);
                if ((Camera.main.transform.position - targetPositionWorld).sqrMagnitude > 0.05f
                    || (Camera.main.transform.rotation.eulerAngles - m_TargetRotation.eulerAngles).sqrMagnitude > 1f)
                {
                    Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, m_TargetRotation, Time.deltaTime * cameraRotateSpeed);
                    Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPositionWorld, Time.deltaTime * cameraTranslateSpeed);
                }
                else
                {
                    m_State = State.LOCKED_ON_PLAYER;
                    Camera.main.transform.SetParent(transform);
                }
                break;
            case State.LOCKED_ON_PLAYER:                
                break;
        }
    }

    public void ChangeView(Vector3 newPosition, Quaternion newRotation)
    {
        m_TargetPosition = newPosition;
        m_TargetRotation = newRotation;
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
