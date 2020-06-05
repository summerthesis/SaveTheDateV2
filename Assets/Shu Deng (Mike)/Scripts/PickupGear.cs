﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pickup))]
public class PickupGear : MonoBehaviour
{
    public GameObject gearsCollectionInfo;

    void OnTriggerEnter(Collider other)
    {
        gearsCollectionInfo.GetComponent<GearUIController>().GearCollected();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}