using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Interactable))]
public class InteractableText : MonoBehaviour
{
    public GameObject textUIPrefab;
    public Canvas screenCanvas;
    [TextArea]
    public string text = 
        "This is your first line\nPress Enter and type in the second line";

    private GameObject m_TextUI;

    void OnInteract()
    {
        m_TextUI = Instantiate(textUIPrefab, screenCanvas.transform, false);
        TextUITypewrite typewriter = m_TextUI.GetComponentInChildren<TextUITypewrite>();
        typewriter.Input(text);
        typewriter.Output();
        GameManager.PlayerInput.PlayerControls.Disable();
        GameManager.PlayerInput.TimeControls.Disable();
        StartCoroutine(WaitThenRespond());
    }

    void OnCancel(InputAction.CallbackContext ctx)
    {
        GameManager.PlayerInput.MenuControls.Back.performed -= OnCancel;
        GameManager.PlayerInput.MenuControls.Enter.performed -= OnCancel;
        Destroy(m_TextUI);
        GameManager.PlayerInput.MenuControls.Disable();
        GameManager.PlayerInput.PlayerControls.Enable();
        GameManager.PlayerInput.TimeControls.Enable();
    }

    IEnumerator WaitThenRespond()
    {
        yield return new WaitForSeconds(2f);
        GameManager.PlayerInput.MenuControls.Enable();
        GameManager.PlayerInput.MenuControls.Back.performed += OnCancel;
        GameManager.PlayerInput.MenuControls.Enter.performed += OnCancel;
    }
}
