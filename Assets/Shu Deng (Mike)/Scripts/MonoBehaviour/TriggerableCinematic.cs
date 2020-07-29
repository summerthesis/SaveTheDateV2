using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent()]
public class TriggerableCinematic : MonoBehaviour
{

    void Awake()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
