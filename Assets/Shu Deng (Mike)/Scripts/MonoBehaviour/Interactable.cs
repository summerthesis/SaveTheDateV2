using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    public GameObject PopupHintPrefab;
    public Vector3 PopupPosition;
    public bool PopupInWorld = false;

    private bool m_PopupShowed = false;
    private GameObject m_Popup;
    private static bool m_InteractBinded = false;
    private static Interactable m_CurrentBinded;
    
    void Awake()
    {
        GetComponent<BoxCollider>().isTrigger = true;     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        KH_PlayerController playerController = other.GetComponent<KH_PlayerController>();
        if (playerController != null)
        {
            StartCoroutine(SubscribeInteract());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        KH_PlayerController playerController = other.GetComponent<KH_PlayerController>();
        if (playerController != null)
        {
            if (m_PopupShowed == true)
            {
                Destroy(m_Popup);
                m_PopupShowed = false;
                StopCoroutine(SubscribeInteract());
                if (m_CurrentBinded == this)
                {
                    GameManager.PlayerInput.PlayerControls.Interact.performed -= OnInteractPerformed;
                    m_InteractBinded = false;
                }                
            }
        }
    }

    private void OnInteractPerformed(InputAction.CallbackContext ctx)
    {
        SendMessage("OnInteract");
    }

    private IEnumerator SubscribeInteract()
    {
        while (m_InteractBinded == true)
        {
            yield return null;
        }
        if (m_PopupShowed == false)
        {
            m_Popup = Instantiate(PopupHintPrefab, this.transform);
            if (PopupInWorld == true)
            {
                m_Popup.transform.position = PopupPosition;
            }
            else
            {
                m_Popup.transform.localPosition = PopupPosition;
            }
            m_PopupShowed = true;
            GameManager.PlayerInput.PlayerControls.Interact.performed += OnInteractPerformed;
            m_InteractBinded = true;
            m_CurrentBinded = this;
        }        
    }
}
