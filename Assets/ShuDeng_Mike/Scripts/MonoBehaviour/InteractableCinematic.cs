using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

[RequireComponent(typeof(Interactable), typeof(CinematicInvoker))]
public class InteractableCinematic : MonoBehaviour
{
    void OnInteract()
    {
        GetComponent<CinematicInvoker>().InstantiateCinematic();
    }
}
