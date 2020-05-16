using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
  
    PlayerInputAction controls;
    private float SlowAxis, FastAxis, Energy;
    public float MaxCastRange = 100;
    public float MaxEnergy = 1000;
    private int CoolDownDuration = 300;
    private int Count;
    GameObject[] TimeTaggedObjects;
    GameObject mPlayer;
    private enum TimeStates { Slowing, Stopping, FastForwarding, Available, Overheated, Cooldown}
    private TimeStates TimeState;
    void Awake()
    {
        controls = new PlayerInputAction();
        controls.TimeControls.TimeForward.performed += ctx => FastAxis = ctx.ReadValue<float>();
        controls.TimeControls.TimeBackward.performed += ctx => SlowAxis = ctx.ReadValue<float>();
        controls.TimeControls.TimeForward.canceled += ctx => EndFastForward();
        controls.TimeControls.TimeBackward.canceled += ctx => EndSlow();
        
        TimeTaggedObjects = GameObject.FindGameObjectsWithTag("TimeInteractable");
        mPlayer = GameObject.FindGameObjectWithTag("Player");

        TimeState = TimeStates.Available;

        Energy = MaxEnergy;
    }

    void Update()
    {
        switch (TimeState)
        {
            case TimeStates.Available:
                if (SlowAxis != 0) { TimeState = TimeStates.Slowing; break; }
                if (SlowAxis == 1) { TimeState = TimeStates.Stopping; break; }
                if (FastAxis == 1) { TimeState = TimeStates.FastForwarding; break; }

                if (Energy < MaxEnergy) Energy += 5;
                if (Energy > MaxEnergy) Energy  = MaxEnergy;                
                break;

            case TimeStates.Slowing:
                Energy-=25;
                break;

            case TimeStates.Stopping:

                break;

            case TimeStates.FastForwarding:

                break;

            case TimeStates.Overheated:
                //Reserved for Events; sounds, animation etc.
                TimeState = TimeStates.Cooldown;
                break;
           
            case TimeStates.Cooldown:
                Count++;
                if (Count > CoolDownDuration)
                {
                    Count = 0;
                    TimeState = TimeStates.Available;
                }
                break;

            default:
                break;

        }
    }

    void Slow()
    {

        foreach (GameObject obj in TimeTaggedObjects)
        {
            float distance = Vector3.Distance(mPlayer.transform.position, obj.transform.position);
            if (distance < MaxCastRange)
                obj.gameObject.SendMessage("TimeSlow");
        }
        TimeState = TimeStates.Slowing;
    }

    void FastForward()
    {
        foreach (GameObject obj in TimeTaggedObjects)
            {
            float distance = Vector3.Distance(mPlayer.transform.position, obj.transform.position);
            if (distance < MaxCastRange)
            obj.gameObject.SendMessage("TimeFastForward");
            }
        TimeState = TimeStates.FastForwarding;
    }

    void EndSlow()
    {

    }
    void EndFastForward()
    {

    }
}
