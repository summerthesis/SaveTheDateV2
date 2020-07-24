using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Cinematic : MonoBehaviour
{
    public float fadeInSpeed = 0.5f, fadeOutSpeed = 0.5f;
    [Tooltip("The time for camera to reach target is 1/CameraSpeed")]
    public float cameraSpeed = 1f;   
    [TextArea]
    public string textContent = "";
    public Transform cameraTargetTransform;
    public Image[] letterBoxes;
    public GameObject hintToSkip;

    private GameObject[] m_OtherCameras;
    private Camera m_Camera, m_MainCamera;
    private TextUITypewrite m_Text;
    private Transform m_OriginalCameraTransform;
    private bool m_HUDActive, m_JumpAvailable, m_FirstUpdateofState = true;
    private float m_TimeProportion = 0;
    private enum State
    {
        NA,
        IDLING,
        LETTERBOX_FADEIN,
        CAMERA_ADJUST,
        TEXT_RENDERING,
        LETTERBOX_FADEOUT,
        CAMERA_RESTORE,
        EXIT
    }
    private State m_State, m_PreviousState = State.NA;
        
    void Start()  //Dont change to Awake() because this object needs to check textContent to initialize
    {
        if (textContent != "")
        {
            m_State = State.LETTERBOX_FADEIN;
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
        hintToSkip.SetActive(false);

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
                if (m_FirstUpdateofState == true)
                {
                    if (m_PreviousState == State.CAMERA_ADJUST)
                    {
                        GameManager.PlayerInput.MenuControls.Back.performed += OnCancelCamera;
                        GameManager.PlayerInput.MenuControls.Enable();
                    }
                    GameManager.PlayerInput.MenuControls.Enter.performed += OnCancelCamera;
                }
                break;
            case State.LETTERBOX_FADEIN:
                float a1 = letterBoxes[0].color.a + Time.deltaTime * fadeInSpeed;
                if (a1 > 1)
                {
                    a1 = 1f;
                    m_State = State.CAMERA_ADJUST;  //Exit transition
                    hintToSkip.SetActive(true);
                    GameManager.PlayerInput.MenuControls.Back.performed += OnCancelCamera;
                    GameManager.PlayerInput.MenuControls.Enable();
                    m_FirstUpdateofState = true;
                }
                letterBoxes[0].color = new Color(0, 0, 0, a1);
                letterBoxes[1].color = new Color(0, 0, 0, a1);
                break;
            case State.CAMERA_ADJUST:
                m_TimeProportion += Time.deltaTime * cameraSpeed;
                m_Camera.transform.position = Vector3.Lerp(
                    m_OriginalCameraTransform.position, cameraTargetTransform.position, m_TimeProportion);
                m_Camera.transform.rotation = Quaternion.Slerp(
                    m_OriginalCameraTransform.rotation, cameraTargetTransform.rotation, m_TimeProportion);
                if (m_Camera.transform.position == cameraTargetTransform.position)  //Exit transition               
                {                    
                    if (textContent != "")
                    {
                        m_State = State.TEXT_RENDERING;
                        m_Text.Input(textContent);
                        m_Text.Output();
                    }
                    else
                    {
                        m_State = State.IDLING;
                        m_PreviousState = State.CAMERA_ADJUST;
                    }
                }                
                break;            
            case State.TEXT_RENDERING:
                if (m_Text.finished == true)
                {
                    m_Text.Input("");
                    m_State = State.LETTERBOX_FADEOUT;
                    hintToSkip.SetActive(false);
                }
                break;
            case State.LETTERBOX_FADEOUT:
                float a2 = letterBoxes[0].color.a - Time.deltaTime * fadeOutSpeed;
                if (a2 < 0)
                {
                    a2 = 0f;
                    m_State = State.IDLING;
                    m_PreviousState = State.LETTERBOX_FADEOUT;
                }
                letterBoxes[0].color = new Color(0, 0, 0, a2);
                letterBoxes[1].color = new Color(0, 0, 0, a2);
                break;
            case State.CAMERA_RESTORE:
                m_TimeProportion -= Time.deltaTime * cameraSpeed;
                m_Camera.transform.position = Vector3.Lerp(
                    m_OriginalCameraTransform.position, cameraTargetTransform.position, m_TimeProportion);
                m_Camera.transform.rotation = Quaternion.Slerp(
                    m_OriginalCameraTransform.rotation, cameraTargetTransform.rotation, m_TimeProportion);
                if (m_Camera.transform.position == m_OriginalCameraTransform.position)
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
        if (m_State == State.TEXT_RENDERING)
        {
            m_Text.Input("");
        }
        m_State = State.CAMERA_RESTORE;
        m_FirstUpdateofState = true;
        GameManager.PlayerInput.MenuControls.Back.performed -= OnCancelCamera;
        GameManager.PlayerInput.MenuControls.Enter.performed -= OnCancelCamera;
        GameManager.PlayerInput.MenuControls.Disable();
    }    
}
