using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController_Commented : MonoBehaviour
{
    PlayerInputAction controls;
    private float Energy;
    private float MaxEnergy = 1000;
    private float MaxCastRange = 1000;//To be serialized when gameplay is being tested.
    private float EnergyCost = 9;
    private int Count;
    private GameObject[] TimeTaggedObjects;
    private GameObject Player;
    private GameObject EnergyBar;
    private bool Stopping;

    private enum TimeStates
    {
        Available,
        Slowing,
        FastForwarding
    }
    private TimeStates TimeState = TimeStates.Available;
    void Awake()
    {
        SetupControls(); //This is moved to a function because I don't like Awake, Start, and Update cluttered.
        //Keep these main functions clean. 
        Player = GameObject.FindGameObjectWithTag("Player");
        EnergyBar = GameObject.FindGameObjectWithTag("EnergyBar");
    }

    void Update()
    {
        switch (TimeState)
        {
            case TimeStates.Available:
                //Notice to lines merged onto one line. This is because they are logically part of the same "sentence".
                //"Add 4 to energy, then - if energy overflows set it to the max. "
                //Thats the only way i know how to explain that. Its cleaner.
                Energy += 4; if (Energy > MaxEnergy) Energy = MaxEnergy;
                SetEnergyBarScale();//Use very descriptive function names. You don't even have to 
                //go look at what this function does - you already know.This can be used once instead of 3 times but its fine.
                break;

            case TimeStates.Slowing:
                Energy -= EnergyCost; if (Energy < 0) { Energy = 0; EndSlow(); }
                SetEnergyBarScale();
                break;

            case TimeStates.FastForwarding:
                Energy -= EnergyCost; if (Energy < 0) { Energy = 0; EndFastForward(); }
                SetEnergyBarScale();
                break;

            default:
                break;
        }
        if (Stopping)
        //Because stopping is an ongoing state for the platform, we need to allow the controller freedom
        //within its own statemachine while still keeping track of how long it has stopped everything else.
        //This is why a bool is used instead of a state.
        {
            Count++;
            if (Count > 120) { Stopping = false; Count = 0; LoopThroughObjects("RestoreToNormal", false); }
        }
        //^ simple way to add custom events. 
        //Notice the message? Go to the Platform and look for that. 
    }
    //Thats all i got for now, If you have any questions bother me anytime. I will get back to u asap.
    void LoopThroughObjects(string SendThisMessage, bool CheckDistance)
    {
        TimeTaggedObjects = GameObject.FindGameObjectsWithTag("TimeInteractable");

        foreach (GameObject obj in TimeTaggedObjects)
        {
            float distance = Vector3.Distance(Player.transform.position, obj.transform.position);
            if (CheckDistance)
            {
                if (distance < MaxCastRange)
                    obj.gameObject.SendMessage(SendThisMessage);
            }
            else
                obj.gameObject.SendMessage(SendThisMessage);
        }
    }


    void Slow()
    {
        if (Energy >= EnergyCost)
        {
            LoopThroughObjects("TimeSlow", true);
            TimeState = TimeStates.Slowing;
        }
        else
        {
            LoopThroughObjects("RestoreToNormal", false);
            TimeState = TimeStates.Available;
        }
    }

    void Stop()
    {
        if (Energy >= 750)
        {
            Stopping = true;
            LoopThroughObjects("TimeStop", true);
            Energy -= 750;
            SetEnergyBarScale();
        }
    }

    void FastForward()
    {
        if (Energy >= EnergyCost)
        {
            LoopThroughObjects("TimeFastForward", true);
            TimeState = TimeStates.FastForwarding;
        }
        else
        {
            LoopThroughObjects("RestoreToNormal", false);
            TimeState = TimeStates.Available;
        }
    }
    void JumpForward()
    {
        if (Energy >= 750)
        {
            LoopThroughObjects("JumpForward", true);
            Energy -= 750;
            SetEnergyBarScale();
        }
    }
    void EndSlow()
    {

        LoopThroughObjects("RestoreToNormal", true);
        TimeState = TimeStates.Available;
    }
    void EndFastForward()
    {
        LoopThroughObjects("RestoreToNormal", false);
        TimeState = TimeStates.Available;
    }
    void SetupControls()
    {
        controls = new PlayerInputAction();
        controls.TimeControls.TimeFastForward.performed += ctx => FastForward();
        controls.TimeControls.TimeFastForward.canceled += ctx => EndFastForward();
        controls.TimeControls.TimeSlow.performed += ctx => Slow();
        controls.TimeControls.TimeSlow.canceled += ctx => EndSlow();

        controls.TimeControls.TimeJumpForward.canceled += ctx => JumpForward();

        controls.TimeControls.TimeStop.canceled += ctx => Stop();
    }
    void SetEnergyBarScale()
    {
        float EnergyBarScale = Energy / MaxEnergy;
        EnergyBar.transform.localScale = new Vector3(EnergyBarScale, 1, 1);
    }
    private void OnEnable() { controls.Enable(); }
    private void OnDisable() { controls.Disable(); }
}
