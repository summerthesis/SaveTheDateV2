using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static PlayerInputAction m_PlayerInputAction;
    public static PlayerInputAction PlayerInput
    {
        get
        {
            if (m_PlayerInputAction == null)
            {
                m_PlayerInputAction = new PlayerInputAction();
                m_PlayerInputAction.PlayerControls.Enable();
                m_PlayerInputAction.TimeControls.Enable();
                m_PlayerInputAction.CameraDebugAngles.Enable();
            }
            return m_PlayerInputAction;
        }
    }

    private static GameObject m_HUD;
    public static GameObject HUD
    {
        get
        {
            if (m_HUD == null)
            {
                m_HUD = GameObject.FindGameObjectWithTag("HUD");
            }
            return m_HUD;
        }
    }

    private static GameObject m_Player;
    public static GameObject Player
    {
        get
        {
            if (m_Player == null)
            {
                m_Player = GameObject.FindGameObjectWithTag("Player");
            }
            return m_Player;
        }        
    }

    private static Camera m_MainCamera;
    public static Camera MainCamera
    {
        get
        {
            if (m_MainCamera == null)
            {
                m_MainCamera = Camera.main;
            }
            return m_MainCamera;
        }
    }

    public static GameObject CurrentCheckpoint { get; set; }

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);        
    }
}
