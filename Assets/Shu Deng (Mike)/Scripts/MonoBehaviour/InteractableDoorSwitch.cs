using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class InteractableDoorSwitch : MonoBehaviour
{
    public GameObject GearDoorObject;

    void Awake()
    {
        GetComponent<Interactable>().InteractionEvent += OnInteract;
    }

    void OnInteract()
    {
        GearDoorObject.GetComponentInChildren<GearDoorController>().OpenOrClose();
    }

    void OnDestroy()
    {
        GetComponent<Interactable>().InteractionEvent -= OnInteract;
    }
}
