using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeVfxController : MonoBehaviour
{
    public GameObject oSlow, oFast, oStop, mPlayer;
    private bool slowing, fasting, stopping;
    // Start is called before the first frame update
    void Start()
    {
        mPlayer = GameObject.FindGameObjectWithTag("Player");
        oSlow.SetActive(false); 
        oFast.SetActive(false); 
        oStop.SetActive(false);
        this.transform.position = mPlayer.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = mPlayer.transform.position;
    }
    void TimeSlow()
    {
        oSlow.SetActive(false);
        oSlow.SetActive(true);
    }
    void TimeStop()
    {
        oStop.SetActive(false);
        oStop.SetActive(true);
    }
    void TimeFastForward()
    {
        oFast.SetActive(false);
        oFast.SetActive(true);
    }
    void RestoreToNormal()
    {
       
    }
    void JumpForward()
    {
    }
}
