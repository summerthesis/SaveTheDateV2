using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraViewChange : MonoBehaviour
{
    public Transform TargetRotationL, TargetRotationR;
    public Vector3 TargetPositionL, TargetPositionR;

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
                FindObjectOfType<MainCameraController>().ChangeView(TargetPositionR, TargetRotationR.rotation);
            }
            else
            {
                FindObjectOfType<MainCameraController>().ChangeView(TargetPositionL, TargetRotationL.rotation);
            }
        }            
    }


}
