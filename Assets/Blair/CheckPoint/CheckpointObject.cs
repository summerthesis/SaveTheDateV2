using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointObject : MonoBehaviour
{
    private GameObject mPlayer;
    void Start()
    {
        mPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
     
    }
    
    void OnTriggerEnter(Collider Col)
    {
        if(Col.gameObject.tag == "Checkpoint")
        {
            if(Col.gameObject.tag != "CurrentCheckpoint")
            {
                Col.gameObject.tag = "CurrentCheckpoint";
                mPlayer.GetComponent<DeathController>().RecordedTransforms.Clear();
            }
        }
    }
}
