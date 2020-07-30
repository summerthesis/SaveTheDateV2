using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideAmbTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.LogError("PlayerEntered Here");
            PlayOneShot("event:/Level/General/Amb 2D");
        }
    }

    private void PlayOneShot(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
    }
}
