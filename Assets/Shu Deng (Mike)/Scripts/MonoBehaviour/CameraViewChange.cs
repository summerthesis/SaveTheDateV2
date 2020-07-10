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

            //Vector3 direction = -(m_TargetRotation * Vector3.forward);
            //m_TargetPosition = Camera.main.transform.parent.InverseTransformPoint(GameManager.Player.transform.position + focusOffset + direction * cameraDistance);
            StartCoroutine(ChangeAngle());
        }            
    }

    private IEnumerator ChangeAngle()
    {
        while (Camera.main.transform.localPosition != m_TargetPosition && Camera.main.transform.rotation != m_TargetRotation)
        {
            Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, m_TargetRotation, Time.deltaTime * cameraRotateSpeed);
            Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, m_TargetPosition, Time.deltaTime * cameraTranslateSpeed);
            yield return null;        
        }
    }
}
