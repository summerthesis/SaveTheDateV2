using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CinematicInvoker))]
public class TriggerableCinematic : MonoBehaviour
{
    private bool m_Triggered = false;

    void Awake()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_Triggered == false)
        {
            KH_PlayerController playerController = other.GetComponent<KH_PlayerController>();
            if (playerController != null)
            {
                GetComponent<CinematicInvoker>().InstantiateCinematic();
                m_Triggered = true;
            }
        }        
    }
}
