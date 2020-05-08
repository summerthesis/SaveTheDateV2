/**************************************************************
 * 
 *************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TimeInteractable))]
public class PlatformScript : MonoBehaviour
{
    #region Time Interactable
    private TimeInteractable m_speed;
    #endregion

    #region Platform Waypoints
    [SerializeField, Tooltip("Waypoint Positions")]
    private Vector3 m_waypoints;
    [SerializeField, Tooltip("Should the platform cycle through the waypoints?\nIt'll \"swing\" back and forth if it doesn't cycle")]
    private bool m_Loops;
    #endregion

    private void Awake() {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
