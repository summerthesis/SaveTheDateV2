using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInTutorial : MonoBehaviour
{
    private GameObject mPlayer;
    private Vector3 lockPos;
    private Quaternion lockRot;
    public bool isDoneSound;
    
    void Start()
    {
        mPlayer = GameObject.FindGameObjectWithTag("Player");
        lockPos = mPlayer.transform.position;
        lockRot = mPlayer.transform.rotation;
        PlaySound2DOneShot("event:/Level/Onboarding/Arrival");
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDoneSound)
        {
            mPlayer.transform.position = lockPos;
            mPlayer.transform.rotation = lockRot;
        }
        
    }
    public static void PlaySound2DOneShot(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, Camera.main.transform.position);
    }
    void unlock()
    {
        isDoneSound = true;
    }
}
