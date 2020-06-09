using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TimeInteractable))]
public class PlatformAtoB : MonoBehaviour
{
    [SerializeField, Tooltip("Transform of point A, also the start position")]
    private Transform m_pointA;
    [SerializeField, Tooltip("Transform of point B")]
    private Transform m_pointB;
    [SerializeField, Tooltip("Whether the movement is looped or not")]
    private bool m_loop;

    private Vector3 m_velocityDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        gameObject.transform.position = m_pointA.position;
        m_velocityDirection = m_pointB.position - m_pointA.position;
        m_velocityDirection.Normalize();
    }

    void FixedUpdate()
    {
        float displacement = this.gameObject.GetComponent<TimeInteractable>().CurrentSpeed * Time.fixedDeltaTime;
        Vector3 newPosition = gameObject.transform.position + Vector3.Scale(m_velocityDirection, new Vector3(displacement, displacement, displacement));

        // Checks if newPosition is out of range, if so, corrects its value
        float t = (newPosition.x - m_pointA.position.x) / (m_pointB.position.x - m_pointA.position.x);
        if (t < 0)
        {
            if (m_loop == true)
            {
                newPosition = (m_pointA.position - newPosition) + m_pointA.position;
                m_velocityDirection.Scale(new Vector3(-1.0f, -1.0f, -1.0f));
            }
            else
            {
                newPosition = m_pointA.position;
            }
        }
        else if (t > 1)
        {
            if (m_loop == true)
            {
                newPosition = (m_pointB.position - newPosition) + m_pointB.position;
                m_velocityDirection.Scale(new Vector3(-1.0f, -1.0f, -1.0f));
            }
            else
            {
                newPosition = m_pointB.position;
            }
            
        }
        gameObject.transform.position = newPosition;
    }    
}
