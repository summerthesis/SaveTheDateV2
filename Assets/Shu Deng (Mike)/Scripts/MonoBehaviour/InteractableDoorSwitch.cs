using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class InteractableDoorSwitch : MonoBehaviour
{
    public GameObject GearDoorObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnInteract()
    {
        GearDoorObject.GetComponentInChildren<GearDoorController>().OpenOrClose();
    }
}
