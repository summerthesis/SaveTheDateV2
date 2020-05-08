/***************************************************************
 * Time Input Controls
 * 
 * Author: Hercules (HErC) Dias Campos
 * Created:         May 7, 2020
 * Last Modified:   May 7, 2020
 * 
 * Input for Time Mechanics.
 * Inherits from Monobehaviour
 * 
 * Class responsible (at the time) for all time-related 
 * detection and manipulation.
 * 
 * Workings:
 * -> Sphere casts from camera to detect tag "TimeInteractable"
 * -> Read reads slow/stop input, applying slow/stop mechanics
 *      to the object
 * -> Currently, it has no cooldown or UI apart from what it
 *      visibly manipulates, time-wise
 *      
 * OBS.: Has room for improvement and optimization. This is just
 *          a preliminary version of the script
 **************************************************************/
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
    public GameObject viewedObject;
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
    //variables
    private bool m_slowInput;
    private bool m_stopInput;
    #endregion

    #region GamePad Vibration Variables
    private Gamepad m_pad; //This has technically to he on a separate script
    [SerializeField, Range(0.0f, 1.0f), Tooltip("Motor speed")]
    private float m_lowFrequencySpeed;
    [SerializeField, Range(0.0f, 1.0f), Tooltip("Motor speed")]
    private float m_highFrequencySpeed;
    #endregion

    void Awake() {
        //Gets camera
        m_playerTransform = this.gameObject.transform.parent.transform;
        if (m_playerTransform) {
            m_cam = m_playerTransform.GetComponentInChildren<Camera>();
        }
        //Input startup
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

    /// <summary>
    /// Time Interactable Detection Function
    /// SphereCasts from camera towards Camera's forward
    /// If it detects time interactable, reads time interaction inputs
    /// Slows or stops object accordingly.
    /// 
    /// OBS.: Affects all detected time-interactable objects (for now)
    /// </summary>
    void Detect() {
        viewedObject = null;
        Vector3 temp = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        foreach(var result in Physics.SphereCastAll(m_cam.ScreenPointToRay(temp), m_castRadius, m_castDistance)) {
            TimeInteractable timeComponent = result.transform.gameObject.GetComponentInParent<TimeInteractable>();
            if (timeComponent != null) {
                viewedObject = timeComponent.gameObject;
                //OBS: Still not sure where to implement vibration
                //if (m_pad != null) { m_pad.SetMotorSpeeds(m_lowFrequencySpeed, m_highFrequencySpeed); }
                if (m_slowInput ^ m_stopInput) {
                    if (m_slowInput) timeComponent.Slow();
                    if (m_stopInput) timeComponent.Stop();
                }
                //Debug.Log("Time interactable object found!");
            }
        }
        #region Debug Draws
        Debug.DrawLine(m_cam.ScreenPointToRay(temp).origin, m_cam.ScreenPointToRay(temp).direction*m_castDistance, Color.red);
        Debug.DrawLine(m_cam.ScreenPointToRay(temp).origin + m_cam.transform.up * m_castRadius, (m_cam.ScreenPointToRay(temp).direction * m_castDistance) + m_cam.transform.up * m_castRadius, Color.red);
        Debug.DrawLine(m_cam.ScreenPointToRay(temp).origin - m_cam.transform.up * m_castRadius, (m_cam.ScreenPointToRay(temp).direction * m_castDistance) - m_cam.transform.up * m_castRadius, Color.red);
        Debug.DrawLine(m_cam.ScreenPointToRay(temp).origin + m_cam.transform.right * m_castRadius, (m_cam.ScreenPointToRay(temp).direction * m_castDistance) + m_cam.transform.right * m_castRadius, Color.red);
        Debug.DrawLine(m_cam.ScreenPointToRay(temp).origin - m_cam.transform.right * m_castRadius, (m_cam.ScreenPointToRay(temp).direction * m_castDistance) - m_cam.transform.right * m_castRadius, Color.red);
        #endregion
    }

    void OnDisable() {
        m_timeInput.Disable();
        if (m_pad != null) { m_pad.ResetHaptics(); }
    }
}
