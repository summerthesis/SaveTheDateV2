using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Cinematic : MonoBehaviour
{
    public float FadeInSpeed = 0.5f, FadeOutSpeed = 0.5f;
    public float AngularAdjustSpeed = 25f, LinearAdjustSpeed = 0.25f;   
    [TextArea]
    public string TextContent = "";
    public Transform CameraTargetTransform;

    public bool finished { get; private set; } = false;
    
    private GameObject[] m_OtherCameras;
    private GameObject m_MainCameraObject;
    private Image[] m_LetterBoxes;
    private Camera m_Camera;
    private TextUITypewrite m_Text;
    private PlayerInputAction m_PlayerInput;

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
        
    void Start()
    {
        if (TextContent != "")
        {
            m_State = State.LETTERBOX_FADEIN;
            m_LetterBoxes = GetComponentsInChildren<Image>();
            m_LetterBoxes[0].color = new Color(0, 0, 0, 0);
            m_LetterBoxes[1].color = new Color(0, 0, 0, 0);
        }
        else
        {
            m_State = State.CAMERA_ADJUST;
        }
        
        m_Camera = GetComponentInChildren<Camera>();
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
        m_MainCameraObject = Camera.main.gameObject;
        m_MainCameraObject.SetActive(false);

        m_Text = GetComponentInChildren<TextUITypewrite>();
        m_PlayerInput = InputManagerSingleton.Instance;
        m_PlayerInput.PlayerControls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        switch(m_State)
        {
            case State.IDLING:
                break;
            case State.LETTERBOX_FADEIN:
                float a1 = m_LetterBoxes[0].color.a + Time.deltaTime * FadeInSpeed;
                if (a1 > 1)
                {
                    a1 = 1f;
                    m_State = State.CAMERA_ADJUST;
                }
                m_LetterBoxes[0].color = new Color(0, 0, 0, a1);
                m_LetterBoxes[1].color = new Color(0, 0, 0, a1);
                break;
            case State.CAMERA_ADJUST:

                if (true)
                {
                    if (TextContent != "")
                    {
                        m_State = State.TEXT_RENDERING;
                        m_Text.Input(TextContent);
                        m_Text.Output();
                    }
                    else
                    {
                        m_State = State.IDLING;
                        m_PlayerInput.MenuControls.CancelBack.performed += OnCancelCamera;
                        m_PlayerInput.MenuControls.Enable();
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
                float a2 = m_LetterBoxes[0].color.a - Time.deltaTime * FadeOutSpeed;
                if (a2 < 0)
                {
                    a2 = 0f;
                    m_State = State.CAMERA_RESTORE;                    
                }
                m_LetterBoxes[0].color = new Color(0, 0, 0, a2);
                m_LetterBoxes[1].color = new Color(0, 0, 0, a2);
                break;
            case State.CAMERA_RESTORE: 
                
                if (true)
                {
                    m_State = State.EXIT;
                }
                break;
            case State.EXIT:
                foreach (var camera in m_OtherCameras)
                {
                    camera.SetActive(true);
                }
                m_MainCameraObject.SetActive(true);
                m_PlayerInput.PlayerControls.Enable();
                Destroy(this.gameObject);
                break;
        }        
    }

    void OnCancelCamera(InputAction.CallbackContext ctx)
    {
        m_State = State.CAMERA_RESTORE;
        m_PlayerInput.MenuControls.CancelBack.performed -= OnCancelCamera;
        m_PlayerInput.MenuControls.Disable();
    }
    
}
