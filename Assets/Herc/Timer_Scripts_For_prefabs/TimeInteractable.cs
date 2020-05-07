/**************************************************************
 * Time Interactable Class
 * Created:         May 7, 2020
 * Last Modified:   May 7, 2020
 * 
 * Inherits from MonoBehaviour
 * DOES NOT IMPLEMENT ITimeInteractable interface (for now)
 * 
 * Use for every component that can be time-controlled
 * 
 * Implements speed reading functionality to be used in other
 * scripts. The reason why it was implemented as its own class
 * was to enable this component being caught and manipulated
 * by the player
 * 
 * OBS: Object cooldown won't be the same as the player's Time
 * Mech cooldown
 * 
 *************************************************************/
using System.Collections;
using System.Collections.Generic;
using TimeManipuation;
using UnityEngine;

public class TimeInteractable : MonoBehaviour
{
    #region Time States
    public TimeStates CurrentState { get; private set; }
    [SerializeField, Tooltip("Speed Cooldown Time")]
    private float m_cooldownTime;
    private float m_currentTime; // counter
    //returns normalized cooldown time for UI
    public float Progress { get; private set; }
    #endregion
    #region Speed Variables
    [SerializeField, Tooltip("Original Speed")]
    private float m_originalSpeed;
    [SerializeField, Tooltip("Half-speed(slow)")]
    private float m_halfSpeed;
    [SerializeField, Tooltip("Stopped Speed")]
    private float m_stoppedSpeed;
    public float CurrentSpeed { get; private set; }
    #endregion

    void FixedUpdate() {

        if (CurrentState != TimeStates.Normal) {
            m_currentTime += Time.fixedDeltaTime;
            if (m_currentTime >= m_cooldownTime) {
                Restore();
            }
        }
    }
    void Awake() {

        Restore();
    }

    #region Functionality
    public void Slow() {
        CurrentState = TimeStates.Slowed;
        CurrentSpeed = m_halfSpeed;
    }
    public void Stop() {
        CurrentState = TimeStates.Stopped;
        CurrentSpeed = m_stoppedSpeed;
    }
    public void Restore() {
        m_currentTime = 0;
        CurrentState = TimeStates.Normal;
        CurrentSpeed = m_originalSpeed;
    }
    #endregion
}
