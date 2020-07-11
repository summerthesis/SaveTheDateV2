using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraViewChange : MonoBehaviour
{
    public Transform TargetRotationL, TargetRotationR;
    public Vector3 TargetPositionL, TargetPositionR;
    public float cameraRotateSpeed = 2.5f, cameraTranslateSpeed = 2.5f;
        //cameraDistance = 15f;
    //public Vector3 focusOffset = new Vector3(0, 0, 0);

    private Vector3 m_TargetPosition;
    private Quaternion m_TargetRotation;
    private enum State
    {
        INACTIVE,
        CHANGING
    }
    private State m_State = State.INACTIVE;

    private void OnTriggerExit(Collider other)
    {
        KH_PlayerController playerController = other.GetComponent<KH_PlayerController>();
        if (playerController != null)
        {
            Vector3 left = transform.TransformPoint(new Vector3(-5, 0, 0));
            Vector3 right = transform.TransformPoint(new Vector3(5, 0, 0));
            if ((other.transform.position - left).sqrMagnitude > 
                (other.transform.position - right).sqrMagnitude)  // CameraRight
            {
                m_TargetRotation = TargetRotationR.rotation;
                m_TargetPosition = TargetPositionR;
            }
            else
            {
                m_TargetRotation = TargetRotationL.rotation;
                m_TargetPosition = TargetPositionL;
            }
            m_State = State.CHANGING;
        }            
    }

    private void Update()
    {
        switch (m_State)
        {
            case State.CHANGING:
                if (Camera.main.transform.localPosition != m_TargetPosition || Camera.main.transform.rotation != m_TargetRotation)
                {
                    Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, m_TargetRotation, Time.deltaTime * cameraRotateSpeed);
                    Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, m_TargetPosition, Time.deltaTime * cameraTranslateSpeed);
                }
                else
                {
                    m_State = State.INACTIVE;
                }
                break;
        }
    }
}
