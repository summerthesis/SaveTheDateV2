using UnityEngine;
 
public class Rotator : MonoBehaviour
{
    [Range(-1.0f, 1.0f)]
    public float xForceDirection = 0.0f;
    [Range(-1.0f, 1.0f)]
    public float yForceDirection = 0.0f;
    [Range(-1.0f, 1.0f)]
    public float zForceDirection = 0.0f;
    public float NormalSpeed, SlowFactor, FastFactor;
    [HideInInspector]
    public float StopSpeed, mSpeed;
    private float SlowedSpeed, FastSpeed;
    public bool worldPivote = false;

    [SerializeField]
    FMODUnity.StudioEventEmitter FMODAudio;
 
    private Space spacePivot = Space.Self;
 
 
    void Start()
    {
        
        if (worldPivote) spacePivot = Space.World;
        SlowedSpeed = NormalSpeed / SlowFactor;
        FastSpeed = NormalSpeed * FastFactor;
        StopSpeed = 0;
        mSpeed = NormalSpeed;
        FMODAudio.SetParameter("State", 0);
    }
 
    void Update()
    {
        this.transform.Rotate(xForceDirection * mSpeed
                            , yForceDirection * mSpeed
                            , zForceDirection * mSpeed
                            , spacePivot);
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
 