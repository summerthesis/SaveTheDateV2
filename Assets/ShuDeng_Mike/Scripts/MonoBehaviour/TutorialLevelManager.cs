using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLevelManager : MonoBehaviour
{
    public LocalCameraTransform startCameraTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTutorialLevel()
    {
        GameManager.HUD.GetComponentInChildren<FadingCurtainController>().CurtainFadeOut();
        FindObjectOfType<MainCameraController>().ChangeView(startCameraTransform.position, 
            Quaternion.Euler(startCameraTransform.rotation), true);
        StartCoroutine(ReadyPlayer());
    }

    private IEnumerator ReadyPlayer()
    {
        var curtain = GameManager.HUD.GetComponentInChildren<FadingCurtainController>();
        while (curtain.FinishedFading == false)
        {
            yield return null;
        }

        GameManager.HUD.transform.GetChild(0).gameObject.SetActive(true);
        GameManager.PlayerInput.PlayerControls.Move.Enable();
        GameManager.PlayerInput.PlayerControls.Interact.Enable();
        GameManager.PlayerInput.PlayerControls.Jump.Enable();
        GameManager.PlayerInput.TimeControls.Enable();
    }
}
