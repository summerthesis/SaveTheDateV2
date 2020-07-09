using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBoardingLevelManager : MonoBehaviour
{
    public Transform TutorialStartTarget;

    private bool m_Ended = false;

    private void Awake()
    {
        if (TutorialStartTarget == null)
        {
            TutorialStartTarget = FindObjectOfType<TutorialLevelManager>().transform;
        }
    }

    private void Start()
    {
        GameManager.HUD.transform.GetChild(0).gameObject.SetActive(false);
        GameManager.PlayerInput.PlayerControls.Jump.Disable();
        GameManager.PlayerInput.TimeControls.Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
        KH_PlayerController playerController = other.GetComponent<KH_PlayerController>();
        if (playerController != null && m_Ended == false)
        {
            GameManager.HUD.GetComponentInChildren<FadingCurtainController>().CurtainFadeIn();
            GameManager.PlayerInput.PlayerControls.Move.Disable();
            GameManager.PlayerInput.PlayerControls.Interact.Disable();
            StartCoroutine(MoveToTutorial());
            m_Ended = true;
        }
    }

    private IEnumerator MoveToTutorial()
    {
        var curtain = GameManager.HUD.GetComponentInChildren<FadingCurtainController>();
        while (curtain.FinishedFading == false)
        {
            yield return null;
        }

        GameManager.Player.transform.position = TutorialStartTarget.position;
        GameManager.Player.transform.rotation = TutorialStartTarget.rotation;
        TutorialStartTarget.GetComponent<TutorialLevelManager>().StartTutorialLevel();
    }
}
