/***************************************************************
 * Rotating Gear Class
 * 
 * Author: Hercules (HErC) Dias Campos
 * Created:         May  8, 2020
 * Last Modified:   May 17, 2020
 * 
 * Inherits from MonoBehaviour
 * 
 * Class for rotating gear
 * 
 * Reads its speed and state from a required TimeInteractable
 * instance, and applies rotations accordingly
 * 
 * Changes in May 17, 2020:
 *      Adapted the code to the new starndard
 * 
 * ***REQUIRES THAT THE PREFAB HAVE A COLLIDER!***
 * 
 **************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingGear : MonoBehaviour
{
    #region Time Controls
    //This syntax makes it so that the value of the variable is readonly.
    //Alternatively, this can be set on the script's Awake or Start functions
    [SerializeField] private float m_fSpeed;
    [SerializeField] private float m_fNormalSpeed;
                     private float m_fSlowedSpeed;
    #endregion

    #region Prefab and Collider.
    //Considering future possible implementations
    [SerializeField, Tooltip("Model prefab for the rotating platform")]
    private GameObject m_Model;
    [SerializeField, Tooltip("Use this to adjust box collider's size")]
    private Vector3 m_ColliderSize;
    #endregion

    #region Parenting Script (optional)
    [SerializeField, Tooltip("Is this gear a platform?")]
    private bool m_isPlatform;
    #endregion

    void Awake() {
        if (m_isPlatform) {
            //OBS: The way Unity works, it'll attach the script to the same game objext that has
            //     the first collider it finds
            GetComponentInChildren<Collider>().transform.gameObject.AddComponent<PlatformParenting>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.RotateAround(this.gameObject.transform.position, 
                                               this.gameObject.transform.up, 
                                               m_fSpeed * Time.deltaTime);
    }

    //Copied straight from Blair's script, changed variable names for consistency
    void TimeSlow() {
        if (m_fSpeed > m_fSlowedSpeed) m_fSpeed -= 0.1f;
    }
    void TimeStop() {
        m_fSpeed = 0;
    }
    void TimeFastForward() {
        m_fSpeed = m_fNormalSpeed * 2;
    }
    void RestoreToNormal() {
        m_fSpeed = m_fNormalSpeed;
    }
    //End of the copy...
}
