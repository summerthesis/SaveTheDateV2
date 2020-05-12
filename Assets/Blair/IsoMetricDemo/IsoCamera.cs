using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoCamera : MonoBehaviour
{

    public GameObject CameraTarget;
    enum CameraStates
    {
        Default, 
        RotateMapCCW, //This is to divide the stage into different sections.
        RotateMapCW,  
        CinematicA, 
        Shoulder, 
    };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
