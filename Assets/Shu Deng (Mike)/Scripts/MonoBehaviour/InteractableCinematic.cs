using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

[RequireComponent(typeof(Interactable), typeof(CinematicInvoker))]
public class InteractableCinematic : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Interactable>().InteractionEvent += OnInteract;
    }

    void OnInteract()
    {
        GetComponent<CinematicInvoker>().InstantiateCinematic();
    }

    void OnDestroy()
    {
        GetComponent<Interactable>().InteractionEvent -= OnInteract;
    }
}
