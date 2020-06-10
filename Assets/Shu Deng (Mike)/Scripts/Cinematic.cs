using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cinematic : MonoBehaviour
{
    public float FadeInSpeed = 0.5f, FadeOutSpeed = 0.5f, TextRenderInterval = 2f;
    public float AngularAdjustSpeed = 25f, LinearAdjustSpeed = 0.25f;
    
    public bool finished { get; private set; } = false;

    private GameObject m_Player;
    private GameObject[] m_OtherCameras;
    private Image[] m_LetterBoxes;
    private Text m_Text;
    private Camera m_Camera;
    private Vector3 m_InteractablePosition, m_PlayerPosition;
    private bool m_RenderNextText = true;
    private Transform m_CameraEndingTransform;
    private enum State
    {
        LETTERBOX_FADEIN,
        CAMERA_ADJUST,
        CAMERA_ROTATE,
        TEXT_RENDERING,
        LETTERBOX_FADEOUT,
        CAMERA_RESTORE,
        EXIT
    }
    private State m_State;


    private int counter = 0, counterMax = 3;
        
    void Awake()
    {        
        m_State = State.LETTERBOX_FADEIN;
        m_LetterBoxes = GetComponentsInChildren<Image>();
        m_LetterBoxes[0].color = new Color(0, 0, 0, 0);
        m_LetterBoxes[1].color = new Color(0, 0, 0, 0);
        m_Camera = GetComponentInChildren<Camera>();
        m_Camera.transform.position = Camera.main.transform.position;
        m_Camera.transform.rotation = Camera.main.transform.rotation;
        m_Text = GetComponentInChildren<Text>();
        m_Text.text = "";
        m_OtherCameras = GameObject.FindGameObjectsWithTag("Camera");
        foreach(var camera in m_OtherCameras)
        {
            camera.SetActive(false);
        }
        Camera.main.gameObject.SetActive(false);
        m_Player = GameObject.FindWithTag("Player");
        m_Player.GetComponent<KH_PlayerController>().controls.Disable();
        m_PlayerPosition = m_Player.transform.position;
        m_InteractablePosition = GetComponentsInParent<Transform>()[1].position;
        m_CameraEndingTransform.rotation = Quaternion.LookRotation(m_InteractablePosition - m_PlayerPosition);
        Vector3 horizontalOffset = m_PlayerPosition - m_InteractablePosition;
        horizontalOffset.y = 0f;
        horizontalOffset.Normalize();
        m_CameraEndingTransform.position = m_PlayerPosition + horizontalOffset + new Vector3(0, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        switch(m_State)
        {
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
                Vector3 cameraToInteractable = m_InteractablePosition - m_Camera.transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(cameraToInteractable);
                if (m_Camera.transform.rotation != targetRotation)
                {
                    m_Camera.transform.rotation = Quaternion.RotateTowards(m_Camera.transform.rotation, targetRotation, Time.deltaTime * AngularAdjustSpeed);
                }
                else 
                {
                    m_State = State.CAMERA_ROTATE;
                }
                break;
            case State.CAMERA_ROTATE:

                cameraToInteractable = m_InteractablePosition - m_Camera.transform.position;
                targetRotation = Quaternion.LookRotation(cameraToInteractable);

                break;
            case State.TEXT_RENDERING:
                if (m_RenderNextText == true)
                {
                    if (++counter == counterMax)
                    {
                        m_State = State.LETTERBOX_FADEOUT;
                    }

                }
                StartCoroutine(Wait());
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
                if (m_Camera.transform.rotation != Camera.main.transform.rotation)
                {
                    m_Camera.transform.rotation = Quaternion.RotateTowards(
                        m_Camera.transform.rotation, Camera.main.transform.rotation, Time.deltaTime * AngularAdjustSpeed);
                }
                else if (m_Camera.transform.position != Camera.main.transform.position)
                {
                    m_Camera.transform.position = Vector3.MoveTowards(
                        m_Camera.transform.position, Camera.main.transform.position, Time.deltaTime * LinearAdjustSpeed);
                }
                else
                {
                    m_State = State.EXIT;
                }
                break;
            case State.EXIT:
                foreach (var camera in m_OtherCameras)
                {
                    camera.SetActive(true);
                }
                Camera.main.gameObject.SetActive(true);
                m_Player.GetComponent<KH_PlayerController>().controls.Enable();
                Destroy(this.gameObject);
                break;
        }        
    }

    IEnumerator Wait()
    {
        m_RenderNextText = false;
        yield return new WaitForSeconds(TextRenderInterval);
        m_RenderNextText = true;
    }
}
