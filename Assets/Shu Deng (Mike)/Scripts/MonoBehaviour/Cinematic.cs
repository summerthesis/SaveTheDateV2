using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Cinematic : MonoBehaviour
{
    public float fadeInSpeed = 0.5f, fadeOutSpeed = 0.5f;
    public float cameraRotateSpeed = 2.5f, cameraTranslateSpeed = 2.5f;   
    [TextArea]
    public string textContent = "";
    public Transform cameraTargetTransform;

    private GameObject[] m_OtherCameras;
    private Image[] m_LetterBoxes;
    private Camera m_Camera, m_MainCamera;
    private TextUITypewrite m_Text;
    private Transform m_OriginalCameraTransform;
    private bool m_HUDActive, m_JumpAvailable;
    private const float m_Epsilon = 0.01f;  // A small value approximating to zero
    private enum State
    {
        IDLING,
        LETTERBOX_FADEIN,
        CAMERA_ADJUST,
        TEXT_RENDERING,
        LETTERBOX_FADEOUT,
        CAMERA_RESTORE,
        EXIT
    }
    private State m_State;
        
    void Awake()
    {
        if (textContent != "")
        {
            m_State = State.LETTERBOX_FADEIN;
            m_LetterBoxes = GetComponentsInChildren<Image>();
        }
        else
        {
            m_State = State.CAMERA_ADJUST;
        }
        
        m_Camera = GetComponentInChildren<Camera>();
        m_OriginalCameraTransform = Camera.main.transform;
        m_Camera.transform.position = Camera.main.transform.position;
        m_Camera.transform.rotation = Camera.main.transform.rotation;
        m_OtherCameras = GameObject.FindGameObjectsWithTag("Camera");
        foreach(var cameraObject in m_OtherCameras)
        {
            if (cameraObject != m_Camera.gameObject)
            {
                cameraObject.SetActive(false);
            }            
        }

        m_MainCamera = Camera.main;
        if (m_MainCamera != null)
        {
            m_MainCamera.gameObject.SetActive(false);
        }

        if (GameManager.HUD.transform.GetChild(0).gameObject.activeInHierarchy == true)
        {
            m_HUDActive = true;
            GameManager.HUD.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            m_HUDActive = false;
        }

        m_Text = GetComponentInChildren<TextUITypewrite>();
        GameManager.PlayerInput.PlayerControls.Move.Disable();
        GameManager.PlayerInput.PlayerControls.Interact.Disable();
        if (GameManager.PlayerInput.PlayerControls.Jump.enabled == false)
        {
            m_JumpAvailable = false;
        }
        else
        {
            m_JumpAvailable = true;
            GameManager.PlayerInput.PlayerControls.Jump.Disable();
        }        
        GameManager.PlayerInput.TimeControls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        switch(m_State)
        {
            case State.IDLING:
                break;
            case State.LETTERBOX_FADEIN:
                float a1 = m_LetterBoxes[0].color.a + Time.deltaTime * fadeInSpeed;
                if (a1 > 1)
                {
                    a1 = 1f;
                    m_State = State.CAMERA_ADJUST;
                }
                m_LetterBoxes[0].color = new Color(0, 0, 0, a1);
                m_LetterBoxes[1].color = new Color(0, 0, 0, a1);
                break;
            case State.CAMERA_ADJUST:
                m_Camera.transform.position = Vector3.Lerp(
                    m_Camera.transform.position, cameraTargetTransform.position, Time.deltaTime * cameraTranslateSpeed);
                m_Camera.transform.rotation = Quaternion.Slerp(
                    m_Camera.transform.rotation, cameraTargetTransform.rotation, Time.deltaTime * cameraRotateSpeed);
                if ((m_Camera.transform.position - cameraTargetTransform.position).sqrMagnitude < m_Epsilon)
                {
                    m_State = State.TEXT_RENDERING;
                    if (textContent != "")
                    {                        
                        m_Text.Input(textContent);
                        m_Text.Output();
                    }
                    else
                    {
                        m_State = State.IDLING;
                        GameManager.PlayerInput.MenuControls.CancelBack.performed += OnCancelCamera;
                        GameManager.PlayerInput.MenuControls.Enable();
                    }                    
                }                
                break;            
            case State.TEXT_RENDERING:
                if (m_Text.finished == true)
                {
                    m_Text.Input("");
                    m_State = State.LETTERBOX_FADEOUT;
                }
                break;
            case State.LETTERBOX_FADEOUT:
                float a2 = m_LetterBoxes[0].color.a - Time.deltaTime * fadeOutSpeed;
                if (a2 < 0)
                {
                    a2 = 0f;
                    m_State = State.IDLING;
                    GameManager.PlayerInput.MenuControls.CancelBack.performed += OnCancelCamera;
                    GameManager.PlayerInput.MenuControls.Enable();                   
                }
                m_LetterBoxes[0].color = new Color(0, 0, 0, a2);
                m_LetterBoxes[1].color = new Color(0, 0, 0, a2);
                break;
            case State.CAMERA_RESTORE:
                m_Camera.transform.position = Vector3.Lerp(
                    m_Camera.transform.position, m_OriginalCameraTransform.position, Time.deltaTime * cameraTranslateSpeed);
                m_Camera.transform.rotation = Quaternion.Slerp(
                    m_Camera.transform.rotation, m_OriginalCameraTransform.rotation, Time.deltaTime * cameraRotateSpeed);
                if ((m_Camera.transform.position - m_OriginalCameraTransform.position).sqrMagnitude < m_Epsilon)
                {
                    m_State = State.EXIT;
                }
                break;
            case State.EXIT:
                foreach (var camera in m_OtherCameras)
                {
                    camera.SetActive(true);
                }

                if (m_MainCamera != null)
                {
                    m_MainCamera.gameObject.SetActive(true);
                }

                if (m_HUDActive == true)
                {
                    GameManager.HUD.transform.GetChild(0).gameObject.SetActive(true);
                }
                
                GameManager.PlayerInput.PlayerControls.Move.Enable();
                GameManager.PlayerInput.PlayerControls.Interact.Enable();
                if (m_JumpAvailable == true)
                {
                    GameManager.PlayerInput.PlayerControls.Jump.Enable();
                }
                GameManager.PlayerInput.TimeControls.Enable();
                Destroy(this.gameObject);
                break;
        }        
    }

    void OnCancelCamera(InputAction.CallbackContext ctx)
    {
        m_State = State.CAMERA_RESTORE;
        GameManager.PlayerInput.MenuControls.CancelBack.performed -= OnCancelCamera;
        GameManager.PlayerInput.MenuControls.Disable();
    }    
}
