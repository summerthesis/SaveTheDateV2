using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TimeInteractable))]
public class dummy_PlatformMovement : MonoBehaviour
{
    TimeInteractable m_time;

    void Awake() {
        m_time = GetComponent<TimeInteractable>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //m_time.Cu
    }
}
