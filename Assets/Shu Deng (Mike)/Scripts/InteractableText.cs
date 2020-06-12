using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InteractableText : MonoBehaviour
{
    public GameObject TextUIPrefab;
    public Canvas ScreenCanvas;
    [TextArea]
    public string Text = 
        "This is your first line\nPress Enter and type in the second line";

    private PlayerInputAction m_playerInput;
    private GameObject m_textUI;

    void Awake()
    {
        m_playerInput = InputManagerSingleton.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnInteract()
    {
        m_textUI = Instantiate(TextUIPrefab, ScreenCanvas.transform, false);
        m_textUI.GetComponentInChildren<Text>().text = Text;
        m_playerInput.PlayerControls.Disable();
        StartCoroutine(WaitThenRespond());
    }

    void OnCancel(InputAction.CallbackContext ctx)
    {
        m_playerInput.MenuControls.CancelBack.performed -= OnCancel;
        Destroy(m_textUI);
        m_playerInput.MenuControls.Disable();
        m_playerInput.PlayerControls.Enable();
    }

    IEnumerator WaitThenRespond()
    {
        yield return new WaitForSeconds(2f);
        m_playerInput.MenuControls.Enable();
        m_playerInput.MenuControls.CancelBack.performed += OnCancel;
    }
}
