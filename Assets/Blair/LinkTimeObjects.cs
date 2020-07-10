using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkTimeObjects : MonoBehaviour
{
    public GameObject[] LinkedObjects;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void TimeSlow()
    {
        for(int i = 0; i < LinkedObjects.Length; i++)
        {
            LinkedObjects[i].SendMessage("TimeSlow");
        }
    }
    void TimeStop()
    {
        for (int i = 0; i < LinkedObjects.Length; i++)
        {
            LinkedObjects[i].SendMessage("TimeStop");
        }
    }
    void TimeFastForward()
    {
        for (int i = 0; i < LinkedObjects.Length; i++)
        {
            LinkedObjects[i].SendMessage("TimeFastForward");
        }
    }
    void JumpForward()
    {

    }
    void RestoreToNormal()
    {
        for (int i = 0; i < LinkedObjects.Length; i++)
        {
            LinkedObjects[i].SendMessage("RestoreToNormal");
        }
    }
}
