using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAngleChange : MonoBehaviour
{
    public Transform Target;
    public float cameraRotateSpeed = 2.5f, cameraTranslateSpeed = 2.5f, cameraDistance = 15f;
    public Vector3 focusOffset = new Vector3(0, 0, 0);

    private Vector3 m_TargetPosition;
    private Quaternion m_TargetRotation;

    // Start is called before the first frame update
    void Awake()
    {
        //m_TargetPosition = Target.position;
        m_TargetRotation = Target.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 direction = - (m_TargetRotation * Vector3.forward);
        m_TargetPosition = Camera.main.transform.parent.InverseTransformPoint(GameManager.Player.transform.position + focusOffset + direction * cameraDistance);
        StartCoroutine(ChangeAngle());
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
