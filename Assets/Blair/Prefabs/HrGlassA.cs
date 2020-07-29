using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HrGlassA : MonoBehaviour
{
    private DOTweenAnimation mTween;
    private bool TweenCompleted;
    private int DelayCount, loops = 1;
    public int DelayDuration;
    public float SlowFactor, FastFactor;
    public string FMODPath;

    void Start()
    {
       mTween = GetComponent<DOTweenAnimation>();
       TweenCompleted = true;
    }

    void Update()
    {
        if(TweenCompleted)
        {
            DelayCount++;
            if (DelayCount > DelayDuration)
            {
                TweenCompleted = false;
                mTween.tween.TogglePause();
                Debug.LogError("hourglassspins");
                FMODUnity.RuntimeManager.PlayOneShot(FMODPath, transform.position);
            }
        }
        if (mTween.tween.CompletedLoops() == loops && TweenCompleted == false)
        {
           // Debug.Log("completed tween");
            loops++;
            Completed();
        }
    }
    void Completed()
    {
        mTween.tween.Pause();
        TweenCompleted = true;
        DelayCount = 0;
    }
    void TimeSlow()
    {
        mTween.tween.timeScale = SlowFactor;
    }
    void TimeStop()
    {
        mTween.tween.timeScale = 0.0f;
    }
    void TimeFastForward()
    {
        mTween.tween.timeScale = FastFactor;
    }
    void RestoreToNormal()
    {
        mTween.tween.timeScale = 1.0f;
    }
    void JumpForward()
    {
    }

}
