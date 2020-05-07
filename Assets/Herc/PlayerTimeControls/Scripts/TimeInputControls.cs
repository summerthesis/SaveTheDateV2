using System;
using System.Collections;
using System.Collections.Generic;
using TimeManipuation;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimeInputControls : MonoBehaviour
{
    [SerializeField, Tooltip("Actual character's transform")]
    private Transform m_playerTransform;

    #region Camera Detection
    [SerializeField, Tooltip("Player's Camera for SphereCast")]
    private Camera m_cam;
    [SerializeField, Tooltip("How wide the cast is")]
    private float m_castRadius;
    [SerializeField, Tooltip("How far the cast travels")]
    private float m_castDistance;
    #endregion

    #region Controls
    //hardware and bindings
    private PlayerInputAction m_timeInput;
    private Gamepad m_pad; //This has technically to he on a separate script
    //variables
    private bool m_slowInput;
    private bool m_stopInput;
    #endregion


    void Awake() {
        m_playerTransform = this.gameObject.transform.parent.transform;
        if (m_playerTransform) {
            m_cam = m_playerTransform.GetComponentInChildren<Camera>();
        }

        m_timeInput = new PlayerInputAction();

        m_timeInput.TimeControls.TimeSlow.performed += slowContext => m_slowInput = true;
        m_timeInput.TimeControls.TimeSlow.canceled += slowCcontext => m_slowInput = false;

        m_timeInput.TimeControls.TimeStop.performed += stopContext => m_stopInput = true;
        m_timeInput.TimeControls.TimeStop.canceled += stopContext => m_stopInput = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Detect();
    }

    void OnEnable() {
        m_timeInput.Enable();
    }

    void Detect() {
        Vector3 temp = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        foreach(var result in Physics.SphereCastAll(m_cam.ScreenPointToRay(temp), m_castRadius, m_castDistance)) {
            if (result.transform.gameObject.tag == "TimeInteractable") {
                if (m_slowInput ^ m_stopInput) {

                }
                Debug.Log("Time interactable object found!");
            }
        }
        ///DEBUG FUNCTIONS (CLEANUP LATER)
        Debug.DrawLine(m_cam.ScreenPointToRay(temp).origin, m_cam.ScreenPointToRay(temp).direction*m_castDistance, Color.red);
        Debug.DrawLine(m_cam.ScreenPointToRay(temp).origin + m_cam.transform.up * m_castRadius, (m_cam.ScreenPointToRay(temp).direction * m_castDistance) + m_cam.transform.up * m_castRadius, Color.red);
        Debug.DrawLine(m_cam.ScreenPointToRay(temp).origin - m_cam.transform.up * m_castRadius, (m_cam.ScreenPointToRay(temp).direction * m_castDistance) - m_cam.transform.up * m_castRadius, Color.red);
        Debug.DrawLine(m_cam.ScreenPointToRay(temp).origin + m_cam.transform.right * m_castRadius, (m_cam.ScreenPointToRay(temp).direction * m_castDistance) + m_cam.transform.right * m_castRadius, Color.red);
        Debug.DrawLine(m_cam.ScreenPointToRay(temp).origin - m_cam.transform.right * m_castRadius, (m_cam.ScreenPointToRay(temp).direction * m_castDistance) - m_cam.transform.right * m_castRadius, Color.red);

    }

    void OnDisable() {
        m_timeInput.Disable();
        if (m_pad != null) { m_pad.ResetHaptics(); }
    }
}
