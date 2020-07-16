using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShotSpawner : MonoBehaviour
{
    private GameObject mPlayer, mParent;
    private bool triggered;
    // Start is called before the first frame update
    void Start()
    {
        mPlayer = GameObject.FindGameObjectWithTag("Player");
        mParent = this.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject == mPlayer)
        {
            Debug.Log("Triggered One Shot Platform");
            if (!triggered)
            {
                triggered = true;
                mParent.SendMessage("StartMe");
            }
                
        }
    }
}
