using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantGearScript : MonoBehaviour
{
    public float SlowFactor, FastFactor, NormalSpeed;
    
    [HideInInspector]
    public float mSpeed, FastSpeed, SlowedSpeed;

    [SerializeField]
    FMODUnity.StudioEventEmitter FMODAudio;

    // Start is called before the first frame update
    void Start()
    {
        mSpeed = NormalSpeed;
        SlowedSpeed = NormalSpeed / SlowFactor;
        FastSpeed = NormalSpeed * FastFactor;
      
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * mSpeed * Time.deltaTime);
    }
    void TimeSlow()
    {
        mSpeed = SlowedSpeed;
        FMODAudio.SetParameter("State", 1);
    }
    void TimeStop()
    {
        mSpeed = 0;
    }
    void TimeFastForward()
    {
        mSpeed = FastSpeed;
        FMODAudio.SetParameter("State", 2);
    }
    void RestoreToNormal()
    {
        mSpeed = NormalSpeed;
        FMODAudio.SetParameter("State", 0);
    }
    void JumpForward()
    {
    }

}
