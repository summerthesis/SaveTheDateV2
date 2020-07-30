using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CameraViewChange : MonoBehaviour
{
    public LocalCameraTransform targetLeft, targetRight;

    private void Awake()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

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
                FindObjectOfType<MainCameraController>().ChangeView(targetRight.position, Quaternion.Euler(targetRight.rotation), false);
            }
            else
            {
                FindObjectOfType<MainCameraController>().ChangeView(targetLeft.position, Quaternion.Euler(targetLeft.rotation), false);
            }
        }            
    }


}
