using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBoardingLevelEnd : MonoBehaviour
{
    private bool m_Started = false, m_Ended = false;

    private void Start()
    {
        if (m_Started == false)
        {            
            GameManager.HUD.SetActive(false);
            GameManager.PlayerInput.PlayerControls.Jump.Disable();
            GameManager.PlayerInput.TimeControls.Disable();
            m_Started = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        KH_PlayerController playerController = other.GetComponent<KH_PlayerController>();
        if (playerController != null && m_Ended == false)
        {
            GameManager.HUD.SetActive(true);
            GameManager.PlayerInput.PlayerControls.Jump.Enable();
            GameManager.PlayerInput.TimeControls.Enable();
            m_Ended = true;
        }
    }
}
