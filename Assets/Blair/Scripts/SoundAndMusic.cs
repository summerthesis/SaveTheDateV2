using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAndMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound2DOneShot(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, Camera.main.transform.position);
    }
}
