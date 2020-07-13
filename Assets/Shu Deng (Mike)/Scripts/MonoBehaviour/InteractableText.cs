using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Interactable))]
public class InteractableText : MonoBehaviour
{
    public GameObject TextUIPrefab;
    public Canvas ScreenCanvas;
    [TextArea]
    public string Text = 
        "This is your first line\nPress Enter and type in the second line";

    private PlayerInputAction m_playerInput;
    private GameObject m_textUI;

    void Start()
    {
        m_playerInput = GameManager.PlayerInput;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnInteract()
    {
        m_textUI = Instantiate(TextUIPrefab, ScreenCanvas.transform, false);
        TextUITypewrite typewriter = m_textUI.GetComponentInChildren<TextUITypewrite>();
        typewriter.Input(Text);
        typewriter.Output();
        m_playerInput.PlayerControls.Disable();
        m_playerInput.TimeControls.Disable();
        StartCoroutine(WaitThenRespond());
    }

    void OnCancel(InputAction.CallbackContext ctx)
    {
        m_playerInput.MenuControls.CancelBack.performed -= OnCancel;
        Destroy(m_textUI);
        m_playerInput.MenuControls.Disable();
        m_playerInput.PlayerControls.Enable();
        m_playerInput.TimeControls.Enable();
    }

    IEnumerator WaitThenRespond()
    {
        yield return new WaitForSeconds(2f);
        m_playerInput.MenuControls.Enable();
        m_playerInput.MenuControls.CancelBack.performed += OnCancel;
    }
}
