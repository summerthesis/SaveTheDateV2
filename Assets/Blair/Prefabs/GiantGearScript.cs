﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantGearScript : MonoBehaviour
{
    public float mSpeed, FastSpeed, SlowedSpeed, NormalSpeed;
    

    // Start is called before the first frame update
    void Start()
    {
        mSpeed = NormalSpeed;
        SlowedSpeed = NormalSpeed / 3;
        FastSpeed = NormalSpeed * 5;
      
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * mSpeed * Time.deltaTime);
    }
    void TimeSlow()
    {
        mSpeed = SlowedSpeed;
    }
    void TimeStop()
    {
        mSpeed = 0;
    }
    void TimeFastForward()
    {
        mSpeed = FastSpeed;
    }
    void RestoreToNormal()
    {
        mSpeed = NormalSpeed;
    }
    void JumpForward()
    {
    }

}