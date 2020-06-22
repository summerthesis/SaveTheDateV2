using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    public GameObject PopupHintPrefab;
    public Vector3 PopupPosition;
    public bool PopupInWorld = false;
    bool m_PopupShowed = false;
    GameObject m_Popup;

    // Start is called before the first frame update
    void Start()
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
                InputManagerSingleton.Instance.PlayerControls.Interact.performed += OnInteractPerformed;
            }
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
                InputManagerSingleton.Instance.PlayerControls.Interact.performed -= OnInteractPerformed;
            }
        }
    }

    void OnInteractPerformed(InputAction.CallbackContext ctx)
    {
        SendMessage("OnInteract");
    }
}
